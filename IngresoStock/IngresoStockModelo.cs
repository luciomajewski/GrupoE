using System;
using System.Collections.Generic;
using System.Linq;

namespace GrupoE.IngresoStock
{
    public class IngresoStockModelo
    {
        #region Datos que ingresa el operario

        public string NumeroOrden { get; set; } = string.Empty;
        public ItemOrdenRecepcion? SkuSeleccionado { get; set; }
        public string Cantidad { get; set; } = string.Empty;
        public string Pasillo { get; set; } = string.Empty;
        public string Fila { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;
        public AsignacionUbicacion? UbicacionSeleccionada { get; set; }

        #endregion

        #region Datos que muestra la pantalla

        public string ClienteOrigen
        {
            get { return ordenActual == null ? string.Empty : ordenActual.ClienteOrigen; }
        }

        public List<ItemOrdenRecepcion> Items
        {
            get { return ordenActual == null ? new List<ItemOrdenRecepcion>() : ordenActual.Items; }
        }

        public List<AsignacionUbicacion> Ubicaciones { get; } = new List<AsignacionUbicacion>();

        // Coordenadas de las posiciones de los depósitos (Depositos.json).
        public List<string> Pasillos { get; } = Almacenes.DepositosAlmacen.Depositos
            .SelectMany(d => d.Posiciones).Select(p => p.Pasillo).Distinct().OrderBy(p => p).ToList();
        public List<string> Filas { get; } = Almacenes.DepositosAlmacen.Depositos
            .SelectMany(d => d.Posiciones).Select(p => p.Fila).Distinct().OrderBy(p => p).ToList();
        public List<string> Niveles { get; } = Almacenes.DepositosAlmacen.Depositos
            .SelectMany(d => d.Posiciones).Select(p => p.Nivel).Distinct().OrderBy(p => p).ToList();

        #endregion

        private readonly List<RegistroAuditoria> historialAuditoria = new List<RegistroAuditoria>();

        private OrdenRecepcion? ordenActual;

        #region Pasos 3 a 6: buscar la orden

        // Devuelve el error a mostrar, o null si se encontró la orden.
        public string? BuscarOrden()
        {
            // Paso 4
            if (!int.TryParse(NumeroOrden.Trim(), out int numero) || numero <= 0)
            {
                NumeroOrden = string.Empty;
                return "El número de orden debe ser un valor numérico";
            }

            // Paso 5. Se pueden ubicar las órdenes pendientes, con diferencias o con excedente.
            var ordenEntidad = Almacenes.OrdenesRecepcionAlmacen.OrdenesRecepcion.FirstOrDefault(o => o.Numero == numero);
            var clienteEntidad = ordenEntidad == null
                ? null
                : Almacenes.ClientesAlmacen.Clientes.FirstOrDefault(c => c.Codigo == ordenEntidad.CodigoCliente);

            if (ordenEntidad == null || clienteEntidad == null || ordenEntidad.Estado == Almacenes.EstadoOrdenRecepcionEnum.Cumplida)
            {
                Restablecer();
                return "No se encontró ninguna orden pendiente de ubicación con ese número";
            }

            var orden = new OrdenRecepcion(ordenEntidad.Numero, clienteEntidad.RazonSocial, EstadoOrdenRecepcion.PendienteDeUbicacion,
                ordenEntidad.Items.Select(i => new ItemOrdenRecepcion(i.Sku, i.Descripcion, i.CantidadContada)).ToList());

            // Paso 6. Lo que ya se ubicó en un ingreso anterior no se vuelve a ubicar.
            ordenActual = orden;
            foreach (ItemOrdenRecepcion item in orden.Items)
            {
                int yaUbicada = ordenEntidad.Items.First(i => i.Sku == item.Sku).Asignaciones.Sum(a => a.Cantidad);
                item.CantidadAUbicar = item.CantidadRecepcionada - yaUbicada;
            }
            Ubicaciones.Clear();
            UbicacionSeleccionada = null;
            SkuSeleccionado = null;
            LimpiarDatosUbicacion();
            return null;
        }

        #endregion

        #region Pasos 9 a 11: agregar y quitar ubicaciones

        // Devuelve el error a mostrar, o null si se agregó la ubicación.
        public string? AgregarUbicacion()
        {
            if (SkuSeleccionado == null || !Items.Contains(SkuSeleccionado))
            {
                return "Debe seleccionar un SKU de la orden de recepción";
            }

            // Paso 8
            bool cantidadValida = int.TryParse(Cantidad.Trim(), out int cantidad) && cantidad > 0;
            // La posición tiene que existir en el depósito del cliente de la orden.
            var ordenEntidad = Almacenes.OrdenesRecepcionAlmacen.OrdenesRecepcion.First(o => o.Numero == ordenActual!.Numero);
            var clienteEntidad = Almacenes.ClientesAlmacen.Clientes.First(c => c.Codigo == ordenEntidad.CodigoCliente);
            var deposito = Almacenes.DepositosAlmacen.Depositos.FirstOrDefault(d => d.Codigo == clienteEntidad.CodigoDeposito);
            bool coordenadasValidas = deposito != null
                && deposito.Posiciones.Any(p => p.Pasillo == Pasillo && p.Fila == Fila && p.Nivel == Nivel);

            if (!cantidadValida || !coordenadasValidas)
            {
                return "Debe seleccionar Pasillo, Fila, Nivel e ingresar una cantidad válida";
            }

            // Paso 10
            if (cantidad > SkuSeleccionado.CantidadAUbicar)
            {
                Cantidad = string.Empty;
                return "La cantidad a ubicar no puede ser mayor a la cantidad pendiente recepcionada";
            }

            // Solo se ubica en posiciones libres o con mercadería del mismo cliente.
            bool ocupadaPorOtroCliente = Almacenes.MercaderiasAlmacen.Mercaderias.Any(m =>
                m.CodigoDeposito == clienteEntidad.CodigoDeposito && m.CodigoCliente != clienteEntidad.Codigo
                && m.Ubicaciones.Any(u => u.Cantidad > 0 && u.Posicion.Pasillo == Pasillo && u.Posicion.Fila == Fila && u.Posicion.Nivel == Nivel));
            if (ocupadaPorOtroCliente)
            {
                return $"La posición {Pasillo}-{Fila}-{Nivel} ya está ocupada con mercadería de otro cliente";
            }

            // Si el SKU ya tiene esa misma posición, se suma la cantidad
            AsignacionUbicacion? existente = Ubicaciones.FirstOrDefault(u =>
                u.Item == SkuSeleccionado && u.Pasillo == Pasillo && u.Fila == Fila && u.Nivel == Nivel);

            if (existente != null)
            {
                existente.Cantidad += cantidad;
            }
            else
            {
                Ubicaciones.Add(new AsignacionUbicacion(SkuSeleccionado, cantidad, Pasillo, Fila, Nivel));
            }

            SkuSeleccionado.CantidadAUbicar -= cantidad;
            LimpiarDatosUbicacion();
            return null;
        }

        // Devuelve el error a mostrar, o null si se quitó la ubicación.
        public string? QuitarUbicacion()
        {
            if (UbicacionSeleccionada == null)
            {
                return "Debe seleccionar una ubicación de la lista para quitar";
            }

            UbicacionSeleccionada.Item.CantidadAUbicar += UbicacionSeleccionada.Cantidad;
            Ubicaciones.Remove(UbicacionSeleccionada);
            UbicacionSeleccionada = null;
            return null;
        }

        #endregion

        #region Pasos 12 a 16: confirmar el ingreso

        // Devuelve el error a mostrar, o null si el ingreso se puede confirmar.
        public string? ValidarIngreso()
        {
            if (ordenActual == null)
            {
                return "Debe buscar una orden de recepción antes de confirmar el ingreso";
            }

            if (ordenActual.Items.Any(i => i.CantidadAUbicar > 0))
            {
                return "Debe ubicar toda la mercadería recepcionada antes de confirmar el ingreso";
            }

            return null;
        }

        public string ConfirmarIngreso()
        {
            var ordenEntidad = Almacenes.OrdenesRecepcionAlmacen.OrdenesRecepcion.First(o => o.Numero == ordenActual!.Numero);
            var clienteEntidad = Almacenes.ClientesAlmacen.Clientes.First(c => c.Codigo == ordenEntidad.CodigoCliente);

            // Paso 13: el stock suma en cada posición y la orden guarda dónde quedó cada SKU.
            foreach (AsignacionUbicacion ubicacion in Ubicaciones)
            {
                var mercaderia = Almacenes.MercaderiasAlmacen.Mercaderias.FirstOrDefault(m => m.CodigoCliente == clienteEntidad.Codigo
                    && m.Sku == ubicacion.Item.Sku && m.CodigoDeposito == clienteEntidad.CodigoDeposito);
                if (mercaderia == null)
                {
                    mercaderia = new Almacenes.MercaderiaEntidad
                    {
                        CodigoCliente = clienteEntidad.Codigo,
                        Sku = ubicacion.Item.Sku,
                        CodigoDeposito = clienteEntidad.CodigoDeposito
                    };
                    Almacenes.MercaderiasAlmacen.Mercaderias.Add(mercaderia);
                }

                var enPosicion = mercaderia.Ubicaciones.FirstOrDefault(u =>
                    u.Posicion.Pasillo == ubicacion.Pasillo && u.Posicion.Fila == ubicacion.Fila && u.Posicion.Nivel == ubicacion.Nivel);
                if (enPosicion == null)
                {
                    enPosicion = new Almacenes.UbicacionMercaderia
                    {
                        Posicion = new Almacenes.Posicion { Pasillo = ubicacion.Pasillo, Fila = ubicacion.Fila, Nivel = ubicacion.Nivel }
                    };
                    mercaderia.Ubicaciones.Add(enPosicion);
                }
                enPosicion.Cantidad += ubicacion.Cantidad;
                mercaderia.CantidadFisica = mercaderia.Ubicaciones.Sum(u => u.Cantidad);
                mercaderia.CantidadDisponible = mercaderia.CantidadFisica - mercaderia.CantidadComprometida;

                var itemEntidad = ordenEntidad.Items.First(i => i.Sku == ubicacion.Item.Sku);
                var asignacion = itemEntidad.Asignaciones.FirstOrDefault(a =>
                    a.EnPosicion.Pasillo == ubicacion.Pasillo && a.EnPosicion.Fila == ubicacion.Fila && a.EnPosicion.Nivel == ubicacion.Nivel);
                if (asignacion == null)
                {
                    asignacion = new Almacenes.AsignacionUbicacion
                    {
                        EnPosicion = new Almacenes.Posicion { Pasillo = ubicacion.Pasillo, Fila = ubicacion.Fila, Nivel = ubicacion.Nivel }
                    };
                    itemEntidad.Asignaciones.Add(asignacion);
                }
                asignacion.Cantidad += ubicacion.Cantidad;
            }

            // Paso 14
            ordenActual!.Estado = EstadoOrdenRecepcion.Cumplida;
            ordenEntidad.Estado = Almacenes.EstadoOrdenRecepcionEnum.Cumplida;
            Almacenes.MercaderiasAlmacen.Guardar();
            Almacenes.OrdenesRecepcionAlmacen.Guardar();

            // Paso 15
            historialAuditoria.Add(new RegistroAuditoria(Environment.UserName, DateTime.Now, ordenActual.Numero));

            // Paso 16
            Restablecer();
            return "Mercadería ingresada a stock con éxito";
        }

        #endregion

        #region Excepción paso 12: cancelar

        public void Cancelar()
        {
            Restablecer();
        }

        #endregion

        private void Restablecer()
        {
            ordenActual = null;
            NumeroOrden = string.Empty;
            SkuSeleccionado = null;
            UbicacionSeleccionada = null;
            Ubicaciones.Clear();
            LimpiarDatosUbicacion();
        }

        private void LimpiarDatosUbicacion()
        {
            Cantidad = string.Empty;
            Pasillo = string.Empty;
            Fila = string.Empty;
            Nivel = string.Empty;
        }
    }
}

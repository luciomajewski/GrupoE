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

        public List<string> Pasillos { get; } = new List<string> { "Pasillo 1", "Pasillo 2", "Pasillo 3" };
        public List<string> Filas { get; } = new List<string> { "Fila 1", "Fila 2", "Fila 3" };
        public List<string> Niveles { get; } = new List<string> { "Nivel 1", "Nivel 2", "Nivel 3" };

        #endregion

        #region Datos de prueba

        // Al.9 Órdenes de Recepción
        private readonly List<OrdenRecepcion> ordenesRecepcion = new List<OrdenRecepcion>
        {
            new OrdenRecepcion(1001, "Distribuidora del Sur S.A.", EstadoOrdenRecepcion.PendienteDeUbicacion,
                new List<ItemOrdenRecepcion>
                {
                    new ItemOrdenRecepcion("ELE-0001", "Auriculares inalámbricos Bluetooth", 40),
                    new ItemOrdenRecepcion("ELE-0002", "Cargador USB-C 20W", 60),
                    new ItemOrdenRecepcion("HOG-0010", "Juego de sábanas 2 plazas", 25)
                }),

            new OrdenRecepcion(1002, "Textil Pampeana S.R.L.", EstadoOrdenRecepcion.PendienteDeUbicacion,
                new List<ItemOrdenRecepcion>
                {
                    new ItemOrdenRecepcion("IND-0101", "Remera de algodón talle M", 80),
                    new ItemOrdenRecepcion("IND-0102", "Buzo de frisa talle L", 35)
                }),

            new OrdenRecepcion(1003, "Librería Belgrano", EstadoOrdenRecepcion.PendienteDeUbicacion,
                new List<ItemOrdenRecepcion>
                {
                    new ItemOrdenRecepcion("LIB-0201", "Cuaderno A4 rayado", 150),
                    new ItemOrdenRecepcion("LIB-0202", "Resma A4 500 hojas", 50)
                }),

            // Orden ya cumplida: sirve para probar la excepción del paso 5
            new OrdenRecepcion(1004, "Ferretería Los Andes", EstadoOrdenRecepcion.Cumplida,
                new List<ItemOrdenRecepcion>
                {
                    new ItemOrdenRecepcion("FER-0301", "Taladro percutor 650W", 10)
                })
        };

        // Al.2 Mercaderías: stock que ya existe en el depósito
        private readonly List<Mercaderia> mercaderias = new List<Mercaderia>
        {
            new Mercaderia("ELE-0001", "Pasillo 1", "Fila 1", "Nivel 1", 15),
            new Mercaderia("IND-0101", "Pasillo 2", "Fila 1", "Nivel 2", 50),
            new Mercaderia("LIB-0202", "Pasillo 3", "Fila 3", "Nivel 1", 12)
        };

        private readonly List<RegistroAuditoria> historialAuditoria = new List<RegistroAuditoria>();

        #endregion

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

            // Paso 5
            OrdenRecepcion? orden = ordenesRecepcion.FirstOrDefault(o =>
                o.Numero == numero && o.Estado == EstadoOrdenRecepcion.PendienteDeUbicacion);

            if (orden == null)
            {
                Restablecer();
                return "No se encontró ninguna orden pendiente de ubicación con ese número";
            }

            // Paso 6
            ordenActual = orden;
            foreach (ItemOrdenRecepcion item in orden.Items)
            {
                item.CantidadAUbicar = item.CantidadRecepcionada;
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
            bool coordenadasValidas = Pasillos.Contains(Pasillo) && Filas.Contains(Fila) && Niveles.Contains(Nivel);

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
            // Paso 13
            foreach (AsignacionUbicacion ubicacion in Ubicaciones)
            {
                Mercaderia? mercaderia = mercaderias.FirstOrDefault(m =>
                    m.Sku == ubicacion.Item.Sku && m.Pasillo == ubicacion.Pasillo && m.Fila == ubicacion.Fila && m.Nivel == ubicacion.Nivel);

                if (mercaderia == null)
                {
                    mercaderia = new Mercaderia(ubicacion.Item.Sku, ubicacion.Pasillo, ubicacion.Fila, ubicacion.Nivel, 0);
                    mercaderias.Add(mercaderia);
                }

                mercaderia.Cantidad += ubicacion.Cantidad;
            }

            // Paso 14
            ordenActual!.Estado = EstadoOrdenRecepcion.Cumplida;

            // Paso 15
            historialAuditoria.Add(new RegistroAuditoria("operario.almacen", DateTime.Now, ordenActual.Numero));

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

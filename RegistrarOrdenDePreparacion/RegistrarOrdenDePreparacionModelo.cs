#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoE.RegistrarOrdenDePreparacion;

internal class RegistrarOrdenDePreparacionModelo
{
    public string NombreCliente { get; private set; } = "";
    // Clientes que se pueden elegir: con contrato vigente y stock disponible en Mercaderias.json.
    public List<string> Clientes { get; }
    public List<Deposito> Depositos { get; } = new();
    public Deposito? DepositoSeleccionado { get; private set; }
    public ModalidadEmpaquetado? Modalidad { get; private set; }
    public List<ItemOrden> ItemsOrden { get; } = new();

    public DateTime FechaRetiro { get; set; } = DateTime.Today;
    public string NombreDestinatario { get; set; } = "";
    public string DireccionDestino { get; set; } = "";
    public string NombreTransportista { get; set; } = "";
    public int? DniTransportista { get; set; }
    public string EmpresaTransportista { get; set; } = "";

    public RegistrarOrdenDePreparacionModelo()
    {
        // Mientras no haya login, el cliente se elige de una lista. Al abrir la pantalla queda elegido
        // uno al azar de los clientes con contrato vigente y stock disponible.
        Clientes = Almacenes.ClientesAlmacen.Clientes.Where(c => c.ContratoVigente
            && Almacenes.MercaderiasAlmacen.Mercaderias.Any(m => m.CodigoCliente == c.Codigo && m.CantidadDisponible > 0))
            .Select(c => c.RazonSocial)
            .ToList();
        if (Clientes.Count > 0)
        {
            SeleccionarCliente(Clientes[Random.Shared.Next(Clientes.Count)]);
        }
    }

    // Cambia el cliente de la orden: carga sus depósitos y productos, y la orden empieza de cero.
    // Devuelve el mensaje a mostrar, o null si no había nada cargado.
    public string? SeleccionarCliente(string? nombreCliente)
    {
        if (nombreCliente == null || nombreCliente == NombreCliente)
        {
            return null;
        }

        var clienteEntidad = Almacenes.ClientesAlmacen.Clientes.FirstOrDefault(c => c.RazonSocial == nombreCliente);
        if (clienteEntidad == null)
        {
            return null;
        }

        bool habiaOrden = ItemsOrden.Count > 0;
        NombreCliente = clienteEntidad.RazonSocial;
        codigoCliente = clienteEntidad.Codigo;
        DepositoSeleccionado = null;
        ItemsOrden.Clear();
        Depositos.Clear();

        // Los pallets no tienen una cantidad fija de unidades (cada uno tiene las que se contaron al recibirlo):
        // UnidadesPorPalet queda en 0 y las unidades disponibles se cargan en UnidadesSueltas.
        var productos = Almacenes.ProductosAlmacen.Productos
            .Where(p => p.CodigoCliente == clienteEntidad.Codigo)
            .Select(p => new Producto { Sku = p.Sku, Nombre = p.Descripcion, StockMinimoContratado = p.StockMinimoContratado, UnidadesPorPalet = 0 })
            .ToList();

        // Solo los depósitos donde el cliente tiene mercadería, con su stock disponible (físico - comprometido)
        // y la cantidad de pallets completos que puede retirar a palet cerrado.
        foreach (var depositoEntidad in Almacenes.DepositosAlmacen.Depositos)
        {
            var stock = Almacenes.MercaderiasAlmacen.Mercaderias
                .Where(m => m.CodigoCliente == clienteEntidad.Codigo && m.CodigoDeposito == depositoEntidad.Codigo)
                .Select(m => new StockProducto
                {
                    Producto = productos.First(p => p.Sku == m.Sku),
                    CantidadPalets = PalletsLibres(m).Count,
                    UnidadesSueltas = m.CantidadDisponible
                })
                .ToList();

            if (stock.Count > 0)
            {
                Depositos.Add(new Deposito { Nombre = depositoEntidad.Nombre, Stock = stock });
            }
        }

        return habiaOrden ? "Se cambió el cliente, la orden actual ha sido reiniciada" : null;
    }

    private string codigoCliente = "";

    // Pallets completos (con CodigoPallet) que el cliente puede pedir a palet cerrado, en el orden en que se toman
    // (por posición). Se descartan los que ya tienen unidades en una orden de selección pendiente, los que ya pidieron
    // otras órdenes de preparación a palet cerrado todavía pendientes, y los que no entran en el stock disponible.
    private static List<Almacenes.UbicacionMercaderia> PalletsLibres(Almacenes.MercaderiaEntidad mercaderia)
    {
        var reservadas = Almacenes.OrdenesSeleccionAlmacen.OrdenesSeleccion
            .Where(s => s.Estado == Almacenes.EstadoOrdenSeleccionEnum.Pendiente && s.CodigoDeposito == mercaderia.CodigoDeposito)
            .SelectMany(s => s.Renglones)
            .Where(r => r.CodigoCliente == mercaderia.CodigoCliente && r.Sku == mercaderia.Sku)
            .Select(r => $"{r.Posicion.Pasillo}-{r.Posicion.Fila}-{r.Posicion.Nivel}")
            .ToList();

        var completos = mercaderia.Ubicaciones
            .Where(u => u.CodigoPallet != null && EsPalletDeUnSoloSku(u.CodigoPallet)
                        && !reservadas.Contains($"{u.Posicion.Pasillo}-{u.Posicion.Fila}-{u.Posicion.Nivel}"))
            .OrderBy(u => u.Posicion.Pasillo.Length).ThenBy(u => u.Posicion.Pasillo, StringComparer.Ordinal)
            .ThenBy(u => u.Posicion.Fila.Length).ThenBy(u => u.Posicion.Fila, StringComparer.Ordinal)
            .ThenBy(u => u.Posicion.Nivel.Length).ThenBy(u => u.Posicion.Nivel, StringComparer.Ordinal)
            .ToList();

        // Las órdenes a palet cerrado pendientes se quedan con los primeros pallets, en el mismo orden
        // en que después los toma el corte que genera las órdenes de selección.
        var pedidas = Almacenes.OrdenesPreparacionAlmacen.OrdenesPreparacion
            .Where(p => p.Estado == Almacenes.EstadoOrdenPreparacionEnum.Pendiente
                        && p.Modalidad == Almacenes.ModalidadEmpaquetadoEnum.PaletCerrado
                        && p.CodigoCliente == mercaderia.CodigoCliente && p.CodigoDeposito == mercaderia.CodigoDeposito)
            .OrderBy(p => p.FechaRetiro).ThenBy(p => p.Numero)
            .SelectMany(p => p.Renglones.Where(r => r.Sku == mercaderia.Sku));
        foreach (var renglon in pedidas)
        {
            int unidades = 0;
            while (unidades < renglon.Cantidad && completos.Count > 0)
            {
                unidades += completos[0].Cantidad;
                completos.RemoveAt(0);
            }
        }

        // Solo los pallets cuyas unidades entran en el stock disponible (lo que no está comprometido).
        var libres = new List<Almacenes.UbicacionMercaderia>();
        int acumulado = 0;
        foreach (var pallet in completos)
        {
            if (acumulado + pallet.Cantidad > mercaderia.CantidadDisponible)
            {
                break;
            }
            acumulado += pallet.Cantidad;
            libres.Add(pallet);
        }
        return libres;
    }

    // Un pallet puede salir a palet cerrado si tiene un solo SKU: si su código está en una sola ubicación.
    private static bool EsPalletDeUnSoloSku(string codigoPallet)
    {
        return Almacenes.MercaderiasAlmacen.Mercaderias
            .SelectMany(m => m.Ubicaciones)
            .Count(u => u.CodigoPallet == codigoPallet) == 1;
    }

    private Almacenes.MercaderiaEntidad MercaderiaDe(Producto producto)
    {
        var depositoEntidad = Almacenes.DepositosAlmacen.Depositos.First(d => d.Nombre == DepositoSeleccionado!.Nombre);
        return Almacenes.MercaderiasAlmacen.Mercaderias.First(m => m.CodigoCliente == codigoCliente
            && m.CodigoDeposito == depositoEntidad.Codigo && m.Sku == producto.Sku);
    }

    public bool RequiereDatosEtiquetado => Modalidad == ModalidadEmpaquetado.Fullfilment;

    public DateTime FechaMinimaRetiro => DateTime.Today;

    public string UnidadDeCantidad => Modalidad == ModalidadEmpaquetado.PaletCerrado ? "palets" : "unidades";

    public List<Producto> ProductosDisponibles =>
        DepositoSeleccionado?.Stock.Select(s => s.Producto).ToList() ?? new List<Producto>();

    public string? SeleccionarDeposito(Deposito? deposito)
    {
        if (deposito == DepositoSeleccionado)
        {
            return null;
        }

        bool habiaDepositoSeleccionado = DepositoSeleccionado != null;
        DepositoSeleccionado = deposito;
        ItemsOrden.Clear();

        if (!habiaDepositoSeleccionado)
        {
            return null;
        }

        return "Se cambió el depósito, la orden actual ha sido reiniciada";
    }

    public string? SeleccionarModalidad(ModalidadEmpaquetado? modalidad)
    {
        if (modalidad == Modalidad)
        {
            return null;
        }

        Modalidad = modalidad;

        if (ItemsOrden.Count == 0)
        {
            return null;
        }

        ItemsOrden.Clear();
        return "Se cambió la modalidad de empaquetado, la orden actual ha sido reiniciada";
    }

    public bool EsCaracterValidoParaNombre(char caracter)
    {
        return char.IsLetter(caracter) || caracter == ' ';
    }

    public string QuitarCaracteresInvalidosDeNombre(string nombre)
    {
        return new string(nombre.Where(EsCaracterValidoParaNombre).ToArray());
    }

    public string? AgregarProducto(Producto? producto, int? cantidad)
    {
        if (Modalidad == null)
        {
            return "Error: seleccione una modalidad de empaquetado antes de agregar productos";
        }

        if (DepositoSeleccionado == null || producto == null || cantidad == null || cantidad <= 0)
        {
            return "Error: seleccione un producto e ingrese una cantidad mayor a cero";
        }

        StockProducto? stock = DepositoSeleccionado.Stock.Find(s => s.Producto == producto);
        ItemOrden? item = ItemsOrden.Find(i => i.Producto == producto);
        int cantidadYaEnOrden = item?.Cantidad ?? 0;
        int cantidadDisponible = Modalidad == ModalidadEmpaquetado.PaletCerrado
            ? stock?.CantidadPalets ?? 0
            : stock?.CantidadUnidades ?? 0;

        if (cantidadDisponible < cantidadYaEnOrden + cantidad.Value)
        {
            return "Error: Stock insuficiente";
        }

        if (item == null)
        {
            ItemsOrden.Add(new ItemOrden { Producto = producto, Cantidad = cantidad.Value });
        }
        else
        {
            item.Cantidad += cantidad.Value;
        }

        return null;
    }

    // Devuelve el error a mostrar, o null si la orden se puede confirmar.
    public string? ValidarOrden()
    {
        bool faltanDatosEtiquetado = RequiereDatosEtiquetado
            && (string.IsNullOrWhiteSpace(NombreDestinatario) || string.IsNullOrWhiteSpace(DireccionDestino));

        if (DepositoSeleccionado == null || Modalidad == null || faltanDatosEtiquetado
            || string.IsNullOrWhiteSpace(NombreTransportista)
            || DniTransportista == null
            || string.IsNullOrWhiteSpace(EmpresaTransportista))
        {
            return "Error: por favor rellene todos los campos antes de completar la orden";
        }

        if (ItemsOrden.Count == 0)
        {
            return "Error: la orden de preparación debe contener al menos 1 producto";
        }

        return null;
    }

    public List<string> RegistrarOrden()
    {
        var mensajes = new List<string>();

        // Unidades de cada producto: a palet cerrado son las de los pallets completos que se le asignan
        // (los primeros libres por posición); en fulfillment, las pedidas.
        var unidadesPorItem = new Dictionary<ItemOrden, int>();
        foreach (ItemOrden item in ItemsOrden)
        {
            var mercaderia = MercaderiaDe(item.Producto);
            int unidades = Modalidad == ModalidadEmpaquetado.PaletCerrado
                ? PalletsLibres(mercaderia).Take(item.Cantidad).Sum(u => u.Cantidad)
                : item.Cantidad;
            unidadesPorItem[item] = unidades;

            if (item.Producto.StockMinimoContratado != null
                && mercaderia.CantidadDisponible - unidades < item.Producto.StockMinimoContratado)
            {
                mensajes.Add($"Advertencia: El producto {item.Producto.Nombre} ha quedado por debajo del límite establecido, por favor generar un nuevo ingreso de este");
            }
        }

        // Se guarda la orden en Datos y lo pedido queda comprometido en el stock del cliente.
        // Los renglones guardan unidades en las dos modalidades.
        var clienteEntidad = Almacenes.ClientesAlmacen.Clientes.First(c => c.RazonSocial == NombreCliente);
        var depositoEntidad = Almacenes.DepositosAlmacen.Depositos.First(d => d.Nombre == DepositoSeleccionado!.Nombre);
        var ordenesEntidad = Almacenes.OrdenesPreparacionAlmacen.OrdenesPreparacion;
        var ordenEntidad = new Almacenes.OrdenPreparacionEntidad
        {
            Numero = ordenesEntidad.Count > 0 ? ordenesEntidad.Max(o => o.Numero) + 1 : 1,
            CodigoCliente = clienteEntidad.Codigo,
            CodigoDeposito = depositoEntidad.Codigo,
            Modalidad = Modalidad == ModalidadEmpaquetado.PaletCerrado
                ? Almacenes.ModalidadEmpaquetadoEnum.PaletCerrado
                : Almacenes.ModalidadEmpaquetadoEnum.Fulfillment,
            FechaRetiro = DateTime.SpecifyKind(FechaRetiro.Date, DateTimeKind.Unspecified),
            NombreDestinatario = RequiereDatosEtiquetado ? NombreDestinatario.Trim() : "",
            DireccionDestino = RequiereDatosEtiquetado ? DireccionDestino.Trim() : "",
            NombreTransportista = NombreTransportista.Trim(),
            DniTransportista = DniTransportista!.Value,
            EmpresaTransporte = EmpresaTransportista.Trim(),
            Estado = Almacenes.EstadoOrdenPreparacionEnum.Pendiente,
            OrdenSeleccionCumplida = false,
            RequiereDatosEtiquetado = RequiereDatosEtiquetado
        };

        foreach (ItemOrden item in ItemsOrden)
        {
            ordenEntidad.Renglones.Add(new Almacenes.RenglonOrdenPreparacion
            {
                Sku = item.Producto.Sku,
                Cantidad = unidadesPorItem[item],
                Producto = item.Producto.Nombre
            });

            var mercaderia = Almacenes.MercaderiasAlmacen.Mercaderias.First(m => m.CodigoCliente == clienteEntidad.Codigo
                && m.CodigoDeposito == depositoEntidad.Codigo && m.Sku == item.Producto.Sku);
            mercaderia.CantidadComprometida += unidadesPorItem[item];
            mercaderia.CantidadDisponible = mercaderia.CantidadFisica - mercaderia.CantidadComprometida;
        }

        ordenesEntidad.Add(ordenEntidad);
        Almacenes.OrdenesPreparacionAlmacen.Guardar();
        Almacenes.MercaderiasAlmacen.Guardar();

        // El stock que muestra la pantalla se vuelve a calcular: unidades disponibles y pallets libres.
        foreach (StockProducto stock in DepositoSeleccionado!.Stock)
        {
            var mercaderia = MercaderiaDe(stock.Producto);
            stock.UnidadesSueltas = mercaderia.CantidadDisponible;
            stock.CantidadPalets = PalletsLibres(mercaderia).Count;
        }

        mensajes.Add("Orden de preparación guardada con éxito");

        DepositoSeleccionado = null;
        Modalidad = null;
        ItemsOrden.Clear();
        FechaRetiro = DateTime.Today;
        NombreDestinatario = "";
        DireccionDestino = "";
        NombreTransportista = "";
        DniTransportista = null;
        EmpresaTransportista = "";

        return mensajes;
    }
}

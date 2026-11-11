#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoE.RegistrarOrdenDePreparacion;

internal class RegistrarOrdenDePreparacionModelo
{
    public string NombreCliente { get; }
    public List<Deposito> Depositos { get; }
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
        // La pantalla registra órdenes para un único cliente: CLI-001.
        var clienteEntidad = Almacenes.ClientesAlmacen.Clientes.First(c => c.Codigo == "CLI-001");
        NombreCliente = clienteEntidad.RazonSocial;

        // Los palets todavía no se manejan: todo el stock se toma como unidades sueltas.
        var productos = Almacenes.ProductosAlmacen.Productos
            .Where(p => p.CodigoCliente == clienteEntidad.Codigo)
            .Select(p => new Producto { Sku = p.Sku, Nombre = p.Descripcion, StockMinimoContratado = p.StockMinimoContratado, UnidadesPorPalet = 0 })
            .ToList();

        // Solo los depósitos donde el cliente tiene mercadería, con su stock disponible (físico - comprometido).
        Depositos = new List<Deposito>();
        foreach (var depositoEntidad in Almacenes.DepositosAlmacen.Depositos)
        {
            var stock = Almacenes.MercaderiasAlmacen.Mercaderias
                .Where(m => m.CodigoCliente == clienteEntidad.Codigo && m.CodigoDeposito == depositoEntidad.Codigo)
                .Select(m => new StockProducto
                {
                    Producto = productos.First(p => p.Sku == m.Sku),
                    CantidadPalets = 0,
                    UnidadesSueltas = m.CantidadDisponible
                })
                .ToList();

            if (stock.Count > 0)
            {
                Depositos.Add(new Deposito { Nombre = depositoEntidad.Nombre, Stock = stock });
            }
        }
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

        if (Modalidad == ModalidadEmpaquetado.PaletCerrado)
        {
            return "La funcionalidad de pallets aún no está implementada";
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

        foreach (ItemOrden item in ItemsOrden)
        {
            StockProducto stock = DepositoSeleccionado!.Stock.Find(s => s.Producto == item.Producto)!;

            if (Modalidad == ModalidadEmpaquetado.PaletCerrado)
            {
                stock.CantidadPalets -= item.Cantidad;
            }
            else if (stock.UnidadesSueltas >= item.Cantidad)
            {
                stock.UnidadesSueltas -= item.Cantidad;
            }
            else
            {
                int unidadesFaltantes = item.Cantidad - stock.UnidadesSueltas;
                int paletsAAbrir = (unidadesFaltantes + item.Producto.UnidadesPorPalet - 1) / item.Producto.UnidadesPorPalet;
                stock.CantidadPalets -= paletsAAbrir;
                stock.UnidadesSueltas = paletsAAbrir * item.Producto.UnidadesPorPalet - unidadesFaltantes;
            }

            if (item.Producto.StockMinimoContratado != null && stock.CantidadUnidades < item.Producto.StockMinimoContratado)
            {
                mensajes.Add($"Advertencia: El producto {item.Producto.Nombre} ha quedado por debajo del límite establecido, por favor generar un nuevo ingreso de este");
            }
        }

        // Se guarda la orden en Datos y lo pedido queda comprometido en el stock del cliente.
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
                Cantidad = item.Cantidad,
                Producto = item.Producto.Nombre
            });

            var mercaderia = Almacenes.MercaderiasAlmacen.Mercaderias.First(m => m.CodigoCliente == clienteEntidad.Codigo
                && m.CodigoDeposito == depositoEntidad.Codigo && m.Sku == item.Producto.Sku);
            mercaderia.CantidadComprometida += item.Cantidad;
            mercaderia.CantidadDisponible = mercaderia.CantidadFisica - mercaderia.CantidadComprometida;
        }

        ordenesEntidad.Add(ordenEntidad);
        Almacenes.OrdenesPreparacionAlmacen.Guardar();
        Almacenes.MercaderiasAlmacen.Guardar();

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

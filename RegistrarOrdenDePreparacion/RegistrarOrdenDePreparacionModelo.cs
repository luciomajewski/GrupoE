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
        NombreCliente = "Distribuidora Del Plata S.A.";

        var yerba = new Producto { Sku = "YER-500-001", Nombre = "Yerba mate 500 g", StockMinimoContratado = 500, UnidadesPorPalet = 480 };
        var cafe = new Producto { Sku = "CAF-250-002", Nombre = "Café molido 250 g", UnidadesPorPalet = 600 };
        var azucar = new Producto { Sku = "AZU-1000-003", Nombre = "Azúcar 1 kg", StockMinimoContratado = 400, UnidadesPorPalet = 400 };
        var aceite = new Producto { Sku = "ACE-900-004", Nombre = "Aceite de girasol 900 ml", UnidadesPorPalet = 360 };
        var harina = new Producto { Sku = "HAR-1000-005", Nombre = "Harina 000 1 kg", StockMinimoContratado = 400, UnidadesPorPalet = 400 };

        Depositos = new List<Deposito>
        {
            new Deposito
            {
                Nombre = "Depósito Avellaneda",
                Stock = new List<StockProducto>
                {
                    new StockProducto { Producto = yerba, CantidadPalets = 3, UnidadesSueltas = 120 },
                    new StockProducto { Producto = cafe, CantidadPalets = 2, UnidadesSueltas = 0 },
                    new StockProducto { Producto = azucar, CantidadPalets = 1, UnidadesSueltas = 200 },
                    new StockProducto { Producto = aceite, CantidadPalets = 4, UnidadesSueltas = 90 }
                }
            },
            new Deposito
            {
                Nombre = "Depósito Pilar",
                Stock = new List<StockProducto>
                {
                    new StockProducto { Producto = yerba, CantidadPalets = 2, UnidadesSueltas = 40 },
                    new StockProducto { Producto = harina, CantidadPalets = 3, UnidadesSueltas = 150 },
                    new StockProducto { Producto = aceite, CantidadPalets = 1, UnidadesSueltas = 0 }
                }
            },
            new Deposito
            {
                Nombre = "Depósito Córdoba",
                Stock = new List<StockProducto>
                {
                    new StockProducto { Producto = cafe, CantidadPalets = 5, UnidadesSueltas = 300 },
                    new StockProducto { Producto = azucar, CantidadPalets = 6, UnidadesSueltas = 0 },
                    new StockProducto { Producto = harina, CantidadPalets = 1, UnidadesSueltas = 50 }
                }
            }
        };
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

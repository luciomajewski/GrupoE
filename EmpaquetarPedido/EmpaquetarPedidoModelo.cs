namespace GrupoE.EmpaquetarPedido;

internal class EmpaquetarPedidoModelo
{
    // Operario identificado en el sistema y su depósito asignado (precondición del caso de uso).
    public string Usuario { get; } = "OP-PREP-01";
    public int Deposito { get; } = 1;

    public OrdenPreparacion? OrdenActual { get; private set; }

    private readonly List<OrdenPreparacion> ordenesPreparacion = new()
    {
        // Ya preparada: no se muestra.
        new OrdenPreparacion
        {
            Numero = 2043, Deposito = 1, TipoPedido = TipoPedido.Fulfillment, Transportista = "Expreso Litoral",
            OrdenSeleccionCumplida = true, Estado = EstadoOrdenPreparacion.Preparada,
            Renglones = new()
            {
                new RenglonOrdenPreparacion { Sku = "YER-1000", Cantidad = 4 }
            }
        },
        // Tomada por otro puesto: no se muestra.
        new OrdenPreparacion
        {
            Numero = 2044, Deposito = 1, TipoPedido = TipoPedido.Fulfillment, Transportista = "Expreso Litoral",
            OrdenSeleccionCumplida = true, Estado = EstadoOrdenPreparacion.EnPreparacion, TomadaPor = "OP-PREP-02",
            Renglones = new()
            {
                new RenglonOrdenPreparacion { Sku = "ACE-0900", Cantidad = 12 }
            }
        },
        new OrdenPreparacion
        {
            Numero = 2045, Deposito = 1, TipoPedido = TipoPedido.Fulfillment, Transportista = "Expreso Litoral",
            OrdenSeleccionCumplida = true, Estado = EstadoOrdenPreparacion.EnPreparacion,
            Renglones = new()
            {
                new RenglonOrdenPreparacion { Sku = "YER-1000", Cantidad = 10 },
                new RenglonOrdenPreparacion { Sku = "ACE-0900", Cantidad = 6 },
                new RenglonOrdenPreparacion { Sku = "ARR-0500", Cantidad = 8 }
            }
        },
        new OrdenPreparacion
        {
            Numero = 2046, Deposito = 1, TipoPedido = TipoPedido.PaleCerrado, Transportista = "Transportes Del Sur",
            OrdenSeleccionCumplida = true, Estado = EstadoOrdenPreparacion.EnPreparacion,
            Renglones = new()
            {
                new RenglonOrdenPreparacion { Sku = "LIC-0200", Cantidad = 24 },
                new RenglonOrdenPreparacion { Sku = "PAV-0150", Cantidad = 36 }
            }
        },
        // De otro depósito: no se muestra.
        new OrdenPreparacion
        {
            Numero = 2047, Deposito = 2, TipoPedido = TipoPedido.Fulfillment, Transportista = "Expreso Litoral",
            OrdenSeleccionCumplida = true, Estado = EstadoOrdenPreparacion.EnPreparacion,
            Renglones = new()
            {
                new RenglonOrdenPreparacion { Sku = "YER-1000", Cantidad = 2 }
            }
        },
        // Su orden de selección todavía no está "Cumplida": no se muestra.
        new OrdenPreparacion
        {
            Numero = 2048, Deposito = 1, TipoPedido = TipoPedido.PaleCerrado, Transportista = "Transportes Del Sur",
            OrdenSeleccionCumplida = false, Estado = EstadoOrdenPreparacion.EnPreparacion,
            Renglones = new()
            {
                new RenglonOrdenPreparacion { Sku = "LIC-0200", Cantidad = 48 }
            }
        }
    };

    private readonly List<OrdenEntrega> ordenesEntrega = new()
    {
        new OrdenEntrega
        {
            Numero = 7310, FechaHora = new DateTime(2026, 10, 26, 18, 30, 0), Deposito = 1,
            NumeroOrdenPreparacion = 2043, Transportista = "Expreso Litoral", Usuario = "OP-PREP-01",
            Estado = EstadoOrdenEntrega.Pendiente
        }
    };

    // Paso 2: orden "En preparación" del depósito, con su orden de selección "Cumplida" y no tomada
    // por otro operario. Toma la de número más bajo y la marca como tomada con el código del usuario.
    // Devuelve null si no hay órdenes para preparar (excepción del paso 2).
    public OrdenPreparacion? TomarSiguienteOrden()
    {
        OrdenActual = ordenesPreparacion
            .Where(o => o.Deposito == Deposito
                        && o.Estado == EstadoOrdenPreparacion.EnPreparacion
                        && o.OrdenSeleccionCumplida
                        && (o.TomadaPor == null || o.TomadaPor == Usuario))
            .OrderBy(o => o.Numero)
            .FirstOrDefault();

        if (OrdenActual != null)
        {
            OrdenActual.TomadaPor = Usuario;
        }
        return OrdenActual;
    }

    public string DescripcionTipoPedido(TipoPedido tipoPedido)
    {
        return tipoPedido == TipoPedido.Fulfillment ? "Fulfillment" : "Palé cerrado";
    }

    // Pasos 8 y 9: registra la orden de entrega "Pendiente" y deja la orden de preparación
    // "Preparada" con fecha, hora y usuario, liberándola. Devuelve null si no hay orden con productos.
    public OrdenEntrega? ConfirmarEmpaquetado()
    {
        if (OrdenActual == null || OrdenActual.Renglones.Count == 0)
        {
            return null;
        }

        DateTime ahora = DateTime.Now;
        var ordenEntrega = new OrdenEntrega
        {
            Numero = ordenesEntrega.Max(o => o.Numero) + 1,
            FechaHora = ahora,
            Deposito = Deposito,
            NumeroOrdenPreparacion = OrdenActual.Numero,
            Transportista = OrdenActual.Transportista,
            Usuario = Usuario,
            Estado = EstadoOrdenEntrega.Pendiente
        };
        ordenesEntrega.Add(ordenEntrega);

        OrdenActual.Estado = EstadoOrdenPreparacion.Preparada;
        OrdenActual.FechaPreparada = ahora;
        OrdenActual.PreparadaPor = Usuario;
        OrdenActual.TomadaPor = null;
        OrdenActual = null;

        return ordenEntrega;
    }

    // Cierre sin confirmar: la orden sigue "En preparación" y queda liberada para otro puesto.
    public void LiberarOrdenActual()
    {
        if (OrdenActual == null)
        {
            return;
        }

        OrdenActual.TomadaPor = null;
        OrdenActual = null;
    }
}

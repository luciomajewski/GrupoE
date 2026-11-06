#nullable enable
namespace GrupoE.EntregaATransportista;

internal class EntregaATransportistaModelo
{
    // Usuario identificado en el sistema y su depósito.
    public string CodigoUsuario { get; } = "OPD01";
    public string CodigoDeposito { get; } = "DEP01";

    // Datos de prueba de la planilla "Prueba - Entregar mercadería al transportista".
    // 99999999 no figura en ninguna orden a propósito (excepción "no autorizado").
    public List<OrdenPreparacion> OrdenesPreparacion { get; } = new()
    {
        // Martínez, Luis: dos órdenes listas para retirar en DEP01.
        new OrdenPreparacion { Numero = 2001, CodigoCliente = "CLI001", CodigoDeposito = "DEP01", TransportistaNombre = "Martínez, Luis", TransportistaDni = 30123456, Estado = EstadoOrdenPreparacion.Preparada },
        new OrdenPreparacion { Numero = 2005, CodigoCliente = "CLI002", CodigoDeposito = "DEP01", TransportistaNombre = "Martínez, Luis", TransportistaDni = 30123456, Estado = EstadoOrdenPreparacion.Preparada },
        // Martínez, Luis en otro depósito: no debe aparecer en la grilla.
        new OrdenPreparacion { Numero = 2006, CodigoCliente = "CLI003", CodigoDeposito = "DEP02", TransportistaNombre = "Martínez, Luis", TransportistaDni = 30123456, Estado = EstadoOrdenPreparacion.Preparada },
        // Gómez, Carlos: otro transportista con una orden lista.
        new OrdenPreparacion { Numero = 2002, CodigoCliente = "CLI001", CodigoDeposito = "DEP01", TransportistaNombre = "Gómez, Carlos", TransportistaDni = 27888999, Estado = EstadoOrdenPreparacion.Preparada },
        // Sosa, Ana: autorizada, pero una orden ya fue despachada y la otra todavía no está preparada.
        new OrdenPreparacion { Numero = 2003, CodigoCliente = "CLI002", CodigoDeposito = "DEP01", TransportistaNombre = "Sosa, Ana", TransportistaDni = 28555111, Estado = EstadoOrdenPreparacion.Cumplida, NumeroRemito = 5000 },
        new OrdenPreparacion { Numero = 2004, CodigoCliente = "CLI003", CodigoDeposito = "DEP01", TransportistaNombre = "Sosa, Ana", TransportistaDni = 28555111, Estado = EstadoOrdenPreparacion.Pendiente }
    };

    public List<OrdenEntrega> OrdenesEntrega { get; } = new()
    {
        new OrdenEntrega { NumeroOrdenPreparacion = 2001, CodigoDeposito = "DEP01", CantidadPaquetes = 3, Estado = EstadoOrdenEntrega.Pendiente },
        new OrdenEntrega { NumeroOrdenPreparacion = 2002, CodigoDeposito = "DEP01", CantidadPaquetes = 1, Estado = EstadoOrdenEntrega.Pendiente },
        new OrdenEntrega { NumeroOrdenPreparacion = 2003, CodigoDeposito = "DEP01", CantidadPaquetes = 4, Estado = EstadoOrdenEntrega.Despachada, FechaHoraDespacho = new DateTime(2026, 10, 1, 10, 30, 0) },
        new OrdenEntrega { NumeroOrdenPreparacion = 2005, CodigoDeposito = "DEP01", CantidadPaquetes = 2, Estado = EstadoOrdenEntrega.Pendiente },
        new OrdenEntrega { NumeroOrdenPreparacion = 2006, CodigoDeposito = "DEP02", CantidadPaquetes = 4, Estado = EstadoOrdenEntrega.Pendiente }
    };

    // Último remito registrado: el próximo será el 5001.
    public List<RemitoEntrega> RemitosEntrega { get; } = new()
    {
        new RemitoEntrega { Numero = 5000, FechaHora = new DateTime(2026, 10, 1, 10, 15, 0), CodigoDeposito = "DEP01", TransportistaNombre = "Sosa, Ana", TransportistaDni = 28555111, CodigoUsuario = "OPD01", Estado = EstadoRemito.Firmado, FechaHoraCumplido = new DateTime(2026, 10, 1, 10, 30, 0), CodigoUsuarioCumplido = "OPD01" }
    };

    public List<DetalleRemitoEntrega> DetallesRemitoEntrega { get; } = new();
    public List<MovimientoMercaderia> MovimientosMercaderia { get; } = new();

    // Entrega en curso.
    public string? TransportistaNombre { get; private set; }
    public int? TransportistaDni { get; private set; }
    public List<OrdenEntrega> OrdenesAEntregar { get; } = new();
    public RemitoEntrega? Remito { get; private set; }
    public bool RemitoFirmado { get; set; }

    public bool PuedeBuscar => Remito == null;
    public bool PuedeGenerarRemito => Remito == null && OrdenesAEntregar.Count > 0;
    public bool PuedeMarcarRemitoFirmado => Remito != null;
    public bool PuedeConfirmarDespacho => Remito != null && RemitoFirmado;

    // Devuelve el error a mostrar, o null si encontró órdenes para el transportista.
    // Si está autorizado pero no tiene órdenes pendientes, el nombre queda cargado igual.
    public string? Buscar(int? dni)
    {
        Reiniciar();

        if (dni == null || dni < 10000000 || dni > 99999999)
        {
            return "Ingrese un DNI válido";
        }

        // Autorizado: el DNI figura en alguna orden de preparación del depósito.
        List<OrdenPreparacion> ordenesDelTransportista = OrdenesPreparacion
            .Where(op => op.CodigoDeposito == CodigoDeposito && op.TransportistaDni == dni)
            .OrderBy(op => op.Numero)
            .ToList();

        if (ordenesDelTransportista.Count == 0)
        {
            return "El transportista no está autorizado a retirar mercadería en este depósito";
        }

        TransportistaNombre = ordenesDelTransportista[0].TransportistaNombre;
        TransportistaDni = dni;

        foreach (OrdenPreparacion op in ordenesDelTransportista)
        {
            OrdenEntrega? oe = OrdenesEntrega.Find(oe => oe.NumeroOrdenPreparacion == op.Numero
                                                      && oe.CodigoDeposito == CodigoDeposito
                                                      && oe.Estado == EstadoOrdenEntrega.Pendiente);
            if (oe != null)
            {
                OrdenesAEntregar.Add(oe);
            }
        }

        if (OrdenesAEntregar.Count == 0)
        {
            return "El transportista no tiene órdenes listas para retirar";
        }

        // Se muestra el nombre registrado en las órdenes que se van a entregar.
        TransportistaNombre = BuscarOrdenPreparacion(OrdenesAEntregar[0]).TransportistaNombre;
        return null;
    }

    // Paso 8: registra el remito en estado Emitido y un renglón de detalle por orden.
    public RemitoEntrega GenerarRemito()
    {
        if (!PuedeGenerarRemito)
        {
            throw new InvalidOperationException("No hay órdenes para generar el remito, o el remito ya fue generado.");
        }

        int numero = RemitosEntrega.Count == 0 ? 1 : RemitosEntrega.Max(r => r.Numero) + 1;

        Remito = new RemitoEntrega
        {
            Numero = numero,
            FechaHora = DateTime.Now,
            CodigoDeposito = CodigoDeposito,
            TransportistaNombre = TransportistaNombre!,
            TransportistaDni = TransportistaDni!.Value,
            CodigoUsuario = CodigoUsuario,
            Estado = EstadoRemito.Emitido
        };
        RemitosEntrega.Add(Remito);

        foreach (OrdenEntrega oe in OrdenesAEntregar)
        {
            DetallesRemitoEntrega.Add(new DetalleRemitoEntrega
            {
                NumeroRemito = numero,
                NumeroOrdenPreparacion = oe.NumeroOrdenPreparacion,
                CantidadPaquetes = oe.CantidadPaquetes
            });
        }

        return Remito;
    }

    // Pasos 14 a 16: registra el cumplido, despacha las órdenes y el egreso de mercadería.
    public void ConfirmarDespacho()
    {
        if (!PuedeConfirmarDespacho || Remito!.Estado != EstadoRemito.Emitido)
        {
            throw new InvalidOperationException("El remito tiene que estar generado y firmado para confirmar el despacho.");
        }

        DateTime ahora = DateTime.Now;

        Remito.Estado = EstadoRemito.Firmado;
        Remito.FechaHoraCumplido = ahora;
        Remito.CodigoUsuarioCumplido = CodigoUsuario;

        foreach (OrdenEntrega oe in OrdenesAEntregar)
        {
            OrdenPreparacion op = BuscarOrdenPreparacion(oe);
            op.Estado = EstadoOrdenPreparacion.Cumplida;
            op.NumeroRemito = Remito.Numero;

            oe.Estado = EstadoOrdenEntrega.Despachada;
            oe.FechaHoraDespacho = ahora;

            MovimientosMercaderia.Add(new MovimientoMercaderia
            {
                CodigoCliente = op.CodigoCliente,
                NumeroOrdenPreparacion = op.Numero,
                NumeroRemito = Remito.Numero,
                Fecha = ahora.Date
            });
        }
    }

    // Excepción "Cancelar": si el remito ya se había generado y no se confirmó, queda Anulado.
    // Las órdenes siguen Pendiente.
    public void Cancelar()
    {
        if (Remito != null && Remito.Estado == EstadoRemito.Emitido)
        {
            Remito.Estado = EstadoRemito.Anulado;
        }

        Reiniciar();
    }

    public void Reiniciar()
    {
        TransportistaNombre = null;
        TransportistaDni = null;
        OrdenesAEntregar.Clear();
        Remito = null;
        RemitoFirmado = false;
    }

    private OrdenPreparacion BuscarOrdenPreparacion(OrdenEntrega oe)
    {
        return OrdenesPreparacion.Find(op => op.Numero == oe.NumeroOrdenPreparacion && op.CodigoDeposito == oe.CodigoDeposito)!;
    }
}

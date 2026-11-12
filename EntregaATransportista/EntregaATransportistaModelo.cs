#nullable enable
namespace GrupoE.EntregaATransportista;

internal class EntregaATransportistaModelo
{
    // Usuario identificado en el sistema: el usuario de Windows con el que se abrió el programa.
    public string CodigoUsuario { get; } = Environment.UserName;

    // El depósito es el de las órdenes que se van a entregar (OrdenesPreparacion.json).
    public string CodigoDeposito => OrdenesAEntregar.Count == 0
        ? ""
        : Almacenes.OrdenesPreparacionAlmacen.OrdenesPreparacion.First(p => p.Numero == OrdenesAEntregar[0].NumeroOrdenPreparacion).CodigoDeposito;

    // Órdenes y remitos guardados en Datos: se cargan desde los almacenes en cada búsqueda.
    public List<OrdenPreparacion> OrdenesPreparacion { get; } = new();
    public List<OrdenEntrega> OrdenesEntrega { get; } = new();
    public List<RemitoEntrega> RemitosEntrega { get; } = new();

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

        // Órdenes y remitos guardados, convertidos a las clases de la pantalla.
        var preparacionesEntidad = Almacenes.OrdenesPreparacionAlmacen.OrdenesPreparacion;
        var entregasEntidad = Almacenes.OrdenesEntregaAlmacen.OrdenesEntrega;
        var remitosEntidad = Almacenes.RemitosEntregaAlmacen.RemitosEntrega;

        OrdenesPreparacion.Clear();
        OrdenesPreparacion.AddRange(preparacionesEntidad.Select(p => new OrdenPreparacion
        {
            Numero = p.Numero,
            CodigoCliente = p.CodigoCliente,
            CodigoDeposito = p.CodigoDeposito,
            TransportistaNombre = p.NombreTransportista,
            TransportistaDni = p.DniTransportista,
            Estado = p.Estado switch
            {
                Almacenes.EstadoOrdenPreparacionEnum.Preparada => EstadoOrdenPreparacion.Preparada,
                Almacenes.EstadoOrdenPreparacionEnum.Cumplida => EstadoOrdenPreparacion.Cumplida,
                _ => EstadoOrdenPreparacion.Pendiente
            },
            NumeroRemito = remitosEntidad
                .Where(r => r.Estado == Almacenes.EstadoRemitoEnum.Firmado
                            && entregasEntidad.Any(oe => oe.NumeroOrdenPreparacion == p.Numero && r.OrdenesEntrega.Contains(oe.Numero)))
                .Select(r => (int?)r.Numero)
                .FirstOrDefault()
        }));

        OrdenesEntrega.Clear();
        OrdenesEntrega.AddRange(entregasEntidad.Select(oe => new OrdenEntrega
        {
            NumeroOrdenPreparacion = oe.NumeroOrdenPreparacion,
            CodigoDeposito = preparacionesEntidad.First(p => p.Numero == oe.NumeroOrdenPreparacion).CodigoDeposito,
            CantidadPaquetes = oe.CantidadPaquetes,
            Estado = oe.Estado == Almacenes.EstadoOrdenEntregaEnum.Despachada ? EstadoOrdenEntrega.Despachada : EstadoOrdenEntrega.Pendiente,
            FechaHoraDespacho = oe.FechaHoraDespacho
        }));

        RemitosEntrega.Clear();
        RemitosEntrega.AddRange(remitosEntidad.Select(r => new RemitoEntrega
        {
            Numero = r.Numero,
            FechaHora = r.FechaHora,
            CodigoDeposito = r.CodigoDeposito,
            TransportistaNombre = r.NombreTransportista,
            TransportistaDni = r.DniTransportista,
            CodigoUsuario = r.CodigoUsuario,
            Estado = r.Estado switch
            {
                Almacenes.EstadoRemitoEnum.Firmado => EstadoRemito.Firmado,
                Almacenes.EstadoRemitoEnum.Anulado => EstadoRemito.Anulado,
                _ => EstadoRemito.Emitido
            },
            FechaHoraCumplido = r.FechaHoraCumplido,
            CodigoUsuarioCumplido = r.CodigoUsuarioCumplido
        }));

        // Autorizado: el DNI figura en alguna orden de preparación.
        var preparacionesDelTransportista = preparacionesEntidad
            .Where(p => p.DniTransportista == dni)
            .OrderBy(p => p.Numero)
            .ToList();

        if (preparacionesDelTransportista.Count == 0)
        {
            return "El transportista no está autorizado a retirar mercadería en este depósito";
        }

        TransportistaNombre = preparacionesDelTransportista[0].NombreTransportista;
        TransportistaDni = dni;

        // Órdenes de entrega pendientes que no estén ya en un remito emitido. El remito es de un solo
        // depósito: el de la primera orden lista del transportista.
        var enRemitoEmitido = remitosEntidad
            .Where(r => r.Estado == Almacenes.EstadoRemitoEnum.Emitido)
            .SelectMany(r => r.OrdenesEntrega)
            .ToHashSet();
        var listas = preparacionesDelTransportista
            .Where(p => entregasEntidad.Any(oe => oe.NumeroOrdenPreparacion == p.Numero
                                                  && oe.Estado == Almacenes.EstadoOrdenEntregaEnum.Pendiente
                                                  && !enRemitoEmitido.Contains(oe.Numero)))
            .ToList();

        if (listas.Count == 0)
        {
            return "El transportista no tiene órdenes listas para retirar";
        }

        foreach (var preparacion in listas.Where(p => p.CodigoDeposito == listas[0].CodigoDeposito))
        {
            OrdenesAEntregar.Add(OrdenesEntrega.First(oe => oe.NumeroOrdenPreparacion == preparacion.Numero));
        }

        // Se muestra el nombre registrado en las órdenes que se van a entregar.
        TransportistaNombre = listas[0].NombreTransportista;
        return null;
    }

    // Paso 8: registra el remito en estado Emitido y un renglón de detalle por orden.
    public RemitoEntrega GenerarRemito()
    {
        if (!PuedeGenerarRemito)
        {
            throw new InvalidOperationException("No hay órdenes para generar el remito, o el remito ya fue generado.");
        }

        var remitosEntidad = Almacenes.RemitosEntregaAlmacen.RemitosEntrega;
        int numero = remitosEntidad.Count == 0 ? 1 : remitosEntidad.Max(r => r.Numero) + 1;
        DateTime ahora = DateTime.Now;
        var fechaHora = new DateTime(ahora.Year, ahora.Month, ahora.Day, ahora.Hour, ahora.Minute, ahora.Second);

        Remito = new RemitoEntrega
        {
            Numero = numero,
            FechaHora = fechaHora,
            CodigoDeposito = CodigoDeposito,
            TransportistaNombre = TransportistaNombre!,
            TransportistaDni = TransportistaDni!.Value,
            CodigoUsuario = CodigoUsuario,
            Estado = EstadoRemito.Emitido
        };
        RemitosEntrega.Add(Remito);

        // Se guarda el remito Emitido con sus órdenes de entrega.
        remitosEntidad.Add(new Almacenes.RemitoEntregaEntidad
        {
            Numero = numero,
            FechaHora = fechaHora,
            OrdenesEntrega = OrdenesAEntregar
                .Select(oe => Almacenes.OrdenesEntregaAlmacen.OrdenesEntrega.First(e => e.NumeroOrdenPreparacion == oe.NumeroOrdenPreparacion).Numero)
                .ToList(),
            CodigoUsuario = CodigoUsuario,
            Estado = Almacenes.EstadoRemitoEnum.Emitido,
            CodigoDeposito = CodigoDeposito,
            NombreTransportista = TransportistaNombre!,
            DniTransportista = TransportistaDni!.Value
        });
        Almacenes.RemitosEntregaAlmacen.Guardar();

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

        DateTime momento = DateTime.Now;
        var ahora = new DateTime(momento.Year, momento.Month, momento.Day, momento.Hour, momento.Minute, momento.Second);

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

        // Se guarda: remito Firmado con el cumplido, órdenes de entrega Despachadas y órdenes de
        // preparación Cumplidas. El stock ya había salido de su posición al cumplir la selección.
        var remitoEntidad = Almacenes.RemitosEntregaAlmacen.RemitosEntrega.First(r => r.Numero == Remito.Numero);
        remitoEntidad.Estado = Almacenes.EstadoRemitoEnum.Firmado;
        remitoEntidad.FechaHoraCumplido = ahora;
        remitoEntidad.CodigoUsuarioCumplido = CodigoUsuario;

        foreach (var entregaEntidad in Almacenes.OrdenesEntregaAlmacen.OrdenesEntrega.Where(oe => remitoEntidad.OrdenesEntrega.Contains(oe.Numero)))
        {
            entregaEntidad.Estado = Almacenes.EstadoOrdenEntregaEnum.Despachada;
            entregaEntidad.FechaHoraDespacho = ahora;
            Almacenes.OrdenesPreparacionAlmacen.OrdenesPreparacion.First(p => p.Numero == entregaEntidad.NumeroOrdenPreparacion).Estado =
                Almacenes.EstadoOrdenPreparacionEnum.Cumplida;
        }

        Almacenes.RemitosEntregaAlmacen.Guardar();
        Almacenes.OrdenesEntregaAlmacen.Guardar();
        Almacenes.OrdenesPreparacionAlmacen.Guardar();
    }

    // Excepción "Cancelar": si el remito ya se había generado y no se confirmó, queda Anulado.
    // Las órdenes siguen Pendiente.
    public void Cancelar()
    {
        if (Remito != null && Remito.Estado == EstadoRemito.Emitido)
        {
            Remito.Estado = EstadoRemito.Anulado;

            var remitoEntidad = Almacenes.RemitosEntregaAlmacen.RemitosEntrega.First(r => r.Numero == Remito.Numero);
            remitoEntidad.Estado = Almacenes.EstadoRemitoEnum.Anulado;
            Almacenes.RemitosEntregaAlmacen.Guardar();
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

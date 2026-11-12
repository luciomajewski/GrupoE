namespace GrupoE.EmpaquetarPedido;

internal class EmpaquetarPedidoModelo
{
    // Operario identificado en el sistema: el usuario de Windows con el que se abrió el programa.
    public string Usuario { get; } = Environment.UserName;

    // El depósito es el código de la orden en pantalla (OrdenesPreparacion.json).
    public string Deposito => OrdenActual == null
        ? ""
        : Almacenes.OrdenesPreparacionAlmacen.OrdenesPreparacion.First(o => o.Numero == OrdenActual.Numero).CodigoDeposito;

    public OrdenPreparacion? OrdenActual { get; private set; }

    // Paso 2: orden "En preparación", con su orden de selección "Cumplida" y no tomada por otro operario.
    // Toma la de número más bajo y la marca como tomada con el código del usuario.
    // Devuelve null si no hay órdenes para preparar (excepción del paso 2).
    public OrdenPreparacion? TomarSiguienteOrden()
    {
        // Órdenes en preparación de OrdenesPreparacion.json, de cualquier depósito.
        var ordenesPreparacion = Almacenes.OrdenesPreparacionAlmacen.OrdenesPreparacion
            .Where(o => o.Estado == Almacenes.EstadoOrdenPreparacionEnum.EnPreparacion)
            .Select(o => new OrdenPreparacion
            {
                Numero = o.Numero,
                TipoPedido = o.Modalidad == Almacenes.ModalidadEmpaquetadoEnum.PaletCerrado ? TipoPedido.PaleCerrado : TipoPedido.Fulfillment,
                Transportista = o.EmpresaTransporte,
                OrdenSeleccionCumplida = o.OrdenSeleccionCumplida,
                Estado = EstadoOrdenPreparacion.EnPreparacion,
                FechaPreparada = o.FechaPreparada,
                PreparadaPor = o.PreparadaPor,
                Renglones = o.Renglones.Select(r => new RenglonOrdenPreparacion { Sku = r.Sku, Cantidad = r.Cantidad }).ToList()
            })
            .ToList();

        OrdenActual = ordenesPreparacion
            .Where(o => o.Estado == EstadoOrdenPreparacion.EnPreparacion
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
        var ordenesEntrega = Almacenes.OrdenesEntregaAlmacen.OrdenesEntrega;
        var ordenEntrega = new OrdenEntrega
        {
            Numero = ordenesEntrega.Count > 0 ? ordenesEntrega.Max(o => o.Numero) + 1 : 1,
            FechaHora = ahora,
            NumeroOrdenPreparacion = OrdenActual.Numero,
            Transportista = OrdenActual.Transportista,
            Usuario = Usuario,
            Estado = EstadoOrdenEntrega.Pendiente
        };

        // Se guarda en Datos: la orden de entrega pendiente (CantidadPaquetes = total de unidades de todos
        // los productos de la orden) y la orden de preparación queda Preparada. El transportista que
        // retira sale de la orden de preparación.
        var preparacionEntidad = Almacenes.OrdenesPreparacionAlmacen.OrdenesPreparacion.First(o => o.Numero == OrdenActual.Numero);
        var fechaHora = new DateTime(ahora.Year, ahora.Month, ahora.Day, ahora.Hour, ahora.Minute, ahora.Second);
        ordenesEntrega.Add(new Almacenes.OrdenEntregaEntidad
        {
            Numero = ordenEntrega.Numero,
            NumeroOrdenPreparacion = ordenEntrega.NumeroOrdenPreparacion,
            FechaHora = fechaHora,
            Usuario = Usuario,
            CantidadPaquetes = preparacionEntidad.Renglones.Sum(r => r.Cantidad),
            Estado = Almacenes.EstadoOrdenEntregaEnum.Pendiente,
            FechaHoraDespacho = null,
            NombreTransportista = preparacionEntidad.NombreTransportista,
            DniTransportista = preparacionEntidad.DniTransportista
        });
        preparacionEntidad.Estado = Almacenes.EstadoOrdenPreparacionEnum.Preparada;
        preparacionEntidad.FechaPreparada = fechaHora;
        preparacionEntidad.PreparadaPor = Usuario;
        Almacenes.OrdenesEntregaAlmacen.Guardar();
        Almacenes.OrdenesPreparacionAlmacen.Guardar();

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

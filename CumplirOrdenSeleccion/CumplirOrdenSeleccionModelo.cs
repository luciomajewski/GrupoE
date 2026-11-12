#nullable enable
namespace GrupoE.CumplirOrdenSeleccion;

internal class CumplirOrdenSeleccionModelo
{
    public string UsuarioActual { get; }

    // Excepción autorizada: el depósito es el de la orden en pantalla (OrdenesSeleccion.json).
    public string DepositoActual => OrdenActual == null
        ? ""
        : Almacenes.OrdenesSeleccionAlmacen.OrdenesSeleccion.First(o => o.Numero == OrdenActual.Numero).CodigoDeposito;
    public List<OrdenSeleccion> OrdenesSeleccion { get; }
    public List<ItemInventario> Inventario { get; }
    public OrdenSeleccion? OrdenActual { get; private set; }

    public DateTime Fecha => DateTime.Today;

    public CumplirOrdenSeleccionModelo()
    {
        // Operario identificado en el sistema: el usuario de Windows con el que se abrió el programa.
        UsuarioActual = Environment.UserName;

        // Órdenes de selección guardadas. Las posiciones se muestran como "A-01-1" (pasillo, fila y nivel).
        OrdenesSeleccion = Almacenes.OrdenesSeleccionAlmacen.OrdenesSeleccion
            .Select(o => new OrdenSeleccion
            {
                Numero = o.Numero,
                Deposito = o.CodigoDeposito,
                Estado = o.Estado == Almacenes.EstadoOrdenSeleccionEnum.Cumplida ? EstadoOrdenSeleccion.Cumplida : EstadoOrdenSeleccion.Pendiente,
                FechaHoraCumplida = o.FechaHoraCumplida,
                UsuarioCumplida = o.UsuarioCumplida,
                Renglones = o.Renglones.Select(r => new RenglonOrdenSeleccion
                {
                    Cliente = r.CodigoCliente,
                    Posicion = $"{r.Posicion.Pasillo}-{r.Posicion.Fila}-{r.Posicion.Nivel}",
                    Sku = r.Sku,
                    Descripcion = r.Descripcion,
                    Cantidad = r.Cantidad
                }).ToList()
            })
            .ToList();

        // El inventario es el del depósito de la orden: se carga al buscarla.
        Inventario = new List<ItemInventario>();
    }

    // Paso 2: toma la orden del depósito del usuario en estado Pendiente con el número más bajo.
    // Devuelve el mensaje a mostrar, o null si encontró una orden.
    public string? BuscarOrdenPendiente()
    {
        // Selección pendiente de número más bajo, de cualquier depósito.
        OrdenActual = OrdenesSeleccion
            .Where(o => o.Estado == EstadoOrdenSeleccion.Pendiente)
            .OrderBy(o => o.Numero)
            .FirstOrDefault();

        // Stock en guarda del depósito de la orden, por cliente, SKU y posición.
        Inventario.Clear();
        Inventario.AddRange(Almacenes.MercaderiasAlmacen.Mercaderias
            .Where(m => m.CodigoDeposito == DepositoActual)
            .SelectMany(m => m.Ubicaciones.Select(u => new ItemInventario
            {
                Cliente = m.CodigoCliente,
                Sku = m.Sku,
                Posicion = $"{u.Posicion.Pasillo}-{u.Posicion.Fila}-{u.Posicion.Nivel}",
                Cantidad = u.Cantidad
            })));

        // Excepción 2.1
        if (OrdenActual == null)
        {
            return "No hay órdenes de selección pendientes";
        }

        return null;
    }

    public bool HayOrdenPendiente => OrdenActual != null;

    // Paso 3
    public string NumeroOrdenActual => OrdenActual?.Numero.ToString() ?? "-";

    // Paso 4: renglones de la orden ordenados por posición (sector, posición y fila) de menor a mayor.
    public List<RenglonOrdenSeleccion> RenglonesOrdenados =>
        OrdenActual?.Renglones.OrderBy(r => ClaveDePosicion(r.Posicion)).ToList() ?? new List<RenglonOrdenSeleccion>();

    // Paso 7
    public string PreguntaConfirmacion =>
        $"¿Confirma que entregó a Preparación toda la mercadería de la orden de selección N° {OrdenActual?.Numero}?";

    // Pasos 9 a 11. Devuelve el mensaje a mostrar.
    public string CumplirOrden()
    {
        if (OrdenActual == null)
        {
            return "No hay una orden de selección para confirmar";
        }

        // Paso 9
        OrdenActual.Estado = EstadoOrdenSeleccion.Cumplida;
        OrdenActual.FechaHoraCumplida = DateTime.Now;
        OrdenActual.UsuarioCumplida = UsuarioActual;

        // Paso 10: resta del inventario cada renglón (clave: cliente, SKU y posición).
        foreach (var renglon in OrdenActual.Renglones)
        {
            var stock = Inventario.FirstOrDefault(i => i.Cliente == renglon.Cliente
                                                    && i.Sku == renglon.Sku
                                                    && i.Posicion == renglon.Posicion);
            if (stock != null)
            {
                stock.Cantidad -= renglon.Cantidad;
            }
        }

        // Se guarda en Datos: la orden queda cumplida, la mercadería sale de su posición (ya no está en guarda,
        // así que deja de estar comprometida) y sus órdenes de preparación quedan listas para empaquetar.
        var ordenEntidad = Almacenes.OrdenesSeleccionAlmacen.OrdenesSeleccion.First(o => o.Numero == OrdenActual.Numero);
        DateTime cumplida = OrdenActual.FechaHoraCumplida!.Value;
        ordenEntidad.Estado = Almacenes.EstadoOrdenSeleccionEnum.Cumplida;
        ordenEntidad.FechaHoraCumplida = new DateTime(cumplida.Year, cumplida.Month, cumplida.Day, cumplida.Hour, cumplida.Minute, cumplida.Second);
        ordenEntidad.UsuarioCumplida = UsuarioActual;

        foreach (var renglonEntidad in ordenEntidad.Renglones)
        {
            var mercaderia = Almacenes.MercaderiasAlmacen.Mercaderias.FirstOrDefault(m => m.CodigoCliente == renglonEntidad.CodigoCliente
                && m.Sku == renglonEntidad.Sku && m.CodigoDeposito == ordenEntidad.CodigoDeposito);
            var ubicacion = mercaderia?.Ubicaciones.FirstOrDefault(u => u.Posicion.Pasillo == renglonEntidad.Posicion.Pasillo
                && u.Posicion.Fila == renglonEntidad.Posicion.Fila && u.Posicion.Nivel == renglonEntidad.Posicion.Nivel);
            if (mercaderia == null || ubicacion == null)
            {
                continue;
            }

            ubicacion.Cantidad -= renglonEntidad.Cantidad;
            if (ubicacion.Cantidad <= 0)
            {
                mercaderia.Ubicaciones.Remove(ubicacion);
            }
            mercaderia.CantidadFisica = mercaderia.Ubicaciones.Sum(u => u.Cantidad);
            mercaderia.CantidadComprometida = Math.Max(0, mercaderia.CantidadComprometida - renglonEntidad.Cantidad);
            mercaderia.CantidadDisponible = mercaderia.CantidadFisica - mercaderia.CantidadComprometida;
        }

        foreach (var ordenPreparacion in Almacenes.OrdenesPreparacionAlmacen.OrdenesPreparacion
                     .Where(p => ordenEntidad.OrdenesPreparacion.Contains(p.Numero)))
        {
            ordenPreparacion.OrdenSeleccionCumplida = true;
        }

        Almacenes.OrdenesSeleccionAlmacen.Guardar();
        Almacenes.MercaderiasAlmacen.Guardar();
        Almacenes.OrdenesPreparacionAlmacen.Guardar();

        // Paso 11
        return $"Orden de selección N° {OrdenActual.Numero} cumplida";
    }

    // Convierte "A-01-2" en (1, 1, 2) para ordenar por número y no como texto.
    // El pasillo es una letra (A = 1, B = 2, ...); fila y nivel son números.
    private (int Sector, int Posicion, int Fila) ClaveDePosicion(string posicion)
    {
        var partes = posicion.Split('-');
        int pasillo = int.TryParse(partes[0], out int numero)
            ? numero
            : partes[0].ToUpper().Aggregate(0, (total, letra) => total * 26 + (letra - 'A' + 1));
        return (pasillo, int.Parse(partes[1]), int.Parse(partes[2]));
    }
}

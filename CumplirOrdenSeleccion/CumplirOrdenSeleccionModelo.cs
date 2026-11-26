#nullable enable
namespace GrupoE.CumplirOrdenSeleccion;

internal class CumplirOrdenSeleccionModelo
{
    public string UsuarioActual { get; }

    // El depósito es el de la orden en pantalla (OrdenesSeleccion.json).
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

        // Órdenes de selección guardadas. Los renglones quedan ordenados por posición (pasillo, fila y nivel)
        // comparando los datos de la posición, sin convertir texto: a menor largo, menor valor ("2" < "10").
        // Las posiciones se muestran como "A-01-1".
        OrdenesSeleccion = Almacenes.OrdenesSeleccionAlmacen.OrdenesSeleccion
            .Select(ConvertirOrden)
            .ToList();

        // El inventario es el del depósito de la orden: se carga al buscarla.
        Inventario = new List<ItemInventario>();
    }

    private static OrdenSeleccion ConvertirOrden(Almacenes.OrdenSeleccionEntidad o)
    {
        return new OrdenSeleccion
        {
            Numero = o.Numero,
            Deposito = o.CodigoDeposito,
            Estado = o.Estado == Almacenes.EstadoOrdenSeleccionEnum.Cumplida ? EstadoOrdenSeleccion.Cumplida : EstadoOrdenSeleccion.Pendiente,
            FechaHoraCumplida = o.FechaHoraCumplida,
            UsuarioCumplida = o.UsuarioCumplida,
            Renglones = OrdenarPorPosicion(o.Renglones, r => r.Posicion)
                .Select(r => new RenglonOrdenSeleccion
                {
                    Cliente = r.CodigoCliente,
                    Posicion = $"{r.Posicion.Pasillo}-{r.Posicion.Fila}-{r.Posicion.Nivel}",
                    Sku = r.Sku,
                    Descripcion = r.Descripcion,
                    Cantidad = r.Cantidad
                }).ToList()
        };
    }

    // Ordena por pasillo, fila y nivel sin convertir texto: a menor largo, menor valor ("2" < "10").
    private static IEnumerable<T> OrdenarPorPosicion<T>(IEnumerable<T> elementos, Func<T, Almacenes.Posicion> posicion)
    {
        return elementos
            .OrderBy(e => posicion(e).Pasillo.Length).ThenBy(e => posicion(e).Pasillo, StringComparer.Ordinal)
            .ThenBy(e => posicion(e).Fila.Length).ThenBy(e => posicion(e).Fila, StringComparer.Ordinal)
            .ThenBy(e => posicion(e).Nivel.Length).ThenBy(e => posicion(e).Nivel, StringComparer.Ordinal);
    }

    #region Corte: el sistema genera las órdenes de selección (desencadenante del caso de uso)

    // Horarios en los que el sistema hace el corte (una o dos veces por día).
    private static readonly TimeSpan[] HorariosCorte = { new TimeSpan(8, 0, 0), new TimeSpan(14, 0, 0) };

    // Último corte hecho mientras el programa está abierto (se comparte entre pantallas abiertas).
    // No se guarda en Datos: si se reabre el programa, el último corte vuelve a correr.
    private static DateTime ultimoCorteEjecutado = DateTime.MinValue;

    // Último horario de corte que ya pasó (hoy o, antes de las 08:00, el de ayer a las 14:00).
    private static DateTime UltimoCorteProgramado(DateTime ahora)
    {
        for (int diasAtras = 0; diasAtras <= 1; diasAtras++)
        {
            DateTime dia = ahora.Date.AddDays(-diasAtras);
            foreach (TimeSpan horario in HorariosCorte.OrderByDescending(h => h))
            {
                if (dia + horario <= ahora)
                {
                    return dia + horario;
                }
            }
        }
        return ahora.Date.AddDays(-1) + HorariosCorte.Max();
    }

    // Si ya pasó un horario de corte que no se hizo, genera las órdenes de selección
    // y las suma a la lista de la pantalla.
    private void EjecutarCorteSiCorresponde()
    {
        DateTime corte = UltimoCorteProgramado(DateTime.Now);
        if (ultimoCorteEjecutado >= corte)
        {
            return;
        }
        ultimoCorteEjecutado = corte;

        foreach (var nueva in GenerarOrdenesSeleccion())
        {
            OrdenesSeleccion.Add(ConvertirOrden(nueva));
        }
    }

    // Junta las órdenes de preparación "Pendiente" de cada depósito en una orden de selección "Pendiente"
    // y las pasa a "En preparación". Devuelve las órdenes de selección generadas.
    private static List<Almacenes.OrdenSeleccionEntidad> GenerarOrdenesSeleccion()
    {
        var generadas = new List<Almacenes.OrdenSeleccionEntidad>();
        var ordenesSeleccion = Almacenes.OrdenesSeleccionAlmacen.OrdenesSeleccion;
        // Primero las de palet cerrado, así los pallets completos no se desarman antes para fulfillment.
        var pendientes = Almacenes.OrdenesPreparacionAlmacen.OrdenesPreparacion
            .Where(p => p.Estado == Almacenes.EstadoOrdenPreparacionEnum.Pendiente)
            .OrderBy(p => p.Modalidad == Almacenes.ModalidadEmpaquetadoEnum.PaletCerrado ? 0 : 1)
            .ThenBy(p => p.FechaRetiro).ThenBy(p => p.Numero)
            .ToList();

        foreach (var depositoPendientes in pendientes.GroupBy(p => p.CodigoDeposito))
        {
            string deposito = depositoPendientes.Key;

            // Lo que ya reservan en cada posición las órdenes de selección pendientes del depósito.
            var reservado = new Dictionary<string, int>();
            foreach (var r in ordenesSeleccion
                         .Where(s => s.Estado == Almacenes.EstadoOrdenSeleccionEnum.Pendiente && s.CodigoDeposito == deposito)
                         .SelectMany(s => s.Renglones))
            {
                string clave = ClaveReserva(r.CodigoCliente, r.Sku, r.Posicion);
                reservado[clave] = reservado.GetValueOrDefault(clave) + r.Cantidad;
            }

            var nueva = new Almacenes.OrdenSeleccionEntidad
            {
                CodigoDeposito = deposito,
                Estado = Almacenes.EstadoOrdenSeleccionEnum.Pendiente
            };

            foreach (var preparacion in depositoPendientes)
            {
                // Se arma aparte: si algún renglón no tiene stock en posiciones, la orden de preparación
                // no entra y sigue "Pendiente" para el próximo corte.
                var reservadoConEsta = new Dictionary<string, int>(reservado);
                var renglonesDeEsta = new List<Almacenes.RenglonOrdenSeleccion>();
                bool alcanza = true;

                foreach (var renglon in preparacion.Renglones)
                {
                    var mercaderia = Almacenes.MercaderiasAlmacen.Mercaderias.FirstOrDefault(m =>
                        m.CodigoCliente == preparacion.CodigoCliente && m.Sku == renglon.Sku && m.CodigoDeposito == deposito);
                    int falta = renglon.Cantidad;
                    bool paletCerrado = preparacion.Modalidad == Almacenes.ModalidadEmpaquetadoEnum.PaletCerrado;

                    // Se toma de las posiciones en el orden del recorrido; si una no alcanza, se sigue con la próxima.
                    // Palet cerrado: solo pallets completos (con CodigoPallet) y enteros, sin nada reservado.
                    // Fulfillment: primero los pallets ya desarmados, para no desconsolidar otros sin necesidad.
                    var ubicaciones = mercaderia == null
                        ? new List<Almacenes.UbicacionMercaderia>()
                        : paletCerrado
                            ? OrdenarPorPosicion(mercaderia.Ubicaciones.Where(u => u.CodigoPallet != null && EsPalletDeUnSoloSku(u.CodigoPallet)), u => u.Posicion).ToList()
                            : OrdenarPorPosicion(mercaderia.Ubicaciones.Where(u => u.CodigoPallet == null), u => u.Posicion)
                                .Concat(OrdenarPorPosicion(mercaderia.Ubicaciones.Where(u => u.CodigoPallet != null), u => u.Posicion))
                                .ToList();
                    foreach (var ubicacion in ubicaciones)
                    {
                        if (falta <= 0)
                        {
                            break;
                        }

                        string clave = ClaveReserva(preparacion.CodigoCliente, renglon.Sku, ubicacion.Posicion);
                        int libre = ubicacion.Cantidad - reservadoConEsta.GetValueOrDefault(clave);
                        if (libre <= 0 || (paletCerrado && libre < ubicacion.Cantidad))
                        {
                            continue;
                        }

                        int tomar = paletCerrado ? ubicacion.Cantidad : Math.Min(libre, falta);
                        renglonesDeEsta.Add(new Almacenes.RenglonOrdenSeleccion
                        {
                            CodigoCliente = preparacion.CodigoCliente,
                            Sku = renglon.Sku,
                            Posicion = new Almacenes.Posicion
                            {
                                Pasillo = ubicacion.Posicion.Pasillo,
                                Fila = ubicacion.Posicion.Fila,
                                Nivel = ubicacion.Posicion.Nivel
                            },
                            Cantidad = tomar,
                            Descripcion = renglon.Producto
                        });
                        reservadoConEsta[clave] = reservadoConEsta.GetValueOrDefault(clave) + tomar;
                        falta -= tomar;
                    }

                    // A palet cerrado los pallets tienen que sumar justo las unidades que comprometió la orden.
                    if (falta != 0)
                    {
                        alcanza = false;
                        break;
                    }
                }

                if (!alcanza)
                {
                    continue;
                }

                // Mismo cliente, SKU y posición en varias órdenes de preparación: un solo renglón con la suma.
                foreach (var r in renglonesDeEsta)
                {
                    var existente = nueva.Renglones.FirstOrDefault(n => n.CodigoCliente == r.CodigoCliente && n.Sku == r.Sku
                        && ClaveReserva(n.CodigoCliente, n.Sku, n.Posicion) == ClaveReserva(r.CodigoCliente, r.Sku, r.Posicion));
                    if (existente == null)
                    {
                        nueva.Renglones.Add(r);
                    }
                    else
                    {
                        existente.Cantidad += r.Cantidad;
                    }
                }

                reservado = reservadoConEsta;
                nueva.OrdenesPreparacion.Add(preparacion.Numero);
                preparacion.Estado = Almacenes.EstadoOrdenPreparacionEnum.EnPreparacion;
            }

            if (nueva.OrdenesPreparacion.Count == 0)
            {
                continue;
            }

            nueva.Numero = ordenesSeleccion.Count > 0 ? ordenesSeleccion.Max(s => s.Numero) + 1 : 1;
            ordenesSeleccion.Add(nueva);
            generadas.Add(nueva);
        }

        if (generadas.Count > 0)
        {
            Almacenes.OrdenesSeleccionAlmacen.Guardar();
            Almacenes.OrdenesPreparacionAlmacen.Guardar();
        }

        return generadas;
    }

    // Un pallet puede salir a palet cerrado si tiene un solo SKU: si su código está en una sola ubicación.
    private static bool EsPalletDeUnSoloSku(string codigoPallet)
    {
        return Almacenes.MercaderiasAlmacen.Mercaderias
            .SelectMany(m => m.Ubicaciones)
            .Count(u => u.CodigoPallet == codigoPallet) == 1;
    }

    private static string ClaveReserva(string cliente, string sku, Almacenes.Posicion posicion)
    {
        return $"{cliente}|{sku}|{posicion.Pasillo}-{posicion.Fila}-{posicion.Nivel}";
    }

    #endregion

    // Paso 2: toma la orden del depósito del usuario en estado Pendiente con el número más bajo.
    // Devuelve el mensaje a mostrar, o null si encontró una orden.
    public string? BuscarOrdenPendiente()
    {
        // Antes del paso 2: si ya pasó un horario de corte, el sistema genera las órdenes de selección.
        EjecutarCorteSiCorresponde();

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

    // Paso 4: renglones de la orden ordenados por posición (pasillo, fila y nivel) de menor a mayor.
    // Ya quedan ordenados al cargar la orden.
    public List<RenglonOrdenSeleccion> RenglonesOrdenados =>
        OrdenActual?.Renglones.ToList() ?? new List<RenglonOrdenSeleccion>();

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

        // Validación de negocio: cada posición tiene que tener stock suficiente para su renglón.
        // Si en alguna no alcanza, no se guarda nada y la orden sigue Pendiente.
        var ordenAValidar = Almacenes.OrdenesSeleccionAlmacen.OrdenesSeleccion.First(o => o.Numero == OrdenActual.Numero);
        foreach (var renglonAValidar in ordenAValidar.Renglones)
        {
            var ubicacionAValidar = Almacenes.MercaderiasAlmacen.Mercaderias
                .Where(m => m.CodigoCliente == renglonAValidar.CodigoCliente && m.Sku == renglonAValidar.Sku
                            && m.CodigoDeposito == ordenAValidar.CodigoDeposito)
                .SelectMany(m => m.Ubicaciones)
                .FirstOrDefault(u => u.Posicion.Pasillo == renglonAValidar.Posicion.Pasillo
                                     && u.Posicion.Fila == renglonAValidar.Posicion.Fila
                                     && u.Posicion.Nivel == renglonAValidar.Posicion.Nivel);
            int disponible = ubicacionAValidar?.Cantidad ?? 0;
            if (disponible < renglonAValidar.Cantidad)
            {
                return $"No se puede cumplir la orden de selección N° {OrdenActual.Numero}: en la posición " +
                       $"{renglonAValidar.Posicion.Pasillo}-{renglonAValidar.Posicion.Fila}-{renglonAValidar.Posicion.Nivel} " +
                       $"hay {disponible} unidades de {renglonAValidar.Sku} y la orden pide {renglonAValidar.Cantidad}. " +
                       "Avise al Jefe de Almacén. La orden sigue Pendiente";
            }
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

            string? codigoPallet = ubicacion.CodigoPallet;
            ubicacion.Cantidad -= renglonEntidad.Cantidad;
            if (ubicacion.Cantidad <= 0)
            {
                mercaderia.Ubicaciones.Remove(ubicacion);
            }

            // Si a un pallet se le sacan unidades y algo queda en la posición (de este SKU o de otro SKU
            // del mismo pallet), queda desconsolidado y ya no puede salir a palet cerrado.
            if (codigoPallet != null)
            {
                foreach (var restoDelPallet in Almacenes.MercaderiasAlmacen.Mercaderias
                             .SelectMany(m => m.Ubicaciones)
                             .Where(u => u.CodigoPallet == codigoPallet))
                {
                    restoDelPallet.CodigoPallet = null;
                }
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

        // Paso 12
        return $"Orden de selección N° {OrdenActual.Numero} cumplida";
    }
}

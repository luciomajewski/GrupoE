#nullable enable
namespace GrupoE.CumplirOrdenSeleccion;

internal class CumplirOrdenSeleccionModelo
{
    public string UsuarioActual { get; }
    public string DepositoActual { get; }
    public List<OrdenSeleccion> OrdenesSeleccion { get; }
    public List<ItemInventario> Inventario { get; }
    public OrdenSeleccion? OrdenActual { get; private set; }

    public DateTime Fecha => DateTime.Today;

    public CumplirOrdenSeleccionModelo()
    {
        // Usuario identificado en el sistema: operario de Almacenamiento del depósito DEP01.
        UsuarioActual = "OPA01";
        DepositoActual = "DEP01";

        OrdenesSeleccion = new List<OrdenSeleccion>
        {
            // Ya cumplida: no se muestra.
            new OrdenSeleccion
            {
                Numero = 1000, Deposito = "DEP01", Estado = EstadoOrdenSeleccion.Cumplida,
                FechaHoraCumplida = new DateTime(2026, 10, 21, 9, 30, 0), UsuarioCumplida = "OPA01",
                Renglones = new List<RenglonOrdenSeleccion>
                {
                    new RenglonOrdenSeleccion { Cliente = "CLI001", Posicion = "5-1-1", Sku = "AUR-200", Descripcion = "Auriculares inalámbricos BT-200", Cantidad = 2 }
                }
            },
            // Pendiente de DEP01. Está antes que la 1001, pero se muestra después porque tiene número más alto.
            new OrdenSeleccion
            {
                Numero = 1002, Deposito = "DEP01", Estado = EstadoOrdenSeleccion.Pendiente,
                Renglones = new List<RenglonOrdenSeleccion>
                {
                    new RenglonOrdenSeleccion { Cliente = "CLI003", Posicion = "7-15-2", Sku = "CAB-001", Descripcion = "Cable HDMI 2 m", Cantidad = 8 },
                    new RenglonOrdenSeleccion { Cliente = "CLI003", Posicion = "7-2-1", Sku = "TEC-300", Descripcion = "Teclado inalámbrico K300", Cantidad = 3 }
                }
            },
            // Pendiente de DEP01 con el número más bajo: es la primera que se muestra.
            // Las posiciones están desordenadas a propósito para probar el orden del paso 4.
            new OrdenSeleccion
            {
                Numero = 1001, Deposito = "DEP01", Estado = EstadoOrdenSeleccion.Pendiente,
                Renglones = new List<RenglonOrdenSeleccion>
                {
                    new RenglonOrdenSeleccion { Cliente = "CLI001", Posicion = "12-43-2", Sku = "AUR-200", Descripcion = "Auriculares inalámbricos BT-200", Cantidad = 4 },
                    new RenglonOrdenSeleccion { Cliente = "CLI002", Posicion = "3-10-1", Sku = "CAR-020", Descripcion = "Cargador USB-C 20W", Cantidad = 10 },
                    new RenglonOrdenSeleccion { Cliente = "CLI001", Posicion = "12-5-3", Sku = "FUN-015", Descripcion = "Funda de silicona para celular", Cantidad = 6 },
                    new RenglonOrdenSeleccion { Cliente = "CLI002", Posicion = "3-10-4", Sku = "MOU-100", Descripcion = "Mouse óptico M100", Cantidad = 2 }
                }
            },
            // Pendiente pero de otro depósito: no se muestra.
            new OrdenSeleccion
            {
                Numero = 1003, Deposito = "DEP02", Estado = EstadoOrdenSeleccion.Pendiente,
                Renglones = new List<RenglonOrdenSeleccion>
                {
                    new RenglonOrdenSeleccion { Cliente = "CLI001", Posicion = "1-1-1", Sku = "AUR-200", Descripcion = "Auriculares inalámbricos BT-200", Cantidad = 1 }
                }
            }
        };

        Inventario = new List<ItemInventario>
        {
            new ItemInventario { Cliente = "CLI001", Sku = "AUR-200", Posicion = "12-43-2", Cantidad = 20 },
            new ItemInventario { Cliente = "CLI002", Sku = "CAR-020", Posicion = "3-10-1", Cantidad = 50 },
            new ItemInventario { Cliente = "CLI001", Sku = "FUN-015", Posicion = "12-5-3", Cantidad = 30 },
            new ItemInventario { Cliente = "CLI002", Sku = "MOU-100", Posicion = "3-10-4", Cantidad = 12 },
            new ItemInventario { Cliente = "CLI003", Sku = "TEC-300", Posicion = "7-2-1", Cantidad = 9 },
            new ItemInventario { Cliente = "CLI003", Sku = "CAB-001", Posicion = "7-15-2", Cantidad = 40 }
        };
    }

    // Paso 2: toma la orden del depósito del usuario en estado Pendiente con el número más bajo.
    // Devuelve el mensaje a mostrar, o null si encontró una orden.
    public string? BuscarOrdenPendiente()
    {
        OrdenActual = OrdenesSeleccion
            .Where(o => o.Deposito == DepositoActual && o.Estado == EstadoOrdenSeleccion.Pendiente)
            .OrderBy(o => o.Numero)
            .FirstOrDefault();

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

        // Paso 11
        return $"Orden de selección N° {OrdenActual.Numero} cumplida";
    }

    // Convierte "12-43-2" en (12, 43, 2) para ordenar por número y no como texto.
    private (int Sector, int Posicion, int Fila) ClaveDePosicion(string posicion)
    {
        var partes = posicion.Split('-');
        return (int.Parse(partes[0]), int.Parse(partes[1]), int.Parse(partes[2]));
    }
}

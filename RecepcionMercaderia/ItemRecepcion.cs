#nullable enable
namespace GrupoE.RecepcionMercaderia;

internal class ItemRecepcion
{
    public Producto Producto { get; }

    // true: las cantidades están expresadas en pallets cerrados; false: en ítems sueltos.
    public bool EsPallet { get; }
    public int CantidadContada { get; }
    public int CantidadRemito { get; }

    // null si no se contaron unidades.
    public Posicion? Posicion { get; }

    public ItemRecepcion(Producto producto, bool esPallet, int cantidadContada, int cantidadRemito, Posicion? posicion)
    {
        Producto = producto;
        EsPallet = esPallet;
        CantidadContada = cantidadContada;
        CantidadRemito = cantidadRemito;
        Posicion = posicion;
    }

    public string Codigo => EsPallet ? Producto.CodigoPallet! : Producto.Sku;

    public string Descripcion => EsPallet
        ? $"{Producto.Descripcion} (pallet x {Producto.UnidadesPorPallet} u.)"
        : Producto.Descripcion;

    // Negativa: faltante respecto del remito. Positiva: excedente.
    public int Diferencia => CantidadContada - CantidadRemito;
}

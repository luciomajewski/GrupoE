#nullable enable
namespace GrupoE.RecepcionMercaderia;

internal class ItemRecepcion
{
    public Producto Producto { get; }

    // ID del pallet armado por el cliente al que pertenece la línea; null si llegó suelta
    // (Pampazon arma el pallet en la descarga, al ubicarla en Ingreso a Stock).
    public string? CodigoPallet { get; }
    public int CantidadContada { get; }
    public int CantidadRemito { get; }

    // null si no se contaron unidades.
    public Posicion? Posicion { get; }

    public ItemRecepcion(Producto producto, string? codigoPallet, int cantidadContada, int cantidadRemito, Posicion? posicion)
    {
        Producto = producto;
        CodigoPallet = codigoPallet;
        CantidadContada = cantidadContada;
        CantidadRemito = cantidadRemito;
        Posicion = posicion;
    }

    public string Codigo => Producto.Sku;

    public string Descripcion => Producto.Descripcion;

    // Lo que muestra la columna Pallet de la grilla.
    public string Pallet => CodigoPallet ?? "Carga suelta";

    // Negativa: faltante respecto del remito. Positiva: excedente.
    public int Diferencia => CantidadContada - CantidadRemito;
}

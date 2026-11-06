#nullable enable
namespace GrupoE.RecepcionMercaderia;

internal class Producto
{
    public string Sku { get; }
    public string Descripcion { get; }

    // Código con el que el cliente envía el producto en pallet cerrado (null si no lo envía en pallet).
    public string? CodigoPallet { get; }
    public int UnidadesPorPallet { get; }

    public Producto(string sku, string descripcion, string? codigoPallet, int unidadesPorPallet)
    {
        Sku = sku;
        Descripcion = descripcion;
        CodigoPallet = codigoPallet;
        UnidadesPorPallet = unidadesPorPallet;
    }
}

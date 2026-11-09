namespace GrupoE.Almacenes;

internal class ProductoEntidad
{
    public string CodigoCliente { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int? StockMinimoContratado { get; set; }
}

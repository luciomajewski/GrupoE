namespace GrupoE.RegistrarOrdenDePreparacion;

internal class Producto
{
    public required string Sku { get; set; }
    public required string Nombre { get; set; }
    public int? StockMinimoContratado { get; set; }
    public int UnidadesPorPalet { get; set; }
}

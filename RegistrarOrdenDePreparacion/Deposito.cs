namespace GrupoE.RegistrarOrdenDePreparacion;

internal class Deposito
{
    public required string Nombre { get; set; }
    public required List<StockProducto> Stock { get; set; }
}

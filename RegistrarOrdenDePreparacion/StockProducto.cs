namespace GrupoE.RegistrarOrdenDePreparacion;

internal class StockProducto
{
    public required Producto Producto { get; set; }
    public int CantidadPalets { get; set; }
    // Unidades de palets ya abiertos (no cuentan como palets completos).
    public int UnidadesSueltas { get; set; }

    public int CantidadUnidades => CantidadPalets * Producto.UnidadesPorPalet + UnidadesSueltas;
}

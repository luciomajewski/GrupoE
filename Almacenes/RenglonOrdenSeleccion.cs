namespace GrupoE.Almacenes;

internal class RenglonOrdenSeleccion
{
    public string CodigoCliente { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public Posicion Posicion { get; set; } = new();
    public int Cantidad { get; set; }
    public string Descripcion { get; set; } = string.Empty; // Derivado
}

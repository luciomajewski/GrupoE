namespace GrupoE.Almacenes;

internal class DepositoEntidad
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public List<Posicion> Posiciones { get; set; } = new();
}

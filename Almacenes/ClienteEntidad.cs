namespace GrupoE.Almacenes;

internal class ClienteEntidad
{
    public string Codigo { get; set; } = string.Empty;
    public string RazonSocial { get; set; } = string.Empty;
    public string CodigoDeposito { get; set; } = string.Empty;
    public bool ContratoVigente { get; set; }
    public List<string> RemitosRegistrados { get; set; } = new(); // Derivado
}

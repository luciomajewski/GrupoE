namespace GrupoE.Almacenes;

internal class RemitoEntregaEntidad
{
    public int Numero { get; set; }
    public DateTime FechaHora { get; set; }
    public List<int> OrdenesEntrega { get; set; } = new();
    public string CodigoUsuario { get; set; } = string.Empty;
    public EstadoRemitoEnum Estado { get; set; }
    public DateTime? FechaHoraCumplido { get; set; }
    public string? CodigoUsuarioCumplido { get; set; }
    public string CodigoDeposito { get; set; } = string.Empty; // Derivado
    public string NombreTransportista { get; set; } = string.Empty; // Derivado
    public int DniTransportista { get; set; } // Derivado
}

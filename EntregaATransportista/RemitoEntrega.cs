namespace GrupoE.EntregaATransportista;

internal class RemitoEntrega
{
    public int Numero { get; set; }
    public DateTime FechaHora { get; set; }
    public required string CodigoDeposito { get; set; }
    public required string TransportistaNombre { get; set; }
    public int TransportistaDni { get; set; }
    public required string CodigoUsuario { get; set; }
    public EstadoRemito Estado { get; set; }
    // Registro del cumplido, al confirmar el despacho.
    public DateTime? FechaHoraCumplido { get; set; }
    public string? CodigoUsuarioCumplido { get; set; }
}

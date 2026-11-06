namespace GrupoE.EntregaATransportista;

internal class OrdenEntrega
{
    public int NumeroOrdenPreparacion { get; set; }
    public required string CodigoDeposito { get; set; }
    public int CantidadPaquetes { get; set; }
    public EstadoOrdenEntrega Estado { get; set; }
    public DateTime? FechaHoraDespacho { get; set; }
}

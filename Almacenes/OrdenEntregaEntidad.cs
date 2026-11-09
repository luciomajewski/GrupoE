namespace GrupoE.Almacenes;

internal class OrdenEntregaEntidad
{
    public int Numero { get; set; }
    public int NumeroOrdenPreparacion { get; set; }
    public DateTime FechaHora { get; set; }
    public string Usuario { get; set; } = string.Empty;
    public int CantidadPaquetes { get; set; }
    public EstadoOrdenEntregaEnum Estado { get; set; }
    public DateTime? FechaHoraDespacho { get; set; }
    public string NombreTransportista { get; set; } = string.Empty;
    public int DniTransportista { get; set; }
}

namespace GrupoE.CumplirOrdenSeleccion;

internal class OrdenSeleccion
{
    public int Numero { get; set; }
    public required string Deposito { get; set; }
    public EstadoOrdenSeleccion Estado { get; set; }
    public DateTime? FechaHoraCumplida { get; set; }
    public string? UsuarioCumplida { get; set; }
    public required List<RenglonOrdenSeleccion> Renglones { get; set; }
}

namespace GrupoE.Almacenes;

internal class OrdenSeleccionEntidad
{
    public int Numero { get; set; }
    public List<int> OrdenesPreparacion { get; set; } = new();
    public List<RenglonOrdenSeleccion> Renglones { get; set; } = new();
    public EstadoOrdenSeleccionEnum Estado { get; set; }
    public DateTime? FechaHoraCumplida { get; set; }
    public string? UsuarioCumplida { get; set; }
    public string CodigoDeposito { get; set; } = string.Empty;
}

namespace GrupoE.Almacenes;

internal class ItemRecepcion
{
    public string Sku { get; set; } = string.Empty;
    public int CantidadContada { get; set; }
    public int CantidadRemito { get; set; }
    public List<AsignacionUbicacion> Asignaciones { get; set; } = new();
    public int CantidadAUbicar { get; set; } // Derivado
    public string Descripcion { get; set; } = string.Empty; // Derivado
    public int Diferencia { get; set; } // Derivado
}

namespace GrupoE.Almacenes;

internal class OrdenRecepcionEntidad
{
    public int Numero { get; set; }
    public string CodigoCliente { get; set; } = string.Empty;
    public string NumeroRemito { get; set; } = string.Empty;
    public DateTime FechaRemito { get; set; }
    public DateTime FechaHoraRecepcion { get; set; }
    public string Transportista { get; set; } = string.Empty;
    public string Chofer { get; set; } = string.Empty;
    public string Patente { get; set; } = string.Empty;
    public string Observaciones { get; set; } = string.Empty;
    public List<ItemRecepcion> Items { get; set; } = new();
    public EstadoOrdenRecepcionEnum Estado { get; set; }
    public string Usuario { get; set; } = string.Empty;
}

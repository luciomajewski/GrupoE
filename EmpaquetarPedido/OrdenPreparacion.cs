namespace GrupoE.EmpaquetarPedido;

internal class OrdenPreparacion
{
    public int Numero { get; set; }
    public int Deposito { get; set; }
    public TipoPedido TipoPedido { get; set; }
    public string Transportista { get; set; } = "";
    public bool OrdenSeleccionCumplida { get; set; }
    public EstadoOrdenPreparacion Estado { get; set; }
    public string? TomadaPor { get; set; }
    public DateTime? FechaPreparada { get; set; }
    public string? PreparadaPor { get; set; }
    public List<RenglonOrdenPreparacion> Renglones { get; set; } = new();
}

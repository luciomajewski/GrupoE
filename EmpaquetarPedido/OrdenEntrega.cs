namespace GrupoE.EmpaquetarPedido;

internal class OrdenEntrega
{
    public int Numero { get; set; }
    public DateTime FechaHora { get; set; }
    public int Deposito { get; set; }
    public int NumeroOrdenPreparacion { get; set; }
    public string Transportista { get; set; } = "";
    public string Usuario { get; set; } = "";
    public EstadoOrdenEntrega Estado { get; set; }
}

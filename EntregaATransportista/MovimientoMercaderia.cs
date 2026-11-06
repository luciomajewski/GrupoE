namespace GrupoE.EntregaATransportista;

internal class MovimientoMercaderia
{
    public required string CodigoCliente { get; set; }
    public int NumeroOrdenPreparacion { get; set; }
    public int NumeroRemito { get; set; }
    public DateTime Fecha { get; set; }
}

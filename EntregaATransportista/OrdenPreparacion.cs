namespace GrupoE.EntregaATransportista;

internal class OrdenPreparacion
{
    public int Numero { get; set; }
    public required string CodigoCliente { get; set; }
    public required string CodigoDeposito { get; set; }
    // Transportista autorizado por el cliente para retirar la orden.
    public required string TransportistaNombre { get; set; }
    public int TransportistaDni { get; set; }
    public EstadoOrdenPreparacion Estado { get; set; }
    public int? NumeroRemito { get; set; }
}

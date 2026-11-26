namespace GrupoE.Almacenes;

internal class ServicioAFacturarEntidad
{
    public int Numero { get; set; }
    public int NumeroOrdenPreparacion { get; set; }
    public string CodigoCliente { get; set; } = string.Empty;
    public string CodigoDeposito { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public ModalidadEmpaquetadoEnum Modalidad { get; set; }
    public List<RenglonServicioAFacturar> Detalle { get; set; } = new();
    public EstadoServicioAFacturarEnum Estado { get; set; }
    public DateTime? FechaHoraFacturado { get; set; }
    public string Usuario { get; set; } = string.Empty;
    public int CantidadUnidades { get; set; } // Derivado
}

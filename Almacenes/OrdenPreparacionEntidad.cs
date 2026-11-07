namespace GrupoE.Almacenes;

internal class OrdenPreparacionEntidad
{
    public int Numero { get; set; }
    public string CodigoCliente { get; set; } = string.Empty;
    public string CodigoDeposito { get; set; } = string.Empty;
    public ModalidadEmpaquetadoEnum Modalidad { get; set; }
    public DateTime FechaRetiro { get; set; }
    public string NombreDestinatario { get; set; } = string.Empty;
    public string DireccionDestino { get; set; } = string.Empty;
    public string NombreTransportista { get; set; } = string.Empty;
    public int DniTransportista { get; set; }
    public string EmpresaTransporte { get; set; } = string.Empty;
    public EstadoOrdenPreparacionEnum Estado { get; set; }
    public DateTime? FechaPreparada { get; set; }
    public string? PreparadaPor { get; set; }
    public List<RenglonOrdenPreparacion> Renglones { get; set; } = new();
    public bool OrdenSeleccionCumplida { get; set; } // Derivado
    public bool RequiereDatosEtiquetado { get; set; } // Derivado
}

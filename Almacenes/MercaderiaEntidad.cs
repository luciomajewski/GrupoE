namespace GrupoE.Almacenes;

internal class MercaderiaEntidad
{
    public string CodigoCliente { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public string CodigoDeposito { get; set; } = string.Empty;
    public List<UbicacionMercaderia> Ubicaciones { get; set; } = new();
    public int CantidadComprometida { get; set; } // Derivado
    public int CantidadFisica { get; set; } // Derivado
    public int CantidadDisponible { get; set; } // Derivado
}

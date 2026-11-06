#nullable enable
namespace GrupoE.RecepcionMercaderia;

internal class OrdenRecepcion
{
    public int Numero { get; }
    public Cliente Cliente { get; }
    public string NumeroRemito { get; }
    public DateTime FechaRemito { get; }
    public DateTime FechaHoraRecepcion { get; }
    public string Transportista { get; }
    public string Chofer { get; }
    public string Patente { get; }
    public string Observaciones { get; }
    public List<ItemRecepcion> Items { get; }
    public string Estado { get; }

    public OrdenRecepcion(int numero, Cliente cliente, string numeroRemito, DateTime fechaRemito, string transportista,
                          string chofer, string patente, string observaciones, List<ItemRecepcion> items, string estado)
    {
        Numero = numero;
        Cliente = cliente;
        NumeroRemito = numeroRemito;
        FechaRemito = fechaRemito;
        FechaHoraRecepcion = DateTime.Now;
        Transportista = transportista;
        Chofer = chofer;
        Patente = patente;
        Observaciones = observaciones;
        Items = items;
        Estado = estado;
    }
}

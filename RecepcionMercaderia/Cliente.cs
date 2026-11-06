#nullable enable
namespace GrupoE.RecepcionMercaderia;

internal class Cliente
{
    public string RazonSocial { get; }
    public bool ContratoVigente { get; }
    public List<Producto> Productos { get; }
    public List<string> RemitosRegistrados { get; }

    public Cliente(string razonSocial, bool contratoVigente, List<Producto> productos, List<string> remitosRegistrados)
    {
        RazonSocial = razonSocial;
        ContratoVigente = contratoVigente;
        Productos = productos;
        RemitosRegistrados = remitosRegistrados;
    }
}

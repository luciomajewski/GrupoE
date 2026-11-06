#nullable enable
namespace GrupoE.RecepcionMercaderia;

internal class Posicion
{
    public string Codigo { get; }
    public bool Ocupada { get; set; }

    public Posicion(string codigo, bool ocupada)
    {
        Codigo = codigo;
        Ocupada = ocupada;
    }
}

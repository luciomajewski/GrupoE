using System.Text.Json;

namespace GrupoE.Almacenes;

static class MercaderiasAlmacen
{
    public static List<MercaderiaEntidad> Mercaderias { get; private set; }

    static MercaderiasAlmacen()
    {
        if (File.Exists(@"Datos\Mercaderias.json"))
        {
            string contenido = File.ReadAllText(@"Datos\Mercaderias.json");
            Mercaderias = JsonSerializer.Deserialize<List<MercaderiaEntidad>>(contenido) ?? new List<MercaderiaEntidad>();
        }
        else //si el archivo no existe...
        {
            throw new ApplicationException($"No encuentro el archivo Mercaderias.json");
        }
    }

    public static void Guardar()
    {
        string contenido = JsonSerializer.Serialize(Mercaderias, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(@"Datos\Mercaderias.json", contenido);
    }
}

using System.Text.Json;

namespace GrupoE.Almacenes;

static class OrdenesPreparacionAlmacen
{
    public static List<OrdenPreparacionEntidad> OrdenesPreparacion { get; private set; }

    static OrdenesPreparacionAlmacen()
    {
        if (File.Exists(@"Datos\OrdenesPreparacion.json"))
        {
            string contenido = File.ReadAllText(@"Datos\OrdenesPreparacion.json");
            OrdenesPreparacion = JsonSerializer.Deserialize<List<OrdenPreparacionEntidad>>(contenido) ?? new List<OrdenPreparacionEntidad>();
        }
        else //si el archivo no existe...
        {
            throw new ApplicationException($"No encuentro el archivo OrdenesPreparacion.json");
        }
    }

    public static void Guardar()
    {
        string contenido = JsonSerializer.Serialize(OrdenesPreparacion, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(@"Datos\OrdenesPreparacion.json", contenido);
    }
}

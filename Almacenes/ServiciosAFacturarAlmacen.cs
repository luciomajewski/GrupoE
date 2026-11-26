using System.Text.Json;

namespace GrupoE.Almacenes;

static class ServiciosAFacturarAlmacen
{
    public static List<ServicioAFacturarEntidad> ServiciosAFacturar { get; private set; }

    static ServiciosAFacturarAlmacen()
    {
        if (File.Exists(@"Datos\ServiciosAFacturar.json"))
        {
            string contenido = File.ReadAllText(@"Datos\ServiciosAFacturar.json");
            ServiciosAFacturar = JsonSerializer.Deserialize<List<ServicioAFacturarEntidad>>(contenido) ?? new List<ServicioAFacturarEntidad>();
        }
        else //si el archivo no existe...
        {
            throw new ApplicationException($"No encuentro el archivo ServiciosAFacturar.json");
        }
    }

    public static void Guardar()
    {
        string contenido = JsonSerializer.Serialize(ServiciosAFacturar, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(@"Datos\ServiciosAFacturar.json", contenido);
    }
}

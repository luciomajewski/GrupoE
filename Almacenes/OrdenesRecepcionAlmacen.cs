using System.Text.Json;

namespace GrupoE.Almacenes;

static class OrdenesRecepcionAlmacen
{
    public static List<OrdenRecepcionEntidad> OrdenesRecepcion { get; private set; }

    static OrdenesRecepcionAlmacen()
    {
        if (File.Exists(@"Datos\OrdenesRecepcion.json"))
        {
            string contenido = File.ReadAllText(@"Datos\OrdenesRecepcion.json");
            OrdenesRecepcion = JsonSerializer.Deserialize<List<OrdenRecepcionEntidad>>(contenido) ?? new List<OrdenRecepcionEntidad>();
        }
        else //si el archivo no existe...
        {
            throw new ApplicationException($"No encuentro el archivo OrdenesRecepcion.json");
        }
    }

    public static void Guardar()
    {
        string contenido = JsonSerializer.Serialize(OrdenesRecepcion, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(@"Datos\OrdenesRecepcion.json", contenido);
    }
}

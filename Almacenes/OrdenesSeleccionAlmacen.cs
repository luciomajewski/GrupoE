using System.Text.Json;

namespace GrupoE.Almacenes;

static class OrdenesSeleccionAlmacen
{
    public static List<OrdenSeleccionEntidad> OrdenesSeleccion { get; private set; }

    static OrdenesSeleccionAlmacen()
    {
        if (File.Exists(@"Datos\OrdenesSeleccion.json"))
        {
            string contenido = File.ReadAllText(@"Datos\OrdenesSeleccion.json");
            OrdenesSeleccion = JsonSerializer.Deserialize<List<OrdenSeleccionEntidad>>(contenido) ?? new List<OrdenSeleccionEntidad>();
        }
        else //si el archivo no existe...
        {
            throw new ApplicationException($"No encuentro el archivo OrdenesSeleccion.json");
        }
    }

    public static void Guardar()
    {
        string contenido = JsonSerializer.Serialize(OrdenesSeleccion, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(@"Datos\OrdenesSeleccion.json", contenido);
    }
}

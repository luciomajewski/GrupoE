using System.Text.Json;

namespace GrupoE.Almacenes;

static class OrdenesEntregaAlmacen
{
    public static List<OrdenEntregaEntidad> OrdenesEntrega { get; private set; }

    static OrdenesEntregaAlmacen()
    {
        if (File.Exists(@"Datos\OrdenesEntrega.json"))
        {
            string contenido = File.ReadAllText(@"Datos\OrdenesEntrega.json");
            OrdenesEntrega = JsonSerializer.Deserialize<List<OrdenEntregaEntidad>>(contenido) ?? new List<OrdenEntregaEntidad>();
        }
        else //si el archivo no existe...
        {
            throw new ApplicationException($"No encuentro el archivo OrdenesEntrega.json");
        }
    }

    public static void Guardar()
    {
        string contenido = JsonSerializer.Serialize(OrdenesEntrega, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(@"Datos\OrdenesEntrega.json", contenido);
    }
}

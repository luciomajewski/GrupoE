using System.Text.Json;

namespace GrupoE.Almacenes;

static class RemitosEntregaAlmacen
{
    public static List<RemitoEntregaEntidad> RemitosEntrega { get; private set; }

    static RemitosEntregaAlmacen()
    {
        if (File.Exists(@"Datos\RemitosEntrega.json"))
        {
            string contenido = File.ReadAllText(@"Datos\RemitosEntrega.json");
            RemitosEntrega = JsonSerializer.Deserialize<List<RemitoEntregaEntidad>>(contenido) ?? new List<RemitoEntregaEntidad>();
        }
        else //si el archivo no existe...
        {
            throw new ApplicationException($"No encuentro el archivo RemitosEntrega.json");
        }
    }

    public static void Guardar()
    {
        string contenido = JsonSerializer.Serialize(RemitosEntrega, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(@"Datos\RemitosEntrega.json", contenido);
    }
}

using System.Text.Json;

namespace GrupoE.Almacenes;

static class ClientesAlmacen
{
    public static List<ClienteEntidad> Clientes { get; private set; }

    static ClientesAlmacen()
    {
        if (File.Exists(@"Datos\Clientes.json"))
        {
            string contenido = File.ReadAllText(@"Datos\Clientes.json");
            Clientes = JsonSerializer.Deserialize<List<ClienteEntidad>>(contenido) ?? new List<ClienteEntidad>();
        }
        else //si el archivo no existe...
        {
            throw new ApplicationException($"No encuentro el archivo Clientes.json");
        }
    }

    public static void Guardar()
    {
        string contenido = JsonSerializer.Serialize(Clientes, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(@"Datos\Clientes.json", contenido);
    }
}

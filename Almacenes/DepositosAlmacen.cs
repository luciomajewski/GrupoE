using System.Text.Json;

namespace GrupoE.Almacenes;

static class DepositosAlmacen
{
    public static List<DepositoEntidad> Depositos { get; private set; }

    static DepositosAlmacen()
    {
        if (File.Exists(@"Datos\Depositos.json"))
        {
            string contenido = File.ReadAllText(@"Datos\Depositos.json");
            Depositos = JsonSerializer.Deserialize<List<DepositoEntidad>>(contenido) ?? new List<DepositoEntidad>();
        }
        else //si el archivo no existe...
        {
            throw new ApplicationException($"No encuentro el archivo Depositos.json");
        }
    }

    public static void Guardar()
    {
        string contenido = JsonSerializer.Serialize(Depositos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(@"Datos\Depositos.json", contenido);
    }
}

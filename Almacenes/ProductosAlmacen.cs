using System.Text.Json;

namespace GrupoE.Almacenes;

static class ProductosAlmacen
{
    public static List<ProductoEntidad> Productos { get; private set; }

    static ProductosAlmacen()
    {
        if (File.Exists(@"Datos\Productos.json"))
        {
            string contenido = File.ReadAllText(@"Datos\Productos.json");
            Productos = JsonSerializer.Deserialize<List<ProductoEntidad>>(contenido) ?? new List<ProductoEntidad>();
        }
        else //si el archivo no existe...
        {
            throw new ApplicationException($"No encuentro el archivo Productos.json");
        }
    }

    public static void Guardar()
    {
        string contenido = JsonSerializer.Serialize(Productos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(@"Datos\Productos.json", contenido);
    }
}

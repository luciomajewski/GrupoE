namespace GrupoE.IngresoStock
{
    public class ItemOrdenRecepcion
    {
        public string Sku { get; }
        public string Descripcion { get; }
        public int CantidadRecepcionada { get; }

        // Saldo pendiente de ubicar (columna "Cantidad a ubicar" de la grilla)
        public int CantidadAUbicar { get; set; }

        public ItemOrdenRecepcion(string sku, string descripcion, int cantidadRecepcionada)
        {
            Sku = sku;
            Descripcion = descripcion;
            CantidadRecepcionada = cantidadRecepcionada;
            CantidadAUbicar = cantidadRecepcionada;
        }

        // Texto que se ve en el desplegable de SKU
        public override string ToString()
        {
            return $"{Sku} - {Descripcion}";
        }
    }
}

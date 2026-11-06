namespace GrupoE.IngresoStock
{
    // Existencia "Disponible en Guarda" de un SKU en una posición del depósito
    public class Mercaderia
    {
        public string Sku { get; }
        public string Pasillo { get; }
        public string Fila { get; }
        public string Nivel { get; }
        public int Cantidad { get; set; }

        public Mercaderia(string sku, string pasillo, string fila, string nivel, int cantidad)
        {
            Sku = sku;
            Pasillo = pasillo;
            Fila = fila;
            Nivel = nivel;
            Cantidad = cantidad;
        }
    }
}

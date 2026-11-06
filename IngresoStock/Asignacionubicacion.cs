namespace GrupoE.IngresoStock
{
    // Ubicación temporal agregada con "Agregar Ubicación", antes de confirmar el ingreso
    public class AsignacionUbicacion
    {
        public ItemOrdenRecepcion Item { get; }
        public int Cantidad { get; set; }
        public string Pasillo { get; }
        public string Fila { get; }
        public string Nivel { get; }

        public string Posicion
        {
            get { return $"{Pasillo} - {Fila} - {Nivel}"; }
        }

        public AsignacionUbicacion(ItemOrdenRecepcion item, int cantidad, string pasillo, string fila, string nivel)
        {
            Item = item;
            Cantidad = cantidad;
            Pasillo = pasillo;
            Fila = fila;
            Nivel = nivel;
        }
    }
}

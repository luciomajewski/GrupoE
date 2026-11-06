using System.Collections.Generic;

namespace GrupoE.IngresoStock
{
    public class OrdenRecepcion
    {
        public int Numero { get; }
        public string ClienteOrigen { get; }
        public EstadoOrdenRecepcion Estado { get; set; }
        public List<ItemOrdenRecepcion> Items { get; }

        public OrdenRecepcion(int numero, string clienteOrigen, EstadoOrdenRecepcion estado, List<ItemOrdenRecepcion> items)
        {
            Numero = numero;
            ClienteOrigen = clienteOrigen;
            Estado = estado;
            Items = items;
        }
    }
}

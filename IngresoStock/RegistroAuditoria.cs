using System;

namespace GrupoE.IngresoStock
{
    public class RegistroAuditoria
    {
        public string Usuario { get; }
        public DateTime FechaHora { get; }
        public int NumeroOrden { get; }

        public RegistroAuditoria(string usuario, DateTime fechaHora, int numeroOrden)
        {
            Usuario = usuario;
            FechaHora = fechaHora;
            NumeroOrden = numeroOrden;
        }
    }
}

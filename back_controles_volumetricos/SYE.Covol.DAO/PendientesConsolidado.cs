using System;

namespace SYE.Covol
{
    [Serializable]
    public class PendientesConsolidado
    {
        public string RFC { get; set; }
        public string NumeroPermisoCRE { get; set; }
        public DateTime FechaCorte { get; set; }
        public DateTime FechaCarga { get; set; }
        public int Archivos { get; set; }
    }
}

using System;
namespace SYE.Covol
{
    [Serializable]
    public class Permicionarios
    {
        public string RFC { get; set; }
        public int Archivos { get; set; }
        public string NumeroPermisoCRE { get; set; }
        public DateTime FecInicialOper { get; set; }
        public DateTime FecInicial { get; set; }
        public DateTime FecFinal { get; set; }
        public DateTime FecFinalOper { get; set; }
    }
}

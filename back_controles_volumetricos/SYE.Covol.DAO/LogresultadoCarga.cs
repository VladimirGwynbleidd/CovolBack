using System;
using System.Collections.Generic;
using System.Text;
namespace SYE.Covol
{
    [Serializable]
    public class LogresultadoCarga
    {
        public int TotalArchivos { get; set; }
        public DateTime Fecha { get; set; }
        public string Level { get; set; }
        public string RFC { get; set; }
        public string Permisionario { get; set; }
        public string Message { get; set; }
    }
}

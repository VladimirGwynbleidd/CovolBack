using System;

namespace SYE.Covol
{
    [Serializable]
    public class RegistroLog
    {
        public string RFC { get; set; }
        public DateTime Fecha { get; set; }
        public string Level { get; set; }
        public string Mensaje { get; set; }
        public string Archivo { get; set; }
    }
}

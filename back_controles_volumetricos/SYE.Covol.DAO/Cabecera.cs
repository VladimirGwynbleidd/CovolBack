using System;
using System.Collections.Generic;
using System.Text;

namespace SYE.Covol
{
    [Serializable]
    public class Cabecera
    {
        public string Version { get; set; }
        public string RFC { get; set; }
        public string NoCertificado { get; set; }
        public string Certificado { get; set; }
        public string NumeroPermisoCRE { get; set; }
    }
}

using System;

namespace SYE.Covol
{
    [Serializable]
    public class ResultadoCarga
    {
        public int Correctos { get; set; }
        public int Incorrectos { get; set; }
        public int Errores { get; set; }
    }
}

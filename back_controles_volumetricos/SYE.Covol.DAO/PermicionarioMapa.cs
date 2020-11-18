using System;

namespace SYE.Covol
{
    [Serializable]
    public class PermicionarioMapa
    {
        public string Entidad { get; set; }
        public string Zonas { get; set; }
        public string RFC { get; set; }
        public int Permisionarios { get; set; }
    }
}

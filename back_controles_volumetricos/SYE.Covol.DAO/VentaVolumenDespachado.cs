using System;
namespace SYE.Covol
{
    [Serializable]
    public class VentaVolumenDespachado
    {
        public string TipoDeRegistro { get; set; }
        public decimal VolumenTotal { get; set; }
        public decimal ImporteTotal { get; set; }
    }
}

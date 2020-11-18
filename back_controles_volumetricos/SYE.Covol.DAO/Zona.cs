using System;
namespace SYE.Covol
{
    [Serializable]
    public class Zona
    {
        public int c_zonavalor { get; set; }
        public string Identificador { get; set; }
        public decimal m_valorMagna { get; set; }
        public decimal m_valorPremium { get; set; }
        public string Entidades { get; set; }
    }
}

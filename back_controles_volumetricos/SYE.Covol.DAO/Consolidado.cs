using System;
using System.Collections.Generic;
using System.Text;
namespace SYE.Covol
{
    [Serializable]
    public class Consolidado
    {
        public int Index { set; get; }
        public DateTime FechaCorte { set; get; }
        public string NumeroPermisocre { set; get; }
        public string TipoGasolina { set; get; }
        public decimal LitrosdeVenta { set; get; }
        public decimal Total { set; get; }
        public decimal LtsExcedentes { set; get; }
        public decimal LtsEtimad { set; get; }
        public decimal MontoEstimulo { set; get; }
        public decimal ValorEst { set; get; }
        public string ClaveSubProducto { set; get; }
        public string Rfc { set; get; }
    }
}

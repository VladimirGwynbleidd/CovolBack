using System;

namespace SYE.Covol
{
    [Serializable]
    public class VentaDetalleMesDispensario
    {
        public int Mes { get; set; }
        public string ClaveProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public int NumeroDispensario { get; set; }
        public decimal Volumen { get; set; }
        public decimal Total { get; set; }
    }
}

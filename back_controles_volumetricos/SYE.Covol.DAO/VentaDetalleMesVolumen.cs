using System;

namespace SYE.Covol
{
    [Serializable]
    public class VentaDetalleMesVolumen
    {
        public int Mes { get; set; }
        public string ClaveProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public decimal Volumen { get; set; }
        public decimal Total { get; set; }
    }
}

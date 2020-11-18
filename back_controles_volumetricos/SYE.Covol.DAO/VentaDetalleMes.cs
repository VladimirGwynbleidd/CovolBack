using System;

namespace SYE.Covol
{
    [Serializable]
    public class VentaDetalleMes
    {
        public int Mes { get; set; }
        public string ClaveProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public decimal PrecioUnitarioProducto { get; set; }
        public decimal Total { get; set; }
    }
}

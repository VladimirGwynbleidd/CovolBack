using System;

namespace SYE.Covol
{
    public class Reporte
    {
        public int ControlVolumetrico { get; set; }
        public string NumeroPermisoCRE { get; set; }
        public string NoCertificado { get; set; }
        public string Certificado { get; set; }
        public string Rfc { get; set; }
        public DateTime FechaYHoraCorte { get; set; }
        public string TipoDeRegistro { get; set; }
        public string Registro { get; set; }
        public int NumeroUnicoTransaccionVenta { get; set; }
        public int NumeroDispensario { get; set; }
        public int IdentificadorManguera { get; set; }
        public string ClaveProducto { get; set; }
        public string ClaveSubProducto { get; set; }
        public string SubProducto { get; set; }
        public decimal VolumenDespachado { get; set; }
        public decimal PrecioUnitarioProducto { get; set; }
        public decimal ImporteTotalTransaccion { get; set; }
        public DateTime FechaVenta { get; set; }
    }
}

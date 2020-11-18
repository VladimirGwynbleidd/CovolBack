using System;
using System.Collections.Generic;
using System.Text;

namespace SYE.Covol
{
    [Serializable]
    public class Movimiento
    {
        public int ControlVolumetrico { get; set; }
        public string NoCertificado { get; set; }
        public string Certificado { get; set; }
        public DateTime FechaYHoraCorte { get; set; }
        public string TipoDeRegistro { get; set; }
        public int NumeroUnicoTransaccionVenta { get; set; }
        public int NumeroDispensario { get; set; }
        public int IdentificadorManguera { get; set; }
        public decimal VolumenDespachado { get; set; }
        public decimal PrecioUnitarioProducto { get; set; }
        public decimal ImporteTotalTransaccion { get; set; }
        public DateTime FechaYHoraTransaccionVenta { get; set; }
        public string NumeroPermisoCRE { get; set; }
        public string SubProducto { get; set; }
    }
}

using SYE.Covol.DAO.Carga;
using SYE.Covol.PI;

namespace SYE.Covol.BI
{
    public class CargaCovol: ICarga
    {
        private readonly Carga carga = new Carga();

        public Success<LogresultadoCarga> ObtenerBitacora(params object[] parametros)
        {
            return carga.ObtenerBitacora(parametros);
        }

        public Success<RegistroLog> ObtenerdetalleLog(params object[] parametros)
        {
            return carga.ObtenerdetalleLog(parametros);
        }

        public Success<Marcadores> ObtenerMarcadores()
        {
            return carga.ObtenerMarcadores();
        }
    }
}

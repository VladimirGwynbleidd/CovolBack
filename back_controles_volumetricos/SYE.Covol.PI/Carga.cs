using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SYE.Covol.PI
{
    public class Carga
    {
        public Success<Marcadores> ObtenerMarcadores()
        {
            Func<
                FuncionDelegado<Marcadores>.ObtenerResultado,
                string,
                IDictionary<string, object>,
                Success<Marcadores>> response = FuncionDelegado<Marcadores>.obtenerListaResultado;

            return response(new SqlHelperFactory().ExecuteList<Marcadores>, "sp_ObtenerEstadisticaLogMarcadores", null);
        }

        public Success<LogresultadoCarga> ObtenerBitacora(params object[] parametros)
        {
            Func<
                FuncionDelegado<LogresultadoCarga>.ObtenerResultado,
                string,
                IDictionary<string, object>,
                Success<LogresultadoCarga>> response = FuncionDelegado<LogresultadoCarga>.obtenerListaResultado;

            IDictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@FechaLogIni", parametros[0] },
                    { "@FechaLogFin", parametros[1] },
                    { "@level", parametros[2] }
                };

            return response(new SqlHelperFactory().ExecuteList<LogresultadoCarga>, "sp_ObtenerResultadoCarga", values);
        }

        public Success<RegistroLog> ObtenerdetalleLog(params object[] parametros)
        {
            Func<
                FuncionDelegado<RegistroLog>.ObtenerResultado,
                string,
                IDictionary<string, object>,
                Success<RegistroLog>> response = FuncionDelegado<RegistroLog>.obtenerListaResultado;

            IDictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@RFC", parametros[0] },
                    { "@CRE", parametros[1] },
                    { "@FechaLogIni", parametros[2] },
                    { "@FechaLogFin", parametros[2] },
                    { "@level", parametros[3] }
                };

            return response(new SqlHelperFactory().ExecuteList<RegistroLog>, "sp_ObtenerDetalleCarga", values);
        }
    }
}

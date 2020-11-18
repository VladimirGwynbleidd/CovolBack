using System;
using System.Collections.Generic;
using System.Text;

namespace SYE.Covol.PI
{
    public static class Permisionarios
    {
        public static Success<PermicionarioMapa> ObtenerPermisionarios(string entidad, string zona)
        {
            Func<
                FuncionDelegado<PermicionarioMapa>.ObtenerResultado,
                string,
                IDictionary<string, object>,
                Success<PermicionarioMapa>> response = FuncionDelegado<PermicionarioMapa>.obtenerListaResultado;

            Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@ENTIDADFED",entidad },
                    { "@zona",zona }
                };

            return response(new SqlHelperFactory().ExecuteList<PermicionarioMapa>, "sp_mapaestacionesserv", values);
        }

        public static Success<EstacionesServicio> ObtenerEstacionesServicio(string entidad, string rfc)
        {
            Func<
                FuncionDelegado<EstacionesServicio>.ObtenerResultado,
                string,
                IDictionary<string, object>,
                Success<EstacionesServicio>> response = FuncionDelegado<EstacionesServicio>.obtenerListaResultado;

            Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@ENTIDADFED",entidad },
                    { "@RFC",rfc }
                };

            return response(new SqlHelperFactory().ExecuteList<EstacionesServicio>, "SP_MAPAESTACIONESSERVDET", values);
        }

        public static Success<Zona> ObtenerZonas(string zona)
        {
            Func<
                FuncionDelegado<Zona>.ObtenerResultado,
                string,
                IDictionary<string, object>,
                Success<Zona>> response = FuncionDelegado<Zona>.obtenerListaResultado;

            Dictionary<string, object> values = string.IsNullOrWhiteSpace(zona) ? null : new Dictionary<string, object>
                {
                    { "@ZONA",zona }
                };

            return response(new SqlHelperFactory().ExecuteList<Zona>, "SPS_OBTENZONAVALORXENTIDADES", values);
        }
    }
}

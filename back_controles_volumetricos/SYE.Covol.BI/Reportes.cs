using System;
using System.Collections.Generic;
namespace SYE.Covol
{
    public static class Reportes
    {
        public static List<Permicionarios> ObtenerResultados(string rfc, string NoCRE, string tipoRegisdtro, string producto, string fechaIni, string FechaFin)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@RFC", rfc},
                    { "@NOCRE", NoCRE},
                    { "@tipoProducto",  producto},
                    { "@tipoDeRegistro",  tipoRegisdtro},
                    { "@fecIni", fechaIni },
                    { "@fecFin", FechaFin }
                };
                return new SqlHelperFactory().ExecuteList<Permicionarios>("sp_rptControlVol", values);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw new ArgumentNullException(ex.Message);
            }
        }

        public static List<PendientesConsolidado> ObtenerDetalle(string rfc, string NoCRE, string tipoRegisdtro, string producto, string fechaIni, string FechaFin)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@RFC", rfc},
                    { "@NoCRE", NoCRE},
                    { "@tipoProducto",  producto},
                    { "@tipoDeRegistro",  tipoRegisdtro},
                    { "@fecIni", fechaIni },
                    { "@fecFin", FechaFin }
                };
                return new SqlHelperFactory().ExecuteList<PendientesConsolidado>("SP_RPTCONTROLVOLDETA", values);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw new ArgumentNullException(ex.Message);
            }
        }

        public static List<Reporte> ObtenerReporte(string rfc, string tipoRegisdtro, string producto, string fechaIni, string FechaFin)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@RFC", rfc},
                    { "@tipoProducto",  producto},
                    { "@tipoDeRegistro",  tipoRegisdtro},
                    { "@fecIni", fechaIni },
                    { "@fecFin", FechaFin }
                };
                return new SqlHelperFactory().ExecuteList<Reporte>("sp_rptCtrolVolVtaMovimientos", values);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw new ArgumentNullException(ex.Message);
            }
        }


    }
}

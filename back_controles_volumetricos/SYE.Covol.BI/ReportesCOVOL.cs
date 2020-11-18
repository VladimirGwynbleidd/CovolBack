using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SYE.Covol
{
    public static class ReportesCovol
    {
        public static List<VentaDetalleMes> RptObtenVentaMes()
        {
            try
            {
                return new SqlHelperFactory().ExecuteList<VentaDetalleMes>("sp_VentasMes", null);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public static List<VentaDetalleMesVolumen> RptVentaDetalleMesVolumen()
        {
            try
            {
                return new SqlHelperFactory().ExecuteList<VentaDetalleMesVolumen>("sp_VentasMesVolumen", null);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public static List<VentaDetalleMesDispensario> RptVentaDetalleMesDispensario()
        {
            try
            {
                return new SqlHelperFactory().ExecuteList<VentaDetalleMesDispensario>("sp_VentasMesDispensario", null);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public static async Task<List<Movimiento>> ObtenerMovimientos(params object[] parametros)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@rfc", parametros[0] },
                    { "@NoCre", parametros[1] },
                    { "@fecIni", parametros[2] },
                    { "@fecFin", parametros[3] }
                };
                return await new SqlHelperFactoryAsync().ExecuteList<Movimiento>("sp_cruceroControlVolumetrico", values);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public static void CambiarStatus(params object[] parametros)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@RFC", parametros[0] },
                    { "@FECINI", parametros[2] },
                    { "@FECFIN", parametros[3] },
                    { "@DINAMICA", 2 }
                };

                new SqlHelperFactory().Execute("sp_ActualizarProcesados", ExecuteType.ExecuteNonQuery, values);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public static async Task<List<Movimiento>> ObtenerExcedentes(params object[] parametros)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@rfc", parametros[0] },
                    { "@NoCre", parametros[1] },
                    { "@fecIni", parametros[2] },
                    { "@fecFin", parametros[3] }
                };
                return await new SqlHelperFactoryAsync().ExecuteList<Movimiento>("sp_Excedentes", values);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }
        public static async Task<List<Cabecera>> ObtenerCabecera(params object[] parametros)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@rfc", parametros[0] },
                    { "@NoCre", parametros[1] },
                    { "@fecIni", parametros[2] },
                    { "@fecFin", parametros[3] }
                };
                return await new SqlHelperFactoryAsync().ExecuteList<Cabecera>("sp_Cabecera", values);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }
        public static async Task<List<Consolidado>> ObtenerConsolidado(params object[] parametros)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@rfc", parametros[0] },
                    { "@NoCre", parametros[1] },
                    { "@fecIni", parametros[2] },
                    { "@fecFin", parametros[3] }
                };
                return await new SqlHelperFactoryAsync().ExecuteList<Consolidado>("sp_Consolidado", values);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }
        public static async Task<List<Consolidado>> ObtenerConsolidadoMaestro(params object[] parametros)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@RFC", parametros[0] },
                    { "@NoCre", parametros[1] },
                    { "@fecIni", parametros[2] },
                    { "@fecFin", parametros[3] }
                };
                return await new SqlHelperFactoryAsync().ExecuteList<Consolidado>("sp_ConsolidadoRFCPeriodo", values);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public static List<VentaVolumenDespachado> RptVentaVolDespachado(params object[] parametros)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@c_CRE", parametros[0] },
                    { "@d_tipoDeRegistro", parametros[1] },
                    { "@f_fecIni", parametros[2] },
                    { "@f_fecFin", parametros[3] }
                };
                return new SqlHelperFactory().ExecuteList<VentaVolumenDespachado>("sp_cruceroVentaVolumenDespachado", values);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public static List<Marcadores> ObtenerMarcadores()
        {
            try
            {
                return new SqlHelperFactory().ExecuteList<Marcadores>("sp_ObtenerEstadisticaLogMarcadores", null);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }
        public static List<LogresultadoCarga> ObtenerBitacora(params object[] parametros)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@FechaLogIni", parametros[0] },
                    { "@FechaLogFin", parametros[1] },
                    { "@level", parametros[2] }
                };
                return new SqlHelperFactory().ExecuteList<LogresultadoCarga>("sp_ObtenerResultadoCarga", values);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }
        public static List<RegistroLog> ObtenerdetalleLog(params object[] parametros)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@RFC", parametros[0] },
                    { "@CRE", parametros[1] },
                    { "@FechaLogIni", parametros[2] },
                    { "@FechaLogFin", parametros[2] },
                    { "@level", parametros[3] }
                };
                return new SqlHelperFactory().ExecuteList<RegistroLog>("sp_ObtenerDetalleCarga", values);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }
        public static List<Permicionarios> ObtenerPermisionarios(int estatus)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@EstadoFile",estatus }
                };
                return new SqlHelperFactory().ExecuteList<Permicionarios>("sp_PendientesConsolidado", values);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }
        public static List<PendientesConsolidado> ObtenerConsolidadosPendientes(string rfc, string numeroPermisoCRE, int estatus)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@RFC", rfc },
                    { "@numeroPermisoCRE", numeroPermisoCRE },
                    { "@EstadoFile", estatus }
                };
                return new SqlHelperFactory().ExecuteList<PendientesConsolidado>("sp_PendientesDetalleConsolidado", values);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public static List<Estadisticas> VerificarArchivosProcesados()
        {
            try
            {
                return new SqlHelperFactory().ExecuteList<Estadisticas>("sp_ObtenerEstadisticaLogMarcadores",null);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
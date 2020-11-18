using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SYE.Covol
{
    public class CtrolVolumetrico
    {
        public ILogger Logger { get; }

        public CtrolVolumetrico(ILogger logger = null)
        {
            Logger = logger;
        }

        public async Task<int> InsertaXML_CtrolVol(string XML, string fileName)
        {
            try
            {

                Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "@datosXML", XML },
                        { "@fileName", @fileName }
                    };
                int rest = await new SqlHelperFactoryAsync(Logger).ExecuteNonQuery("sp_ControlVolumetricosXML", values);
                if (rest > 0)
                {
                    Logger?.LogInformation("Se ejecuto con exito el procedimiento");
                }
                else
                {
                    Logger?.LogWarning("El archivo " + fileName + " ya fue cargado");
                }
                return rest;
            }
            catch (Exception ex)
            {
                Logger?.LogError($"Error de excepción InsertaXML_CtrolVol: {ex.Message}");
                throw new ArgumentException(ex.Message, ex);
            }
        }

        public string InsertaResultadoCarga(string XML, params object[] parametros)
        {
            try
            {
                using (SqlHelperFactory dal = new SqlHelperFactory(Logger))
                {
                    Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "@datosXML", XML },
                        { "@res_level", parametros[0] },
                        { "@res_message", parametros[1] },
                        { "@fileName", parametros[2] }
                    };
                    Logger?.LogInformation($"Inicia inserción de resultados carga archivo: {parametros[2]}");
                    int rest = (int)dal.ExecuteProcedure("sp_ResultadoCarga", ExecuteType.ExecuteNonQuery, values);
                    if (rest > 1)
                    {
                        Logger?.LogInformation("Se ejecuto con exito el procedimiento almacenado: sp_ResultadoCarga.");
                    }
                    else
                    {
                        Logger?.LogWarning($"Warning: {rest}");
                    }

                    return rest.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError($"Error de excepción InsertaResultadoCarga: {ex.Message}");
                throw new ArgumentException(ex.Message, ex);
            }

        }
    }
}

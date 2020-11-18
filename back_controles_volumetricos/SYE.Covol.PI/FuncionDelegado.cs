using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SYE.Covol.PI
{
    class FuncionDelegado<T> where T : class
    {
        public delegate List<T> ObtenerResultado(string sp, IDictionary<string, object> parametros);
        public delegate int ObtenerResultadoEscalar(string sp, Dictionary<string, object> parametros);

        public static Success<T> obtenerListaResultado(ObtenerResultado metodo,
            string sp, IDictionary<string, object> parametros)
        {
            Success<T> success = new Success<T>();

            try
            {
                success.Exito = true;
                success.ResponseDataEnumerable = metodo(sp, parametros);
                success.ResponseData = new List<T>();

                return success;
            }
            catch (Exception ex)
            {
                success.Exito = false;
                success.Mensaje = ex.Message;
                success.ResponseData = new List<T>();

                Log.Error(ex.Message, ex);
                throw new ArgumentException(JsonConvert.SerializeObject(success), ex);
            }
        }

        public static Success<T> obtenerResultado(ObtenerResultadoEscalar metodo, string sp, Dictionary<string, object> parametros, T arg)
        {
            Success<T> success = new Success<T>();

            try
            {
                int valor = metodo(sp, parametros);
                string mensaje = valor > 0 ? "Se actualizó correctamente el registro" : "No se actualizó correctamente el resgistro";

                success.Exito = true;
                success.Valor = valor;
                success.Mensaje = mensaje;

                return success;
            }
            catch (Exception ex)
            {
                success.Exito = false;
                success.Mensaje = ex.Message;
                success.Data = arg;

                Log.Error(ex.Message, ex);
                throw new ArgumentException(JsonConvert.SerializeObject(success), ex);
            }

        }
    }
}

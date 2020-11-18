using System;
using System.Collections.Generic;
using System.Text;

namespace SYE.Covol.PI
{
    public class CatalogoTipoProducto
    {
        public Success<TipoProducto> Obtener()
        {
            Func<
                FuncionDelegado<TipoProducto>.ObtenerResultado,
                string,
                IDictionary<string, object>,
                Success<TipoProducto>> response = FuncionDelegado<TipoProducto>.obtenerListaResultado;
            return response(new SqlHelperFactory().ExecuteList<TipoProducto>, "sp_ObtenerTipoProducto", null);
        }

        public Success<TipoProducto> Agregar(TipoProducto parameters)
        {
            Func<
                FuncionDelegado<TipoProducto>.ObtenerResultadoEscalar,
                string,
                Dictionary<string, object>,
                TipoProducto,
                Success<TipoProducto>> response = FuncionDelegado<TipoProducto>.obtenerResultado;

            Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "@claveProducto", parameters.ClaveProducto},
                        { "@Producto", parameters.Producto},
                        { "@Comentario", parameters.Comentario }
                    };

            return response(new SqlHelperFactory().ExecuteNonQuery, "sp_AgregarTipoProducto", values, parameters);
        }

        public Success<TipoProducto> Actualizar(TipoProducto parameters)
        {
            Func<
                FuncionDelegado<TipoProducto>.ObtenerResultadoEscalar,
                string,
                Dictionary<string, object>,
                TipoProducto,
                Success<TipoProducto>> response = FuncionDelegado<TipoProducto>.obtenerResultado;

            Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "@idProductos", parameters.IdProducto },
                        { "@claveProducto", parameters.ClaveProducto},
                        { "@Producto", parameters.Producto},
                        { "@Comentario", parameters.Comentario},
                    };

            return response(new SqlHelperFactory().ExecuteNonQuery, "sp_ActualizarTipoProducto", values, parameters);
        }

        public Success<TipoProducto> Eliminar(TipoProducto parameters)
        {
            Func<
                FuncionDelegado<TipoProducto>.ObtenerResultadoEscalar,
                string,
                Dictionary<string, object>,
                TipoProducto,
                Success<TipoProducto>> response = FuncionDelegado<TipoProducto>.obtenerResultado;

            Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "@idProductos", parameters.IdProducto }
                    };

            return response(new SqlHelperFactory().ExecuteNonQuery, "sp_EliminarTipoProducto", values, parameters);
        }
    }
}

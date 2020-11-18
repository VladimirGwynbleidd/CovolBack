using System;
using System.Collections.Generic;
using System.Text;

namespace SYE.Covol.PI
{
    public class CatalogoSubProducto
    {
        public Success<SubProducto> Obtener()
        {
            Func<
                FuncionDelegado<SubProducto>.ObtenerResultado,
                string,
                IDictionary<string, object>,
                Success<SubProducto>> response = FuncionDelegado<SubProducto>.obtenerListaResultado;

            return response(new SqlHelperFactory().ExecuteList<SubProducto>, "sp_ObtenerSubProducto", null);
        }

        public Success<SubProducto> Agregar(SubProducto parameters)
        {
            Func<
                FuncionDelegado<SubProducto>.ObtenerResultadoEscalar,
                string,
                Dictionary<string, object>,
                SubProducto,
                Success<SubProducto>> response = FuncionDelegado<SubProducto>.obtenerResultado;

            Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "@claveProducto", parameters.ClaveProducto},
                        { "@claveSubProducto", parameters.ClaveSubProducto},
                        { "@SubProducto", parameters.DetalleSubProducto },
                        { "@Comentario", parameters.Comentario },
                    };

            return response(new SqlHelperFactory().ExecuteNonQuery, "sp_AgregarSubProducto", values, parameters);
        }

        public Success<SubProducto> Actualizar(SubProducto parameters)
        {
            Func<
                FuncionDelegado<SubProducto>.ObtenerResultadoEscalar,
                string,
                Dictionary<string, object>,
                SubProducto,
                Success<SubProducto>> response = FuncionDelegado<SubProducto>.obtenerResultado;

            Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "@idSubProducto", parameters.IdSubProducto },
                        { "@claveProducto", parameters.ClaveProducto},
                        { "@claveSubProducto", parameters.ClaveSubProducto},
                        { "@DetalleSubProducto", parameters.DetalleSubProducto },
                        { "@Comentario", parameters.Comentario },
                    };

            return response(new SqlHelperFactory().ExecuteNonQuery, "sp_ActualizarSubProducto", values, parameters);
        }

        public Success<SubProducto> Eliminar(SubProducto parameters)
        {
            Func<
                FuncionDelegado<SubProducto>.ObtenerResultadoEscalar,
                string,
                Dictionary<string, object>,
                SubProducto,
                Success<SubProducto>> response = FuncionDelegado<SubProducto>.obtenerResultado;

            Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "@idSubProducto", parameters.IdSubProducto }
                    };

            return response(new SqlHelperFactory().ExecuteNonQuery, "sp_EliminarSubProducto", values, parameters);
        }
    }
}

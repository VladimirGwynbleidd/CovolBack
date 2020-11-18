using System;
using System.Collections.Generic;
using System.Text;

namespace SYE.Covol.PI
{
    public class CatalogoValorEstimulo
    {
        public Success<ValorEstimulo> Obtener()
        {
            Func<
                FuncionDelegado<ValorEstimulo>.ObtenerResultado,
                string,
                IDictionary<string, object>,
                Success<ValorEstimulo>> response = FuncionDelegado<ValorEstimulo>.obtenerListaResultado;

            return response(new SqlHelperFactory().ExecuteList<ValorEstimulo>, "sp_ObtenerValorEstimulo", null);
        }

        public Success<ValorEstimulo> Agregar(ValorEstimulo parameters)
        {
            Func<
                FuncionDelegado<ValorEstimulo>.ObtenerResultadoEscalar,
                string,
                Dictionary<string, object>,
                ValorEstimulo,
                Success<ValorEstimulo>> response = FuncionDelegado<ValorEstimulo>.obtenerResultado;

            Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "@c_subproducto", parameters.ClaveSubProducto },
                        { "@m_Valor", parameters.Valor },
                        { "@d_valor", parameters.DescripcionValor }
                    };

            return response(new SqlHelperFactory().ExecuteNonQuery, "sp_AgregarValorEstimulo", values, parameters);
        }

        public Success<Estimulo> Actualizar(Estimulo parameters)
        {
            Func<
                FuncionDelegado<Estimulo>.ObtenerResultadoEscalar,
                string,
                Dictionary<string, object>,
                Estimulo,
                Success<Estimulo>> response = FuncionDelegado<Estimulo>.obtenerResultado;

            Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "@idvalorestimulo", parameters.IdValorEstimulo },
                        { "@m_valorMagna", parameters.valorMagna },
                        { "@m_valorPremium", parameters.valorPremium }
                    };

            return response(new SqlHelperFactory().ExecuteNonQuery, "sp_ActualizarValorEstimulo", values, parameters);
        }

        public Success<ValorEstimulo> Eliminar(ValorEstimulo parameters)
        {
            Func<
                FuncionDelegado<ValorEstimulo>.ObtenerResultadoEscalar,
                string,
                Dictionary<string, object>,
                ValorEstimulo,
                Success<ValorEstimulo>> response = FuncionDelegado<ValorEstimulo>.obtenerResultado;

            Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "@c_valorestimulo", parameters.IdValorEstimulo }
                    };

            return response(new SqlHelperFactory().ExecuteNonQuery, "sp_EliminarValorEstimulo", values, parameters);
        }
    }
}

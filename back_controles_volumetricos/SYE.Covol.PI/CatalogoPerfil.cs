using System;
using System.Collections.Generic;

namespace SYE.Covol.PI
{
    public class CatalogoPerfil
    {
        public Success<Perfil> Obtener(Perfil parameters = null)
        {
            Dictionary<string, object> values = null;
            if (parameters != null)
            {
                values = new Dictionary<string, object>
                {
                    { "@rfc", parameters.RFC},
                };
            }


            Func<
                FuncionDelegado<Perfil>.ObtenerResultado,
                string,
                IDictionary<string, object>,
                Success<Perfil>> response = FuncionDelegado<Perfil>.obtenerListaResultado;

            return response(new SqlHelperFactory().ExecuteList<Perfil>, "sp_ObtenerPerfil", values ?? null);
        }

        public Success<Perfil> Agregar(Perfil parameters)
        {
            Func<
                FuncionDelegado<Perfil>.ObtenerResultadoEscalar,
                string,
                Dictionary<string, object>,
                Perfil,
                Success<Perfil>> response = FuncionDelegado<Perfil>.obtenerResultado;

            Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "@IdPerfil", parameters.IdPerfil},
                        { "@Nombre", parameters.Nombre},
                        { "@RFC", parameters.RFC},
                        { "@carga", parameters.carga},
                        { "@catalogo", parameters.catalogo},
                        { "@cruce", parameters.cruce},
                    };

            return response(new SqlHelperFactory().ExecuteNonQuery, "sp_AgregarPerfil", values, parameters);
        }

        public Success<Perfil> Actualizar(Perfil parameters)
        {
            Func<
                FuncionDelegado<Perfil>.ObtenerResultadoEscalar,
                string,
                Dictionary<string, object>,
                Perfil,
                Success<Perfil>> response = FuncionDelegado<Perfil>.obtenerResultado;

            Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "@IdUsuario", parameters.IdUsuario},
                        { "@IdPerfil", parameters.IdPerfil},
                        { "@Nombre", parameters.Nombre},
                        { "@RFC", parameters.RFC},
                        { "@carga", parameters.carga},
                        { "@catalogo", parameters.catalogo},
                        { "@cruce", parameters.cruce},
                    };

            return response(new SqlHelperFactory().ExecuteNonQuery, "sp_ActualizarPerfil", values, parameters);
        }

        public Success<Perfil> Eliminar(Perfil parameters)
        {
            Func<
                FuncionDelegado<Perfil>.ObtenerResultadoEscalar,
                string,
                Dictionary<string, object>,
                Perfil,
                Success<Perfil>> response = FuncionDelegado<Perfil>.obtenerResultado;

            Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "@IdUsuario", parameters.IdUsuario}
                    };

            return response(new SqlHelperFactory().ExecuteNonQuery, "sp_EliminarPerfil", values, parameters);
        }

        public Success<Perfil> CatalogoPerfiles()
        {
            Func<
                FuncionDelegado<Perfil>.ObtenerResultado,
                string,
                IDictionary<string, object>,
                Success<Perfil>> response = FuncionDelegado<Perfil>.obtenerListaResultado;

            return response(new SqlHelperFactory().ExecuteList<Perfil>, "sp_ObtenerCatalogoPerfiles", null);
        }
    }
}

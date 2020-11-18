
using SYE.Covol.DAO;
using SYE.Covol.PI;
using System;
using System.Collections.Generic;

namespace SYE.Covol
{
    public class PerfilCovol : ICatalogo<Perfil>, IPerfil<Perfil>
    {
        private readonly CatalogoPerfil cls = new CatalogoPerfil();
        public Success<Perfil> Obtener(Perfil parameters = null)
        {
            return cls.Obtener(parameters);
        }
        public Success<Perfil> Agregar(Perfil parameters)
        {
            return cls.Agregar(parameters);
        }
        public Success<Perfil> Eliminar(Perfil parameters)
        {
            return cls.Eliminar(parameters);
        }
        public Success<Perfil> Actualizar(Perfil parameters)
        {
            return cls.Actualizar(parameters);
        }

        public Success<Perfil> CatalogoPerfiles()
        {
            return cls.CatalogoPerfiles();
        }

        public Success<Perfil> Obtener(string param = "")
        {
            throw new NotImplementedException();
        }
    }
}

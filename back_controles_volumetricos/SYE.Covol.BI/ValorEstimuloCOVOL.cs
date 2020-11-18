using SYE.Covol.DAO;
using SYE.Covol.PI;
using System;
using System.Collections.Generic;
namespace SYE.Covol
{
    public class ValorEstimuloCovol : ICatalogo<ValorEstimulo>
    {
        private readonly CatalogoValorEstimulo cls = new CatalogoValorEstimulo();
        public Success<ValorEstimulo> Obtener(ValorEstimulo parameters = null)
        {
            return cls.Obtener();
        }

        public Success<ValorEstimulo> Agregar(ValorEstimulo parameters)
        {
            return cls.Agregar(parameters);
        }

        //public Success<ValorEstimulo> Actualizar(ValorEstimulo parameters)
        //{
        //    return cls.Actualizar(parameters);
        //}

        public Success<ValorEstimulo> Eliminar(ValorEstimulo parameters)
        {
            return cls.Eliminar(parameters);
        }

        public static Success<PermicionarioMapa> ObtenerPermisionarios(string entidad, string zona)
        {
            return Permisionarios.ObtenerPermisionarios(entidad, zona);
        }

        public static Success<EstacionesServicio> ObtenerEstacionesServicio(string entidad, string rfc)
        {
            return Permisionarios.ObtenerEstacionesServicio(entidad, rfc);
        }

        public static Success<Zona> ObtenerZonas(string zona)
        {
            return Permisionarios.ObtenerZonas(zona);
        }

        public Success<ValorEstimulo> Actualizar(ValorEstimulo parameters)
        {
            throw new NotImplementedException();
        }
    }
}
using SYE.Covol.DAO;
using SYE.Covol.PI;
using System;
using System.Collections.Generic;

namespace SYE.Covol
{
    public class EstimuloCOVOL : ICatalogo<Estimulo>
    {
        private readonly CatalogoValorEstimulo cls = new CatalogoValorEstimulo();

        public Success<Estimulo> Actualizar(Estimulo parameters)
        {
            return cls.Actualizar(parameters);
        }

        public Success<Estimulo> Agregar(Estimulo parameters)
        {
            throw new NotImplementedException();
        }

        public Success<Estimulo> Eliminar(Estimulo parameters)
        {
            throw new NotImplementedException();
        }

        public Success<Estimulo> Obtener(Estimulo param = null)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SYE.Covol.DAO.Carga
{
    public interface IMarcador<T> where T : class
    {
        Success<T> ObtenerMarcadores();
    }
}

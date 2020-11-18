using System;
using System.Collections.Generic;
using System.Text;

namespace SYE.Covol.DAO.Carga
{
    public interface IDetalleLog<T> where T : class
    {
        Success<T> ObtenerdetalleLog(params object[] parametros);
    }
}

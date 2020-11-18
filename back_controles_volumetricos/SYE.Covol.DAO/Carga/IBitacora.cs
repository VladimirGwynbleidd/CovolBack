using System;
using System.Collections.Generic;
using System.Text;

namespace SYE.Covol.DAO.Carga
{
    public interface IBitacora<T> where T : class
    {
        Success<T> ObtenerBitacora(params object[] parametros);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SYE.Covol.DAO
{
    public interface ICatalogo<T> where T: class
    {
        Success<T> Obtener(T param = null);
        Success<T> Agregar(T parameters);
        Success<T> Actualizar(T parameters);
        Success<T> Eliminar(T parameters);
    }
}

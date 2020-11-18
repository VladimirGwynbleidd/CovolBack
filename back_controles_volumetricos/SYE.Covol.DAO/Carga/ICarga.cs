using System;
using System.Collections.Generic;
using System.Text;

namespace SYE.Covol.DAO.Carga
{
    public interface ICarga : 
        IMarcador<Marcadores>, 
        IBitacora<LogresultadoCarga>, 
        IDetalleLog<RegistroLog>
    {
    }
}

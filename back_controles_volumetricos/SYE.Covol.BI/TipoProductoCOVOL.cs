using SYE.Covol.DAO;
using SYE.Covol.PI;

namespace SYE.Covol
{
    public class TipoProductoCovol : ICatalogo<TipoProducto>
    {
        private readonly CatalogoTipoProducto cls = new CatalogoTipoProducto();
        public Success<TipoProducto> Obtener(TipoProducto parameters = null)
        {
            return cls.Obtener();
        }
        public Success<TipoProducto> Agregar(TipoProducto parameters)
        {
            return cls.Agregar(parameters);
        }
        public Success<TipoProducto> Eliminar(TipoProducto parameters)
        {
            return cls.Eliminar(parameters);
        }
        public Success<TipoProducto> Actualizar(TipoProducto parameters)
        {
            return cls.Actualizar(parameters);
        }
    }
}

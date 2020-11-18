using SYE.Covol.DAO;
using SYE.Covol.PI;

namespace SYE.Covol
{
    public class SubProductoCovol : ICatalogo<SubProducto>
    {
        private readonly CatalogoSubProducto cls = new CatalogoSubProducto();
        public Success<SubProducto> Obtener(SubProducto parameters = null)
        {
            return cls.Obtener();
        }
        public Success<SubProducto> Agregar(SubProducto parameters)
        {
            return cls.Agregar(parameters);
        }
        public Success<SubProducto> Eliminar(SubProducto parameters)
        {
            return cls.Eliminar(parameters);
        }
        public Success<SubProducto> Actualizar(SubProducto parameters)
        {
            return cls.Actualizar(parameters);
        }
    }
}

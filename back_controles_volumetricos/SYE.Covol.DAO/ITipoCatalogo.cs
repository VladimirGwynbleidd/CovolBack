namespace SYE.Covol
{
    public interface ITipoCatalogo<T> where T : class
    {
        Success<T> Obtener();
        Success<T> Agregar(T agregar);
        Success<T> Actualizar(T actualizar);
        Success<T> Eliminar(T eliminar);
    }
}

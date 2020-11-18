namespace SYE.Covol
{
    public interface IPerfil<T> where T : class
    {
        Success<T> CatalogoPerfiles();
    }
}

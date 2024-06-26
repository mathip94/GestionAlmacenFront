namespace Servicios
{
    public interface IServicioLogin<I, O>
    {
        O Login(I dto);
    }
}

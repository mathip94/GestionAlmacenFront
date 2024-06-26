namespace Dto
{
    public class ArticuloResponseDto
    {
        public int Count { get; set; }

        public IEnumerable<Articulo> Articulos { get; set; }
    }
}

namespace Dto
{
    public class TipoDeMovimientoResponseDto
    {
        public int Count { get; set; }

        public IEnumerable<TipoDeMovimiento> Tipos { get; set; }
    }
}

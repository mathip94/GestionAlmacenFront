namespace Dto
{
    public class MovimientoDeStockResponseDto
    {
        public int Count { get; set; }

        public IEnumerable<MovimientoDeStockConsultaDto> Movimientos { get; set; }
    }
}

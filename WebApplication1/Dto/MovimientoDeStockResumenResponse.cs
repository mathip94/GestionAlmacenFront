namespace Dto
{
    public class MovimientoDeStockResumenResponse
    {
        public int Año { get; set; }
        public IEnumerable<DetalleMovimientoDto> Detalles { get; set; }
        public int TotalAnual { get; set; }
    }
}

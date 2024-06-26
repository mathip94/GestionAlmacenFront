namespace Models
{
    public class MovimientoDeStockResumenViewModel
    {
        public int Año { get; set; }
        public IEnumerable<DetalleMovimientoViewModel> Detalles { get; set; }
        public int TotalAnual { get; set; }
    }
}

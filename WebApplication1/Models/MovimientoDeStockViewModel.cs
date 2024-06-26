using Dto;

namespace Models
{
    public class MovimientoDeStockViewModel
    {
        public MovimientoDeStock Movimiento { get; set; }
        public IEnumerable<Articulo> Articulos { get; set; }
        public IEnumerable<TipoDeMovimiento> TiposDeMovimiento { get; set; }
        public int ArticuloId { get; set; }
        public int TipoDeMovimientoId { get; set; }
    }
}

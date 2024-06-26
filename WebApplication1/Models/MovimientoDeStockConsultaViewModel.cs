using Dto;

namespace Models
{
    public class MovimientoDeStockConsultaViewModel
    {
        public int? ArticuloId { get; set; }
        public int? TipoDeMovimientoId { get; set; }
        public Articulo Articulo { get; set; }
        public TipoDeMovimiento TipoDeMovimiento { get; set; }
        public IEnumerable<MovimientoDeStockConsultaDto> Movimientos { get; set; }
        public PaginacionModel Paginacion { get; set; }
        public bool BusquedaRealizada { get; set; }
    }
}

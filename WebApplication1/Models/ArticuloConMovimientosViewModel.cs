using Dto;

namespace Models
{
    public class ArticuloConMovimientosViewModel
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public IEnumerable<Articulo> Articulos { get; set; }
        public PaginacionModel Paginacion { get; set; }
        public bool BusquedaRealizada { get; set; }
    }
}

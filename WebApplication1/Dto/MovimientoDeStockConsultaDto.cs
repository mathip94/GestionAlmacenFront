using Dto;

namespace Dto
{
    public class MovimientoDeStockConsultaDto
    {
        public int Id { get; set; }
        public DateTime FechaYHora { get; set; }
        public Articulo Articulo { get; set; }
        public TipoDeMovimiento TipoDeMovimiento { get; set; }
        public Usuario Empleado { get; set; }
        public int CantidadDeUnidadesMovidas { get; set; }
        public int ArticuloId { get; set; }
        public int UsuarioId { get; set; }
        public int TipoDeMovimientoId { get; set; }

        public MovimientoDeStockConsultaDto(int id, DateTime fechaYHora, Articulo articulo, TipoDeMovimiento tipoDeMovimiento, Usuario empleado, int cantidadDeUnidadesMovidas, int articuloId, int usuarioId, int tipoDeMovimientoId)
        {
            Id = id;
            FechaYHora = fechaYHora;
            Articulo = articulo;
            TipoDeMovimiento = tipoDeMovimiento;
            Empleado = empleado;
            CantidadDeUnidadesMovidas = cantidadDeUnidadesMovidas;
            ArticuloId = articuloId;
            UsuarioId = usuarioId;
            TipoDeMovimientoId = tipoDeMovimientoId;
        }

        public MovimientoDeStockConsultaDto() { }
    }
}

using Dto;

namespace Servicios
{
    public interface IServicioMovimientoDeStock
    {
        MovimientoDeStockResponseDto GetMovimientosPorArticuloYTipo(int articuloId, int tipoDeMovimientoId, int skip, int take);

        ArticuloResponseDto GetArticuloConMovimientosEnRangoDeFechas(DateTime fechaInicio, DateTime fechaFin, int skip, int take);

        MovimientoDeStock Add(MovimientoDeStock movimiento);

        ResumenResponseDto GetResumenMovimientos();
    }
}

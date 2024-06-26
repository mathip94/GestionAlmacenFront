using Dto;
using HttpAccess;

namespace HttpAccess
{
    public interface IContextHttpMovimientoDeStock : IContextHttp<MovimientoDeStock>
    {
        Task<MovimientoDeStockResponseDto> GetMovimientosPorArticuloYTipo(string filters);
        Task<ArticuloResponseDto> GetArticuloConMovimientosEnRangoDeFechas(string filters);
        Task<ResumenResponseDto> GetResumenMovimientos(string filters);

    }
}

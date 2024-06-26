using Dto;
using HttpAccess;

namespace HttpAccess
{
    public interface IContextHttpTipoDeMovimiento : IContextHttp<TipoDeMovimiento>
    {
        Task<TipoDeMovimientoResponseDto> GetAll();
    }
}

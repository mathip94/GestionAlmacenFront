using Dto;

namespace HttpAccess
{
    public interface IContextHttpArticulo : IContextHttp<Articulo>
    {
        Task<ArticuloResponseDto> GetAll();
    }
}

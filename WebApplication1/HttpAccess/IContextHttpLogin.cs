using Dto;

namespace HttpAccess
{
    public interface IContextHttpLogin
    {
        Task<LoginOutDto> Login(LoginDto entity);
    }
}

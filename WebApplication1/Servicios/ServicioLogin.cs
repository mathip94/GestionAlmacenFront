using Dto;
using HttpAccess;
using Servicios;

namespace Servicios
{
    public class ServicioLogin : IServicioLogin<LoginDto, LoginOutDto>
    {
        private IContextHttpLogin _repository;
        public ServicioLogin(IContextHttpLogin repository)
        {
            _repository = repository;
        }

        public LoginOutDto Login(LoginDto loginDto)
        {
            LoginOutDto usuario = _repository.Login(loginDto).GetAwaiter().GetResult();
            //grabo token
            return usuario;
        }
    }
}

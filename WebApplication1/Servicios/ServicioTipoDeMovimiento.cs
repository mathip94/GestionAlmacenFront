using Dto;
using HttpAccess;
using Servicios;

namespace Servicios
{
    public class ServicioTipoDeMovimiento : IServicioTipoDeMovimiento
    {
        private IContextHttpTipoDeMovimiento _repository;
        public ServicioTipoDeMovimiento(IContextHttpTipoDeMovimiento repository)
        {
            _repository = repository;
        }

        public TipoDeMovimientoResponseDto GetAll()
        {
            TipoDeMovimientoResponseDto tipoResponse = _repository.GetAll().GetAwaiter().GetResult();
            return tipoResponse;
        }
    }
}

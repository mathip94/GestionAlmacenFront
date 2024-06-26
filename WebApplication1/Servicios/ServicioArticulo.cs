using Dto;
using HttpAccess;

namespace Servicios
{
    public class ServicioArticulo : IServicioArticulo
    {
        private IContextHttpArticulo _repository;
        public ServicioArticulo(IContextHttpArticulo repository)
        {
            _repository = repository;
        }

        public ArticuloResponseDto GetAll()
        {
            ArticuloResponseDto artoculosResponse = _repository.GetAll().GetAwaiter().GetResult();
            return artoculosResponse;
        }
    }
}

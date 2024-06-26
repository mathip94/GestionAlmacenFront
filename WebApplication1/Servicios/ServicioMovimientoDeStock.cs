using Dto;
using HttpAccess;

namespace Servicios
{
    public class ServicioMovimientoDeStock : IServicioMovimientoDeStock
    {
        private IContextHttpMovimientoDeStock _repository;
        public ServicioMovimientoDeStock(IContextHttpMovimientoDeStock repository)
        {
            _repository = repository;
        }

        public MovimientoDeStock Add(MovimientoDeStock movimiento)
        {
            MovimientoDeStock newMovimiento = _repository.Add(movimiento).GetAwaiter().GetResult();
            return newMovimiento;
        }

        public ArticuloResponseDto GetArticuloConMovimientosEnRangoDeFechas(DateTime fechaInicio, DateTime fechaFin, int skip, int take)
        {
            String filters = "/articulos?";

            filters += "fechainicio=" + fechaInicio.ToString("o"); 
            filters += "&fechafin=" + fechaFin.ToString("o"); 
            filters += "&skip=" + skip;
            filters += "&take=" + take;

            ArticuloResponseDto artoculosResponse = _repository.GetArticuloConMovimientosEnRangoDeFechas(filters).GetAwaiter().GetResult();
            return artoculosResponse;
        }

        public MovimientoDeStockResponseDto GetMovimientosPorArticuloYTipo(int articuloId, int tipoDeMovimientoId, int skip, int take)
        {
            String filters = "/movimientos?";

            filters += "articuloid=" + articuloId;
            filters += "&tipodemovimientoid=" + tipoDeMovimientoId;
            filters += "&skip=" + skip;
            filters += "&take=" + take;

            MovimientoDeStockResponseDto movimientoResponse = _repository.GetMovimientosPorArticuloYTipo(filters).GetAwaiter().GetResult();
            return movimientoResponse;
        }

        public ResumenResponseDto GetResumenMovimientos()
        {
            String filters = "/resumen";

            ResumenResponseDto resumenResponse = _repository.GetResumenMovimientos(filters).GetAwaiter().GetResult();
            return resumenResponse;
        }
    }
}

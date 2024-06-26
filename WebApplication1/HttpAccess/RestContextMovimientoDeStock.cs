using Dto;
using HttpAccess;
using System.Text.Json;

namespace HttpAccess
{
    public class RestContextMovimientoDeStock : RestContext<MovimientoDeStock>, IContextHttpMovimientoDeStock
    {
        
        public async Task<MovimientoDeStockResponseDto> GetMovimientosPorArticuloYTipo(string filters)
        {
            // Agregar el token de acceso en la cabecera de autorización
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetToken());

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl + filters);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                WriteIndented = true                                // write pretty json
            };
            // pass options to deserializer
            var movimientoResponseDto = JsonSerializer.Deserialize<MovimientoDeStockResponseDto>(responseBody, options);
            return movimientoResponseDto;
        }

        public async Task<ArticuloResponseDto> GetArticuloConMovimientosEnRangoDeFechas(string filters)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetToken());

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl + filters);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                WriteIndented = true                                // write pretty json
            };
            // pass options to deserializer
            var articulosResponseDto = JsonSerializer.Deserialize<ArticuloResponseDto>(responseBody, options);
            return articulosResponseDto;
        }

        public async Task<ResumenResponseDto> GetResumenMovimientos(string filters)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetToken());

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl + filters);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                WriteIndented = true                                // write pretty json
            };
            // pass options to deserializer
            var resumenResponseDto = JsonSerializer.Deserialize<ResumenResponseDto>(responseBody, options);
            return resumenResponseDto;
        }

        public RestContextMovimientoDeStock(string apiUrl, IHttpContextAccessor httpContextAccessor) : base(apiUrl, httpContextAccessor)
        {
        }
    }
}

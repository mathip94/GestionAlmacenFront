using Dto;
using HttpAccess;
using System.Text.Json;

namespace HttpAccess
{
    public class RestContextTipoDeMovimiento : RestContext<TipoDeMovimiento>, IContextHttpTipoDeMovimiento
    {
        public async Task<TipoDeMovimientoResponseDto> GetAll()
        {
            // Agregar el token de acceso en la cabecera de autorización
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetToken());

            // Cambia la URL para que apunte al endpoint que devuelve todos los movimientos
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                WriteIndented = true                                // write pretty json
            };
            // Deserializar el JSON en una lista de DTOs
            var tipoList = JsonSerializer.Deserialize<TipoDeMovimientoResponseDto>(responseBody, options);
            return tipoList;
        }

        public RestContextTipoDeMovimiento(string apiUrl, IHttpContextAccessor httpContextAccessor) : base(apiUrl, httpContextAccessor)
        {
        }
    }
}

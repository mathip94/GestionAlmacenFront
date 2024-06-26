using Dto;
using System.Text.Json;

namespace HttpAccess
{
    public class RestContextArticulo : RestContext<Articulo>, IContextHttpArticulo
    {
        public async Task<ArticuloResponseDto> GetAll()
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
            var articulosList = JsonSerializer.Deserialize<ArticuloResponseDto>(responseBody, options);
            return articulosList;
        }

        public RestContextArticulo(string apiUrl, IHttpContextAccessor httpContextAccessor) : base(apiUrl, httpContextAccessor)
        {
        }
    }
}

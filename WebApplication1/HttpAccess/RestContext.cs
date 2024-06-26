using System.Net.Http.Headers;
using System.Text.Json;

namespace HttpAccess
{
    public class RestContext<T> : IContextHttp<T>
    {
        protected readonly HttpClient httpClient;
        protected readonly string apiUrl = "";
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public RestContext(string apiUrl, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            this.apiUrl = apiUrl;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected string GetToken()
        {
            // Acceder al HttpContext
            var httpContext = _httpContextAccessor.HttpContext;
            // Usar el HttpContext como necesites
            // Por ejemplo, acceder a los datos de la sesión:
            var token = httpContext.Session.GetString("token");
            return token;
        }

        public async Task<T> Add(T entity)
        {
            string entityJson = JsonSerializer.Serialize(entity);
            // Agregar el token de acceso en la cabecera de autorización
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetToken());
            StringContent content = new StringContent(entityJson, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await httpClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode(); // Esto lanza una excepción si el código de estado no indica éxito
            }
            catch (HttpRequestException ex) when (response != null && !response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                // Puedes lanzar una excepción personalizada con el contenido del error
                throw new ApplicationException(errorContent, ex);
            }
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                WriteIndented = true                                // write pretty json
            };
            // pass options to deserializer
            var createdEntity = JsonSerializer.Deserialize<T>(responseBody, options);
            return createdEntity;
        }

        public async Task<T> GetById(int id)
        {
            // Agregar el token de acceso en la cabecera de autorización
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetToken());
            HttpResponseMessage response = await httpClient.GetAsync($"{apiUrl}/{id}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                WriteIndented = true                                // write pretty json
            };
            // pass options to deserializer
            var entity = JsonSerializer.Deserialize<T>(responseBody, options);
            return entity;
        }

        public async Task<bool> Remove(int id)
        {
            // Agregar el token de acceso en la cabecera de autorización
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetToken());
            HttpResponseMessage response = await httpClient.DeleteAsync($"{apiUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(int id, T entity)
        {
            string entityJson = JsonSerializer.Serialize(entity);
            // Agregar el token de acceso en la cabecera de autorización
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetToken());
            StringContent content = new StringContent(entityJson, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PutAsync($"{apiUrl}/{id}", content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                WriteIndented = true                                // write pretty json
            };
            // pass options to deserializer

            return true;
        }
    }
}

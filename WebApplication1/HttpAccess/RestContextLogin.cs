using Dto;
using System.Net.Http.Headers;
using System.Text.Json;
using WebApplication1.Exceptions;

namespace HttpAccess
{
    public class RestContextLogin : IContextHttpLogin
    {
        private readonly HttpClient httpClient;
        private readonly string apiUrl = "";

        public RestContextLogin(string apiUrl)
        {
            this.apiUrl = apiUrl;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<LoginOutDto> Login(LoginDto entity)
        {
            string entityJson = JsonSerializer.Serialize(entity);
            StringContent content = new StringContent(entityJson, System.Text.Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                    WriteIndented = true                                // write pretty json
                };
                // pass options to deserializer
                var createdEntity = JsonSerializer.Deserialize<LoginOutDto>(responseBody, options);
                return createdEntity;
            }
            catch (HttpRequestException ex)
            {
                throw new LoginException("Error al logear");
            }
           
        }
    }
}

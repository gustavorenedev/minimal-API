using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.ModelViews;
using System.Text;
using System.Text.Json;

namespace Test.Helpers
{
    public class TokenProvider
    {
        private readonly HttpClient _client;
        private string _token;

        public TokenProvider(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetTokenAsync()
        {
            if (!string.IsNullOrEmpty(_token))
            {
                return _token;
            }

            var loginDTO = new LoginDTO
            {
                Email = "adm@teste.com",
                Senha = "123456",
            };

            var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "Application/json");

            var response = await _client.PostAsync("/administradores/login", content);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            _token = admLogado?.Token;

            return _token;
        }
    }

}

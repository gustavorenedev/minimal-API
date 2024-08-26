using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.ModelViews;
using System.Net;
using System.Text.Json;
using System.Text;
using Test.Helpers;
using minimal_api.Dominio.Entidade;

namespace Test.Requests
{
    [TestClass]
    public class VeiculoRequestTest
    {
        private static TokenProvider _tokenProvider;
        private static HttpClient _client;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            Setup.ClassInit(testContext);
            _client = Setup.client;
            _tokenProvider = new TokenProvider(_client);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Setup.ClassCleanup();
        }

        [TestMethod]
        public async Task TestarRequestVeiculo()
        {
            //Arrange
            var token = await _tokenProvider.GetTokenAsync();

            var veiculoDTO = new VeiculoDTO
            {
                Nome = "Fusca",
                Marca = "Volkswagen",
                Ano = 1999
            };

            var content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "Application/json");

            //Act
            var request = new HttpRequestMessage(HttpMethod.Post, "/veiculos")
            {
                Content = content
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.SendAsync(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();
            var veiculo = JsonSerializer.Deserialize<Veiculo>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.IsNotNull(veiculo?.Nome ?? "");
            Assert.IsNotNull(veiculo?.Marca ?? "");
            Assert.IsNotNull(veiculo?.Ano);
        }

        [TestMethod]
        public async Task TestarObterTodosVeiculos()
        {
            var token = await _tokenProvider.GetTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Get, "/veiculos");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.SendAsync(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();
            var veiculos = JsonSerializer.Deserialize<List<Veiculo>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.IsNotNull(veiculos);
            Assert.IsTrue(veiculos.Count > 0);
        }

        [TestMethod]
        public async Task TestarObterVeiculoPorId()
        {
            // Arrange
            var token = await _tokenProvider.GetTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Get, "/veiculo/1");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Assert
            var response = await _client.SendAsync(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();
            var veiculo = JsonSerializer.Deserialize<Veiculo>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.IsNotNull(veiculo);
            Assert.AreEqual(1, veiculo?.Id);
        }

        [TestMethod]
        public async Task TestarAtualizarVeiculo()
        {
            // Arrange
            var token = await _tokenProvider.GetTokenAsync();

            var veiculoDTO = new VeiculoDTO
            {
                Nome = "Fusca Turbo",
                Marca = "Volkswagen",
                Ano = 2000
            };

            var content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, "/veiculos/1")
            {
                Content = content
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();
            var veiculo = JsonSerializer.Deserialize<Veiculo>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.IsNotNull(veiculo);
            Assert.AreEqual("Fusca Turbo", veiculo?.Nome);
        }

        [TestMethod]
        public async Task TestarApagarVeiculo()
        {
            // Arrange
            var token = await _tokenProvider.GetTokenAsync();

            // Criar um veículo com ID 2
            var veiculoDTO = new VeiculoDTO
            {
                Nome = "Gol",
                Marca = "Volkswagen",
                Ano = 2010
            };

            var content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "application/json");
            var createRequest = new HttpRequestMessage(HttpMethod.Post, "/veiculos")
            {
                Content = content
            };
            createRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var createResponse = await _client.SendAsync(createRequest);
            createResponse.EnsureSuccessStatusCode(); // Verifica se a criação foi bem-sucedida

            var createdVeiculo = JsonSerializer.Deserialize<Veiculo>(await createResponse.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Agora que o veículo foi criado, usar o ID 2 para o teste de exclusão
            var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, $"/veiculos/{createdVeiculo.Id}");
            deleteRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var deleteResponse = await _client.SendAsync(deleteRequest);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }
    }
}

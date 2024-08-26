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
        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            Setup.ClassInit(testContext);
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
            var veiculoDTO = new VeiculoDTO
            {
                Nome = "Fusca",
                Marca = "Volkswagen",
                Ano = 1999
            };

            var content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "Application/json");

            //Act
            var response = await Setup.client.PostAsync("/veiculos", content);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();
            var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.IsNotNull(admLogado?.Email ?? "");
            Assert.IsNotNull(admLogado?.Perfil ?? "");
            Assert.IsNotNull(admLogado?.Token ?? "");
        }

        [TestMethod]
        public async Task TestarObterTodosVeiculos()
        {
            // Arrange
            var response = await Setup.client.GetAsync("/veiculos");

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
            var response = await Setup.client.GetAsync("/veiculo/1");

            // Assert
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
            var veiculoDTO = new VeiculoDTO
            {
                Nome = "Fusca Turbo",
                Marca = "Volkswagen",
                Ano = 2000
            };

            var content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "application/json");

            // Act
            var response = await Setup.client.PutAsync("/veiculos/1", content);

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
            var response = await Setup.client.DeleteAsync("/veiculos/1");

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}

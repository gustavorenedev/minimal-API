using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidade;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.Db;
using System.Text;
using System.Text.Json;
using Test.Helpers;

namespace Test.Domain.Servico
{
    [TestClass]
    public class VeiculoServicoTest
    {
        private dbContexto CriarContextoDeTeste()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            return new dbContexto(config);
        }

        [TestMethod]
        public async Task TestandoSalvarVeiculoAsync()
        {
            //Arrange
            var loginDTO = new LoginDTO
            {
                Email = "adm@teste.com",
                Senha = "123456",
            };

            var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "Application/json");

            //Act
            var response = await Setup.client.PostAsync("/administradores/login", content);

            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            var veic = new Veiculo();
            veic.Id = 1;
            veic.Nome = "Fusca";
            veic.Marca = "Volkswagen";
            veic.Ano = 1999;

            var veiculoServico = new VeiculoServico(context);

            //Act
            veiculoServico.Incluir(veic);

            //Assert
            Assert.AreEqual(1, veiculoServico.Todos(1).Count());
        }

        [TestMethod]
        public void TestandoAtualizarVeiculo()
        {
            // Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            var veic = new Veiculo
            {
                Id = 1,
                Nome = "Fusca",
                Marca = "Volkswagen",
                Ano = 1999
            };

            var veiculoServico = new VeiculoServico(context);
            veiculoServico.Incluir(veic);

            // Act
            veic.Nome = "Fusca Turbo";
            veiculoServico.Atualizar(veic);

            // Assert
            var veiculoAtualizado = veiculoServico.BuscarPorId(1);
            Assert.AreEqual("Fusca Turbo", veiculoAtualizado?.Nome);
        }

        [TestMethod]
        public void TestandoApagarVeiculo()
        {
            // Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            var veic = new Veiculo
            {
                Id = 1,
                Nome = "Fusca",
                Marca = "Volkswagen",
                Ano = 1999
            };

            var veiculoServico = new VeiculoServico(context);
            veiculoServico.Incluir(veic);

            // Act
            veiculoServico.Apagar(veic);

            // Assert
            Assert.AreEqual(0, veiculoServico.Todos(1).Count());
        }

        [TestMethod]
        public void TestandoBuscarPorId()
        {
            // Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            var veic = new Veiculo
            {
                Id = 1,
                Nome = "Fusca",
                Marca = "Volkswagen",
                Ano = 1999
            };

            var veiculoServico = new VeiculoServico(context);
            veiculoServico.Incluir(veic);

            // Act
            var veiculoBuscado = veiculoServico.BuscarPorId(1);

            // Assert
            Assert.IsNotNull(veiculoBuscado);
            Assert.AreEqual("Fusca", veiculoBuscado.Nome);
        }
    }
}

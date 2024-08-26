using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimal_api.Dominio.Entidade;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.Db;

namespace Test.Domain.Servico
{
    [TestClass]
    public class AdministradorServicoTest
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
        public void TestandoSalvarAdministrador()
        {
            //Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

            var adm = new Administrador();
            adm.Id = 1;
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";

            var administradorServico = new AdministradorServico(context);

            //Act
            administradorServico.Incluir(adm);

            //Assert
            Assert.AreEqual(1, administradorServico.Todos(1).Count());
        }

        [TestMethod]
        public void TestandoBuscaPorId()
        {
            //Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

            var adm = new Administrador();
            adm.Id = 1;
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";

            var administradorServico = new AdministradorServico(context);

            //Act
            administradorServico.Incluir(adm);
            var admDoBanco = administradorServico.BuscarPorId(adm.Id);

            //Assert
            Assert.AreEqual(1, admDoBanco?.Id);
        }
    }
}

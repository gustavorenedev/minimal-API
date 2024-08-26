using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using minimal_api;
using minimal_api.Dominio.Interface;
using Test.Mocks;

namespace Test.Helpers
{
    public class Setup
    {
        public const string PORT = "5001";
        public static TestContext testContext = default!;
        public static WebApplicationFactory<Startup> http = default!;
        public static HttpClient client = default!;

        public static void ClassInit(TestContext testContext)
        {
            Setup.testContext = testContext;
            http = new WebApplicationFactory<Startup>();

            http = http.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("https_port", PORT).UseEnvironment("Testing");

                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IAdministradorServico, AdministradorServicoMock>();
                    services.AddScoped<IVeiculoServico, VeiculoMock>();
                });
            });
            client = http.CreateClient();
        }

        public static void ClassCleanup()
        {
            Setup.http.Dispose();
        }
    }
}

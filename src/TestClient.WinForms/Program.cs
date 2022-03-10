using Health.Services;
using Health.Services.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestClient.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var host =
            Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .Build();

            var form = host.Services.GetRequiredService<EmpiQuery>();

            Application.Run(form);
        }

        private static void ConfigureServices(HostBuilderContext builder, IServiceCollection services)
        {
            services.AddLogging(config => config.AddConsole());

            if (builder.HostingEnvironment.IsEnvironment("Local"))
            {
                // Use a stub if you don't have access to the EMPI API yet
                services.AddClientRegistryServiceStub();
            }
            else
            {
                services.AddClientRegistryService(builder.Configuration.GetSection(ClientRegistrySettings.SectionName));
            }

            services.AddSingleton<EmpiQuery>();
        }
    }
}
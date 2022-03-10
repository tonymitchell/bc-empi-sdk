using Health.Services.Soap;
using Health.Services.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Http;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Health.Services;
using Health.Services.Hl7v3;
using Microsoft.Extensions.Options;

namespace Health.Services.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClientRegistryService(this IServiceCollection services, IConfiguration configSection)
        {
            // Configure via IConfiguration
            services.Configure<ClientRegistrySettings>(configSection);

            ConfigureServices(services, services.BuildServiceProvider().GetRequiredService<IOptions<ClientRegistrySettings>>().Value);

            return services;
        }

        public static IServiceCollection AddClientRegistryService(this IServiceCollection services, Action<ClientRegistrySettings> configureOptions)
        {
            // Configure via delegate
            services.Configure<ClientRegistrySettings>(configureOptions);

            ConfigureServices(services, services.BuildServiceProvider().GetRequiredService<IOptions<ClientRegistrySettings>>().Value);

            return services;
        }


        private static void ConfigureServices(IServiceCollection services, ClientRegistrySettings settings)
        {
            services.AddHttpClient<HttpSoapClient>()
                .ConfigurePrimaryHttpMessageHandler(sp =>
                {
                    var handler = new HttpClientHandler();

                    // Add client certificate to SoapClient
                    string clientCertificateSubjectName = settings.ClientCertificateSubjectName;
                    if (clientCertificateSubjectName != null)
                    {
                        handler.ClientCertificates.AddCertificateBySubjectName(clientCertificateSubjectName);
                    }

                    return handler;
                });

            services.AddSingleton<Hl7v3QueryResponseParser>();
            services.AddTransient<IClientRegistry, ClientRegistryService>();
        }


        public static IServiceCollection AddClientRegistryServiceStub(this IServiceCollection services)
        {
            services.AddSingleton<Hl7v3QueryResponseParser>();
            services.AddTransient<IClientRegistry, ClientRegistryServiceStub>();

            return services;
        }


    }
}

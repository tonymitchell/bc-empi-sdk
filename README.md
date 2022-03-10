# bc-empi-sdk
An unofficial SDK for the BC Enterprise Master Patient Index (EMPI). This project is **not** affiliated with the BC Ministry of Health. 

This SDK provides a simple API for making queries against the BC EMPI.  There are two types of query operations available:
- GetDemographics: Get patient demographics details for a patient given an identifier (MRN, PHN)
- FindCandidates: Find patients that match supplied demographic details such as name, birthdate, address, etc.

There are three example projects that exercise the SDK that you can explore:
- [Web app (src/TestClient.Web/)](src/TestClient.Web/)
- [Console app (src/TestClient.Console/)](src/TestClient.Console/)
- [WinForms app (src/TestClient.WinForms/)](src/TestClient.WinForms/)

## Using ClientRegistry service
The ClientRegistry service will typically be used by defining a dependency on IClientRegistry, and then using that interface to call either GetDemographicsAsync or FindCandidatesAsync. To enable this, the service must be configured via the dependency injection provider.

```csharp
    public class QueryApp
    {
        IClientRegistry _service;

        // ClientRegistry service injected via DI
        public IndexModel(IClientRegistry service)
        {
            _service = service;
        }

        public List<Candidate> QueryByPhn(string phn)
        {
            // Make a GetDemographics call
            var response = await _service.GetDemographicsAsync(new GetDemographicsParameters() { 
                Phn = query.Phn
            });

            // Return the list of candidates (0 or 1 candidates for GetDemographics)
            return response.Candidates.ToList();
        }
    }
```

## Configuring the Client Registry client

### Using Generic Host Builder

***Configure from a configuration source***

If configuration is specified in a config file, the following configuration can be used to configure it.

```csharp
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostBuilder, services) =>
    {
        services.AddClientRegistryService(
            hostBuilder.Configuration.GetSection(ClientRegistrySettings.SectionName)
        );
    })
    .Build();
```

***Configure inline***

If you are not using configuration files, the service can be easily configured inline as in the example below.

```csharp
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostBuilder, services) =>
    {
        services.AddClientRegistryService(config =>
        {
           config.ServiceUrls.QueryServices = new Uri(@"<empi-query-service-url>");
           config.ClientCertificateSubjectName = "<client-certificate-subject-name-here>";
           config.SendingOrgId = "<your-sending-org-id>";
           config.SendingSystemId = "<your-sending-system-id>";
        });
    })
    .Build();
```


### Using WebApplication Builder
Below is an example of configuring the service using a WebApplication builder.

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddClientRegistryService(
    builder.Configuration.GetSection(ClientRegistrySettings.SectionName)
);
```


### Testing Without Access

If you don't have the ability to query EMPI, there is a predefined stub you can use. To use it call the AddClientRegistryServiceStub extension method instead of AddClientRegistryService to wire up the stub.

```csharp
// Use the built-in stub if you don't have access to the EMPI API yet
services.AddClientRegistryServiceStub();
// services.AddClientRegistryService(
//     hostBuilder.Configuration.GetSection(ClientRegistrySettings.SectionName)
// );
```


## License

This project is licensed under the Apache 2.0 License - see the [LICENSE](LICENSE) file for details

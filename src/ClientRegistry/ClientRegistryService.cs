using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;
using Health.Services;
using Health.Services.Hl7v3;
using Health.Services.Soap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Health.Services
{
    public class ClientRegistryService : IClientRegistry
    {
        readonly HttpSoapClient _soapClient;
        readonly Hl7v3QueryResponseParser _responseParser;

        public Uri ServiceUri { get; set; }
        public string SendingOrgId { get; set; }
        public string SendingSystemId { get; set; }


        public ClientRegistryService(IOptions<ClientRegistrySettings> settings, HttpSoapClient soapClient, Hl7v3QueryResponseParser responseParser)
        {
            _soapClient = soapClient;
            _responseParser = responseParser;

            var config = settings.Value;
            this.ServiceUri = config.ServiceUrls.QueryServices;
            this.SendingOrgId = config.SendingOrgId;
            this.SendingSystemId = config.SendingSystemId;
        }

        public async Task<QueryResponse> FindCandidatesAsync(FindCandidatesParameters parameters)
        {
            XElement request = new Hl7v3FindCandidatesMessageBuilder()
                .FromParameters(parameters)
                .WithSender(SendingOrgId, SendingSystemId)
                .Build();

            XElement response = await _soapClient.SendAsync(ClientRegistrySoapAction.HCIM_IN_FindCandidates, ServiceUri, request).ConfigureAwait(false);

            return _responseParser.ParseResponse(response);
        }

        public async Task<QueryResponse> GetDemographicsAsync(GetDemographicsParameters parameters)
        {
            XElement request = new Hl7v3GetDemographicsMessageBuilder()
                .FromParameters(parameters)
                .WithSender(SendingOrgId, SendingSystemId)
                .Build();

            XElement response = await _soapClient.SendAsync(ClientRegistrySoapAction.HCIM_IN_GetDemographics, ServiceUri, request).ConfigureAwait(false);

            return _responseParser.ParseResponse(response);
        }

    }

}

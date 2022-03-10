using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Health.Services.Soap
{
    public class HttpSoapClient
    {
        static readonly XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";

        readonly HttpClient client;

        public HttpSoapClient(HttpClient client)
        {
            this.client = client;
        }

        public Task<XElement> SendAsync(SoapRequest soapRequest)
        {
            return SendAsync(soapRequest.Action, soapRequest.RequestUri, soapRequest.Body);
        }

        public async Task<XElement> SendAsync(string action, Uri serviceUri, XElement requestBody)
        {
            using HttpRequestMessage request = CreateSoapRequestMessage(action, serviceUri, requestBody);

            using var response = await client.SendAsync(request).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var xmlResponse = XElement.Parse(responseContent);

            // Return body
            return UnwrapSoapEnvelope(xmlResponse);
        }


        private static XDocument WrapSoapEnvelope(XElement soapBody)
        {
            XDocument soapEnvelope = new XDocument(new XDeclaration("1.0", "UTF-8", "no"),
                new XElement(soap + "Envelope",
                    new XAttribute(XNamespace.Xmlns + "soap", soap),
                    new XElement(soap + "Body", soapBody)
                    )
                );

            return soapEnvelope;
        }

        private static XElement UnwrapSoapEnvelope(XElement soapMessage)
        {
            return soapMessage.Element(soap + "Body").Descendants().First();
        }

        private static HttpRequestMessage CreateSoapRequestMessage(string soapAction, Uri requestUri, XElement requestBody)
        {
            // Wrap in SOAP envelopm
            XDocument soapRequest = WrapSoapEnvelope(requestBody);

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Headers.Add("SOAPAction", soapAction);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            request.Content = new StringContent(soapRequest.ToString(), Encoding.UTF8, "text/xml");
            return request;
        }
    }
}
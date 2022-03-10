using System;
using System.Xml.Linq;

namespace Health.Services.Soap
{
    public class SoapRequest
    {
        public string Action { get; set; }
        public Uri RequestUri { get; set; } 
        public XElement Body {  get; set; }

        public SoapRequest(string action, Uri requestUri, XElement body)
        {
            Action = action;
            RequestUri = requestUri;
            Body = body;
        }
    }

}

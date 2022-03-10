using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;
using Health;
using Health.Services;
using HealthRegistries;

namespace Health.Services.Hl7v3
{

    public class Hl7v3GetDemographicsMessageBuilder
    {
        private GetDemographicsParameters _parameters = new GetDemographicsParameters();
        public string SendingOrgId { get; set; }
        public string SendingSystemId { get; set; }

        public Hl7v3GetDemographicsMessageBuilder FromParameters(GetDemographicsParameters parameters)
        {
            _parameters = new GetDemographicsParameters(parameters); // Take a copy

            return this;
        }

        public Hl7v3GetDemographicsMessageBuilder WithSender(string sendingOrgId, string sendingSystemId)
        {
            this.SendingOrgId = sendingOrgId;
            this.SendingSystemId = sendingSystemId;
            return this;
        }

        public Hl7v3GetDemographicsMessageBuilder WithPhn(string phn)
        {
            _parameters.Phn = phn;
            return this;
        }

        public Hl7v3GetDemographicsMessageBuilder WithHistory(bool includeHistory = true)
        {
            _parameters.IncludeHistory = includeHistory;
            return this;
        }

        public XElement Build()
        {
            // Ensure build parameters are valid first
            _parameters.Validate();

            XNamespace hl7 = "urn:hl7-org:v3";
            XElement request = LoadTemplate();

            Hl7v3MessageUtil.InitializeRequest(ref request, this.SendingSystemId, this.SendingOrgId);

            // Set the appropriate interaction ID
            //  <interactionId root="2.16.840.1.113883.3.51.1.1.2" extension="HCIM_IN_GetDemographics"/>
            string interactionId = _parameters.IncludeHistory ? "HCIM_GetDemographics.History" : "HCIM_IN_GetDemographics";
            request.Element(hl7 + "interactionId").SetAttributeValue("extension", interactionId);

            // controlActProcess
            XElement controlActProcess = request.Element(hl7 + "controlActProcess");

            //<queryByParameter>
            //    <queryByParameterPayload>
            XElement queryByParameter = controlActProcess.Element(hl7 + "queryByParameter").Element(hl7 + "queryByParameterPayload");

            //<person.id>
            //    <value root="2.16.840.1.113883.3.51.1.1.6.1" extension="9879835895" assigningAuthorityName="MOH_CRS"/>
            //</person.id>
            XElement personId = queryByParameter.Element(hl7 + "person.id");
            if (_parameters.Phn != null)
            {
                var value = personId.Element(hl7 + "value");
                if (value != null)
                {
                    //PHN Search:
                    //      extension = <identifier> 
                    //      root = 2.16.840.1.113883.3.51.1.1.6.1
                    // MRN Search (specific):
                    //      extension = <identifier> 
                    //      root = 2.16.840.1.113883.3.51.1.1.6
                    //      assigningAuthorityName = <assigning authority identifier code> 
                    //      See AssigningAuthority listing in terminology.
                    // MRN Search (generic):
                    //      extension = <identifier> 
                    //      root = 2.16.840.1.113883.3.51.1.1.6
                    value.SetAttributeValue("root", "2.16.840.1.113883.3.51.1.1.6.1");
                    //value.SetAttributeValue("assigningAuthorityName", "MOH_CRS");
                    value.SetAttributeValue("extension", _parameters.Phn);
                }
            }
            else
                personId.Remove();


            return request;
        }


        private static XElement LoadTemplate()
        {
            // If performance is poor, could cache the parsed request object and return a cloned copy.
            XElement request = XElement.Parse(Resources.GetDemographicsRequest);
            return request;
        }



#if false

        #region SampleFluentRequestBuilder
        interface IClientRegistryMessageBuilders
        {
            IGetDemographicsBuilder GetDemographics();
            IFindCandidatesBuilder FindCandidates();
        }
        interface IRequestBuilder
        {
            ITo From(string sender);
        }
        interface ITo
        {
            IBy To(string receiver);
        }
        interface IBy
        {
            IGetDemographicsQueryBuilder By(string user);
        }
        interface IGetDemographicsQueryBuilder
        {
            IRequestMessageBuilder WherePhnIs(string phn);
            IRequestMessageBuilder WhereMrnIs(string mrn);
        }
        interface IRequestMessageBuilder
        {
            XElement GetXmlMessage();
        }
        interface IGetDemographicsBuilder : IRequestBuilder 
        {
            IGetDemographicsBuilder WithHistory();
        }
        interface IFindCandidatesBuilder : IRequestBuilder
        {
        }

        class Request : IGetDemographicsBuilder, IFindCandidatesBuilder, ITo, IBy, IGetDemographicsQueryBuilder, IRequestMessageBuilder
        {
            string _messageType;
            string _sender;
            string _receiver;
            string _dataEnterer;
            Dictionary<string, string> _parameters = new Dictionary<string, string>();

            public Request(string messageType)
            {
                _messageType = messageType;
            }

            IGetDemographicsBuilder IGetDemographicsBuilder.WithHistory()
            {
                Debug.Assert(_messageType == "HCIM_GetDemographics");
                _messageType = "HCIM_GetDemographics.History";
                return this;
            }
            ITo IRequestBuilder.From(string sender)
            {
                _sender = sender;
                return this;
            }
            IBy ITo.To(string receiver)
            {
                _receiver = receiver;
                return this;
            }
            IGetDemographicsQueryBuilder IBy.By(string user)
            {
                _dataEnterer = user;
                return this;
            }
            IRequestMessageBuilder IGetDemographicsQueryBuilder.WherePhnIs(string phn)
            {
                _parameters.Add("phn", phn);
                return this;
            }
            IRequestMessageBuilder IGetDemographicsQueryBuilder.WhereMrnIs(string mrn)
            {
                _parameters.Add("mrn", mrn);
                return this;
            }
            XElement IRequestMessageBuilder.GetXmlMessage()
            {
                throw new NotImplementedException();
            }
        }


        class BuilderFactory : IClientRegistryMessageBuilders
        {
            IGetDemographicsBuilder IClientRegistryMessageBuilders.GetDemographics()
            {
                return new Request("HCIM_GetDemographics");
            }

            IFindCandidatesBuilder IClientRegistryMessageBuilders.FindCandidates()
            {
                return new Request("HCIM_FindCandidates");
            }
        }

        static IClientRegistryMessageBuilders GetBuilderFactory()
        {
            return new BuilderFactory();
        }

        static void SampleFluentRequestBuilder()
        {

            var factory = GetBuilderFactory();
            var builder = factory.GetDemographics().WithHistory();

            XElement request =  builder
                                    .From("me").To("empi")
                                    .By("me")
                                    .WherePhnIs("9123456789")
                                    .GetXmlMessage();

        }
        #endregion
#endif


    }

}

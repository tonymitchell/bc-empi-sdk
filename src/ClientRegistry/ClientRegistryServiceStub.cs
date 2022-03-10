using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Health;
using Health.Services;
using Health.Services.Hl7v3;
using HealthRegistries;

namespace Health.Services
{
    public class ClientRegistryServiceStub : IClientRegistry
    {
        //GetDemographicsMessageBuilder _getDemographicsBuilder;
        //FindCandidatesMessageBuilder _findCandidatesBuilder;
        Hl7v3QueryResponseParser _responseParser;

        public ClientRegistryServiceStub(Hl7v3QueryResponseParser responseParser)
        {
            _responseParser = responseParser;
        }


        public Task<QueryResponse> FindCandidatesAsync(FindCandidatesParameters parameters)
        {
            XElement responseMessage = XElement.Parse(Resources.FindCandidatesResponse);
            QueryResponse response = _responseParser.ParseResponse(responseMessage);
            
            //QueryResponse response = new QueryResponse()
            //{
            //    Code = "BCHCIM.FC.0.0012 | The search completed successfully.",
            //    ResultTotalQuantity = 4,
            //    Candidates = new List<Candidate>
            //    {
            //        new Candidate { Score = 10, Patient = 
            //            new Patient { Phn="90023410534", Surname="Smith", Given=new List<string>{ "Cindy" }, Gender="F", DateOfBirth=new DateTime(1961,2,3) } },
            //        new Candidate { Score = 8, Patient = 
            //            new Patient { Phn="92346362753", Surname="Smith", Given=new List<string>{ "Cynthia" }, Gender="F", DateOfBirth=new DateTime(1961,2,3) } },
            //        new Candidate { Score = 5, Patient = 
            //            new Patient { Phn="91234567891", Surname="Smythe", Given=new List<string>{ "Cynthia" }, Gender="F", DateOfBirth=new DateTime(1965,5,23) } },
            //        new Candidate { Score = 3, Patient = 
            //            new Patient { Phn="91234567890", Surname="Hawkins", Given=new List<string>{ "Cindy" }, Gender="F", DateOfBirth=new DateTime(1958,8,13) } }
            //    }
            //};

            return Task.FromResult(response);
        }

        public Task<QueryResponse> GetDemographicsAsync(GetDemographicsParameters parameters)
        {
            XElement responseMessage = XElement.Parse(Resources.GetDemographicsResponse);
            QueryResponse response = _responseParser.ParseResponse(responseMessage);

            //QueryResponse response = new QueryResponse()
            //{
            //    Code = "BCHCIM.GD.0.0012 | The search completed successfully.",
            //    ResultTotalQuantity = 1,
            //    Candidates = new List<Candidate>
            //    {
            //        new Candidate { Score = 10, Patient = 
            //            new Patient { Phn="90023410534", Surname="Smith", Given=new List<string>{ "Cindy" }, Gender="F", DateOfBirth=new DateTime(1961,2,3) } }
            //    }
            //};

            return Task.FromResult(response);
        }
    }

}

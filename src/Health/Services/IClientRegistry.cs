using System;
using System.Threading.Tasks;
using Health;

namespace Health.Services
{
    public interface IClientRegistry
    {
        Task<QueryResponse> FindCandidatesAsync(FindCandidatesParameters parameters);
        Task<QueryResponse> GetDemographicsAsync(GetDemographicsParameters parameters);
    }
}

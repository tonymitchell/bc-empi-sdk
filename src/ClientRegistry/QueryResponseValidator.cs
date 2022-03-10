using Health;
using Health.Services;
using Microsoft.Extensions.Logging;

namespace Health.Services
{
    class QueryResponseValidator
    {
        ILogger<QueryResponseValidator> _logger;

        public QueryResponseValidator(ILogger<QueryResponseValidator> logger)
        {
            _logger = logger;
        }

        public void ValidateResponse(QueryResponse queryResponse)
        {
            if (queryResponse.ResultTotalQuantity != queryResponse.Candidates.Count)
                _logger.LogWarning("Warning: queryAck/resultTotalQuantity ({0}) does not match count of subject/target elements ({1})", queryResponse.ResultTotalQuantity, queryResponse.Candidates.Count);
            foreach (var cand in queryResponse.Candidates)
            {
                if (cand.Names.Count == 0)
                    _logger.LogWarning("Warning: No names were present on a patient");
                if (cand.DateOfBirth.IsNull && cand.DateOfBirth.NullFlavor != NullFlavor.Masked)
                    _logger.LogWarning("Warning: Date Of Birth was missing on a patient (and not masked)");
                foreach (var id in cand.SourceIdentifiers)
                {
                    if (string.IsNullOrEmpty(id.AssigningAuthority))
                        _logger.LogWarning("Warning: Source identifier was missing an Assigning Authority");
                    if (string.IsNullOrEmpty(id.Type))
                        _logger.LogWarning("Warning: Source identifier was missing an OID");
                }
                foreach (var id in cand.CrsIdentifiers)
                {
                    if (string.IsNullOrEmpty(id.Type))
                        _logger.LogWarning("Warning: CRS identifier was missing an OID");
                }
                if (cand.AlternateIdentifier != null && string.IsNullOrEmpty(cand.AlternateIdentifier.Type))
                    _logger.LogWarning("Warning: Alternate identifier was missing an OID");
            }
        }

    }

}

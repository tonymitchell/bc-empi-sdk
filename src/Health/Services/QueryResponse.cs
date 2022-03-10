using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.Services
{
    public class QueryResponse
    {
        public static readonly QueryResponse Empty = new QueryResponse();

        public QueryResponse()
        {
            Candidates = new List<Candidate>();
        }

        public List<Candidate> Candidates { get; set; }
        public QueryResponseCode Code { get; set; }
        public int ResultTotalQuantity { get; set; }
    }

}

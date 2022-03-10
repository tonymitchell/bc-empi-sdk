using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health;

namespace Health.Services
{
    public class GetDemographicsParameters
    {
        public string Phn { get; set; }
        public bool IncludeHistory { get; set; }


        public GetDemographicsParameters()
        {
        }

        // Copy constructor
        public GetDemographicsParameters(GetDemographicsParameters other)
        {
            Phn = other.Phn;
            IncludeHistory = other.IncludeHistory;
        }

        public void Validate()
        {
            // Must include Phn
            if (BcPhn.IsValid(Phn) == false)
                throw new ArgumentException("PHN checksum failed", "Phn");
        }
    }

}

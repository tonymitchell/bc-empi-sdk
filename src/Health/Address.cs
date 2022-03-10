using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health
{
    public class Address : HealthObject
    {
        public Address()
        {
            StreetAddressLines = new List<string>();
        }

        /// <summary>
        /// Address Use Type.  Must be eitehr PHYS (Physical) or PST (Mailing)
        /// </summary>
        public string Use { get; set; }
        public List<string> StreetAddressLines { get; set; }
        public string StreetAddressLine1 { get { return StreetAddressLines.Count > 0 ? StreetAddressLines[0] : null; } }
        public string StreetAddressLine2 { get { return StreetAddressLines.Count > 1 ? StreetAddressLines[1] : null; } }
        public string StreetAddressLine3 { get { return StreetAddressLines.Count > 2 ? StreetAddressLines[2] : null; } }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public bool IsVerified { get; set; }


    }
}

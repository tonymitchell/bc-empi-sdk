using Health;
using System;
using System.Collections.Generic;

namespace Health.Services
{
    public class FindCandidatesParameters
    {
        // Name
        public string Surname { get; set; }
        public List<string> Given { get; private set; } = new List<string>();

        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string Gender { get; set; }

        // Address
        public string StreetAddressLine1 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public string Telephone { get; set; }
        public string Email { get; set; }

        //(BC CR) supported Id search types:
        //     MRN's:>       root = 2.16.840.1.113883.3.51.1.1.6> 
        //              extension = <mrn value>> 
        // assigningAuthorityName = not required. CR will search all MRN's.
        // OtherId's: >      root = 2.16.840.1.113883.3.51.1.1.6.1 -> 2.16.840.1.113883.3.51.1.1.6.20 (See BC CR OIDs.xls listing.)
        //              extension = <other id value>
        public Identifier Id { get; set; }


        public FindCandidatesParameters()
        {
        }

        // Copy constructor
        public FindCandidatesParameters(FindCandidatesParameters other)
        {
            Surname = other.Surname;
            Given = new List<string>(other.Given);
            DateOfBirth = other.DateOfBirth;
            DateOfDeath = other.DateOfDeath;
            Gender = other.Gender;
            StreetAddressLine1 = other.StreetAddressLine1;
            City = other.City;
            Province = other.Province;
            Country = other.Country;
            PostalCode = other.PostalCode;
            Telephone = other.Telephone;
            Email = other.Email;
            Id = other.Id != null ? new Identifier(other.Id) : null;
        }


        public void Validate()
        {
            // Must include Surname...
            if (string.IsNullOrEmpty(Surname))
                throw new ArgumentException("Surname was not specified", "Surname");

            // and one of: Date of Birth, Street Address Line 1, Postal Code, Telephone
            if (DateOfBirth.HasValue == false && string.IsNullOrEmpty(StreetAddressLine1) && 
                string.IsNullOrEmpty(PostalCode) && string.IsNullOrEmpty(Telephone))
            {
                throw new ArgumentException("One of of the following was not specified: Date of Birth, Street Address Line 1, Postal Code, Telephone");
            }
        }

    }
}

using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;
using Health.Services;
using System.Collections.Generic;
using System;
using HealthRegistries;

namespace Health.Services.Hl7v3
{
    internal class Hl7v3FindCandidatesMessageBuilder
    {
        private FindCandidatesParameters _parameters = new FindCandidatesParameters();
        public string SendingOrgId { get; set; }
        public string SendingSystemId { get; set; }

        public Hl7v3FindCandidatesMessageBuilder FromParameters(FindCandidatesParameters parameters)
        {
            _parameters = new FindCandidatesParameters(parameters); // Take a copy

            return this;
        }

        #region Fluent builder methods

        public Hl7v3FindCandidatesMessageBuilder WithSender(string sendingOrgId, string sendingSystemId)
        {
            this.SendingOrgId = sendingOrgId;
            this.SendingSystemId = sendingSystemId;
            return this;
        }

        public Hl7v3FindCandidatesMessageBuilder WithNames(string surname, params string[] givenNames)
        {
            _parameters.Surname = surname;
            if (givenNames != null)
                _parameters.Given.AddRange(givenNames.ToList());

            return this;
        }
        public Hl7v3FindCandidatesMessageBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            _parameters.DateOfBirth = dateOfBirth;
            return this;
        }
        public Hl7v3FindCandidatesMessageBuilder WithDateOfDeath(DateTime dateOfDeath)
        {
            _parameters.DateOfDeath = dateOfDeath;
            return this;
        }
        public Hl7v3FindCandidatesMessageBuilder WithGender(string gender)
        {
            _parameters.Gender = gender;
            return this;
        }

        public Hl7v3FindCandidatesMessageBuilder WithStreetAddressLines(string addressLine1, params string[] additionalAddressLines)
        {
            _parameters.StreetAddressLine1 = addressLine1;
            if (additionalAddressLines.Length > 0)
                throw new NotImplementedException("No support for additional lines yet...");

            return this;
        }
        public Hl7v3FindCandidatesMessageBuilder WithCity(string city)
        {
            _parameters.City = city;
            return this;
        }
        public Hl7v3FindCandidatesMessageBuilder WithProvince(string province)
        {
            _parameters.Province = province;
            return this;
        }
        public Hl7v3FindCandidatesMessageBuilder WithCountry(string country)
        {
            _parameters.Country = country;
            return this;
        }
        public Hl7v3FindCandidatesMessageBuilder WithPostalCode(string postalCode)
        {
            _parameters.PostalCode = postalCode;
            return this;
        }
        public Hl7v3FindCandidatesMessageBuilder WithTelephone(string telephone)
        {
            _parameters.Telephone = telephone;
            return this;
        }
        public Hl7v3FindCandidatesMessageBuilder WithEmail(string email)
        {
            _parameters.Email = email;
            return this;
        }
        #endregion

        //TODO: This logic depends on parameters being populated in template.  Should clean up to not require that... (i.e. template should have empty parameter list)
        public XElement Build()
        {
            // Ensure build parameters are valid first
            _parameters.Validate();

            XNamespace hl7 = "urn:hl7-org:v3";
            XElement request = LoadTemplate();
            Hl7v3MessageUtil.InitializeRequest(ref request, this.SendingSystemId, this.SendingOrgId);

            XElement queryByParameter = request.Element(hl7 + "controlActProcess").Element(hl7 + "queryByParameter").Element(hl7 + "queryByParameterPayload");

            // Surname
            queryByParameter.Element(hl7 + "person.name").Element(hl7 + "value")
                .SetElementValue(hl7 + "family", _parameters.Surname);

            // Given name
            queryByParameter.Element(hl7 + "person.name").Element(hl7 + "value")
                .Add(from givenName in _parameters.Given
                     where string.IsNullOrEmpty(givenName) == false
                     select new XElement(hl7 + "given", givenName));

            //<person.birthTime>
            //  <value value="19370216" />
            //</person.birthTime>
            XElement birthTime = queryByParameter.Element(hl7 + "person.birthTime");
            if (_parameters.DateOfBirth.HasValue)
                birthTime.Element(hl7 + "value")
                    .SetAttributeValue("value", _parameters.DateOfBirth.Value.ToString("yyyyMMdd"));
            else
                birthTime.Remove();

            ////<person.administrativeGender>
            ////  <value code="M" />
            ////</person.administrativeGender>
            XElement administrativeGender = queryByParameter.Element(hl7 + "person.administrativeGender");
            if (!string.IsNullOrEmpty(_parameters.Gender))
                administrativeGender.Element(hl7 + "value")
                    .SetAttributeValue("code", _parameters.Gender);
            else
                administrativeGender.Remove();

            //<person.addr>
            //    <value use="PHYS PST">
            //        <streetAddressLine>123 Any St</streetAddressLine>
            //    </value>
            //</person.addr>
            XElement address = queryByParameter.Element(hl7 + "person.addr");
            if (!string.IsNullOrEmpty(_parameters.StreetAddressLine1) ||
                !string.IsNullOrEmpty(_parameters.City) ||
                !string.IsNullOrEmpty(_parameters.Province) ||
                !string.IsNullOrEmpty(_parameters.Country) ||
                !string.IsNullOrEmpty(_parameters.PostalCode)
                )
            {
                if (!string.IsNullOrEmpty(_parameters.StreetAddressLine1))
                    address.Element(hl7 + "value")
                        .SetElementValue(hl7 + "streetAddressLine", _parameters.StreetAddressLine1);
                if (!string.IsNullOrEmpty(_parameters.City))
                    address.Element(hl7 + "value")
                        .SetElementValue(hl7 + "city", _parameters.City);
                if (!string.IsNullOrEmpty(_parameters.Province))
                    address.Element(hl7 + "value")
                        .SetElementValue(hl7 + "state", _parameters.Province);
                if (!string.IsNullOrEmpty(_parameters.Country))
                    address.Element(hl7 + "value")
                        .SetElementValue(hl7 + "country", _parameters.Country);
                if (!string.IsNullOrEmpty(_parameters.PostalCode))
                    address.Element(hl7 + "value")
                        .SetElementValue(hl7 + "postalCode", _parameters.PostalCode);
            }
            else
                address.Remove();

            // Telcom
            //<person.telecom>
            //    <value />
            //</person.addr>
            XElement telecom = queryByParameter.Element(hl7 + "person.telecom");
            if (telecom != null)
            {
                //<telecom value="tel:5907844" use="H" />
                if (!string.IsNullOrEmpty(_parameters.Telephone))
                {
                    telecom.Element(hl7 + "value")
                        .SetAttributeValue("value", "tel:" + _parameters.Telephone);
                    telecom.Element(hl7 + "value")
                        .SetAttributeValue("use", "H MC WP");
                }
                else if (!string.IsNullOrEmpty(_parameters.Email))
                {
                    telecom.Element(hl7 + "value")
                        .SetAttributeValue("value", "mailto:" + _parameters.Email);
                    telecom.Element(hl7 + "value")
                        .SetAttributeValue("use", "H MC WP");
                }
                else
                    telecom.Remove();
            }
            
            // Date of Death
            // person.deceasedTime.value
            //<person.deceasedTime>
            //    <value />
            //</person.deceasedTime>
            XElement deceasedTime = queryByParameter.Element(hl7 + "person.deceasedTime");
            if (deceasedTime != null)
            {
                if (_parameters.DateOfDeath.HasValue)
                {
                    deceasedTime.Element(hl7 + "value")
                        .SetAttributeValue("value", _parameters.DateOfDeath.Value.ToString("yyyyMMdd"));
                }
                else
                    deceasedTime.Remove();
            }

            // Id
            // person.id.value
            //<person.id>
            //    <value />
            //</person.id>
            XElement id = queryByParameter.Element(hl7 + "person.id");
            if (id != null)
            {
                //<id root="2.16.840.1.113883.3.51.1.1.6.1" extension="9698756295" assigningAuthorityName="MOH_CRS" />
                if (_parameters.Id != null)
                {
                    var value = id.Element(hl7 + "value");
                    if (value != null)
                    {
                        value.SetAttributeValue("root", _parameters.Id.Type);
                        value.SetAttributeValue("extension", _parameters.Id.Value);
                    }
                }
                else
                    id.Remove();
            }


            return request;
        }

        static XElement LoadTemplate()
        {
            XElement request = XElement.Parse(Resources.FindCandidatesRequest);
            return request;
        }
    }
}

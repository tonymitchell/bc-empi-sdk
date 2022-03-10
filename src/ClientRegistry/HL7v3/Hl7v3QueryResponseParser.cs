using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using Health;
using Health.Services;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Health.Services.Exceptions;

namespace Health.Services.Hl7v3
{
    public class Hl7v3QueryResponseParser
    {
        ILogger<Hl7v3QueryResponseParser> _logger;

        public Hl7v3QueryResponseParser(ILogger<Hl7v3QueryResponseParser> logger)
        {
            _logger = logger;
        }

        //TODO: Handle errors by raising an exception
        //TODO: Parse error messages
        public QueryResponse ParseResponse(XElement response)
        {
            XNamespace hl7 = "urn:hl7-org:v3";
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(new NameTable());
            nsMgr.AddNamespace("h", "urn:hl7-org:v3");

            QueryResponse queryResponse = new QueryResponse();
            queryResponse.Code = QueryResponseCode.Parse(GetQueryResponseCode(response, nsMgr));
            queryResponse.ResultTotalQuantity = GetResultTotalQuantity(hl7, response);
            queryResponse.Candidates = new List<Candidate>();
            queryResponse.Candidates.AddRange(
                from patient in response.Descendants(hl7 + "subject").Elements(hl7 + "target")
                let identifiedPerson = patient.Element(hl7 + "identifiedPerson")
                where identifiedPerson.Descendants().Any()
                select new Candidate()
                {
                    SourceIdentifiers = GetIdentifiers(hl7, patient),
                    CrsIdentifiers = GetIdentifiers(hl7, identifiedPerson),
                    AlternateIdentifier = GetAlternateIdentifier(hl7, identifiedPerson),
                    DateOfBirth = GetBirthDate(hl7, identifiedPerson),
                    DateOfDeath = GetDeathDate(hl7, identifiedPerson),
                    DeathIndicator = GetDeathIndicator(hl7, identifiedPerson),
                    Gender = GetGender(hl7, identifiedPerson),
                    Names = GetNames(hl7, identifiedPerson),
                    Addresses = GetAddresses(hl7, patient),
                    TelecomAddresses = GetTelecomAddresses(hl7, patient),
                    MatchScore = GetCandidateMatchScore(hl7, patient)
                }
            );


            // Interaction specific handlers
            string interactionId = GetInteractionId(hl7, response);
            switch (interactionId)
            {
                case "HCIM_IN_GetDemographicsResponse":
                case "HCIM_IN_GetDemographicsResponse.History":
                    Debug.Assert(queryResponse.Candidates.Count <= 1, "GetDemographics should always return zero or one candidate.");

                    // Fix-up score since a GetDemographics response doesn't include one
                    foreach (var candidate in queryResponse.Candidates)
                    {
                        candidate.MatchScore = 100.0;   // Use 100 to indicate deterministic match
                    }
                    break;

                case "HCIM_IN_FindCandidatesResponse":
                    Debug.Assert(queryResponse.Candidates.Count <= 25, "FindCandidates should always return between 0 and 25 candidates.");
                    break;

                case "HCIM_IN_GetRelatedIdentifiersResponse":
                    break;
                default:
                    throw new ParsingException(String.Format("Unknown response interactionId '{0}'.", interactionId));
            }

            return queryResponse;
        }

        private string GetInteractionId(XNamespace hl7, XElement response)
        {
            //<!-- interaction id -->
            //<interactionId root="2.16.840.1.113883.3.51.1.1.2" extension="HCIM_IN_GetDemographicsResponse.History"/>
            return (string)response.Element(hl7 + "interactionId").Attribute("extension");
        }


        private List<Identifier> GetIdentifiers(XNamespace hl7, XElement patientOrIdentifiedPerson)
        {
            List<Identifier> ids = new List<Identifier>();

            // id elements
            ids.AddRange(
                from id in patientOrIdentifiedPerson.Elements(hl7 + "id")
                select ParseIdentifier(id)
            );

            return ids;
        }
        private Identifier GetAlternateIdentifier(XNamespace hl7, XElement identifiedPerson)
        {
            //<playedOtherIDs classCode="ROL">
            //<id root="2.16.840.1.113883.3.51.1.1.6.7" extension="96325412" assigningAuthorityName="NTPHN"></id>
            //</playedOtherIDs>
            List<Identifier> ids;

            // identifiedPerson/playedOtherIDs/id elements
            XElement playedOtherIDs = identifiedPerson.Element(hl7 + "playedOtherIDs");
            if (playedOtherIDs != null)
            {
                ids = GetIdentifiers(hl7, playedOtherIDs);
            }
            else
                ids = new List<Identifier>();

            if (ids.Count > 1)
                _logger.LogWarning("More than one alternate identifier returned from the EMPI. Storing first one only.");

            return ids.FirstOrDefault();
        }

        private string GetQueryResponseCode(XElement response, XmlNamespaceManager nsMgr)
        {
            //<queryAck>
            //  <queryResponseCode code="BCHCIM.GD.0.0012 | The search completed successfully." />
            //  <resultTotalQuantity value="4" />
            //</queryAck>  
            return (string)response.GetXPathAttributeValue("h:controlActProcess/h:queryAck/h:queryResponseCode", "code", nsMgr);
        }

        private int GetResultTotalQuantity(XNamespace hl7, XElement response)
        {
            //<queryAck>
            //  <queryResponseCode code="BCHCIM.GD.0.0012 | The search completed successfully." />
            //  <resultTotalQuantity value="4" />
            //</queryAck>  
//            return (string)response.GetXPathAttributeValue("h:controlActProcess/h:queryAck/h:resultTotalQuantity", "value", nsMgr);
            return (int)response.Element(hl7 + "controlActProcess").Element(hl7 + "queryAck").Element(hl7 + "resultTotalQuantity").Attribute("value");
        }

        private double GetCandidateMatchScore(XNamespace hl7, XElement patient)
        {
            //<subjectOf>
            //  <observationEvent>
            //    <code code="SCORE" />
            //    <value value="0" />
            //  </observationEvent>
            //</subjectOf>
            return (from so in patient.Elements(hl7 + "subjectOf")
                    let oe = so.Element(hl7 + "observationEvent")
                    where "SCORE" == (string)oe.GetChildElementAttributeValue(hl7 + "code","code")
                    select (double)oe.GetChildElementAttributeValue(hl7 + "value","value"))
                                        .FirstOrDefault() / 10;
        }

        private List<Telecom> GetTelecomAddresses(XNamespace hl7, XElement patient)
        {
            return (from telecom in patient.Elements(hl7 + "telecom")
                    select new Telecom
                    {
                        NullFlavor = GetNullFlavor(telecom),
                        Use = (string)telecom.Attribute("use"),
                        Value = (string)telecom.Attribute("value")
                    }).ToList<Telecom>();
        }

        private List<Address> GetAddresses(XNamespace hl7, XElement patient)
        {
            return (from address in patient.Elements(hl7 + "addr")
                    select GetAddress(hl7, address)).ToList<Address>();
        }
        private Address GetAddress(XNamespace hl7, XElement address)
        {
            var uses = ((string)address.Attribute("use") ?? "").Split(' ');
            return new Address
                {
                    NullFlavor = GetNullFlavor(address),
                    Use = (from u in uses where u != "VER" select u).FirstOrDefault(),
                    IsVerified = (from u in uses where u == "VER" select u).Any(),
                    StreetAddressLines = (from line in address.Elements(hl7 + "streetAddressLine")
                                            select (string)line).ToList(),
                    City = (string)address.Element(hl7 + "city"),
                    StateProvince = (string)address.Element(hl7 + "state"),
                    PostalCode = (string)address.Element(hl7 + "postalCode"),
                    Country = (string)address.Element(hl7 + "country")
                };
        }

        private List<PersonName> GetNames(XNamespace hl7, XElement identifiedPerson)
        {
            return (from name in identifiedPerson.Elements(hl7 + "name")
                    select GetName(hl7, name)).ToList<PersonName>();
        }

        private PersonName GetName(XNamespace hl7, XElement nameElement)
        {
            // Extract values
            NullFlavor nullFlavor = GetNullFlavor(nameElement);
            string nameType = (string)nameElement.Attribute("use");
            string surname = (string)nameElement.Element(hl7 + "family");
            var givenNames = (from gn in nameElement.Elements(hl7 + "given") where (string)gn.Attribute("qualifier") != "CL" select (string)gn).ToArray();
            var preferredGivenNames = (from pgn in nameElement.Elements(hl7 + "given") where (string)pgn.Attribute("qualifier") == "CL" select (string)pgn).ToArray();

            // Assert
            if (string.IsNullOrEmpty(nameType))
                new ParsingException("name/@use is missing");
            if (preferredGivenNames.Length > 1)
                _logger.LogWarning("More than one preferred given name detected");
            if (givenNames.Length > 3)
                _logger.LogWarning("More than three (non-preferred) given names detected");

            // Construct PersonName
            PersonName personName = new PersonName(nameType);
            personName.NullFlavor = nullFlavor;
            personName.Surname = surname;
            personName.FirstPreferredName = (string)preferredGivenNames.FirstOrDefault();
            personName.FirstGivenName = (string)givenNames.FirstOrDefault();
            personName.SecondGivenName = (string)givenNames.Skip(1).FirstOrDefault();
            personName.ThirdGivenName = (string)givenNames.Skip(2).FirstOrDefault();

            return personName;
        }

        private CodedValue GetGender(XNamespace hl7, XElement identifiedPerson)
        {
            return GetCodedValue(identifiedPerson.Element(hl7 + "administrativeGenderCode"));
        }

        private string GetPhn(XNamespace hl7, XElement identifiedPerson)
        {
            return (string)identifiedPerson.GetChildElementAttributeValue(hl7 + "id", "extension");
        }


        private Timestamp GetBirthDate(XNamespace hl7, XElement identifiedPerson)
        {
            return GetTimestampDate(identifiedPerson.Element(hl7 + "birthTime"));
        }
        private Timestamp GetDeathDate(XNamespace hl7, XElement identifiedPerson)
        {
            return GetTimestampDate(identifiedPerson.Element(hl7 + "deceasedTime"));
        }

        private bool? GetDeathIndicator(XNamespace hl7, XElement identifiedPerson)
        {
            //<deceasedInd value="true" />
            //<deceasedTime value="20120716" />
            //controlActProcess/subject/registrationEvent/subject1/identifiedPerson/identifiedPerson/deceasedInd/@value
            string deceasedInd = (string)identifiedPerson.GetChildElementAttributeValue(hl7 + "deceasedInd", "value");
            if (string.IsNullOrEmpty(deceasedInd))
                return null;
            else
                return "true".Equals(deceasedInd, StringComparison.OrdinalIgnoreCase);
        }



        //
        // HELPERS
        //
        private NullFlavor GetNullFlavor(XElement element)
        {
            // If the element is null or xsi:nil="true", using the default null flavor
            if (element == null || element.IsNil())
                return NullFlavor.Default;

            string nullFlavor = (string)element.Attribute("nullFlavor");
            if (string.IsNullOrEmpty(nullFlavor))
                return null;

            try
            {
                return NullFlavor.GetNullFlavor(nullFlavor);
            }
            catch (ArgumentOutOfRangeException)
            {
                // Use an exception from the ParsingException hierarchy instead
                throw new InvalidCodedValueException("nullFlavor", nullFlavor, "Invalid nullFlavor value");
            }
        }


        DateTime? TryParseDateTime(string value, string format)
        {
            DateTime dateValue;
            if (DateTime.TryParseExact(value, format, null, System.Globalization.DateTimeStyles.None, out dateValue))
                return dateValue;
            else
                return null;
        }

        private Timestamp GetTimestampDate(XElement timestampElement)
        {
            // Extract values
            NullFlavor nullFlavor = GetNullFlavor(timestampElement);
            string birthTime = timestampElement == null ? null : (string)timestampElement.Attribute("value");
            DateTime? birthTimeValue = TryParseDateTime(birthTime, "yyyyMMdd");

            // Assert

            // Construct Timestamp
            Timestamp ts = new Timestamp(birthTimeValue, nullFlavor);

            return ts;
        }

        private CodedValue GetCodedValue(XElement codedValueElement)
        {
            // Extract values
            NullFlavor nullFlavor = GetNullFlavor(codedValueElement);
            string codedValue = codedValueElement == null ? null : (string)codedValueElement.Attribute("code");

            // Assert

            // Construct Timestamp
            CodedValue cv = new CodedValue(nullFlavor, "", codedValue);

            return cv;
        }

        /// <summary>
        /// Takes an HL7v3 identifier (II) XML element and returns an object representing it
        /// </summary>
        private Identifier ParseIdentifier(XElement id)
        {
            return new Identifier()
            {
                Type = (string)id.Attribute("root"),
                Value = (string)id.Attribute("extension"),
                AssigningAuthority = (string)id.Attribute("assigningAuthorityName"),
                IsActive = (bool?)id.Attribute("displayable") ?? true
            };
        }

    }

}

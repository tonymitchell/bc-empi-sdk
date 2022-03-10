using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health
{
    public class Patient
    {
        public Patient()
        {
            SourceIdentifiers = new List<Identifier>();
            CrsIdentifiers = new List<Identifier>();
            Names = new List<PersonName>();
            Addresses = new List<Address>();
            TelecomAddresses = new List<Telecom>();
            DateOfBirth = new Timestamp();
            DateOfDeath = new Timestamp();
        }

        public List<Identifier> SourceIdentifiers { get; set; }
        public List<Identifier> CrsIdentifiers { get; set; }
        public Identifier AlternateIdentifier { get; set; }
        public string Phn 
        { 
            get 
            {
                return (from id in CrsIdentifiers 
                        where id.IsActive == true 
                        select id.Value).SingleOrDefault(); // Will throw exception is more than one...
            } 
        }
        //public string PhnId { get { return (from id in Identifiers where id.Type == IdentifierType.BcPhn select id.Value).FirstOrDefault(); } }
        //public bool HasMultipleBcPhns { get { return (from id in Identifiers where id.Type == IdentifierType.BcPhn select id).Count() > 1; } }
        
        // Names
        public List<PersonName> Names { get; set; }
        public PersonName DeclaredName { get { return (from PersonName n in Names where n.Type == "L" select n).FirstOrDefault(); } }
        public PersonName CardName { get { return (from PersonName n in Names where n.Type == "C" select n).FirstOrDefault(); } }

        public Timestamp DateOfBirth { get; set; }
        public Timestamp DateOfDeath { get; set; }
        public bool? DeathIndicator { get; set; }

        //@value	@nullFlavor	Meaning
        //M		Male
        //F		Female
        //UN		Undifferentiated
        //    UNK	Unknown
        //    MSK	Masked (in query response messages)
        public CodedValue Gender { get; set; }
        //public string Gender { get; set; }

        // Addresses
        public List<Address> Addresses { get; set; }
        public Address PhysicalAddress { get { return (from a in Addresses where a.Use == "PHYS" select a).FirstOrDefault(); } }
        public Address MailingAddress { get { return (from a in Addresses where a.Use == "PST" select a).FirstOrDefault(); } }

        // Telecom
        public List<Telecom> TelecomAddresses { get; set; }
        public string HomePhone { get { return (from t in TelecomAddresses where t.Use == "H" && t.EquipmentCode == "tel" select t.PhoneNumber).FirstOrDefault(); } }
        public string HomeEmail { get { return (from t in TelecomAddresses where t.Use == "H" && t.EquipmentCode == "mailto" select t.EmailAddress).FirstOrDefault(); } }
        public string WorkPhone { get { return (from t in TelecomAddresses where t.Use == "WP" && t.EquipmentCode == "tel" select t.PhoneNumber).FirstOrDefault(); } }
        public string WorkEmail { get { return (from t in TelecomAddresses where t.Use == "WP" && t.EquipmentCode == "mailto" select t.EmailAddress).FirstOrDefault(); } }
        public string MobilePhone { get { return (from t in TelecomAddresses where t.Use == "MC" && t.EquipmentCode == "tel" select t.PhoneNumber).FirstOrDefault(); } }
        public string MobileEmail { get { return (from t in TelecomAddresses where t.Use == "MC" && t.EquipmentCode == "mailto" select t.EmailAddress).FirstOrDefault(); } }

        public Telecom HomeTelecomAddress { get { return (from t in TelecomAddresses where t.Use == "H" select t).FirstOrDefault(); } }
        public Telecom WorkTelecomAddress { get { return (from t in TelecomAddresses where t.Use == "WP" select t).FirstOrDefault(); } }
        public Telecom MobileTelecomAddress { get { return (from t in TelecomAddresses where t.Use == "MC" select t).FirstOrDefault(); } }
    }
}

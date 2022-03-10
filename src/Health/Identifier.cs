using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health
{
    //<id root="2.16.840.1.113883.3.51.1.1.6" extension="00100" assigningAuthorityName="VIHA-CERN"/>
    //<id root="2.16.840.1.113883.3.51.1.1.6" extension="00101" assigningAuthorityName="VCHA_PG"/>
    //<id root="2.16.840.1.113883.3.51.1.1.6.1" extension="9888888888" displayable="true"/>
    //<id root="2.16.840.1.113883.3.51.1.1.6.1" extension="9777777777" displayable="false"/>
    //<id root="2.16.840.1.113883.3.51.1.1.6.2" extension="88990092" assigningAuthorityName="ABPHN"/>

    //<id root="2.16.840.1.113883.3.51.1.1.6" extension="04030568" assigningAuthorityName="VCHA_VGH" displayable="true"></id>
    //<id root="2.16.840.1.113883.3.51.1.1.6" extension="14634" assigningAuthorityName="VCHA_COMM" displayable="true"></id>
    //<id root="2.16.840.1.113883.3.51.1.1.6" extension="0719637" assigningAuthorityName="VCHA_LION" displayable="true"></id>
    //<id root="2.16.840.1.113883.3.51.1.1.6" extension="010218956" assigningAuthorityName="VCHA_PHC" displayable="true"></id>
    //<id root="2.16.840.1.113883.3.51.1.1.6" extension="100007161" assigningAuthorityName="VCHA_PHC" displayable="true"></id>

    //<id extension="9879837534" assigningAuthorityName="MOH_CRS" displayable="true" />
    //<id extension="9879837527" assigningAuthorityName="MOH_CRS" displayable="true" />
    //<id root="2.16.840.1.113883.3.51.1.1.6.1" extension="9879837534" assigningAuthorityName="MOH_CRS" />
    
    public class Identifier
    {
        // Id value (from extension attribute)
        public string Value { get; set; }

        // OID (from root attribute)
//        public IdentifierType Type { get; set; }
        public string Type { get; set; }

        // Assigning Authority Name
//        public AssigningAuthority AssigningAuthority { get; set; }
        public string AssigningAuthority { get; set; }

        public bool IsActive { get; set; }

        public Identifier()
        {
        }

        // Copy constructor
        public Identifier(Identifier other)
        {
            Value = other.Value;
            Type = other.Type;
            AssigningAuthority = other.AssigningAuthority;
            IsActive = other.IsActive;
        }
    }

    public class IdentifierType
    {
        public IdentifierType(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Code { get; set; }
        public string Name { get; set; }

        public static readonly IdentifierType Mrn = new IdentifierType("2.16.840.1.113883.3.51.1.1.6", "MRN");
        public static readonly IdentifierType PurportedBcPhn = new IdentifierType("2.16.840.1.113883.3.51.1.1.6.0", "BC PHN - Purported BC PHN");
        public static readonly IdentifierType BcPhn = new IdentifierType("2.16.840.1.113883.3.51.1.1.6.1", "BC PHN - CRS source record identifier");
    }

    public class AssigningAuthority
    {
        public AssigningAuthority(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Code { get; set; }
        public string Name { get; set; }

        public static readonly AssigningAuthority VCHA_VGH = new AssigningAuthority("VCHA_VGH", "VCHA IDX Carecast");
        public static readonly AssigningAuthority MOH_CRS = new AssigningAuthority("MOH_CRS", "BC MOH - Client Registry System");

//VCHA_VGH	VCHA IDX Carecast
//VCHA_PHC	VCHA Eclypsis
//VCHA_LION	VCHA McKesson
//VCHA_PR	VCHA Meditech
//VCHA_COMM	VCHA PARIS
    }

/*
IdentifierType
 * Id
 * Name

2.16.840.1.113883.3.51.1.1.6    MRN
2.16.840.1.113883.3.51.1.1.6.0	BC PHN - Purported BC PHN
2.16.840.1.113883.3.51.1.1.6.1	BC PHN - CRS source record identifier
2.16.840.1.113883.3.51.1.1.6.2	Alberta PHN
2.16.840.1.113883.3.51.1.1.6.3	Manitoba Health Registration
2.16.840.1.113883.3.51.1.1.6.4	New Brunswick Medicare
2.16.840.1.113883.3.51.1.1.6.5	Newfoundland Labrador Healthcare number 
2.16.840.1.113883.3.51.1.1.6.6	Nova Scotia PHN 
2.16.840.1.113883.3.51.1.1.6.7	Northwest territories PHN 
2.16.840.1.113883.3.51.1.1.6.8	Nunavut Healthcare Number 
2.16.840.1.113883.3.51.1.1.6.9	Ontario Healthcare Number 
2.16.840.1.113883.3.51.1.1.6.10	PEI Healthcare Number  
2.16.840.1.113883.3.51.1.1.6.11	Quebec Healthcare Number  
2.16.840.1.113883.3.51.1.1.6.12	Saskatchewan Health Services Number  
2.16.840.1.113883.3.51.1.1.6.13	Yukon Territory Healthcare Number  
2.16.840.1.113883.3.51.1.1.6.14	Alberta Health Unique Lifetime Identifier  PHN 
2.16.840.1.113883.3.51.1.1.6.15	Canadian Armed Forces Identification Number 
2.16.840.1.113883.3.51.1.1.6.16	Candian RCMP Regiment Number 
2.16.840.1.113883.3.51.1.1.6.18	Veteran Affairs Canadian Identification Number
*/
/*
AssigningAuthority
 * Code
 * Name

MOH_CRS BC MOH - Client Registry System
FHA_SF	FHA Meditech - Simon Fraser
FHA_FV	FHA Meditech - Fraser Valley
IHA_IHA	IHA Meditech
NHA_PGRH	NHA PGRH - Eclypsis
PHSA_CAIS	PHSA Cancer Agency CAIS
PHSA_CYT	PHSA Cytology
PHSA_MAMM	PHSA Screening Mammography
PHSA_CW	PHSA Childrens & Womens
PHSA_PHIS	PHSA IPHIS
PHSA_RIV	PHSA Riverview
VCHA_VGH	VCHA IDX Carecast
VCHA_PHC	VCHA Eclypsis
VCHA_LION	VCHA McKesson
VCHA_PR	VCHA Meditech
VCHA_COMM	VCHA PARIS
VCHA_RICH	VCHA Richmond - Eclypsis
VIHA_CENT	VIHA Meditech
VIHA_NORTH	VIHA MediSolution
VIHA_SOUTH	VIHA Cerner
VIHA_JOEADT	VIHA Triple G
VIHA_JOELAB	VIHA St. Josephs Lab
*/
}

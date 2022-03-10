using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health
{
    public class Telecom : HealthObject
    {
        /*
         * 
         H	Communication address at home.  Typically used with urgent cases or if no other contacts are available.
        WP	An office address.  First choice for business related contacts during business hours.
        MC	Mobile Contact.  Telecommunication device that moves and stays with its owner.
        
        telecom is optional and limited to three instances identified by the @use attribute (H-home, WP- orkplace, MC-mobile).  The value attribute can be divided in three parts: the first one goes from the start to the colon character and gets mapped to EMPI.Telecom equipment, if this first part is 'tel', the next three characters after the colon are mapped to EMPI.Area code and the final seven characters to EMPI.Phone number. If the first part is 'mailto', all the value after the colon character is mapped to EMPI.Email address 
Source systems do not need to implement all possible values defined as long as the source values are appropriately mapped to one of the possible values.


        */
        //Telecommunication Address Use Code
        public string Use { get; set; }
        public string Value {
            get
            {
                if (string.IsNullOrEmpty(EquipmentCode))
                    return "";
                else if (EquipmentCode == "tel")
                    return EquipmentCode + ":" + PhoneNumber;
                else if (EquipmentCode == "mailto")
                    return EquipmentCode + ":" + EmailAddress;
                else
                    throw new InvalidOperationException("Invalid telecom format. Scheme should be either tel: or mailto:.");
            }
            set
            {
                // If empty, just set all parts empty
                if (string.IsNullOrEmpty(value))
                {
                    EquipmentCode = "";
                    PhoneNumber = "";
                    EmailAddress = "";
                    return;
                }

                string[] parts = value.Split(':');
                if (parts.Length != 2)
                    throw new ArgumentException("Invalid telecom format.");
                if (parts[0] == "tel")
                {
                    EquipmentCode = "tel";
                    PhoneNumber = parts[1];
                    EmailAddress = "";
                }
                else if (parts[0] == "mailto")
                {
                    EquipmentCode = "mailto";
                    PhoneNumber = "";
                    EmailAddress = parts[1];
                }
                else
                    throw new ArgumentException("Invalid telecom format. Scheme should be either tel: or mailto:.");
            }
        }

        //Telecommunication Equipment Code
        public string EquipmentCode { get; set; }
        //Phone number
        public string PhoneNumber { get; set; }
        //Email Address
        public string EmailAddress { get; set; }

    }
}

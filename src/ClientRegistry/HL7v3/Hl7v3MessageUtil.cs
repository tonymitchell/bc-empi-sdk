using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Health.Services.Hl7v3
{
    internal class Hl7v3MessageUtil
    {
        public static string GetUserName()
        {
            string username = Environment.UserDomainName + "\\" + Environment.UserName;
            return username;
        }

        public static void InitializeRequest(ref XElement request, string sendingSystemId, string sendingOrgId)
        {
            XNamespace hl7 = "urn:hl7-org:v3";

            string hl7DtmNow = DateTime.Now.ToString("yyyyMMddhhmmss");

            // Assign instance specific values (e.g. new ID)
            request.Element(hl7 + "id").SetAttributeValue("extension", Guid.NewGuid().ToString());
            request.Element(hl7 + "creationTime").SetAttributeValue("value", hl7DtmNow);

            //<sender typeCode="SND">
            //	<device determinerCode="INSTANCE" classCode="DEV">
            //		<!-- id/@extension identifies the sending device/system inside the organization -->
            //		<!-- Mapped to EMPI.SSID -->
            //		<id root="2.16.840.1.113883.3.51.1.1.5" extension="PHSA_EDGE"/>
            //		<asAgent classCode="AGNT">
            //			<representedOrganization determinerCode="INSTANCE" classCode="ORG">
            //				<!-- id/@extension identifies the sending organization -->
            //				<!-- Mapped to EMPI.Governance Authority -->
            //				<id root="2.16.840.1.113883.3.51.1.1.3" extension="PHSA_EDGE"/>
            //			</representedOrganization>
            //		</asAgent>
            //	</device>
            //</sender>
            request.Element(hl7 + "sender").Element(hl7 + "device").Element(hl7 + "id").SetAttributeValue("extension", sendingSystemId);
            request.Element(hl7 + "sender").Element(hl7 + "device").Element(hl7 + "asAgent").Element(hl7 + "representedOrganization").Element(hl7 + "id").SetAttributeValue("extension", sendingOrgId);




            XElement controlActProcess = request.Element(hl7 + "controlActProcess");
            //<controlActProcess classCode="ACCM" moodCode="EVN">
            //    <effectiveTime value="20120221000000" />
            controlActProcess.Element(hl7 + "effectiveTime").SetAttributeValue("value", hl7DtmNow);

            //<dataEnterer typeCode="CST">
            //    <assignedPerson classCode="ENT">
            //        <id root="2.16.840.1.113883.3.51.1.1.7" extension="tony.mitchell@phsa.ca"/>
            //    </assignedPerson>
            //</dataEnterer>
            controlActProcess.Element(hl7 + "dataEnterer").Element(hl7 + "assignedPerson").Element(hl7 + "id")
                .SetAttributeValue("extension", GetUserName());
        }
    }
}

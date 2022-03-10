using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Health.Services
{
    internal static class XElementExtensions
    {
        // XPath helpers

        public static XElement GetXPathElementValue(this XElement element, string xpath, IXmlNamespaceResolver nsMgr)
        {
            XElement quantity = element.XPathSelectElement(xpath, nsMgr);
            return quantity;        
        }
        // (string)patient.Element(hl7 + "identifiedPerson").Element(hl7 + "id").Attribute("extension"),
        public static XAttribute GetXPathAttributeValue(this XElement element, string xpath, XName attributeName, IXmlNamespaceResolver nsMgr)
        {
            XAttribute attribute = null;
            XElement quantity = element.XPathSelectElement(xpath, nsMgr);
            if (quantity != null)
                attribute = quantity.Attribute(attributeName);

            return attribute;
        }


        public static XAttribute GetChildElementAttributeValue(this XElement element, XName childElementName, XName attributeName)
        {
            XAttribute attributeValue = null;
            var childElement = element.Element(childElementName);
            if (childElement != null)
                attributeValue = childElement.Attribute(attributeName);
            return attributeValue;
        }

        public static bool IsNil(this XElement element)
        {
            if (element != null)
            {
                XAttribute nil = element.Attribute(XName.Get("nil","http://www.w3.org/2001/XMLSchema-instance"));
                if (nil != null)
                    return (bool)nil;
            }

            return false;
        }
    }
}

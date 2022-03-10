using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using Health.Services;

namespace ClientRegistry.UnitTests
{
    [TestClass]
    public class XElementExtensionsTest
    {
        private XElement GetNilElement(object nilValue)
        {
            XElement element = new XElement("Foo");
            element.SetAttributeValue(XName.Get("nil", "http://www.w3.org/2001/XMLSchema-instance"), nilValue);
            return element;
        }

        [TestMethod]
        public void IsNil_NullElement()
        {
            XElement sut = null;
            Assert.IsFalse(sut.IsNil());
        }
        [TestMethod]
        public void IsNil_NilNotPreset()
        {
            XElement sut = new XElement("Foo");
            Assert.IsFalse(sut.IsNil());
        }
        [TestMethod]
        public void IsNil_NilTrue()
        {
            Assert.IsTrue(GetNilElement(true).IsNil());
        }
        [TestMethod]
        public void IsNil_NilFalse()
        {
            Assert.IsFalse(GetNilElement(false).IsNil());
        }
        [TestMethod]
        public void IsNil_NilOne()
        {
            Assert.IsTrue(GetNilElement(1).IsNil());
        }
        [TestMethod]
        public void IsNil_NilZero()
        {
            Assert.IsFalse(GetNilElement(0).IsNil());
        }
    }
}

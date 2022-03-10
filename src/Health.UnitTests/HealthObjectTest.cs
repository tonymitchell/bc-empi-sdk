using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Health.UnitTests
{
    [TestClass]
    public class HealthObjectTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            HealthObject ho = new HealthObject();

            Assert.IsFalse(ho.IsNull);
            Assert.AreEqual(null , ho.NullFlavor);
        }

        [TestMethod]
        public void ExplicitConstructor()
        {
            HealthObject ho = new HealthObject(NullFlavor.Masked);

            Assert.IsTrue(ho.IsNull);
            Assert.AreEqual(NullFlavor.Masked, ho.NullFlavor);
        }

        [TestMethod]
        public void NoNullFlavorToString()
        {
            HealthObject ho = new HealthObject(null);

            Assert.AreEqual("", ho.ToString());
        }
        [TestMethod]
        public void MaskedToString()
        {
            HealthObject ho = new HealthObject(NullFlavor.Masked);

            Assert.AreEqual("<confidential>", ho.ToString());
        }
        [TestMethod]
        public void UnknownToString()
        {
            HealthObject ho = new HealthObject(NullFlavor.Unknown);

            Assert.AreEqual("<unknown>", ho.ToString());
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.UnitTests
{
    [TestClass]
    public class NullFlavorTest
    {
        [TestMethod]
        public void DefaultIsNoInformation()
        {
            Assert.AreEqual(NullFlavor.Default, NullFlavor.NoInformation);
        }

        [TestMethod]
        public void GetNullFlavorShouldMatchBuiltIns()
        {
            Assert.AreEqual(NullFlavor.NoInformation, NullFlavor.GetNullFlavor("NI"));
            Assert.AreEqual(NullFlavor.Masked, NullFlavor.GetNullFlavor("MSK"));
        }

        [TestMethod]
        public void GetNullFlavorShouldReturnObjectWithMatchingType()
        {
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.NI, NullFlavor.GetNullFlavor("NI").Type);
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.MSK, NullFlavor.GetNullFlavor("MSK").Type);
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.INV, NullFlavor.GetNullFlavor("INV").Type);
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.OTH, NullFlavor.GetNullFlavor("OTH").Type);
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.NINF, NullFlavor.GetNullFlavor("NINF").Type);
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.PINF, NullFlavor.GetNullFlavor("PINF").Type);
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.UNC, NullFlavor.GetNullFlavor("UNC").Type);
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.DER, NullFlavor.GetNullFlavor("DER").Type);
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.UNK, NullFlavor.GetNullFlavor("UNK").Type);
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.ASKU, NullFlavor.GetNullFlavor("ASKU").Type);
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.NAV, NullFlavor.GetNullFlavor("NAV").Type);
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.QS, NullFlavor.GetNullFlavor("QS").Type);
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.NASK, NullFlavor.GetNullFlavor("NASK").Type);
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.TRC, NullFlavor.GetNullFlavor("TRC").Type);
            Assert.AreEqual(NullFlavor.NullFlavorTypeEnum.NA, NullFlavor.GetNullFlavor("NA").Type);
        }

        [TestMethod]
        public void InvalidNullFlavorThrowsException()
        {
            // Empty is invalid
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                NullFlavor.GetNullFlavor("")
            );
            // Not a valid code
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                NullFlavor.GetNullFlavor("Invalid")
            );
        }

        [TestMethod]
        public void EqualsShouldReturnTrueIfTypeIsSame()
        {
            Assert.IsTrue(NullFlavor.GetNullFlavor("NINF").Equals(NullFlavor.GetNullFlavor("NINF")));
            Assert.IsFalse(NullFlavor.GetNullFlavor("NINF").Equals(NullFlavor.GetNullFlavor("PINF")));
        }

    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Health.UnitTests
{
    [TestClass]
    public class TimestampTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            Timestamp ts = new Timestamp();

            Assert.IsTrue(ts.IsNull);
            Assert.AreEqual(NullFlavor.NoInformation, ts.NullFlavor);
            Assert.AreEqual("", ts.ToString());
        }

        [TestMethod]
        public void ConstructorDateTime()
        {
            DateTime dtm = DateTime.Now;
            Timestamp ts = new Timestamp(dtm);

            Assert.IsFalse(ts.IsNull);
            Assert.AreEqual(null, ts.NullFlavor);
            Assert.AreEqual(dtm, ts.Value);
            Assert.AreEqual(dtm.ToString("d"), ts.ToString());
        }

        [TestMethod]
        public void ConstructorNullFlavor()
        {
            Timestamp ts = new Timestamp(NullFlavor.Unknown);

            Assert.IsTrue(ts.IsNull);
            Assert.AreEqual(NullFlavor.Unknown, ts.NullFlavor);
            Assert.AreEqual("<unknown>", ts.ToString());
        }
        [TestMethod]
        public void ConstructorDateTimeAndNullFlavor()
        {
            DateTime dtm = DateTime.Now;
            Timestamp ts = new Timestamp(dtm, NullFlavor.Unknown);

            Assert.IsTrue(ts.IsNull);
            Assert.AreEqual(NullFlavor.Unknown, ts.NullFlavor);
            Assert.AreEqual("<unknown>", ts.ToString());
        }
        [TestMethod]
        public void Assignment()
        {
            DateTime dtm = DateTime.Now;

            Timestamp ts = new Timestamp();
            ts = dtm;
            Assert.IsFalse(ts.IsNull);
            Assert.AreEqual(null, ts.NullFlavor);
            Assert.AreEqual(dtm, ts.Value);
            
            ts.NullFlavor = NullFlavor.NoInformation;
            Assert.IsTrue(ts.IsNull);
            Assert.AreEqual(NullFlavor.NoInformation, ts.NullFlavor);

            ts = dtm;
            Assert.IsFalse(ts.IsNull);
            Assert.AreEqual(null, ts.NullFlavor);
            Assert.AreEqual(dtm, ts.Value);
        }
        [TestMethod]
        public void CastingOperatorTo()
        {
            DateTime dtm = DateTime.Now;

            Timestamp ts1 = new Timestamp(dtm);
            Timestamp ts2 = dtm;

            Assert.AreEqual(ts1, ts2);
        }
        [TestMethod]
        public void IEquatable_DateTime()
        {
            Timestamp ts1 = new Timestamp(new DateTime(2012, 10, 03, 10, 29, 14));
            Timestamp ts2 = new Timestamp(new DateTime(2012, 10, 03, 10, 29, 14));

            Assert.AreEqual(ts1, ts2);
            Assert.IsTrue(ts1.Equals(ts2));
        }
        [TestMethod]
        public void IEquatable_NullFlavor()
        {
            Timestamp ts1 = new Timestamp(NullFlavor.NoInformation);
            Timestamp ts2 = new Timestamp(NullFlavor.NoInformation);

            Assert.AreEqual(ts1, ts2);
        }
        [TestMethod]
        public void IEquatable_NotEqualDateTime()
        {
            Timestamp ts1 = new Timestamp(new DateTime(2012, 10, 03, 10, 29, 14));
            Timestamp ts2 = new Timestamp(new DateTime(2012, 10, 03, 10, 29, 15));

            Assert.AreNotEqual(ts1, ts2);
        }
        [TestMethod]
        public void IEquatable_NotEqualOneNullOneNot()
        {
            Timestamp ts1 = new Timestamp(new DateTime(2012, 10, 03, 10, 29, 14));
            Timestamp ts2 = new Timestamp(null);

            Assert.AreNotEqual(ts1, ts2);
        }
    }
}

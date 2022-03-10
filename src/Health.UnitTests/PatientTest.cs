using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Health.UnitTests
{
    [TestClass]
    public class PatientTest
    {
        [TestMethod]
        public void TestDefaultConstructor()
        {
            Patient pt = new Patient();

            // Collection properties should be default constructed, not null
            VerifyPublicCollectionPropertiesAreNotNull(pt);

            Assert.AreEqual(0, pt.Addresses.Count);
            Assert.IsNull(pt.AlternateIdentifier);
            Assert.IsNull(pt.CardName);
            Assert.IsNull((DateTime?)pt.DateOfBirth);
            Assert.IsNull((DateTime?)pt.DateOfDeath);
            Assert.IsNull(pt.DeathIndicator);
            Assert.IsNull(pt.DeclaredName);
            Assert.IsNull(pt.Gender);
            Assert.IsNull(pt.HomeEmail);
            Assert.IsNull(pt.HomePhone);
            Assert.AreEqual(0, pt.SourceIdentifiers.Count);
            Assert.AreEqual(0, pt.CrsIdentifiers.Count);
            Assert.IsNull(pt.MailingAddress);
            Assert.IsNull(pt.MobileEmail);
            Assert.IsNull(pt.MobilePhone);
            Assert.AreEqual(0, pt.Names.Count);
            Assert.IsNull(pt.Phn);
            Assert.IsNull(pt.PhysicalAddress);
            Assert.AreEqual(0, pt.TelecomAddresses.Count);
            Assert.IsNull(pt.WorkEmail);
            Assert.IsNull(pt.WorkPhone);

        }

        private static void VerifyPublicCollectionPropertiesAreNotNull(object obj)
        {
            // Find all public properties that represent collections of data
            Type t = obj.GetType();
            var collectionProperties =
                from props in t.GetProperties()
                where props.PropertyType.GetInterface("System.Collections.IList") != null
                select props;

            // Get the value from the properties to verify 
            // that they were not left null
            foreach (var property in collectionProperties)
            {
                object propertyValue = property.GetValue(obj, null);
                Assert.IsNotNull(propertyValue, "'{0}' is a public collection property and should never be null.", property.Name);
            }
        }
    }
}

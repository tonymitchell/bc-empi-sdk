using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Health.Services;

namespace Health.Test
{
    /// <summary>
    ///  
    /// </summary>
    [TestClass]
    public class FindCandidatesParametersTest
    {
        private FindCandidatesParameters GetFullyPopulatedObject()
        {
            return new FindCandidatesParameters
            {
                Surname = "Johnson",
                Given = { "Howard", "Reginald", "Xavier" },
                DateOfBirth = new DateTime(1950, 1, 2),
                DateOfDeath = new DateTime(2007, 8, 9),
                Gender = "F",
                StreetAddressLine1 = "590 W 8th Ave",
                City = "Vancouver",
                Province = "BC",
                PostalCode = "H0H0H0",
                Telephone = "604-555-5555",
                Email = "howiejohnson@phsa.ca"
            };
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestDefaultConstrctor()
        {
            FindCandidatesParameters actual = new FindCandidatesParameters();

            Assert.IsNotNull(actual);
            Assert.AreEqual(null, actual.Surname);
            Assert.IsNotNull(actual.Given);
            Assert.AreEqual(0, actual.Given.Count);
            Assert.AreEqual(null, actual.DateOfBirth);
            Assert.AreEqual(null, actual.DateOfDeath);
            Assert.AreEqual(null, actual.Gender);
            Assert.AreEqual(null, actual.StreetAddressLine1);
            Assert.AreEqual(null, actual.City);
            Assert.AreEqual(null, actual.Province);
            Assert.AreEqual(null, actual.PostalCode);
            Assert.AreEqual(null, actual.Telephone);
            Assert.AreEqual(null, actual.Email);
            actual.Validate();
        }


        // Must include Surname and one of: Date of Birth, Street Address Line 1, Postal Code, Telephone
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestIsValid_SurnameOnly()
        {
            var actual = new FindCandidatesParameters();
            actual.Surname = "Foo";
            actual.Validate();
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestIsValid_NullSurname()
        {
            var actual = GetFullyPopulatedObject();

            actual.Surname = null;
            actual.Validate();

        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestIsValid_EmptySurname()
        {
            var actual = GetFullyPopulatedObject();

            actual.Surname = "";
            actual.Validate();
        }
        [TestMethod]
        public void TestIsValid_FullyPopulated()
        {
            var actual = GetFullyPopulatedObject();
            actual.Validate();
        }
        [TestMethod]
        public void TestIsValid_MinimumRequired_SurnameDOB()
        {
            var actual = new FindCandidatesParameters()
            {
                Surname = "Smith",
                DateOfBirth = DateTime.Now
            };
            actual.Validate();
        }
        [TestMethod]
        public void TestIsValid_MinimumRequired_SurnameAddressLine1()
        {
            var actual = new FindCandidatesParameters()
            {
                Surname = "Smith",
                StreetAddressLine1 = "555 Oak Street"
            };
            actual.Validate();
        }
        [TestMethod]
        public void TestIsValid_MinimumRequired_SurnamePostalCode()
        {
            var actual = new FindCandidatesParameters()
            {
                Surname = "Smith",
                PostalCode = "H0H0H0"
            };
            actual.Validate();
        }
        [TestMethod]
        public void TestIsValid_MinimumRequired_SurnameTelephone()
        {
            var actual = new FindCandidatesParameters()
            {
                Surname = "Smith",
                Telephone = "604-555-1234"
            };
            actual.Validate();
        }
    }
}

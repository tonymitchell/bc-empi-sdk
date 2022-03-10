using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Health.Services;

namespace Health.UnitTests
{
    [TestClass]
    public class GetDemographicsParametersTest
    {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestConstructor()
        {
            GetDemographicsParameters actual = new GetDemographicsParameters();
            Assert.IsNull(actual.Phn);
            Assert.IsFalse(actual.IncludeHistory);
            actual.Validate();
        }
        [TestMethod]
        public void TestValidation_Valid()
        {
            GetDemographicsParameters actual = new GetDemographicsParameters();
            actual.IncludeHistory = false;
            actual.Phn = "9698645376";
            actual.Validate();
        }
        [TestMethod]
        public void TestValidation_ValidWithHistory()
        {
            GetDemographicsParameters actual = new GetDemographicsParameters();
            actual.IncludeHistory = true;
            actual.Phn = "9698645376";
            actual.Validate();
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestValidation_Empty()
        {
            GetDemographicsParameters actual = new GetDemographicsParameters();
            actual.Phn = "";
            actual.Validate();
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestValidation_TooShort()
        {
            GetDemographicsParameters actual = new GetDemographicsParameters();
            actual.Phn = "93403543";
            actual.Validate();
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestValidation_DoesntStartWith9()
        {
            GetDemographicsParameters actual = new GetDemographicsParameters();
            actual.Phn = "1234567890";
            actual.Validate();
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestValidation_InvalidChecksum()
        {
            GetDemographicsParameters actual = new GetDemographicsParameters();
            actual.Phn = "9879760593";
            actual.Validate();
        }
    }
}

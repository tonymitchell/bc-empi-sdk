using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Health;

namespace Health.UnitTests
{
    [TestClass]
    public class BcPhnTest
    {
        [TestMethod]
        public void TestBcPhnValidation_TooShort()
        {
            // Arrange
            string phn = "93403543";
            // Act
            bool actual = BcPhn.IsValid(phn);
            // Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void TestBcPhnValidation_DoesntStartWith9()
        {
            // Arrange
            string phn = "1234567890";
            // Act
            bool actual = BcPhn.IsValid(phn);
            // Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void TestBcPhnValidation_InvalidChecksum()
        {
            // Arrange
            string[] invalidPhns = 
            {
                "9879760590",
                "9879760591",
                "9879760593",
                "9879760594",
                "9879760595",
                "9879760596",
                "9879760597",
                "9879760598",
                "9879760599"
            };
            foreach (var phn in invalidPhns)
            {
                // Act
                bool actual = BcPhn.IsValid(phn);
                // Assert
                Assert.IsFalse(actual);
            }
        }
        [TestMethod]
        public void TestBcPhnValidation_Valid()
        {
            // Arrange
            string[] validPhns = 
            {
                "9698645376",
                "9879760592",
                "9123947241",
                "9879705407",
                "9890606354",
                "9879883635"
            };
            foreach (var phn in validPhns)
            {
                // Act
                bool actual = BcPhn.IsValid(phn);
                // Assert
                Assert.IsTrue(actual);
            }
        }
    }
}

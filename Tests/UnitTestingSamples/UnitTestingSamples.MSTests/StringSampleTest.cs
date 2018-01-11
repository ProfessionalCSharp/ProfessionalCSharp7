using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestingSamples.MSTests
{
    [TestClass]
    public class StringSampleTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowOnNull()
        {
            var sample = new StringSample(null);
        }

        [TestMethod]
        public void GetStringDemoBNotInA()
        {
            string expected = "b not found in a";
            var sample = new StringSample(String.Empty);
            string actual = sample.GetStringDemo("a", "b");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStringDemoRemoveBCFromABCD()
        {
            string expected = "removed bc from abcd: ad";
            var sample = new StringSample(String.Empty);
            string actual = sample.GetStringDemo("abcd", "bc");
            Assert.AreEqual(expected, actual);
        }

    }
}

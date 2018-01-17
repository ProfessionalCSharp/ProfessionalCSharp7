using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTestingSamples.xUnit.Tests
{
    public class StringSampleTest
    {
        [Fact]
        public void ConstructorShouldThrowOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => new StringSample(null));
        }

        [Fact]
        public void GetStringDemoExceptions()
        {
            var sample = new StringSample(string.Empty);
            Assert.Throws<ArgumentNullException>(() => sample.GetStringDemo(null, "a"));
            Assert.Throws<ArgumentNullException>(() => sample.GetStringDemo("a", null));
            Assert.Throws<ArgumentException>(() =>
            sample.GetStringDemo(string.Empty, "a"));
        }

        [Fact]
        public void GetStringDemoBNotInA()
        {
            string expected = "b not found in a";
            var sample = new StringSample(String.Empty);
            string actual = sample.GetStringDemo("a", "b");
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "a", "b", "b not found in a")]
        [InlineData("", "longer string", "nger", "removed nger from longer string: lo string")]
        [InlineData("init", "longer string", "string", "INIT")]
        public void GetStringDemoInlineData(string init, string a, string b, string expected)
        {
            var sample = new StringSample(init);
            string actual = sample.GetStringDemo(a, b);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GetStringSampleData))]
        public void GetStringDemoMemberData(string init, string a, string b, string expected)
        {
            var sample = new StringSample(init);
            string actual = sample.GetStringDemo(a, b);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> GetStringSampleData() =>
            new[]
            {
                new object[] { "", "a", "b", "b not found in a" },
                new object[] { "", "longer string", "nger", "removed nger from longer string: lo string" },
                new object[] { "init", "longer string", "string", "INIT" }
            };
    }
}

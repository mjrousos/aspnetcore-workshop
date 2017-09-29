using Lab5.Controllers;
using Lab5.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class ValuesControllerTests
    {
        [TestMethod]
        public void GetReturnsExpectedStrings()
        {
            var controller = new ValuesController(new TestValuesService());

            var result = controller.Get();

            Assert.AreEqual(result.Count(), TestStrings.Count());
            Assert.IsTrue(result.All(v => TestStrings.Contains(v)));
        }

        private static IEnumerable<string> TestStrings => new[] { "test string 1", "test string 2" };

        private class TestValuesService : IValuesService
        {
            public IEnumerable<string> GetValues() => TestStrings;
        }
    }
}

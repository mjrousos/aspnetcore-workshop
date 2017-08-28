using Lab5.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class RequestIdFactoryTests
    {
        [TestMethod]
        public void RequestIdIsIncreasingInt()
        {
            var factory = new RequestIdFactory();

            var first = int.Parse(factory.MakeRequestId());
            var second = int.Parse(factory.MakeRequestId());

            Assert.IsTrue(second == first + 1);
        }
    }
}

using AspNetCoreWorkshop.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class RequestCounterTest
    {
        [TestMethod]
        public void RequestIdIsIncreasingInt()
        {
            var factory = new RequestCounter();

            var first = int.Parse(factory.MakeRequestId());
            var second = int.Parse(factory.MakeRequestId());

            Assert.IsTrue(second == first + 1);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CodilityPracticeTest;

namespace UnitTestProject1
{
    [TestClass]
    public class TruckLoadUnitTest
    {
        [TestMethod]
        public void TruckLoadTest()
        {
            // calculate the entire truck load
            int expected = (6 * 25) + 50 - (2 * 25) + (10 * 20) - 50;
            String[] str = { "+6b25", "+50", "-2b25", "+10b20", "-50" };

            CodilityPracticeTest.TruckLoadClass truckLoad = new TruckLoadClass();
            int totalLoad = truckLoad.getTruckLoad(str);
            Assert.AreEqual(expected, totalLoad);
          
        }
    }
}

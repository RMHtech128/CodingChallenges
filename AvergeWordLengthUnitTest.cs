using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CodilityPracticeTest;

namespace UnitTestProject1
{
    [TestClass]
    public class AverageWordLenClassUnitTest
    {
        [TestMethod]
        public void AverageWordLen()
        {
            AverageWordLenClass avg = new AverageWordLenClass();
            float avglen = avg.AverageWordLength("A B C."); // ➞ 1.00
            Assert.AreEqual(1, avglen);

            avglen = avg.AverageWordLength("What a gorgeous day."); // ➞ 4.00
            Assert.AreEqual(4, avglen);

            avglen = avg.AverageWordLength("Dude, this is so awesome!"); // ➞ 3.80
            double avg111 = 0;
            avg111 = Math.Round(Convert.ToDouble(avglen), 2);
            Assert.AreEqual(3.80, avg111);
        }
    }
}

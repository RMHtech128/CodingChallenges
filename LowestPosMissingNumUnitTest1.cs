using CodilityPracticeTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LowestPosMissingNumUnitTest //LowestPosMissingNumUnitTestProject1
{
    [TestClass]
    public class LowestPosMissingNumUnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Return the lowest value positive number that is missing in the array.
            LowestPosMissingNumberSolution sol = new LowestPosMissingNumberSolution();
            UInt32[] A = { 0, 1, 2, 3, 0, 1, 2, 4, 3, 5, 6, 5, 8, 9, 8, 9 };
            int res = sol.lowestPosMissingNumberSolution(A);
            Assert.AreEqual(7, res);
            //Assert.AreEqual(8, res);
            //    Console.WriteLine(res);
        }
    }


}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CodilityPracticeTest;

namespace UnitTestProject1
{
    [TestClass]
    public class IsValidEmailUnitTest
    {
        [TestMethod]
        public void isValidEmailUnitTest()
        {
            // reverse the letters in any word longer than 5 letters
            String str = "This@email.com";
            IsValidEmailClass IsValidEmail1 = new IsValidEmailClass();
            bool res = IsValidEmail1.IsValidEmail(str);
            Assert.AreEqual(true, res);

            str = "Thisemail.com";
            res = IsValidEmail1.IsValidEmail(str);
            Assert.AreEqual(false, res);

            //    Console.WriteLine(res);
        }
    }
}
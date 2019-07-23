using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CodilityPracticeTest;

namespace UnitTestProject1
{
    [TestClass]
    public class ReverseTheLettersUnitTest
    {
        [TestMethod]
        public void reverseTheLetters()
        {
            // reverse the letters in any word longer than 5 letters
            String str = "This is a Test of REVERSING the LETTERS of long words.";
            CodilityPracticeTest.ReverseTheLetters ReverseTheLetters = new ReverseTheLetters();
            String reversed = ReverseTheLetters.reverseTheLetters(str);
            Assert.AreEqual("This Is A Test Of GNISREVER The SRETTEL Of Long .sdrow ", reversed);

        }
    }
}

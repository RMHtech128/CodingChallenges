using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodilityPracticeTest
{
    public class AverageWordLenClass
    {
        // round to two decimal places
        public float AverageWordLength(string str)
        {
            float avgLen = 0;

            int countWords = str.Split().Length;
            Regex rgx = new Regex("[^a-zA-Z0-9-]");
            //Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            str = rgx.Replace(str, "");
            avgLen = (float)(str.Length) / (float)(countWords);
            return (float)(Math.Round((double)avgLen, 2));
        }
    }
}

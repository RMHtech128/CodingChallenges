using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityPracticeTest
{
    /// <summary>
    /// Return the lowest value positive number that is missing in the array.
    /// </summary>
    public class ReverseTheLetters
    {
        public string reverseTheLetters(string str)
        {
            string res = "";
            string[] strArr = str.Split(' ');
            for (Int32 i = 0; i < strArr.Count(); i++)
            {
                var b = strArr.Count();

                int len = strArr[i].Length;
                if (len > 4)
                { // then reverse the letters
                    char[] charArray = strArr[i].ToCharArray();
                    Array.Reverse(charArray);
                    strArr[i] = new string(charArray);
                }
                // and for another challenge, capitalize the first letter of each word.
                char[] charArray2 = strArr[i].ToCharArray();
                strArr[i] = char.ToUpper(charArray2[0]) + strArr[i].Substring(1);

                res += strArr[i] + " ";
            }
            res.TrimEnd(' ');
            return res;
        }
    }
}

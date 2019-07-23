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
    public class LowestPosMissingNumberSolution
    {
        public int lowestPosMissingNumberSolution(UInt32[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            List<Int32> listPositiveIntsFound = new List<Int32>();
            for (Int32 i = 1; i <= 100000; i++)
            {
                bool found = false;
                for (int j = 0; j < A.Length; j++)
                {
                    if (A[j] == i)
                    {
                        found = true; // i was found in A
                        break;
                    }
                }
                if (found == false) // i was not found in A
                {
                    listPositiveIntsFound.Add(i);
                }
            }
            listPositiveIntsFound.Sort();
            return listPositiveIntsFound.First();
            // now sort them small to big and print the lowest/ reutrn the lowest
        }
    }
}

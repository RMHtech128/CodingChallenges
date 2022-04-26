using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
//using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



namespace CodilityPracticeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            ////////////////////////////////////////////////////////////////////
            //how to get sum of indices
            TwoSum Two = new TwoSum();
            List<Tuple<int, int>> listTwoIndices = Two.FindTwoSum(new List<int>() { 3, 1, 5, 7, 5, 9 }, 10);
            listTwoIndices = Two.FindTwoSum(new List<int>() { 3, 1, 5, 7, 5, 9 }, 8);
            listTwoIndices = Two.FindTwoSum(new List<int>() { 3, 1, 5, 7, 5, 9 }, 6);
            listTwoIndices = Two.FindTwoSum(new List<int>() { 3, 1, 5, 7, 5, 9 }, 1000);

            ///////////////////////////////////////////////////////////
            //// how to call c++ app
            AppInCPP mt = new AppInCPP();
            mt.ManagedMethod();
            /////////////////////////////////////////////////////////
            // how to Call unmanaged CPP library from c#
            NativeMethods.MyStruct myStruct;
            myStruct.SomeId = 23;
            myStruct.SomePrice = 30.52;
            NativeMethods.PassStructIn(ref myStruct);
            Console.WriteLine("SomeId={0}; SomePrice={1}", myStruct.SomeId, myStruct.SomePrice);

            //////////////////////////////////////////////////////////////////////////////
            //how to Return the lowest value positive number that is missing in the array.
            LowestPosMissingNumberSolution sol = new LowestPosMissingNumberSolution();
            UInt32[] A = { 0, 1, 2, 3, 0, 1, 2,4,3,5,6,5,8,9,8,9 };
            int res = sol.lowestPosMissingNumberSolution(A);
            Console.WriteLine(res);

            ////////////////////////////////////////////////////////////////////
            //how to reverse the letters in any word longer than 5 letters
            String str = "This is a Test of REVERSING the LETTERS of long words.";
            CodilityPracticeTest.ReverseTheLetters ReverseTheLetters = new ReverseTheLetters();
            String reversed = ReverseTheLetters.reverseTheLetters(str);
            Console.WriteLine(reversed);

            ////////////////////////////////////////////////////////////////////
            //how to calculate the entire truck load
            int expected = (6 * 25) + 50 - (2 * 25) + (10 * 20) - 50;
            String[] strtruckload = { "+6b25", "+50", "-2b25", "+10b20", "-50" };

            CodilityPracticeTest.TruckLoadClass truckLoad = new TruckLoadClass();
            int total = truckLoad.getTruckLoad(strtruckload);
            Console.WriteLine((expected == total) ? @"Success:" + total.ToString() : @"Failed, Expected: " + expected.ToString() + @" Got: " + total.ToString());

            ////////////////////////////////////////////////////////////////////
            //how to calculate Days Of Travel
            DaysOfTravelClass Days = new DaysOfTravelClass();
            Console.WriteLine(Days.getDaysOfTravel(new[] { -4, -1 }));
            Console.WriteLine(Days.getDaysOfTravel(new[] { 10, 10 }));

            
            ////////////////////////////////////////////////////////////////////
            //how to get avg word lengths
            AverageWordLenClass avg = new AverageWordLenClass();
            Console.WriteLine(avg.AverageWordLength("A B C.")); // ➞ 1.00
            Console.WriteLine(avg.AverageWordLength("What a gorgeous day.")); // ➞ 4.00
            Console.WriteLine(avg.AverageWordLength("Dude, this is so awesome!")); // ➞ 3.80

        }
    }   
   
}

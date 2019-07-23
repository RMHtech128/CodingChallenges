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
  // options
        // use depeendancy walker and find out where it thinks it is looking.
        // 1. access the path directly,
        // 2. Add the dll to the main project and copy to output
        // 3. add the dll as a com object
        // 4. Add a reference to the dll
        //[DllImport(@"..\..\..\..\CodilityPracticeTest\Debug\MathLibraryCpp\MathLibraryCPP.dll")]
        //[DllImport(".\\MathLibraryCPP.dll")]
        //public static extern int Subtract(int a, int b);

        static void Main(string[] args)
        {
            TwoSum Two = new TwoSum();
            List<Tuple<int, int>> listTwoIndices = Two.FindTwoSum(new List<int>() { 3, 1, 5, 7, 5, 9 }, 10);
            listTwoIndices = Two.FindTwoSum(new List<int>() { 3, 1, 5, 7, 5, 9 }, 8);
            listTwoIndices = Two.FindTwoSum(new List<int>() { 3, 1, 5, 7, 5, 9 }, 6);
            listTwoIndices = Two.FindTwoSum(new List<int>() { 3, 1, 5, 7, 5, 9 }, 1000);



            //// call c++ app
            //AppInCPP mt = new AppInCPP();
            //mt.ManagedMethod();
            // Call unmanaged CPP library from c#
            //NativeMethods.MyStruct myStruct;
            //myStruct.SomeId = 23;
            //myStruct.SomePrice = 30.52;
            //NativeMethods.PassStructIn(ref myStruct);
            //Console.WriteLine("SomeId={0}; SomePrice={1}", myStruct.SomeId, myStruct.SomePrice);


            //int subtractCPPDllVal = NativeMethods.Subtract(1,1);
            //Console.WriteLine("{1}", subtractCPPDllVal);



            // implement a redis protobuf on a windows 10





            //Return the lowest value positive number that is missing in the array.
            LowestPosMissingNumberSolution sol = new LowestPosMissingNumberSolution();
            UInt32[] A = { 0, 1, 2, 3, 0, 1, 2,4,3,5,6,5,8,9,8,9 };
            int res = sol.lowestPosMissingNumberSolution(A);
            Console.WriteLine(res);

            // reverse the letters in any word longer than 5 letters
            String str = "This is a Test of REVERSING the LETTERS of long words.";
            CodilityPracticeTest.ReverseTheLetters ReverseTheLetters = new ReverseTheLetters();
            String reversed = ReverseTheLetters.reverseTheLetters(str);
            Console.WriteLine(reversed);

            // calculate the entire truck load
            int expected = (6 * 25) + 50 - (2 * 25) + (10 * 20) - 50;
            String[] strtruckload = { "+6b25", "+50", "-2b25", "+10b20", "-50" };

            CodilityPracticeTest.TruckLoadClass truckLoad = new TruckLoadClass();
            int total = truckLoad.getTruckLoad(strtruckload);
            Console.WriteLine((expected == total) ? @"Success:" + total.ToString() : @"Failed, Expected: " + expected.ToString() + @" Got: " + total.ToString());


            DaysOfTravelClass Days = new DaysOfTravelClass();
            Console.WriteLine(Days.getDaysOfTravel(new[] { -4, -1 }));
            Console.WriteLine(Days.getDaysOfTravel(new[] { 10, 10 }));

            AverageWordLenClass avg = new AverageWordLenClass();
            Console.WriteLine(avg.AverageWordLength("A B C.")); // ➞ 1.00
            Console.WriteLine(avg.AverageWordLength("What a gorgeous day.")); // ➞ 4.00
            Console.WriteLine(avg.AverageWordLength("Dude, this is so awesome!")); // ➞ 3.80


            Entry entry = new Entry();
            entry.Enter("ABCDEF");
            entry.Enter("123456");
            string output;
            output = entry.Leave();
            output = entry.Leave();
            output = entry.Leave();






        }

       // public int Fibon(int i)
       // { }

    }   
    

 

    //public interface IBird
    //{
    //    Egg Lay();
    //}

    //public class Chicken : IBird
    //{
    //    public IBird Createchicken()
    //    {
    //        return new Chicken();
    //    }

    //    public void EggLay()
    //    {
    //        var egg = new Egg(Createchicken); 
    //        IBird newBird = egg.Hatch();
    //    }

    //    public Egg Lay()
    //    {
    //        var egg = new Egg(() => new Chicken());
    //        return egg;
    //    }
    //}

    //public class Egg
    //{
    //    private readonly Func<IBird> _createBird;

    //    public Egg(Func<IBird> createBird)
    //    {
    //        _createBird = createBird; // No "()". createBird is not called, just assigned.
    //    }

    //    public IBird Hatch()
    //    {
    //        if (Egg.Count != 0)
    //         throw new Exception("2nd hatch");
    //        return _createBird(); // Here createBird is called, therefore the "()".
    //    }
    //}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CodilityPracticeTest
{

    public static class NativeMethods
    {
        public struct MyStruct
        {
            public int SomeId;
            public double SomePrice;
        }
        [DllImportAttribute(@"C:\Users\Richard\source\repos\CodilityPracticeTest\Debug\MathLibraryCpp\MathLibraryCPP.dll")]
        //[DllImport(@"MathLibraryCPP.dll")]
        public static extern int Subtract(int a, int b);

        //[DllImport(@"YouDirStructure\YourDLLName.DLL")]
        //[DllImport(@"C:\Users\Richard\source\repos\CodilityPracticeTest\Debug\MathLibraryCpp\MathLibraryCPP.dll")]
        //public static extern void PassStructIn(ref MyStruct theStruct);
    }
}
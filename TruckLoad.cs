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
    public class TruckLoadClass
    {

        public int getTruckLoad(string [] str)
        {
            //String[] str = { "+6b25", "+50", "-2b25", "+10b20", "-50" };

            int numBox = 0;
            int avgweight = 0;
            int grandTotal = 0;

            for (int j = 0; j < str.Length; j++)
            {
                numBox = 0;
                avgweight = 0;

                char[] separator = new char[1];
                separator[0] = 'b';
                string[] strboxes = str[j].Split(separator, 2);
                if (strboxes.Count() == 2)
                {
                    numBox = Convert.ToInt16(strboxes[0]);
                    avgweight = Convert.ToInt16(strboxes[1]);
                }
                else
                {
                    numBox = 1;
                    avgweight = Convert.ToInt16(strboxes[0]);
                }

                grandTotal += numBox * avgweight;
            }


            return grandTotal;
        }
    }
}

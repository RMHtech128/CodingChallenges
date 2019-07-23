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
    public class DaysOfTravelClass
    {

        public int getDaysOfTravel(int [] coordinates)
        {
            //int grandTotal = 0;
            int x = Math.Abs(coordinates[0]);
            int y = Math.Abs(coordinates[1]);
            int days = x + y;
            int restDays = Math.Abs(days/5);
            int totalDays = days + restDays;
            if (days % 5 == 0)
                totalDays -= 1;
            return totalDays;
        }
    }
}

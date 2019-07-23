using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CodilityPracticeTest;

namespace UnitTestProject1
{
    [TestClass]
    public class DaysOfTravelUnitTest
    {
        [TestMethod]
        public void DaysOFTravelTest()
        {
            /*
            Captain Hook and his crew are currently resting at Origin Shore. They are about to embark on their next adventure to an undisclosed location (x, y) to find treasure.

Captain Hook's ship can only move exactly north, south, east or west. It takes exactly 1 day for the ship to travel 1 unit in one of the four cardinal directions.

After every 5 days, the crew will take one day of rest.

Given the location of the treasure, find out how long it takes for Captain Hook and his crew to find the treasure. The ship is currently at coordinate (0, 0).

NumberOfDays([3, 5]) => 9 days
// Since: 3 days east + 2 days north (5 days passed) + 1 day of rest + 3 days north

NumberOfDays([-4, -1]) => 5 days
// Since 4 days west + 1 day south
Examples
NumberOfDays([10, 10]) ➞ 23

NumberOfDays([3, 3]) ➞ 7

NumberOfDays([-10, -9]) ➞ 22

NumberOfDays([-1, -2]) ➞ 3
             */


            // calculate the entire truck load
            int expected = 0;
            int total = 0;
            int[] coordinates = { 0, 0 };

            CodilityPracticeTest.DaysOfTravelClass DaysTraveled = new DaysOfTravelClass();

            // test 1
            coordinates = new int[] { 3, 5 };
            total = DaysTraveled.getDaysOfTravel(coordinates);
            expected = 9;
            Assert.AreEqual(expected, total);

            // test 2
            coordinates = new int[] { -4, -1 };
            total = DaysTraveled.getDaysOfTravel(coordinates);
            expected = 5;
            Assert.AreEqual(expected, total);
            // test 3
            coordinates = new int[] { 10, 10 };
            total = DaysTraveled.getDaysOfTravel(coordinates);
            expected = 23;
            Assert.AreEqual(expected, total);
            AverageWordLenClass AverageWordLen = new AverageWordLenClass();
            float  avgLen = AverageWordLen.AverageWordLength("A B C."); // ➞ 1.00

            avgLen = AverageWordLen.AverageWordLength("What a gorgeous day."); // ➞ 4.00

            avgLen = AverageWordLen.AverageWordLength("Dude, this is so awesome!");// ➞ 3.80
        }


    }
}

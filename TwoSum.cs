using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityPracticeTest
{
    /// <summary>
    /// Write a function that, when passed a list and a target sum, 
    /// returns, efficiently with respect to time used, two distinct 
    /// zero-based indices of any two of the numbers, whose sum is 
    /// equal to the target sum. If there are no two numbers, the 
    /// function should return null.
    /// 
    ///     For example, FindTwoSum(new List<int>() { 3, 1, 5, 7, 5, 9 }, 10) 
    ///     should return a Tuple<int, int> containing any of the following 
    ///     pairs of indices:
    /// 
    /// 0 and 3 (or 3 and 0) as 3 + 7 = 10
    /// 1 and 5 (or 5 and 1) as 1 + 9 = 10
    /// 2 and 4 (or 4 and 2) as 5 + 5 = 10
    /// </summary>
    public class TwoSum
    {
        
        //    FindTwoSum(new List<int>() { 3, 1, 5, 7, 5, 9 }, 10);
        
        public List<Tuple<int, int>> FindTwoSum(List<int> listInts, int Sum) 
        {
            if (listInts == null)
                return null;
            List<Tuple<int, int>> listTupleResults = new List<Tuple<int, int>>();
            for (int i = 0; i < listInts.Count; i++)
                for (int j = i; j < listInts.Count; j++)
                {
                    if (i != j)
                    {
                        if (listInts[i] + listInts[j] == Sum)
                        {
                            listTupleResults.Add(new Tuple<int,int>(i, j));
                        }
                    }
                }
            if (listTupleResults.Count == 0)
                return null;
            else
                return listTupleResults; 
        }
    }

}

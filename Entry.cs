using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityPracticeTest
{

    public class Entry
    {
        public Queue qt = new Queue();


        public void Enter(string passportNumber)
        {
            qt.Enqueue(passportNumber);
        }

        public string Leave()
        {
            string str = null; 
            if (qt.Count != 0)
                str = (string)qt.Dequeue();
            return str;
        }

        //public static void Main(string[] args)
        //{
        //    Entry entry = new Entry();
        //    entry.Enter("AB54321");
        //    entry.Enter("UK32032");
        //    Console.WriteLine(entry.Leave());
        //    Console.WriteLine(entry.Leave());
        //}
    }
}

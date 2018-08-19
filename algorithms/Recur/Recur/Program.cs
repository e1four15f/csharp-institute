using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recur
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter n: ");
            int n = Convert.ToInt32(Console.ReadLine());

            Stopwatch timer = new Stopwatch();
            timer.Start();
            Console.WriteLine(RSum(n));
            timer.Stop();
            Console.WriteLine("Recursion formula: " + timer.ElapsedTicks / 10);

            timer.Restart();
            Console.WriteLine(ISum(n));
            timer.Stop();
            Console.WriteLine("Iteration formula: " + timer.ElapsedTicks / 10);

            Console.Read();
        }

        public static int RSum(int n)
        {
            return n == 0 ? 0 : n + RSum(n - 1); 
        }

        public static int ISum(int n)
        {
            int sum = 0;

            for (int i = 0; i <= n; i++)
            {
                sum += i;
            }

            return sum;
        }

    }
}

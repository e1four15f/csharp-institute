using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5v2
{
    class Program
    {
        [DllImport("..\\..\\MatrixCPP\\Debug\\MatrixCPP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double GetTimeCPP(int n, int k);

        [DllImport("..\\..\\MatrixCPP\\Debug\\MatrixCPP.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SolveCPP(double[] b, double[] c, double[] a, int n, double[] f, double[] x);

        static void Main(string[] args)
        {
            Check();

            Console.Write("\nEnter TimesList file name: ");
            string filename = Console.ReadLine();
            TimesList list = new TimesList(filename);

            int n, k;
            int[] input;

            Console.WriteLine("\nAdd time? [y]");
            while (Console.ReadKey().KeyChar == 'y') 
            {
                Console.WriteLine("\nEnter n and k (' ' or ';'): ");
                try 
                { 
                    input = Console.ReadLine().Split(new char[] { ' ', ';' }).Select(int.Parse).ToArray();
                    n = input[0];
                    k = input[1];
                    list.Add(new TimeItem(n, k, GetTime(n, k), GetTimeCPP(n, k)));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("\nAdd another time? [y]");  
            }
                /*
            for (int i = 1; i < 20; i++)
            {
                try
                {
                    n = (int)Math.Pow(2, i);
                    k = 100;
                    list.Add(new TimeItem(n, k, GetTime(n, k), GetTimeCPP(n, k)));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }
            */

            Console.WriteLine(list);
            list.Save(filename);
            Console.Read();
        }

        public static double GetTime(int n, int k)
        {
            Matrix m = new Matrix(n);
            double[] f = new double[n];
            for (int i = 0; i < n; i++)
            {
                f[i] = (i + 1) * 10;
            }

            Stopwatch timer = Stopwatch.StartNew();
            for (int i = 0; i < k; i++)
            {
                m.Solve(f);
            }
            timer.Stop();

            return (double) timer.ElapsedTicks;
        }

        public static void Check()
        {
            double[] b = new double[] { 1, 5, 2, 8, 9 };
            double[] c = new double[] { 9, 2, 6, 2, 5, 8 };
            double[] a = new double[] { 8, 3, 4, 1, 5 };
            double[] f = new double[] { 3, 4, 2, 8, 1, 5 };

            Matrix m = new Matrix(b, c, a);
            Console.WriteLine(m);

            for (int i = 0; i < f.Length; i++)
            {
                Console.Write(string.Format("{0,5}", f[i]));
            } 
            Console.WriteLine("\n");

            double[] x = m.Solve(f);
            Console.WriteLine("\n\t-MatrixCS-");
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine("x" + i + " = " + x[i]);
            }

            Console.WriteLine("\n\t-MatrixCPP-");
            x = new double[6];
            SolveCPP(b, c, a, 6, f, x);

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("x" + i + " = " + x[i]);
            }
        }
    }
}

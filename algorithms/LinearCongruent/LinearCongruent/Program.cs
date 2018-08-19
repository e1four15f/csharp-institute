using System;

namespace LinearCongruent
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomNumber(101, 432, 0, (int)Math.Pow(2,10));
            Console.Read();
        }

        public static int[] RandomNumber(int b, int x0, int k, int m)
        {
            int[] x = new int[11];
            x[0] = x0;
            double[] y = new double[10];
            for (int i = 1; i < x.Length - 1; i++)
            {
                x[i] = (b * x[i - 1] + k) % m;
                y[i] = (double) x[i] / m;
                Console.WriteLine("x" + i + " = " + x[i] + "\ty" + i + " = " + y[i] + "\n");
            }
            return x;
        }
    }
}

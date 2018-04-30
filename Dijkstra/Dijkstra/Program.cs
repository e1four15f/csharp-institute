using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    class Program
    {
        public static int[,] m = new int[,]
        {{ 0, 0, 0, 0, 0, 0, 1, 6, 1 },
         { 2, 0, 5, 0, 0, 0, 0, 3, 0 },
         { 0, 0, 0, 3, 0, 0, 0, 0, 0 },
         { 0, 0, 0, 0, 1, 0, 0, 0, 6 },
         { 0, 2, 0, 0, 0, 9, 6, 0, 0 },
         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
         { 0, 0, 0, 0, 0, 0, 0, 0, 1 },
         { 0, 0, 0, 0, 1, 0, 0, 0, 0 },
         { 0, 0, 0, 0, 0, 0, 0, 2, 0 }};

        public static int A = 2, B = 5;
        
        /*
        public static int[,] m = new int[,]
        {{ 0, 2, 0, 4, 0, 0, 6, 0 },
         { 2, 0, 1, 1, 2, 0, 0, 8 },
         { 0, 1, 0, 0, 7, 0, 0, 0 },
         { 4, 1, 0, 0, 0, 5, 1, 0 },
         { 0, 2, 7, 0, 0, 0, 2, 2 },
         { 0, 0, 0, 5, 0, 0, 1, 0 },
         { 6, 0, 0, 1, 2, 1, 0, 2 },
         { 0, 8, 0, 0, 2, 0, 2, 0 }};

        public static int A = 1, B = 8;
        */

        public static List<int> vv = new List<int>();
        public static int[,] t = new int[m.GetLength(0), m.GetLength(0)];
        public static string[] vertex = new string[m.GetLength(0)];
        public static int[] way = new int[m.GetLength(0)];
        public static int inf = Int32.MaxValue / 2;

        static void Main(string[] args)
        {
            int step = FillTable();
            if (step == -1)
            {
                Console.WriteLine("There is no way");
            }
            else
            { 
                Console.WriteLine("Way: " + t[step, B - 1]);
                Console.Write("Way: v" + B + " ");

                step = B;
                while (step != A)
                { 
                    step = FindWay(step);
                }
            }
            Console.Read();
        }

        public static int FindWay(int v)
        {
            int min = inf;
            int index = inf;

            for (int i = vertex.Length - 1; i >= 0; i--)
            {
                if (min >= t[i, v - 1] && t[i, v - 1] != 0)
                {
                    min = t[i, v - 1];
                    index = i;
                }
            }

            int next = Int32.Parse(vertex[index][1].ToString());
            Console.Write("v" + next + " ");
            return next;
        }

        public static int FillTable()
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                vv.Add(i);
            }

            int current = A - 1;
            int min = 0;
            int index = inf;

            for (int step = 0; step < m.GetLength(0) - 1; step++)
            {
                vv.Remove(current);

                way[step] = min;
                min = index = inf;
                vertex[step] = "v" + (current + 1).ToString();
                foreach (int i in vv)
                {
                    t[step, i] = (m[current, i] == 0) ? inf : m[current, i] + way[step];
                    t[step, i] = (step != 0 && t[step - 1, i] < t[step, i])
                        ? t[step - 1, i] : t[step, i];

                    if (min > t[step, i])
                    {
                        min = t[step, i];
                        index = i;
                    }
                }
                current = index;
                PrintTable();

                if (current == B - 1)
                {
                    return step;
                }
            }
            return -1;
        }

        public static void PrintTable()
        {
            Console.Write(String.Format("\n{0,-4}{1,-4}", "Ver", "Way"));
            for (int i = 1; i < m.GetLength(0) + 1; i++)
            {
                if (i != A) { 
                   Console.Write(String.Format("{0,-4}", "v" + i));
                }
            }
            Console.WriteLine();
            
            for (int i = 0; i < t.GetLength(0); i++)
            {
                if (i != 0) { 
                    SetColor(way[i]);
                }
                Console.Write(String.Format("{0,-4}{1,-4}", vertex[i], way[i]));
                Console.ForegroundColor = ConsoleColor.Gray;
                for (int j = 0; j < t.GetLength(1); j++)
                {
                    if (j != A - 1)
                    {
                        if (t[i, j] == inf)
                        {
                            Console.Write(String.Format("{0,-4}", "inf"));
                        }
                        else
                        {
                            SetColor(t[i, j]);
                            Console.Write(String.Format("{0,-4}", t[i, j]));
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        public static void SetColor(int num)
        {
            switch (num)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
            }
        }
    }
}

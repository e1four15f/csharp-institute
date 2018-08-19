using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salesman
{
    class Program
    {
        public static int inf = Int32.MaxValue / 2;
        public static List<Matrix> list = new List<Matrix>();
        /*
        public static int[,] matrix = new int[,]
            {{ inf, 68,  73,  24,  70,  9 },
             { 58,  inf, 16,  44,  11,  92 },
             { 63,  9,   inf, 86,  13,  18 },
             { 17,  34,  76,  inf, 52,  70 },
             { 60,  18,  3,   45,  inf, 58 },
             { 16,  82,  11,  60,  48,  inf }};
        */
        /*
        public static int[,] matrix = new int[,]
            {{ inf, 76,  12,  72,  96,  23 },
             { 37,  inf, 33,  33,  59,  5 },
             { 11,  98,   inf, 32,  22,  26 },
             { 27,  82,  30,  inf, 29,  94 },
             { 69,  99,  91,   7,  inf, 33 },
             { 26,  13,  68,  44,  12,  inf }};
        */
        
        public static int[,] matrix = new int[,]
            {{ inf, 12,  41,  26,  1,   12,   41,  26,  12 },
             { 52,  inf, 11,  55,  6,   45,   88,  15,  15 },
             { 22,  13,  inf, 12,  13,  8,    14,  12,  21 },
             { 11,  78,  18,  inf, 17,  9,    4,   96,  24 },
             { 7,   78,  18,  17,  inf, 12,   24,  8,   8 },
             { 8,   83,  8,   22,  17,  inf,  19,  6,   9 },
             { 9,   78,  18,  9,   23,  45,   inf, 26,  22 },
             { 8,   86,  18,  1,   17,  8,    8,   inf, 1 },
             { 56,  15,  32,  24,  23,  7,    15,  26,  inf }};
       
        static void Main(string[] args)
        {
            Matrix m = new Matrix(matrix);
            Matrix[] res;

            while(true)
            {
                Console.WriteLine("\t-Step-");

                res = m.Solve();
                list.Add(res[0]);
                list.Add(res[1]);

                m = new Matrix(res[0]);

                foreach (Matrix mat in list)
                {
                    Console.WriteLine("NEXT (" + (mat.indexI + 1) + "," + (mat.indexJ + 1) + ")" + ((mat.negative) ? "" : "*") + " : " + mat.H);
                    if (m.H > mat.H)
                    {
                        m = new Matrix(mat);
                    }
                }

                Remove(m.indexI, m.indexJ, m.negative);

                Console.WriteLine("Selected (" + (m.indexI + 1) + "," + (m.indexJ + 1) + ")" + ((m.negative) ? "" : "*") + " : " + m.H + "\n");

                if (m.vi.Count == 2)
                {
                    m.vvi.Add(m.vi[0]);
                    m.vvi.Add(m.vi[1]);
                    m.vvj.Add(m.vj[1]);
                    m.vvj.Add(m.vj[0]);
                    break;
                }    
            }
            Console.Write("Way : ");
            int way = 0;
            for (int x = 0; x < m.n; x++)
            {
                Console.Write("(" + (m.vvi[x] + 1) + "," + (m.vvj[x] + 1) + ") ");
                way += matrix[m.vvi[x], m.vvj[x]];
            }
            Console.WriteLine("\nWay = " + way);
            Console.Read();
        }

        public static void Remove(int i, int j, bool negative)
        {
            foreach (Matrix m in list)
            {
                if (m.indexI == i && m.indexJ == j && m.negative == negative)
                {
                    list.Remove(m);
                    break;
                }
            }
        }
    }
}

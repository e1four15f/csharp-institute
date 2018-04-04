using System;
using System.Diagnostics;
using System.Linq;

namespace Hash
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.Write("Enter array lenght: ");
            int lenght = Convert.ToInt32(Console.ReadLine());
            int[] array = new int[lenght];

            Console.Write("Unsorted random array:\n");
            RandomArray(array);
            PrintArray(array);

            Console.Write("\n\nHash table:\n");
            int[,] hk = HashTable(array);

            Console.Write("Enter element: ");
            int elem = Convert.ToInt32(Console.ReadLine());

            Stopwatch timer = new Stopwatch();
            timer.Start();
            HashSearch(array, hk, elem);
            timer.Stop();
            Console.WriteLine(HashSearch(array, hk, elem));
            Console.Write("\nSearch time: " + timer.ElapsedTicks / 10);
                
            Console.ReadLine();
        }

        public static bool HashSearch(int[] array, int[,] hk, int k)
        {
            int kn = k % (array.Max() / 2);
            for (int i = 0; i < hk.GetLength(0); i++)
            {
                if (kn == i)
                {
                    for (int j = 0; j < hk.GetLength(1); j++)
                    {
                        if (k == hk[i, j])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static int[,] HashTable(int[] array)
        {
            int[] h = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                h[i] = array[i] % (array.Max() / 2);
            }

            int[,] hk = new int[array.Max(), h.Length];
            int index = 0;
            for (int i = 0; i < h.Max() + 1; i++)
            {
                Console.Write("{0, 5}", i + ": ");
                for (int j = 0; j < array.Length; j++)
                {
                    if (h[j] == i)
                    {
                        hk[i, index] = array[j];
                        index++;
                        Console.Write("{0, 5}", array[j] + " ");
                    }
                }
                index = 0;
                Console.Write("\n");
            }
            return hk;
        }

        public static void RandomArray(int[] array)
        {
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 100);
            }
        }

        public static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write("{0, 5}", array[i] + " ");
                if ((i + 1) % 10 == 0)
                {
                    Console.Write("\n");
                }
            }
        }
    }
}

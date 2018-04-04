using System;
using System.Diagnostics;

namespace QuickSort
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

            Stopwatch timer = new Stopwatch();

            Console.Write("\nSorted random array:\n");
            timer.Start();
            Quicksort(array, 0, array.Length - 1);
            timer.Stop();
            PrintArray(array);

            Console.Write("\nArray sorting time: " + timer.ElapsedTicks / 10);

            Console.ReadLine();
        }

        public static void RandomArray(int[] array)
        {
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 10000);
            }
        }

        public static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write("{0, 5}", array[i] + " ");
                if ((i + 1)%10 == 0)
                {
                    Console.Write("\n");
                }
            }
        }

        public static void Quicksort(int[] array, int left, int right)
        {
            int i = left, j = right;
            int pivot = array[(left + right) / 2];
 
            while (i <= j)
            {
                while (array[i] < pivot)
                {
                    i++;
                }

                while (array[j] > pivot)
                {
                    j--;
                }
 
                if (i <= j)
                {
                    int tmp = array[i];
                    array[i] = array[j];
                    array[j] = tmp;
 
                    i++;
                    j--;
                }
            }
 
            if (left < j)
            {
                Quicksort(array, left, j);
            }
 
            if (i < right)
            {
                Quicksort(array, i, right);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wave
{
    class Program
    {
        
        public static string[,] maze = new string[,]
        {{ "A", "X", "0", "0", "0", "0", "0", "0" }, 
         { "0", "X", "0", "0", "0", "0", "X", "X" }, 
         { "0", "X", "0", "X", "0", "0", "X", "0" },
         { "0", "0", "0", "X", "X", "X", "X", "0" },
         { "0", "0", "0", "0", "0", "X", "B", "0" },
         { "0", "0", "0", "X", "0", "X", "0", "0" },
         { "0", "X", "0", "0", "0", "0", "X", "0" },
         { "0", "X", "0", "0", "0", "0", "0", "0" }};
        
        /*
        public static string[,] maze = new string[,]
        {{ "A", "0", "0", "0", "0", "0", "0", "0" }, 
         { "X", "X", "X", "X", "X", "X", "X", "0" }, 
         { "0", "0", "0", "0", "0", "0", "0", "0" },
         { "0", "X", "X", "X", "X", "X", "X", "X" },
         { "0", "0", "0", "0", "0", "0", "0", "0" },
         { "X", "X", "X", "X", "X", "X", "X", "0" },
         { "0", "B", "X", "0", "0", "0", "X", "0" },
         { "0", "X", "X", "0", "X", "0", "X", "0" },
         { "0", "X", "X", "0", "X", "0", "X", "0" },
         { "0", "X", "X", "0", "X", "0", "X", "0" },
         { "0", "X", "X", "0", "X", "0", "X", "0" },
         { "0", "X", "X", "0", "X", "0", "X", "0" },
         { "0", "X", "X", "0", "X", "0", "X", "0" },
         { "0", "X", "X", "0", "X", "0", "X", "0" },
         { "0", "X", "X", "0", "X", "0", "X", "0" },
         { "0", "0", "0", "0", "X", "0", "0", "0" }};
        */
        public static int[] A = new int[2];
        public static int[] B = new int[2];

        static void Main(string[] args)
        {
            PrintMaze();
            FindAB();
            StepBack(B[0], B[1], FindSolution());
            Console.WriteLine();
            PrintMaze();
            Console.Read();
        }

        public static bool StepBack(int i, int j, int current)
        {
            string next = (current - 1).ToString();
            
            if (i < maze.GetLength(0) - 1 && maze[i + 1, j] == next)
            {
                maze[i + 1, j] = "+";
                StepBack(i + 1, j, Int32.Parse(next));
            }
            else if (i > 0 && maze[i - 1, j] == next)
            {
                maze[i - 1, j] = "+";
                StepBack(i - 1, j, Int32.Parse(next));
            }
            else if (j < maze.GetLength(1) - 1 && maze[i, j + 1] == next)
            {
                maze[i, j + 1] = "+";
                StepBack(i, j + 1, Int32.Parse(next));
            }
            else if (j > 0 && maze[i, j - 1] == next)
            {
                maze[i, j - 1] = "+";
                StepBack(i, j - 1, Int32.Parse(next));
            }

            if (next == "1")
            {
                return true;
            }

            return false;
        }

        public static int FindSolution()
        {
            int step = 1;
            bool end = false; 
            
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n1)");
            Step(A[0], A[1], 0);
            PrintMaze();

            while (!end)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                end = FindCells(step++);
                Console.WriteLine("\n" + step + ")");
                PrintMaze();
            }

            return step;
        }

        public static bool FindCells(int step)
        {
            bool end = false;
            int n = 0;
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j] == step.ToString())
                    {
                        end = Step(i, j, step) ? true : end;
                        n++;
                    }
                }
            }
            return n == 0 ? true : end;
        }

        public static bool Step(int i, int j, int step) 
        {
            if (i < maze.GetLength(0) - 1 && maze[i + 1, j] == "0")
            { 
                maze[i + 1, j] = (step + 1).ToString();
            }
            if (i > 0 && maze[i - 1, j] == "0")
            {
                maze[i - 1, j] = (step + 1).ToString();
            }
            if (j < maze.GetLength(1) - 1 && maze[i, j + 1] == "0")
            {
                maze[i, j + 1] = (step + 1).ToString();
            }
            if (j > 0 && maze[i, j - 1] == "0")
            {
                maze[i, j - 1] = (step + 1).ToString();
            }

            if (i < maze.GetLength(0) - 1 && maze[i + 1, j] == "B"
                || i > 0 && maze[i - 1, j] == "B"
                || j < maze.GetLength(1) - 1 && maze[i, j + 1] == "B"
                || j > 0 && maze[i, j - 1] == "B")
            {
                return true;
            }

            return false;
        }

        public static void FindAB()
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j] == "A")
                    {
                        A[0] = i;
                        A[1] = j;
                    }
                    if (maze[i, j] == "B")
                    {
                        B[0] = i;
                        B[1] = j;
                    }
                }
            }
        }

        public static void PrintMaze()
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    SetColor(maze[i, j]);
                    Console.Write(String.Format("{0,-4}", maze[i, j]));   
                }
                Console.WriteLine();
            }
        }

        public static void SetColor(string num)
        {
            switch (num)
            {
                case "A":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "B":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "X":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "0":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "+":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
        }
    }
}

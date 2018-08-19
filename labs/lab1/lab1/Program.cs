using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    enum Enducation { Specialist, Bachelor, SecondEnducation };

    class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student();
            Console.WriteLine(student.ToShortString());
            Console.WriteLine("\nSpecialist: " + student[Enducation.Specialist]
                + "\nBachelor: " + student[Enducation.Bachelor]
                + "\nSecond Enducation: " + student[Enducation.SecondEnducation] + "\n");

            student.Person = new Person("Hideo", "Kojima", new DateTime(1963, 8, 24));
            student.Enducation = Enducation.SecondEnducation;
            student.GroupNumber = 55;
            student.AddExams(new Exam("Math", 4, new DateTime(1950, 4, 5)),
                new Exam("Design", 5, new DateTime(1955, 1, 1)));
            Console.WriteLine(student);

            Stopwatch timer = new Stopwatch();
            Console.WriteLine("Введите количество строк и столбцов массивов\nДва числа разделенные ' ' или ';'");
            int[] input = Console.ReadLine().Split(new char[] { ' ', ';' }).Select(int.Parse).ToArray();
            int nrow = input[0];
            int ncolumn = input[1];

            Exam[] first = new Exam[nrow * ncolumn];
            Exam[,] second = new Exam[nrow, ncolumn];
            Exam[][] third = new Exam[nrow * ncolumn][];

            for (int i = 0; i < nrow * ncolumn; i++)
            {
                first[i] = new Exam();
            }
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncolumn; j++)
                {
                    second[i, j] = new Exam();
                }
            }

            for (int i = 0, sum = nrow * ncolumn, size = 1; ; i++)
            {
                if (i < sum)
                {
                    third[i] = new Exam[size];
                    for (int j = 0; j < size; j++)
                    {
                        third[i][j] = new Exam();
                    }
                    sum -= size;
                    size++;
                }
                else
                {
                    third[i] = new Exam[sum];
                    for (int j = 0; j < sum; j++)
                    {
                        third[i][j] = new Exam();
                    }
                    break;
                }
            }

            Console.WriteLine("\nВремя выполнения для " + nrow + "x" 
                + ncolumn + " массива");

            timer.Start();
            for (int i = 0; i < nrow * ncolumn; i++)
            {
                first[i].name = "Physics";
                first[i].mark = 5;
                first[i].date = new DateTime(2017, 5, 5);
            }
            timer.Stop();
            Console.WriteLine("Одномерный массив: " + timer.ElapsedTicks);

            timer.Restart();
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncolumn; j++)
                {
                    second[i, j].name = "Physics";
                    second[i, j].mark = 5;
                    second[i, j].date = new DateTime(2017, 5, 5);
                }
            }
            Console.WriteLine("Двумерный массив: " + timer.ElapsedTicks);

            timer.Restart();
            for (int i = 0; i < nrow * ncolumn; i++)
            {
                if (third[i] != null)
                {
                    for (int j = 0; j < third[i].Length; j++)
                    {
                        third[i][j].name = "Physics";
                        third[i][j].mark = 5;
                        third[i][j].date = new DateTime(2017, 5, 5);
                    }
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("Ступенчатый массив: " + timer.ElapsedTicks);
            timer.Stop();
            
            Console.ReadKey();
        }
    }
}

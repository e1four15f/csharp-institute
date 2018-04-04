using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    enum Enducation { Specialist, Bachelor, SecondEnducation };

    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person("Todd", "Howard", new DateTime(1971, 4, 25));
            Person p2 = new Person("Todd", "Howard", new DateTime(1971, 4, 25));
            Console.WriteLine("Сравнение ссылок на объекты: " 
                + object.ReferenceEquals(p1, p2)
                + "\nСравнение самих объектов: " + (p1 == p2)
                + "\nХеш коды: p1: " + p1.GetHashCode() + "  p2: "
                + p2.GetHashCode() + "\n");

            Student s1 = new Student(p1, Enducation.Specialist, 120);
            s1.AddExams(new Exam(),
                new Exam("Calculus", 5, new DateTime(1985, 5, 30)),
                new Exam("Chemistry", 2, new DateTime(1985, 12, 4)),
                new Exam("OOP", 3, new DateTime(1982, 2, 14)),
                new Exam("C#", 3, new DateTime(1986, 1, 15)),
                new Exam("Linear algebra", 5, new DateTime(1984, 1, 21)));
            s1.AddTests(new Test(), new Test("C#", true), new Test(".NET", false));
            Console.WriteLine("Оригинал объекта\n" + s1 
                + "\n\nЗначение свойства типа Person для объекта типа Student \n"
                + s1.Person + "\n");

            Student s2 = (Student)s1.DeepCopy();
            s2.Exams[2] = new Exam("Physics", 5, new DateTime(1982, 8, 8));
            s2.Tests[2] = new Test("Java", true);
            s2.Name = "Frederic";
            Console.WriteLine("Изменённая копия объекта\n" + s2
                + "\n\nОригинал объекта после изменения копии\n" + s1
                + "\n\nПроверка на исключение");

            try
            {
                s1.GroupNumber = 800;
            }
            catch (Exception ex) 
            {
                 Console.WriteLine(ex.Message);
            }
            
            Console.WriteLine("\nПроверка итераторов\n1) Список всех зачётов и экзаменов");
            foreach (Object obj in s1.GetAll())
            {
                Console.WriteLine(obj.ToString());
            }

            Console.WriteLine("\n2) Список экзаменов с оценкой выше 3");
            foreach (Exam exam in s1.ExamsAboveMark(3))
            {
                Console.WriteLine(exam.ToString());
            }

            Console.WriteLine("\n3) Предметы, которые есть в списке экзаменов и зачетов");
            IEnumerator se = s1.GetEnumerator();
            while (se.MoveNext())
            {
                Console.WriteLine(se.Current);
            }

            Console.WriteLine("\n4) Список сданных зачётов и экзаменов");
            foreach (Object obj in s1.Passed())
            {
                Console.WriteLine(obj.ToString());
            }
            
            Console.WriteLine("\n5) Список зачетов со сданным экзаменом");
            foreach (Object obj in s1.PassedWithExam())
            {
                Console.WriteLine(obj.ToString());
            }
    
            Console.ReadKey();
        }
    }
}

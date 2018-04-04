using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    enum Enducation { Specialist, Bachelor, SecondEnducation };

    class Program
    {
        static void Main(string[] args)
        {
            StudentCollection st = new StudentCollection();
            st.AddDefaults();
            Student s1 = new Student(new Person("Leonardo", "Da Vinci", new DateTime(1452, 4, 15)), Enducation.Specialist, 556);
            s1.AddExams(new Exam("Drawings", 5, new DateTime(1464, 6, 5)), new Exam("Design", 4, new DateTime(1966, 8, 15)));
            s1.AddTests(new Test("Design", true));
            Student s2 = new Student(new Person("Leonardo", "DiCaprio", new DateTime(1974, 11, 11)), Enducation.Bachelor, 451);
            s2.AddExams(new Exam("Listening", 3, new DateTime(1980, 6, 25)));
            s2.AddTests(new Test("Reading", true));
            st.AddStudents(s1, s2);

            Console.WriteLine("Unsorted: \n" + st.ToShortString());
            
            st.SortBySurname();
            Console.WriteLine("Sort By Surname: \n" + st.ToShortString());
            st.SortByDate();
            Console.WriteLine("Sort By Date: \n" + st.ToShortString());
            st.SortByAverage();
            Console.WriteLine("Sort By Average: \n" + st.ToShortString());

            Console.WriteLine("Max Average Mark: " + st.MaxAverageMark);

            Console.WriteLine("\nOnly Specialists: ");
            foreach (Student student in st.GetSpecialist)
            {
                Console.WriteLine(student.ToShortString());
            }
            //TODO Wtf group by?

            Console.WriteLine("\nAverage Mark Group: ");
            foreach (Student student in st.AverageMarkGroup(4.75))
            {
                Console.WriteLine(student.ToShortString());
            }
            
            int input = 0;
            while (true)
            {
                try
                {
                    input = int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
            TestCollections tc = new TestCollections(input);
            tc.SearchTime();
            
            Console.ReadKey();
        }
    }
}

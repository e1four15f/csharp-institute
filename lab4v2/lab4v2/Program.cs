using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4v2
{
    enum Enducation { Specialist, Bachelor, SecondEnducation };
    delegate TKey KeySelector<TKey>(Student st);

    class Program
    {
        static void Main(string[] args)
        {
            KeySelector<string> selector = Select;

            Journal<string> journal = new Journal<string>();
            StudentCollection<string> st1 = new StudentCollection<string>(selector, "Collection 1");
            StudentCollection<string> st2 = new StudentCollection<string>(selector, "Collection 2");
            st1.Journal = journal;
            st2.Journal = journal;

            Student s1 = GenerateStudent(1);
            Student s2 = GenerateStudent(2);
            Student s3 = GenerateStudent(3);

            st1.AddStudents(s1);
            st1.getStudent(s1.ToString()).Enducation = Enducation.Bachelor;
            st1.Remove(s1);
            s1.Enducation = Enducation.SecondEnducation;

            st2.AddStudents(s2);
            st2.AddStudents(s3);
            st2.getStudent(s2.ToString()).GroupNumber = 555;
            st2.Remove(s3);
            Console.WriteLine(journal);
 
            Console.Read();
        }

        private static Student GenerateStudent(int id)
        {
            Student student = new Student();
            student.Name += " " + id;
            student.Surname += " " + id;
            //student.AddExams(new Exam("Cosmology " + id, 5, new DateTime(1985, 12, 4)));
            //student.AddTests(new Test("Astronomy " + id, true));
            return student;
        }

        public static string Select(Student student)
        {
            return student.ToString();
        }
    }
}

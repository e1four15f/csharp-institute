using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class StudentCollection : IComparer<Student>
    {
        private List<Student> students;

        public StudentCollection()
        {
            students = new List<Student>();
        }

        public void AddDefaults()
        {
            students.Add(new Student(new Person("Elon", "Musk", new DateTime(1971, 6, 28)),
                    Enducation.Specialist, 288));
            students[0].AddExams(new Exam(),
                    new Exam("Cosmology", 5, new DateTime(1985, 12, 4)),
                    new Exam("Astronomy", 3, new DateTime(1986, 1, 15)));
            students[0].AddTests(new Test("Cosmology", true), new Test("Astronomy", false));

            students.Add(new Student(new Person("Albert", "Einstein", new DateTime(1879, 4, 14)),
                    Enducation.SecondEnducation, 322));
            students[1].AddExams(new Exam(),
                    new Exam("Calculus", 5, new DateTime(1985, 5, 30)),
                    new Exam("Chemistry", 5, new DateTime(1985, 12, 4)),
                    new Exam("Linear algebra", 5, new DateTime(1984, 1, 21)));
            students[1].AddTests(new Test(), new Test("Calculus", true), new Test("Chemistry", false));

            students.Add(new Student(new Person("Gabe", "Newell", new DateTime(1962, 11, 3)),
                    Enducation.Bachelor, 333));
            students[2].AddExams(new Exam(),
                    new Exam("OOP", 3, new DateTime(1982, 2, 14)),
                    new Exam("C#", 3, new DateTime(1986, 1, 15)));
            students[2].AddTests(new Test(), new Test("C#", true), new Test(".NET", false));
        }

        public void AddStudents(params Student[] students)
        {
            foreach (Student student in students)
            {
                this.students.Add(student);
            }
        }
        
        public override string ToString()
        {
            string result = "";
            foreach (Student student in students)
            {
                result += "-------------------------\n" 
                    + student +
                    "\n-------------------------\n";
            }
            return result;
        }

        public string ToShortString()
        {
            string result = "";
            foreach (Student student in students)
            {
                result += "-------------------------\n" 
                    + student.Person + "\nAverage: " + student.Average 
                    + "\nTests: " + student.Tests.Count
                    + "\nExams: " + student.Exams.Count +
                    "\n-------------------------\n";
            }
            return result;
        }

        public void SortBySurname() 
        {
            students.Sort();
        }

        public void SortByDate()
        {
           students.Sort(new Person());       
        }

        public void SortByAverage()
        {
            students.Sort(this);
        }

        public int Compare(Student s1, Student s2)
        {
            return s1.Average.CompareTo(s2.Average);
        }

        public double MaxAverageMark
        {
            get
            {
                if (students.Any()) 
                { 
                    return students.Max(student => student.Average);
                }
                else
                {
                    return 0;
                }
            }
        }

        public IEnumerable<Student> GetSpecialist
        {
            get
            {
                return students.Where(student => student.Enducation == Enducation.Specialist);
            }
        }

        public List<Student> AverageMarkGroup(double value)
        {
            return students.Where(student => student.Average == value).ToList();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace lab5
{
    [Serializable]
    class Student : Person, IEnumerable, IDateAndCopy, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Enducation enducation;
        private int groupNumber;
        private List<Test> tests;
        private List<Exam> exams;

        public Student()
        {
            enducation = Enducation.Specialist;
            groupNumber = 444;
            exams = new List<Exam>();
            tests = new List<Test>();
        }

        public Student(Person person, Enducation enducation, int groupNumber)
        {
            name = person.Name;
            surname = person.Surname;
            birthday = person.Birthday;

            this.enducation = enducation;
            this.groupNumber = groupNumber;

            exams = new List<Exam>();
            tests = new List<Test>();
        }

        private Student SetStudent
        {
            set
            {
                Person = value.Person;
                tests = value.Tests;
                exams = value.Exams;
                enducation = value.Enducation;
                groupNumber = value.GroupNumber;
            }
        }

        public Person Person
        {
            get { return new Person(name, surname, birthday); }
            set
            {
                name = value.Name;
                surname = value.Surname;
                birthday = value.Birthday;
            }
        }

        public Enducation Enducation
        {
            get { return enducation; }
            set
            {
                enducation = value;
                if (PropertyChanged != null) PropertyChanged(this,
                    new PropertyChangedEventArgs("Enducation was changed to "
                        + this.Enducation));
            }
        }

        public double Average
        {
            get
            {
                double average = 0;
                foreach (Exam exam in exams)
                {
                    average += (double)exam.mark / exams.Count;
                }
                return average;
            }
        }

        public bool this[Enducation enducation]
        {
            get
            {
                if (this.enducation == enducation)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Exam> Exams
        {
            get { return exams; }
            set { exams = value; }
        }

        public List<Test> Tests
        {
            get { return tests; }
            set { tests = value; }
        }

        public void AddExams(params Exam[] exams)
        {
            foreach (Exam exam in exams)
            {
                this.exams.Add(exam);
            }
        }

        public void AddTests(params Test[] tests)
        {
            foreach (Test test in tests)
            {
                this.tests.Add(test);
            }
        }

        public override string ToString()
        {
            string result = "-------------------------\n"
                + "\t-Person- \n" + Person + "\nEnducation: " + enducation
                + "\nGroup: " + groupNumber + "\nAverage Mark: " + Average;

            foreach (Exam exam in exams)
            {
                result += "\n" + exam;
            }

            foreach (Test test in tests)
            {
                result += "\n" + test;
            }

            result += "\n-------------------------";

            return result;
        }

        public override string ToShortString()
        {
            return "-------------------------\n"
                + "\t-Person- \n" + Person + "\nEnducation: " + enducation + "\nGroup: "
                + groupNumber + "\nAverage Mark: " + Average + "\nExams: " + exams.Count
                + "\nTests: " + tests.Count
                + "\n-------------------------";
        }

        public override object DeepCopy()
        {
            MemoryStream ms = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, this);
            ms.Position = 0;
            return formatter.Deserialize(ms);
        }

        public bool Save(string filename)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                using (FileStream fs = new FileStream(Program.path 
                    + filename + ".bin", FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(fs);                    
             
                    ms.Close();
                    fs.Close();
                    Console.WriteLine(filename + ".bin was saved");
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool Load(string filename)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                IFormatter formatter = new BinaryFormatter();
            
                using (FileStream fs = new FileStream(Program.path
                    + filename + ".bin", FileMode.Open, FileAccess.Read))
                {
                    ms.SetLength(fs.Length);
                    fs.CopyTo(ms);
                    ms.Position = 0;
                    Student s = (Student)formatter.Deserialize(ms);
                    this.SetStudent = s;

                    ms.Close();
                    fs.Close();
                    Console.WriteLine(filename + ".bin was loaded");
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddFromConsole()
        {
            Console.WriteLine("Add Exams to " + this.name + "? [y]");        
            while (Console.ReadKey().KeyChar == 'y')
            {
                Console.WriteLine("\nName, Mark, Year, Month, Day (' ' or ';')");
                try
                {
                    string[] i = Console.ReadLine().Split(new char[] { ' ', ';' });
                    if (Int32.Parse(i[1]) <= 0 || Int32.Parse(i[1]) > 5)
                    {
                        throw new ArgumentOutOfRangeException("Mark", "Значение должно быть в пределах от 0 до 5");
                    }
                    AddExams(new Exam(i[0], Int32.Parse(i[1]), new DateTime(Int32.Parse(i[2]), 
                        Int32.Parse(i[3]), Int32.Parse(i[4]))));
                    Console.WriteLine("New Exam was added");     
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("\nAdd another Exam? [y]");        
            }
            Console.WriteLine();
            return true;
        }

        public static bool Save(string filename, Student s)
        { 
            s.Save(filename);
            return true;
        }

        public static bool Load(string filename, Student s)
        {
            s.Load(filename);
            return true;

        }

        public int GroupNumber
        {
            get { return groupNumber; }
            set
            {
                if (value <= 100 || value > 599)
                {
                    throw new ArgumentOutOfRangeException("GroupNumber", "Значение должно быть в пределах от 100 до 600");
                }
                else
                {
                    groupNumber = value;
                }
                if (PropertyChanged != null) PropertyChanged(this,
                    new PropertyChangedEventArgs("Group number was changed to "
                        + groupNumber));
            }
        }

        public IEnumerable<Object> GetAll()
        {
            foreach (Exam exam in exams)
            {
                yield return exam;
            }
            foreach (Test test in tests)
            {
                yield return test;
            }
        }

        public IEnumerable<Exam> ExamsAboveMark(int mark)
        {
            foreach (Exam exam in exams)
            {
                if (exam.mark > mark)
                {
                    yield return exam;
                }
            }
        }

        public IEnumerable<object> Passed()
        {
            foreach (Exam exam in exams)
            {
                if (exam.mark > 2)
                {
                    yield return exam;
                }
            }
            foreach (Test test in tests)
            {
                if (test.passed)
                {
                    yield return test;
                }
            }
        }

        public IEnumerable<Test> PassedWithExam()
        {
            foreach (Test test in tests)
            {
                foreach (Exam exam in exams)
                {
                    if (test.passed && exam.mark > 2 && test.name == exam.name)
                    {
                        yield return test;
                    }
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new StudentEnumerator(this);
        }

        public void SortByName()
        {
            exams.Sort();
        }

        public void SortByMark()
        {
            exams.Sort(new Exam());
        }

        public void SortByDate()
        {
            exams.Sort(new ExamCollections());
        }
    }
}

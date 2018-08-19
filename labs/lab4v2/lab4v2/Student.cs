using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;

namespace lab4v2
{
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
            groupNumber = 44;
            exams = new List<Exam>();
            tests = new List<Test>();
            //PropertyChanged += Display;
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
            //PropertyChanged += Display;
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
            Student s = new Student(Person, enducation, groupNumber);
            s.AddExams(exams.ToArray());
            s.AddTests(tests.ToArray());
            return s;
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

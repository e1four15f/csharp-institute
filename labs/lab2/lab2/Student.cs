﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace lab2
{
    class Student : Person, IEnumerable, IDateAndCopy
    {
        private Enducation enducation;
        private int groupNumber;
        private ArrayList tests;
        private ArrayList exams;

        public Student() 
        {
            enducation = Enducation.Specialist;
            groupNumber = 44;
            exams = new ArrayList();
            tests = new ArrayList();
        }
        
        public Student(Person person, Enducation enducation, int groupNumber)
        {
            name = person.Name;
            surname = person.Surname;
            birthday = person.Birthday;

            this.enducation = enducation;
            this.groupNumber = groupNumber;

            exams = new ArrayList();
            tests = new ArrayList();
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
            set { enducation = value; }
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

        public ArrayList Exams
        {
            get { return exams;  }
            set { exams = value; }
        }
        
        public ArrayList Tests
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
                + "\nGroup: " + groupNumber; 
            
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
                + groupNumber + "\nAverage Mark: " + Average
                + "\n-------------------------";
        }

        public override object DeepCopy()
        {
            Student s = new Student(Person, enducation, groupNumber);
            s.AddExams((Exam[])exams.ToArray(typeof(Exam)));
            s.AddTests((Test[])tests.ToArray(typeof(Test)));
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
    }
}

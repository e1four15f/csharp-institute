﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4v2
{
    enum Action { Add, Remove, Property };
    delegate void StudentsChangedHandler<TKey>(Object source, StudentsChangedEventArgs<TKey> args);
    
    class StudentCollection<TKey>
    {
        private Dictionary<TKey, Student> dictionary = new Dictionary<TKey, Student>();
        private KeySelector<TKey> selector;

        public event StudentsChangedHandler<TKey> StudentChanged;

        public string name { get; set; }

        private Journal<TKey> journal;
        public Journal<TKey> Journal
        {
            set
            {
                journal = value;
                StudentChanged += journal.Display;
            }
        }

        public StudentCollection(KeySelector<TKey> selector, string name)
        {
            this.selector = selector;
            this.name = name;
        }

        public Student getStudent(TKey key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            return null;
        }

        public void AddDefaults()
        {
            Student[] s = new Student[3];

            s[0] = new Student(new Person("Elon", "Musk", new DateTime(1971, 6, 28)),
                       Enducation.Specialist, 288);
            s[0].AddExams(new Exam(),
                    new Exam("Cosmology", 5, new DateTime(1985, 12, 4)),
                    new Exam("Astronomy", 3, new DateTime(1986, 1, 15)));
            s[0].AddTests(new Test("Cosmology", true), new Test("Astronomy", false));
            dictionary.Add(selector(s[0]), s[0]);

            s[1] = new Student(new Person("Albert", "Einstein", new DateTime(1879, 4, 14)),
                    Enducation.SecondEnducation, 322);
            s[1].AddExams(new Exam(),
                    new Exam("Calculus", 5, new DateTime(1985, 5, 30)),
                    new Exam("Chemistry", 5, new DateTime(1985, 12, 4)),
                    new Exam("Linear algebra", 5, new DateTime(1984, 1, 21)));
            s[1].AddTests(new Test(), new Test("Calculus", true), new Test("Chemistry", false));
            dictionary.Add(selector(s[1]), s[1]);

            s[2] = new Student(new Person("Gabe", "Newell", new DateTime(1962, 11, 3)),
                    Enducation.Bachelor, 333);
            s[2].AddExams(new Exam(),
                    new Exam("OOP", 3, new DateTime(1982, 2, 14)),
                    new Exam("C#", 3, new DateTime(1986, 1, 15)));
            s[2].AddTests(new Test(), new Test("C#", true), new Test(".NET", false));
            dictionary.Add(selector(s[2]), s[2]);
            if (StudentChanged != null)
            {
                StudentChanged(this, new StudentsChangedEventArgs<TKey>
                    (name, Action.Add, "Student", selector(s[0])));
                StudentChanged(this, new StudentsChangedEventArgs<TKey>
                    (name, Action.Add, "Student", selector(s[1])));
                StudentChanged(this, new StudentsChangedEventArgs<TKey>
                    (name, Action.Add, "Student", selector(s[2])));
            }
        }

        public void AddStudents(params Student[] students)
        {
            foreach (Student student in students)
            {
                dictionary.Add(selector(student), student);
                if (journal != null)
                { 
                    student.PropertyChanged += journal.Display;
                }
                if (StudentChanged != null) 
                { 
                    StudentChanged(this, new StudentsChangedEventArgs<TKey>
                        (name, Action.Add, "Student", selector(student)));
                }
            }
        }

        public override string ToString()
        {
            string result = "";

            foreach (KeyValuePair<TKey, Student> student in dictionary)
            {
                result += "\n" + student.Value.ToString();
            }

            return result;
        }

        public string ToShortString()
        {
            string result = "";

            foreach (KeyValuePair<TKey, Student> student in dictionary)
            {
                result += "\n" + student.Value.ToShortString();
            }

            return result;
        }

        public double AverageMax
        {
            get
            {
                return dictionary.Max(student => student.Value.Average);
            }
        }

        public IEnumerable<KeyValuePair<TKey, Student>> EnducationForm(Enducation value)
        {
            return dictionary.Where(student => student.Value.Enducation == value);
        }

        public IEnumerable<IGrouping<Enducation, KeyValuePair<TKey, Student>>> GroupByForm
        {
            get
            {
                return dictionary.GroupBy(student => student.Value.Enducation);
            }
        }

        public bool Remove(Student student)
        {
            if(dictionary.ContainsValue(student))
            {
                dictionary.Remove(selector(student));
                if (journal != null)
                {
                    student.PropertyChanged -= journal.Display;
                }
                if (StudentChanged != null)
                { 
                    StudentChanged(this, new StudentsChangedEventArgs<TKey>
                        (name, Action.Remove, "Student", selector(student)));
                }
                return true;
            }
            return false;
        }
    }
}

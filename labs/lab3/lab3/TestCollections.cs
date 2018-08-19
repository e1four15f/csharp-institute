using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class TestCollections
    {
        private List<Person> persons;
        private List<String> strings;
        private Dictionary<Person, Student> personsDictionary;
        private Dictionary<String, Student> stringsDictionary;

        public TestCollections(int amount)
        {
            persons = new List<Person>();
            strings = new List<String>();
            personsDictionary = new Dictionary<Person, Student>();
            stringsDictionary = new Dictionary<String, Student>();

            for (int i = 0; i < amount; i++)
            {
                persons.Add((Person)GenerateElement(i));
                personsDictionary.Add((Person)GenerateElement(i), GenerateElement(i));
                strings.Add(((Person)GenerateElement(i)).ToString());
                stringsDictionary.Add(((Person)GenerateElement(i)).ToString(), GenerateElement(i));
            }
        }

        public static Student GenerateElement(int type)
        {
            Student student = new Student(new Person("Yuri" + type, "Gagarin" + type, DateTime.Today), Enducation.Bachelor, type);
            student.AddExams(new Exam(),
                        new Exam("Cosmology" + type, type, new DateTime(1945, 12, 4)),
                        new Exam("Astronomy" + type, type, new DateTime(1946, 1, 15)));
            student.AddTests(new Test("Cosmology", true), new Test("Astronomy", false));
            return student;
        }

        public long GetTime(Object obj, Student student, bool value)
        {
            Stopwatch timer = new Stopwatch();
            if (obj is List<Person>)
            {
                timer.Start();
                Console.WriteLine(persons.Contains(student));
                timer.Stop();
            }
            if (obj is List<String>)
            {
                timer.Start();
                Console.WriteLine(strings.Contains(student.ToString()));
                timer.Stop();
            }
            if (obj is Dictionary<Person, Student>)
            {
                if (value)
                {
                    timer.Start();
                    Console.WriteLine(personsDictionary.ContainsValue(student));
                    timer.Stop();
                }
                else
                { 
                    timer.Start();
                    Console.WriteLine(personsDictionary.ContainsKey(student));
                    timer.Stop();
                }
            }
            if (obj is Dictionary<String, Student>)
            {
                if (value)
                {
                    timer.Start();
                    Console.WriteLine(stringsDictionary.ContainsValue(student));
                    timer.Stop();
                }
                else
                {
                    timer.Start();
                    Console.WriteLine(stringsDictionary.ContainsKey(student.ToString()));
                    timer.Stop();
                }
            }
            return timer.ElapsedTicks;
        }

        
        public void SearchTime() {
            Student[] elements = new Student[] { GenerateElement(0), GenerateElement((int)persons.Count / 2), GenerateElement(persons.Count - 1), GenerateElement(persons.Count) };
            object[] obj = new Object[4] { persons, strings, personsDictionary, stringsDictionary }; 
            string[] elementTitle = new String[] { "First", "Middle", "Last", "Not in list" };
            string[] title = new String[] { "\n\t-List<Person>-", "\n\t-List<String>-", "\n\t-Dictionary<Person, Student> Key-",
                "\n\t-Dictionary<String, Student> Key-", "\n\t-Dictionary<Person, Student> Value-", "\n\t-Dictionary<String, Student> Value-" };
            bool value = false;

            for (int type = 0; type < 6; type++)
            {
                Console.WriteLine(title[type]);
                for (int i = 0; i < 4; i++)
                {
                    if (type > 3)
                    {
                        value = true;
                        Console.WriteLine(elementTitle[i] + " element: " + GetTime(obj[type-2], elements[i], value));
                    }
                    else
                    {
                        Console.WriteLine(elementTitle[i] + " element: " + GetTime(obj[type], elements[i], value));
                    }
                }
            }
        }
    }
}

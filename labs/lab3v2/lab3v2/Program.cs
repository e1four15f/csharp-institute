using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3v2
{
    enum Enducation { Specialist, Bachelor, SecondEnducation };
    delegate TKey KeySelector<TKey>(Student st);

    class Program
    {
        static void Main(string[] args)
        {
            
            Student student = new Student(new Person("Hans", "Zimmer", 
                new DateTime(1957, 7, 12)), Enducation.Specialist, 678);
            student.AddExams(new Exam(),
                new Exam("Solfeggio", 5, new DateTime(1987, 5, 7)),
                new Exam("Singing", 2, new DateTime(1982, 1, 8)),
                new Exam("Rhythmic", 4, new DateTime(1985, 4, 12)),
                new Exam("Composition", 5, new DateTime(1989, 5, 28)));
            Console.WriteLine("\nОригинальный объект\n" + student);

            student.SortByName();
            Console.WriteLine("\nСортировка по названию предмета\n" + student);

            student.SortByMark();
            Console.WriteLine("\nСортировка по оценке\n" + student);

            student.SortByDate();
            Console.WriteLine("\nСортировка по дате экзамена\n" + student);
            
            KeySelector<string> selector;
            selector = Select;
            StudentCollection<string> st = new StudentCollection<string>(selector);
            st.AddDefaults();
            st.AddStudents(student);
            Console.WriteLine("\nОбъект StudentCollection" + st);

            Console.WriteLine("\nМаксимально значение средного балла в коллекции "
                + st.AverageMax + "\n\nФильтрация коллекции по форме обучения Specialist");
            foreach (KeyValuePair<string, Student> s in st.EnducationForm(Enducation.Specialist))
            {
                Console.WriteLine(s.Value.ToShortString());
            }

            Console.WriteLine("\nГруппировка элементов по форме обучения");
            foreach (IGrouping<Enducation, KeyValuePair<string, Student>> group in st.GroupByForm)
            {
                Console.WriteLine("\n" + group.Key);
                foreach (KeyValuePair<string, Student> s in group)
                {
                    Console.WriteLine(s.Value.ToShortString());
                }
            }

            int size = 0;
            while(true)
            {
                Console.WriteLine("Введите число элементов в коллекции: ");
                try
                {
                    size = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
            TestCollections<Person, Student> tc = new TestCollections<Person, Student>
                (size, TestCollections<Person, Student>.GenerateElement);

            Search(tc, size);

            Console.ReadKey();
        }

        public static string Select(Student student)
        {
            return student.ToString();
        }

        public static void Search(TestCollections<Person, Student> tc, int size)
        {
            KeyValuePair<Person, Student> first = TestCollections<Person, Student>.GenerateElement(0);
            KeyValuePair<Person, Student> middle = TestCollections<Person, Student>.GenerateElement((int)(size / 2));
            KeyValuePair<Person, Student> last = TestCollections<Person, Student>.GenerateElement(size - 1);
            KeyValuePair<Person, Student> notInList = TestCollections<Person, Student>.GenerateElement(size);

            string[] title = new String[] { 
                "\n\t-List<TKey>-", "\n\t-List<string>-", 
                "\n\t-Dictionary<TKey, TValue> Key-",
                "\n\t-Dictionary<string, TValue> Key-", 
                "\n\t-Dictionary<TKey, TValue> Value-", 
                "\n\t-Dictionary<string, TValue> Value-" };

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine(title[i]
                    + "\nFirst: " + GetTime(tc, first, i)
                    + "\nMiddle: " + GetTime(tc, middle, i)
                    + "\nLast: " + GetTime(tc, last, i)
                    + "\nNot In List: " + GetTime(tc, notInList, i));
            }
        }

        public static long GetTime(TestCollections<Person, Student> tc,
            KeyValuePair<Person, Student> current, int method)
        {
            Stopwatch timer = new Stopwatch();

            switch (method)
            {
                case 0:
                    timer.Start();
                    tc.FindInList(current.Key);
                    timer.Stop();
                    break;
                case 1:
                    timer.Start();
                    tc.FindInStringList(current.Key.ToString());
                    timer.Stop();
                    break;
                case 2:
                    timer.Start();
                    tc.FindKeyInDictionary(current.Key);
                    timer.Stop();
                    break;
                case 3:
                    timer.Start();
                    tc.FindKeyInStringDictionary(current.Key.ToString());
                    timer.Stop();
                    break;
                case 4:
                    timer.Start();
                    tc.FindValueInDictionary(current.Value);
                    timer.Stop();
                    break;
                case 5:
                    timer.Start();
                    tc.FindValueInStringDictionary(current.Value);
                    timer.Stop();
                    break;
            }

            return timer.ElapsedTicks;
        }
    }
}

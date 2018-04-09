using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    enum Enducation { Specialist, Bachelor, SecondEnducation };
    delegate TKey KeySelector<TKey>(Student st);
    
    class Program
    {
        public const string path = @"C:\Users\User\Documents\Visual Studio 2013\Projects\C#\lab5\lab5\data\";

        static void Main(string[] args)
        {
            Student s = new Student(new Person("Hideo", "Kojima", new DateTime(1963, 8, 24)),
                       Enducation.Bachelor, 345);
            s.AddExams(new Exam("Design", 5, new DateTime(1970, 2, 26)),
                    new Exam("Scenario", 5, new DateTime(1971, 12, 12)));

            s.AddFromConsole();

            Student sCopy = (Student)s.DeepCopy();
            Console.WriteLine("Original object\n" + s + "\n\nCopied object\n" + sCopy);

            Student sLoad = new Student();
            Console.WriteLine("Enter filename to load: ");
            string filename = Console.ReadLine();
            if (File.Exists(path + filename + ".bin"))
            {
                sLoad.Load(filename);
                Console.WriteLine(sLoad);
            }
            else
            {
                Console.WriteLine("File does not exist\n" + filename + ".bin was created");
                File.Create(path + filename + ".bin");
            }

            sLoad.AddFromConsole();
            sLoad.Save("New File");
            Console.WriteLine(sLoad);

            Student.Load("New File", sLoad);
            sLoad.AddFromConsole();
            Student.Save("Super New File", sLoad);

            Console.WriteLine(sLoad);
            Console.Read();
        }

        public static void DefaultSettings()
        {
            Student[] s = new Student[3];

            s[0] = new Student(new Person("Elon", "Musk", new DateTime(1971, 6, 28)),
                       Enducation.Specialist, 288);
            s[0].AddExams(new Exam(),
                    new Exam("Cosmology", 5, new DateTime(1985, 12, 4)),
                    new Exam("Astronomy", 3, new DateTime(1986, 1, 15)));
            
            s[1] = new Student(new Person("Albert", "Einstein", new DateTime(1879, 4, 14)),
                    Enducation.SecondEnducation, 322);
            s[1].AddExams(new Exam(),
                    new Exam("Calculus", 5, new DateTime(1985, 5, 30)),
                    new Exam("Chemistry", 5, new DateTime(1985, 12, 4)),
                    new Exam("Linear algebra", 5, new DateTime(1984, 1, 21)));
            
            s[2] = new Student(new Person("Gabe", "Newell", new DateTime(1962, 11, 3)),
                    Enducation.Bachelor, 333);
            s[2].AddExams(new Exam(),
                    new Exam("OOP", 3, new DateTime(1982, 2, 14)),
                    new Exam("C#", 3, new DateTime(1986, 1, 15)));
            
            Student.Save(s[0].Name, s[0]);
            Student.Save(s[1].Name, s[1]);
            Student.Save(s[2].Name, s[2]);
        }
    }
}

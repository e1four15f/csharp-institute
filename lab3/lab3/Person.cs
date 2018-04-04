
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class Person : IComparable, IComparer<Person>
    {
        protected string name;
        protected string surname;
        protected DateTime birthday;

        public Person()
        {
            name = "Bill";
            surname = "Gates";
            birthday = new DateTime(1955, 8, 28);
        }

        public Person(string name, string surname, DateTime birthday)
        {
            this.name = name;
            this.surname = surname;
            this.birthday = birthday;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public int BirthYear
        {
            get { return birthday.Year; }
            set { birthday = new DateTime(value, birthday.Month, birthday.Day); }
        }

        public override string ToString()
        {
            return "\t-Person-\nName: " + name + "\nSurname: " + surname + "\nBirthday: " + birthday.ToShortDateString();
        }
        
        public virtual string ToShortString()
        {
            return "\t-Person-\nName: " + name + "\nSurname: " + surname;
        }
        
        public override bool Equals(object obj)
        {
            return ((Person)obj).name.Equals(name) && ((Person)obj).surname.Equals(surname) && ((Person)obj).birthday == birthday;
        }
        
        public static bool operator ==(Person p1, Person p2) 
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Person p1, Person p2)
        {
            return !p1.Equals(p2);
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public int CompareTo(object obj)
        {
            return surname.CompareTo(((Person)obj).Surname);
        }

        public int Compare(Person p1, Person p2)
        {
            return p1.Birthday.CompareTo(p2.Birthday);
        }
    }
}

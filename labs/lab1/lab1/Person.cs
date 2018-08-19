using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class Person
    {
        private string name;
        private string surname;
        private DateTime birthday;

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
            return "Name: " + name + "\nSurname: " + surname + "\nBirthday: "
                + birthday.ToShortDateString();
        }

        public virtual string ToShortString()
        {
            return "Name: " + name + "\nSurname: " + surname;
        }
    }
}

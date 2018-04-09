using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    [Serializable]
    class Exam : IDateAndCopy, IComparable, IComparer<Exam>
    {
        public string name { get; set; }
        public int mark { get; set; }
        public DateTime date { get; set; }

        public Exam()
        {
            name = "History";
            mark = 4;
            date = new DateTime(2008, 5, 12);
        }

        public Exam(string name, int mark, DateTime date)
        {
            this.name = name;
            this.mark = mark;
            this.date = date;
        }

        public override string ToString()
        {
            return "\n\t-Exam-\nName: " + name + "\nMark: " + mark + "\nDate: "
                   + date.ToShortDateString();
        }

        public object DeepCopy()
        {
            return new Exam(name, mark, date);
        }

        public int CompareTo(object obj)
        {
            return name.CompareTo(((Exam)obj).name);
        }

        public int Compare(Exam e1, Exam e2)
        {
            return e1.mark.CompareTo(e2.mark);
        }

    }
}

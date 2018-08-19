using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class Exam
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
            return "\n\n\t-Exam-\nName: " + name + "\nMark: " + mark + "\nDate: "
                + date.ToShortDateString();
        }
    }
}

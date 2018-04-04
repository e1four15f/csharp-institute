using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4v2
{
    class ExamCollections : IComparer<Exam>
    {
        public int Compare(Exam e1, Exam e2)
        {
            return e1.date.CompareTo(e2.date);
        }
    }
}

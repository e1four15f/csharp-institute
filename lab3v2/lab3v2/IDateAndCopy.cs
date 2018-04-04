using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3v2
{
    interface IDateAndCopy
    {
        object DeepCopy();
        DateTime date { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    interface IDateAndCopy
    {
        object DeepCopy();
        DateTime date { get; set; }
    }
}

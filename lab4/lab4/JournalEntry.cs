using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class JournalEntry
    {
        public string name { get; set; }
        public string type { get; set; }
        public string data { get; set; }

        public JournalEntry()
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}

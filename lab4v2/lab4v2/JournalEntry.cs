using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4v2
{
    class JournalEntry
    {
        public string name { get; set; }
        public Action action { get; set; }
        public string propertyName { get; set; }
        public string key { get; set; }

        public JournalEntry(string name, Action action, string propertyName, string key)
        {
            this.name = name;
            this.action = action;
            this.propertyName = propertyName;
            this.key = key;
        }

        public override string ToString()
        {
            return "-------------------------\n"
               + "\t-JournalEntry-\nName: " + name + "\nAction: "
               + action + "\nProperty: " + propertyName + "\nTKey:\n" + key
               + "\n\n"; 
        }
    }
}

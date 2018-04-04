using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4v2
{
    class StudentsChangedEventArgs<TKey> : EventArgs
    {
        public string name { get; set; }
        public Action action { get; set; }
        public string propertyName { get; set; }
        public TKey key { get; set; }

        public StudentsChangedEventArgs(string name, Action action, 
            string propertyName, TKey key)
        {
            this.name = name;
            this.action = action;
            this.propertyName = propertyName;
            this.key = key;
        }

        public override string ToString()
        {
            return "-------------------------\n"
               + "\t-ChangedEvent-\nName: " + name + "\nAction: " 
               + action + "\nProperty name: " + propertyName + "\nTKey:\n" + key 
               + "\n-------------------------\n";;
        }
    }
}

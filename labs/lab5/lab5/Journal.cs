using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    class Journal<TKey>
    {
        private List<JournalEntry> journalList = new List<JournalEntry>();

        public void Display(Object seder, StudentsChangedEventArgs<TKey> e)
        {
            journalList.Add(new JournalEntry(e.name, e.action,
                e.propertyName, e.key.ToString()));
        }

        public void Display(Object sender, PropertyChangedEventArgs e)
        {
            journalList.Add(new JournalEntry("Student", Action.Property,
                e.PropertyName, sender.ToString()));
        }

        public override string ToString()
        {
            string result = "";
            foreach (JournalEntry je in journalList)
            {
                result += je;
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class Test
    {
        public string name { get; set; }
        public bool passed { get; set; }

        public Test()
        {
            name = "OOP";
            passed = true;
        }

        public Test(string name, bool passed)
        {
            this.name = name;
            this.passed = passed;
        }

        public override string ToString()
        {
            return "\t-Test-\nName: " + name + "\nPassed: " + passed;
        }
    }
}

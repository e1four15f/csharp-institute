using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class StudentListHandlerEventArgs : EventArgs
    {
        //TODO Autorealize?
        public string name { get; set; }
        public string type  { get; set; }
        public Student changedStudent { get; set; } 

        public StudentListHandlerEventArgs()
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

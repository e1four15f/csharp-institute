using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class StudentEnumerator : IEnumerator
    {
        private Student student;
        private object current;
        private int index;

        public StudentEnumerator(Student student)
        {
            this.student = student;
            index = 0;
        }
        public object Current
        {
            get
            {
                return current;
            }
        }

        public bool MoveNext()
        {
            Test t = (Test)student.Tests[index];
            foreach (Exam exam in student.Exams)
            {
                if (exam.name == t.name)
                {
                    current = t.name;
                    break;
                }
            }
            index++;
            return (index < student.Tests.Count);
        }

        public void Reset()
        {
            index = 0;
        }
    }
}

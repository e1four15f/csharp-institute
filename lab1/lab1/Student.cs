using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class Student
    {
        private Person person;
        private Enducation enducation;
        private int groupNumber;
        private Exam[] exams;

        public Student()
        {
            person = new Person();
            enducation = Enducation.Specialist;
            groupNumber = 44;
        }

        public Student(Person person, Enducation enducation, int groupNumber)
        {
            this.person = person;
            this.enducation = enducation;
            this.groupNumber = groupNumber;
        }

        public Person Person
        {
            get { return person; }
            set { person = value; }
        }

        public Enducation Enducation
        {
            get { return enducation; }
            set { enducation = value; }
        }

        public int GroupNumber
        {
            get { return groupNumber; }
            set { groupNumber = value; }
        }

        public Exam[] Exams
        {
            get { return exams; }
            set { exams = value; }
        }

        public double Average
        {
            get
            {
                double average = 0;
                for (int i = 0; exams != null && i < exams.Length; i++)
                {
                    average += (double)exams[i].mark / exams.Length;
                }
                return average;
            }
        }

        public bool this[Enducation enducation]
        {
            get
            {
                if (this.enducation == enducation)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void AddExams(params Exam[] exams)
        {
            if (this.exams != null)
            {
                this.exams.Concat(exams);
            }
            else
            {
                this.exams = exams;
            }
        }

        public override string ToString()
        {
            string result = "-------------------------\n" 
                + "\t-Person- \n" + person + "\nEnducation: " + enducation 
                + "\nGroup: " + groupNumber;

            for (int i = 0; i < exams.Length; i++)
            {
                result += exams[i];
            }

            result += "\n-------------------------";

            return result;
        }

        public virtual string ToShortString()
        {
            return "-------------------------\n" 
                + "\t-Person- \n" + person + "\nEnducation: " + enducation + "\nGroup: " 
                + groupNumber + "\nAverage Mark: " + Average
                + "\n-------------------------";
        }
    }
}

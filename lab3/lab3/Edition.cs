using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Edition
    {
        protected string _name;
        protected DateTime _outdate;
        protected int _count;
        public string name
        {
            get { return _name; }
            set { _name = value; }

        }
        public DateTime outDate
        {
            get { return _outdate; }
            set { _outdate = value; }

        }
        public int count
        {
            get { return _count; }
            set
            {
                if (value > 0)
                    _count = value;
                else
                {
                    ArgumentOutOfRangeException MyExeption = new ArgumentOutOfRangeException("_count", "Значение должно быть положительным");
                    throw MyExeption;
                }
            }

        }
        public Edition(string name, DateTime date, int count)
        {
            _name = name;
            _outdate = date;
            _count = count;
        }
        public Edition()
        {
            _name = "";
            _outdate = DateTime.Now;
            _count = 0;
        }
        public virtual object DeepCopy()
        {
            Edition Result = new Edition();
            Result.name = (string)this._name.Clone();
            Result._outdate = this._outdate;
            Result.count = this._count;
            return (object)Result;
        }
        public override string ToString()
        {
            return _name + " " + _outdate.ToShortDateString() + " " + _count;
        }
        public override bool Equals(object obj)
        {
            Edition a = (Edition)obj;
            return (string)a.name.Clone() == (string)this._name.Clone() && a.outDate == this._outdate && a.count == this._count;
        }
        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }
        public static Edition Scan()
        {
            Console.WriteLine("Введите название");
            string tname = Console.ReadLine();
            Console.WriteLine("Введите дату выхода");
            DateTime TDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Введите тираж");
            int tcount = Convert.ToInt32(Console.ReadLine());
            return new Edition((string)tname.Clone(), TDate, tcount);
        }
        public static bool operator ==(Edition a, Edition b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Edition a, Edition b)
        {
            return !a.Equals(b);
        }
    }
}
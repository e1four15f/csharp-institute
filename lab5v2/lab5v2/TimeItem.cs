using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5v2
{
    [Serializable]
    class TimeItem
    {
        private int n, k;
        private double timeCS, timeCPP, ratio;

        public TimeItem(int n, int k, double timeCS, double timeCPP)
        {
            this.n = n;
            this.k = k;
            this.timeCS = timeCS;
            this.timeCPP = timeCPP;
            ratio = timeCS / timeCPP;
        }

        public override string ToString()
        {
            return string.Format("{0,-10}{1,-10}{2,-10}{3,-10}{4,-10}\n",
                n, k, timeCS, timeCPP, ratio);
        }
    }
}

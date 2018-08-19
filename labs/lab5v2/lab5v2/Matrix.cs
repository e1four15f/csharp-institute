using System;

namespace lab5v2
{
    [Serializable]
    class Matrix
    {
        private double[] b;
        private double[] c;
        private double[] a;

        public Matrix(int n)
        {
            b = new double[n - 1];
            c = new double[n];
            a = new double[n - 1];

            for (int i = 0; i < c.Length; i++)
            {
                c[i] = 10000;
                if (i + 1 != c.Length)
                {
                    b[i] = 10;
                    a[i] = 20;
                }
            }
        }

        public Matrix(double[] b, double[] c, double[] a)
        {
            this.b = new double[b.Length];
            this.c = new double[c.Length];
            this.a = new double[a.Length];

            for (int i = 0; i < c.Length; i++)
            {
                this.c[i] = c[i];
                if (i + 1 != c.Length)
                {
                    this.b[i] = b[i];
                    this.a[i] = a[i];
                }
            }
        }

        private double[] beta;
        private double[] gamma;
        private double[] x;

        public double[] Solve(double[] f)
        {
            beta = new double[c.Length];
            gamma = new double[c.Length];

            beta[0] = b[0] / c[0];
            gamma[0] = f[0] / c[0];

            for (int i = 1; i < c.Length - 1; i++)
            {
                beta[i] = b[i] /
                    (c[i] - beta[i - 1] * a[i - 1]);

                gamma[i] = (f[i] - a[i - 1] * gamma[i - 1]) /
                    (c[i] - beta[i - 1] * a[i - 1]);
            }

            gamma[c.Length - 1] = (f[c.Length - 1] - a[c.Length - 2] * gamma[c.Length - 2]) /
                    (c[c.Length - 1] - beta[c.Length - 2] * a[c.Length - 2]);

            x = new double[c.Length];

            x[c.Length - 1] = gamma[c.Length - 1];

            for (int i = c.Length - 2; i >= 0; i--)
            {
                x[i] = gamma[i] - beta[i] * x[i + 1];
            }

            return x;
        }
        
        public override string ToString()
        {
            string result = "\t-Matrix-\n";

            for (int i = 0; i < c.Length; i++)
            {
                for (int j = 0; j < c.Length; j++)
                {
                    switch (i - j)
                    {
                        case 0:
                            result += string.Format("{0,5}", c[i]);
                            break;
                        case 1:
                            result += string.Format("{0,5}", a[j]);
                            break;
                        case -1:
                            result += string.Format("{0,5}", b[i]);
                            break;
                        default:
                            result += string.Format("{0,5}", 0);
                            break;
                    }
                }
                result += "\n";
            }

            return result;
        }
    }
}



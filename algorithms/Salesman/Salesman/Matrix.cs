using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salesman
{
    class Matrix
    {
        public int[,] m;
        public int n;

        public List<int> vi = new List<int>();
        public List<int> vj = new List<int>();

        public List<int> vvi = new List<int>();
        public List<int> vvj = new List<int>();

        private int[] hi, hj;

        public int indexI, indexJ, H = 0;
        public bool negative;

        public Matrix(int[,] m)
        {
            indexI = indexJ = 0;
            negative = true;

            this.m = (int[,]) m.Clone();
            n = m.GetLength(0);
            hi = new int[n];
            hj = new int[n];

            for (int i = 0; i < n; i++)
            {
                vi.Add(i);
                vj.Add(i);
            }
        }

        public Matrix(Matrix mat)
        {
            indexI = mat.indexI;
            indexJ = mat.indexJ;
            negative = mat.negative;

            n = mat.n;
            hi = new int[n];
            hj = new int[n];
            m = new int[n, n];

            H = mat.H;

            m = (int[,]) mat.m.Clone();
            
            vi = mat.vi.ToList();
            vj = mat.vj.ToList();

            vvi = mat.vvi.ToList();
            vvj = mat.vvj.ToList();
        }

        public Matrix[] Solve()
        {
            Matrix[] mat = new Matrix[2];
            
            PrintTable();
            FindMinJ();
            FindMinI();
            PrintTable();

            H += FindH();
            int H0 = H;
            Console.WriteLine("H0 = " + H);
            FindIndex();     

            m[indexI, indexJ] = Program.inf;
            FindMinJ();
            FindMinI();
            H += FindH();
            Console.WriteLine("\nH* = " + H);
            negative = false;
            mat[0] = new Matrix(this);
            PrintTable();
            H = H0;

            vi.Remove(indexI);
            vj.Remove(indexJ);


            vvi.Add(indexI);
            vvj.Add(indexJ);
            /*
            for (int x = 0; x < n; x++)
            {
                if (vvj.Contains(x))
                { 
                    for (int y = 0; y < n; y++)
                    {
                        if (vvi.Contains(y) && x != y)
                        {
                            m[x, y] = Program.inf;
                        }
                    }
                }
            }*/

            m[indexJ, indexI] = Program.inf;
            FindMinJ();
            FindMinI();
            H += FindH();
            Console.WriteLine("H = " + H);
            negative = true;
            mat[1] = new Matrix(this);
            PrintTable(); 
            H = H0;

            return mat;
        }

        private int FindH()
        {
            int H = 0;
            foreach (int i in vi)
            {
                H += hj[i];
            }
            foreach (int j in vj)
            {
                H += hi[j];
            }
            return H;
        }

        private void FindMinJ()
        {
            foreach (int i in vi)
            {
                hj[i] = Program.inf;

                foreach (int j in vj)
                {
                    if (hj[i] > m[i, j])
                    {
                        hj[i] = m[i, j];
                    }
                }

                foreach (int j in vj)
                {
                    if (m[i, j] != Program.inf)
                    {
                        m[i, j] -= hj[i];
                    }
                }
            }
        }

        private void FindMinI()
        {
            foreach (int j in vj)
            {
                hi[j] = Program.inf;

                foreach (int i in vi)
                {
                    if (hi[j] > m[i, j])
                    {
                        hi[j] = m[i, j];
                    }
                }

                foreach (int i in vi)
                {
                    if (m[i, j] != Program.inf)
                    {
                        m[i, j] -= hi[j];
                    }
                }
            }
        }

        private void FindIndex()
        {
            int D = 0;
            foreach (int i in vi)
            {
                foreach (int j in vj)
                {
                    if (m[i, j] == 0)
                    {
                        Console.WriteLine("D(" + (i + 1) + ";" + (j + 1) + ") = " + FindD(i, j));
                        if (D < FindD(i, j))
                        {
                            D = FindD(i, j);
                            indexI = i;
                            indexJ = j;
                        }
                    }
                }
            }
            Console.WriteLine("Remove: (" + (indexI + 1) + ";" + (indexJ + 1) + ")");
        }

        private int FindD(int x, int y)
        {
            int min = Program.inf, D = 0;
            foreach (int i in vi)
            {
                if (i != x && min > m[i, y])
                {
                    min = m[i, y];
                }
            }
            D += min;

            min = Program.inf;
            foreach (int j in vj)
            {
                if (j != y && min > m[x, j])
                {
                    min = m[x, j];
                }
            }
            D += min;

            return D;
        }

        public void PrintTable()
        {
            Console.Write(String.Format("{0,-4}", ""));
            foreach (int j in vj)
            {
                Console.Write(String.Format("{0,-4}", "v" + (j + 1)));
            }
            Console.WriteLine(String.Format("{0,-4}", "hj"));

            foreach (int i in vi)
            {
                Console.Write(String.Format("{0,-4}", "v" + (i + 1)));
                foreach (int j in vj)
                {
                    if (m[i, j] == Program.inf)
                    {
                        Console.Write(String.Format("{0,-4}", "inf"));
                    }
                    else
                    {
                        Console.Write(String.Format("{0,-4}", m[i, j]));
                    }
                }
                Console.WriteLine(String.Format("{0,-4}", hj[i]));
            }

            Console.Write(String.Format("{0,-4}", "hi"));
            foreach (int j in vj)
            {
                Console.Write(String.Format("{0,-4}", hi[j]));
            }
            Console.WriteLine("\n");
        }
    }
}

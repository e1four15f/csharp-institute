#include <iostream>
#include <cstdio>
#include <ctime>

using namespace std;

namespace MatrixCPP
{
	class Matrix
	{
	private:
		double* b;
		double* c;
		double* a;

	public:
		int n;

		Matrix(int n)
		{
			b = new double[n - 1];
			c = new double[n];
			a = new double[n - 1];
			this->n = n;

			for (int i = 0; i < n; i++)
			{
				c[i] = 10000;
				if (i + 1 != n)
				{
					b[i] = 10;
					a[i] = 20;
				}
			}
		}

		Matrix(double* b, double* c, double* a, int n)
		{
			this->b = new double[n - 1];
			this->c = new double[n];
			this->a = new double[n - 1];
			this->n = n;

			for (int i = 0; i < n; i++)
			{
				this->c[i] = c[i];
				if (i + 1 != n)
				{
					this->b[i] = b[i];
					this->a[i] = a[i];
				}
			}
		}

		Matrix(const Matrix& m)
		{
			this->n = m.n;
			this->b = new double[n - 1];
			this->c = new double[n];
			this->a = new double[n - 1];

			for (int i = 0; i < n; i++)
			{
				this->c[i] = m.c[i];
				if (i + 1 != n)
				{
					this->b[i] = m.b[i];
					this->a[i] = m.a[i];
				}
			}
		}

		~Matrix()
		{
			delete[] b;
			delete[] c;
			delete[] a;
		}

		Matrix& operator = (const Matrix& m)
		{
			this->n = m.n;
			this->b = new double[n - 1];
			this->c = new double[n];
			this->a = new double[n - 1];

			for (int i = 0; i < n; i++)
			{
				this->c[i] = m.c[i];
				if (i + 1 != n)
				{
					this->b[i] = m.b[i];
					this->a[i] = m.a[i];
				}
			}
		}

		double* Solve(double* f)
		{
			double* beta = new double[n];
			double* gamma = new double[n];

			beta[0] = b[0] / c[0];
			gamma[0] = f[0] / c[0];

			for (int i = 1; i < n - 1; i++)
			{
				beta[i] = b[i] /
					(c[i] - beta[i - 1] * a[i - 1]);

				gamma[i] = (f[i] - a[i - 1] * gamma[i - 1]) /
					(c[i] - beta[i - 1] * a[i - 1]);
			}

			gamma[n - 1] = (f[n - 1] - a[n - 2] * gamma[n - 2]) /
				(c[n - 1] - beta[n - 2] * a[n - 2]);

			double* x = new double[n];

			x[n - 1] = gamma[n - 1];

			for (int i = n - 2; i >= 0; i--)
			{
				x[i] = gamma[i] - beta[i] * x[i + 1];
			}

			delete[] beta;
			delete[] gamma;

			return x;
		}
	};

	extern "C" __declspec(dllexport) double GetTimeCPP(int n, int k)
	{	
		double duration = 0; 
		Matrix matrix = Matrix(n);
		double* right = new double[n];
		for (int i = 0; i < n; i++)
		{	
			right[i] = (i + 1) * 10;
		}

		clock_t start = clock();
		for (int i = 0; i < k; i++)
		{
			matrix.Solve(right);
		}
		duration = (clock() - start);
		
		delete[] right;
		return duration;
	}

	extern "C" __declspec(dllexport) void SolveCPP(double* b, double* c, double* a, int n, double* f, double* x)
	{	
		Matrix matrix = Matrix(b, c, a, n);
		double* xx = new double[n];
		xx = matrix.Solve(f);
		for (int i = 0; i < n; i++)
		{
			x[i] = xx[i];
		}
	}
}
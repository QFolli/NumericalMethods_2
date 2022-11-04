using System;

namespace Lab_2
{
    class Program
    {
        static double[] diag(double[,] A, double[] B)
        {

            int n = B.Length;
            double d, d1;
            double[] X = new double[n];

            for (int k = 0; k < n; k++)
            {
                for (int j = k + 1; j < n; j++)
                {
                    d = A[j, k] / A[k, k];
                    for (int i = k; i < n; i++)
                        A[j, i] = A[j, i] - A[k, i] * d;
                    B[j] = B[j] - B[k] * d;
                }
            }

            for (int k = n - 1; k >= 0; k--)
            {
                d = 0;
                for (int j = k; j < n; j++)
                {
                    d1 = A[k, j] * X[j];
                    d += d1;
                }
                X[k] = (B[k] - d) / A[k, k];
            }

            return X;
        }

        static double[,] Jcob(double[] x)
        {
            int n = x.Length;
            double[,] A = new double[n, n];
            A[0, 0] = -Math.Sin(x[0] - 1);
            A[0, 1] = 1;
            A[1, 0] = 1;
            A[1, 1] = Math.Sin(x[1]);
            return A;
        }

        static double[] f(double[] x)
        {
            int n = x.Length;
            double[] g = new double[n];
            g[0] = -(Math.Cos(x[0] - 1) + x[1] - 0.5);
            g[1] = -(x[0] - Math.Cos(x[1]) - 3);
            return g;
        }

        static double norma(double[] xnew)
        {
            double s = 0;
            for (int i = 0; i < xnew.Length; i++)
                s += Math.Abs(xnew[i]);
            return s;
        }


        static void Main(string[] args)
        {
            double eps;
            double[] x = new double[2];
            double[] xnew;

            x[0] = 0;   // double.Parse(Console.ReadLine());
            x[1] = 0;   // double.Parse(Console.ReadLine());
            eps = 0.01; // double.Parse(Console.ReadLine());

            do
            {
                xnew = diag(Jcob(x), f(x));

                for (int i = 0; i < 2; i++)
                {
                    x[i] = x[i] + xnew[i];
                    Console.Write($"{x[i]:0.00000}  ");
                }
                Console.WriteLine();
            }
            while (norma(xnew) > eps);

            Console.ReadKey();
        }
    }
}

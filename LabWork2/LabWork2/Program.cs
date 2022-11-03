const int n = 2;
const double eps = 1e-4;

//  вычисление нормы
static double Norma(double[,] a, int n)
{
    double res = 0;
    for (int i = 0; i < n; i++)
    for (int j = 0; j < n; j++)
        res += a[i, j] * a[i, j];

    return Math.Sqrt(res);
}

// задание функции
// x – столбец значений переменных
static double Function(double[] x, int i)
{
    switch (i)
    {
        case 0:
            return Math.Sin(x[0] + 0.5) - 1;
        case 1:
            return Math.Cos(x[1] - 2);
    }

    throw new ArgumentException();
}

// построение матрицы Якоби   
//x – столбец значений неизвестных
static double MatrJacobi(double[] x, int i, int j)
{
    switch (i)
    {
        case 0:
            switch (j)
            {
                // вычисляем значение элемента матрицы Якоби индексами 1,1
                case 0:
                    return -Math.Sin(x[1] - 2);
                // вычисляем значение элемента матрицы Якоби с индексами 1,2
                case 1:
                    return 0;
            }

            break;
        case 1:
            switch (j)
            {
                // вычисляем значение элемента матрицы Якоби с индексами 2,1
                case 0:
                    return 0;
                // вычисляем значение элемента матрицы Якоби с индексами 1,2
                case 1:
                    return Math.Cos(x[0] + 0.5);
            }

            break;
    }

    throw new ArgumentException();
}

//вывод вектора
static void VivodVectr(double[] vector)
{
    for (int j = 0; j < n; j++)
        Console.WriteLine("x" + j + "=" + vector[j]);
}

double[,] a = new double[n, n];
double[] x = new double[n];
double[] x0 = new double[n];
int iter;
double max;

x0[0] = 1;
x0[1] = 1;
iter = 0;
do
{
    for (int i = 0; i < n; i++)
    for (int j = 0; j < n; j++)
        a[i, j] = MatrJacobi(x0, i, j);

    VivodVectr(x0);
    Console.WriteLine("Норма = {0}", Norma(a, n));
    Console.WriteLine("Номер итерации - {0}", iter);
    Console.WriteLine("=================");

    // нахождение нового приближения функции
    for (int i = 0; i < n; i++)
        x[i] = Function(x0, i);

    max = Math.Abs(x[0] - x0[0]);

    for (int i = 1; i < n; i++)
        if (Math.Abs(x[i] - x0[i]) > max)
            max = Math.Abs(x[i] - x0[i]);

    x0 = (double[]) x.Clone();
    iter++;
} while ((max > eps) && (iter < 20));
using System;
using MatrixLibrary;

namespace MatrixTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Matrix matrix1 = new Matrix(2, 2).CountUp();
            Matrix matrix2 = new Matrix(2, 2).CountUp();
            Console.Out.WriteLine(matrix1);
            Console.Out.WriteLine(matrix2);
            Console.Out.WriteLine(matrix1.Multiply(matrix2));
            Console.ReadKey();
        }
    }
}

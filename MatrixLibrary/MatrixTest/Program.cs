using System;
using MatrixLibrary;

namespace MatrixTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix1 = new Matrix(3, 3).Random();
            Matrix matrix2 = new Matrix(3, 3).Random();
            Console.Out.WriteLine(matrix1);
            Console.Out.WriteLine(matrix2);
            Console.Out.WriteLine(matrix1.Multiply(matrix2));
            Console.ReadKey();
        }
    }
}

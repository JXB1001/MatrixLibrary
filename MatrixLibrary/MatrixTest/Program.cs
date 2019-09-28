using System;
using MatrixLibrary;

namespace MatrixProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix1 = new Matrix("2,4,4;1,2,3");
            Matrix matrix2 = new Matrix(3, 2).CountUp();
            Console.Out.WriteLine(matrix1);
            Console.Out.WriteLine(matrix2);
            Console.Out.WriteLine(matrix1.Multiply(matrix2));
            Console.ReadKey();
        }
    }
}

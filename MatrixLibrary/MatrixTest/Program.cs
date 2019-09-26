using System;
using MatrixLibrary;

namespace MatrixTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix<int> matrix = new Matrix<int>(3, 3);
            Console.Out.WriteLine(matrix.Random(30, 100));
        }
    }
}

using System;
using MatrixLibrary;

namespace MatrixTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix = new Matrix(3, 3);
            Console.Out.WriteLine(matrix.Random(30, 100));
        }
    }
}

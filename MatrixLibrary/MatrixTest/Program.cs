using System;
using MatrixLibrary;

namespace MatrixProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix = new Matrix(10, 10).Random(0, 100);
            matrix = matrix.ApplyToAll((v) => { return Math.Round(v); });
            Console.Out.WriteLine(matrix);
            Console.Out.WriteLine(matrix.Determinant());
        }
    }
}

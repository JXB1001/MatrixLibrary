using Microsoft.VisualStudio.TestTools.UnitTesting;
using MatrixLibrary;

namespace MatrixTests
{
    [TestClass]
    public class MatrixArithmeticTests
    {
        [TestMethod]
        public void Adding()
        {
            Matrix matrixA = new Matrix("1,0;4,5;-4,-6");
            Matrix matrixB = new Matrix("1,2;3,-4;5,-3");
            Assert.IsTrue(matrixA.Add(matrixB).Compare(new Matrix("2,2;7,1;1,-9")));
        }

        [TestMethod]
        public void Subtracting()
        {
            Matrix matrixA = new Matrix("1,0;4,5;-4,-6");
            Matrix matrixB = new Matrix("1,2;3,-4;5,-3");
            Assert.IsTrue(matrixA.Subtract(matrixB).Compare(new Matrix("0,-2;1,9;-9,-3")));
        }

        [TestMethod]
        public void MultiplyingByAnotherMatrix()
        {
            Matrix matrixA = new Matrix("1,0;4,5;-4,-6");
            Matrix matrixB = new Matrix("1,2,-1;3,-4,0");
            Assert.IsTrue(matrixA.Multiply(matrixB).Compare(new Matrix(
                "1,2,-1;19,-12,-4;-22,16,4")));
        }

        [TestMethod]
        public void MultiplyingByAScalarValue()
        {
            Matrix matrix = new Matrix("1,0,5;5,-4,-6");
            Assert.IsTrue(matrix.Multiply(2).Compare(new Matrix(
                "2,0,10;10,-8,-12")));
        }
    }
}

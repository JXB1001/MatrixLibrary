using MatrixLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixTests
{
    [TestClass]
    public class MatrixToolsTests
    {
        [TestMethod]
        public void SubMatrixTest()
        {
            Matrix matrixA = new Matrix("1,2,3;4,5,6;7,8,9");
            Matrix matrixB = matrixA.GetSubMatrix(1, 2, 0, 1);
            Assert.IsTrue(matrixB.Compare(new Matrix("4,5;7,8")));
        }

        [TestMethod]
        public void RemoveRowTest()
        {
            Matrix matrixA = new Matrix("1,2,3;4,5,6;7,8,9");
            Matrix matrixB = matrixA.RemoveRow(1);
            Assert.IsTrue(matrixB.Compare(new Matrix("1,2,3;7,8,9")));
        }

        [TestMethod]
        public void RemoveColumnTest()
        {
            Matrix matrixA = new Matrix("1,2,3;4,5,6;7,8,9");
            Matrix matrixB = matrixA.RemoveColumn(1);
            Assert.IsTrue(matrixB.Compare(new Matrix("1,3;4,6;7,9")));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixLibrary
{
    public class Matrix
    {
        private int height;
        private int width;
        private int size;
        private double[] data;

        public Matrix(int height, int width)
        {
            this.height = height;
            this.width = width;
            this.size = height * width;
            this.data = new double[this.size];
        }

        public double Get(int y, int x)
        {
            return this.data[y * this.height + x];
        }

        public void Set(int y, int x, double value)
        {
            this.data[y * this.height + x] = value;
        }

        public void Set(double[] oldData)
        {
            Array.Copy(oldData, this.data, this.size);
        }

        public Matrix Copy()
        {
            Matrix newMatrix = new Matrix(this.height, this.width);
            newMatrix.Set(this.data);
            return newMatrix;
        }

        public Matrix Identity()
        {
            Matrix matrix = this.Copy();
            matrix.ApplyToAll((y, x) => {if (y == x) return 1; else return 0;});
            return matrix;
        }

        public Matrix Random()
        {
            Matrix matrix = this.Copy();
            Random random = new Random();
            matrix = matrix.ApplyToAll(() => { return random.NextDouble(); });
            return matrix;
        }

        public Matrix Random(double minimum, double maximum)
        {
            Matrix matrix = this.Copy();
            matrix = matrix.Random();
            matrix = matrix.ApplyToAll((v) => { return v * (maximum - minimum) + minimum; });
            return matrix;
        }

        public Matrix ApplyToAll(Func<double> action)
        {
            Matrix matrix = this.Copy();
            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    matrix.Set(y, x, action());
                }
            }
            return matrix;
        }

        public Matrix ApplyToAll(Func<double, double> action)
        {
            Matrix matrix = this.Copy();
            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    matrix.Set(y, x, action(Get(y, x)));
                }
            }
            return matrix;
        }

        public Matrix ApplyToAll(Func<int, int, double> action)
        {
            Matrix matrix = this.Copy();
            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    matrix.Set(y, x, action(y, x));
                }
            }
            return matrix;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Vector vector in Horizontal())
            {
                stringBuilder.Append(vector.ToString());
                stringBuilder.Append("\n");
            }
            return stringBuilder.ToString();
        }

        public IEnumerable<Vector> Horizontal()
        {
            Vector output = new Vector(this.width);
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    output.Set(x, Get(y, x));
                }
                yield return output;
            }
        }

        public IEnumerable<Vector> Vertical()
        {
            Vector output = new Vector(this.height);
            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    output.Set(y, Get(y, x));
                }
                yield return output;
            }
        }
    }
}

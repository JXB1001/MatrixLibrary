using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixLibrary
{
    public class Matrix<T>
    {
        private int height;
        private int width;
        private int size;
        private T[] data;

        public Matrix(int height, int width)
        {
            this.height = height;
            this.width = width;
            this.size = height * width;
            this.data = new T[this.size];
        }

        public T Get(int y, int x)
        {
            return this.data[y * this.height + x];
        }

        public void Set(int y, int x, T value)
        {
            this.data[y * this.height + x] = value;
        }

        public void Set(T[] oldData)
        {
            Array.Copy(oldData, this.data, this.size);
        }

        public Matrix<T> Copy()
        {
            Matrix<T> newMatrix = new Matrix<T>(this.height, this.width);
            newMatrix.Set(this.data);
            return newMatrix;
        }

        public Matrix<T> Identity()
        {
            Matrix<T> matrix = this.Copy();
            matrix.ApplyToAll((y, x) => {if (y == x) return Calculator<T>.ToType(1); else return Calculator<T>.ToType(0);});
            return matrix;
        }

        public Matrix<T> Random()
        {
            Matrix<T> matrix = this.Copy();
            Random random = new Random();
            matrix.ApplyToAll(() => { return Calculator<T>.ToType(random.NextDouble()); });
            return matrix;
        }

        public Matrix<T> Random(double minimum, double maximum)
        {
            Matrix<T> matrix = this.Copy();
            matrix = matrix.Random();
            throw new NotImplementedException();
            return matrix;
        }

        public void ApplyToAll(Func<T> action)
        {
            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    Set(y, x, action());
                }
            }
        }

        public void ApplyToAll(Func<T, T> action)
        {
            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    Set(y, x, action(Get(y, x)));
                }
            }
        }

        public void ApplyToAll(Func<int, int, T> action)
        {
            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    Set(y, x, action(y, x));
                }
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Vector<T> vector in Horizontal())
            {
                stringBuilder.Append(vector.ToString());
                stringBuilder.Append("\n");
            }
            return stringBuilder.ToString();
        }

        public IEnumerable<Vector<T>> Horizontal()
        {
            Vector<T> output = new Vector<T>(this.width);
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    output.Set(x, Get(y, x));
                }
                yield return output;
            }
        }

        public IEnumerable<Vector<T>> Vertical()
        {
            Vector<T> output = new Vector<T>(this.height);
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

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MatrixLibrary
{
    public class Matrix : IEnumerable, IEquatable<Matrix>
    {
        private int height;
        private int width;
        private int size;
        private double[] data;

        public Matrix(Tuple<int, int> dimensions) : this(dimensions.Item1, dimensions.Item2) { }

        public Matrix(int height, int width)
        {
            this.height = height;
            this.width = width;
            this.size = height * width;
            this.data = new double[this.size];
        }

        public double this[int y, int x]
        {
            get => this.data[y * this.height + x];
            set => this.data[y * this.height + x] = value;
        }

        public Matrix this[string y, int x]
        {
            get
            {
                Matrix vector = new Matrix(this.height, 1);
                for(int i = 0; i < this.height; i++)
                {
                    vector[i] = this[i, x];
                }
                return vector;
            }

            set
            {
                for (int i = 0; i < this.height; i++)
                {
                    this[i, x] = value[i];
                }
            }
        }

        public Matrix this[int y, string x]
        {
            get
            {
                Matrix vector = new Matrix(1, this.width);
                for (int i = 0; i < this.height; i++)
                {
                    vector[i] = this[y, i];
                }
                return vector;
            }

            set
            {
                for (int i = 0; i < this.width; i++)
                {
                    this[y, i] = value[i];
                }
            }
        }

        public double this[int i]
        {
            get
            {
                if (this.IsVector())
                {
                    return this.data[i];
                }
                else
                    throw new ArgumentException("You can only access values with a single index in a vector");

            }

            set
            {
                if (this.IsVector())
                {
                    this.data[i] = value;
                }
                else
                    throw new ArgumentException("You can only set values with a single index in a vector");
            }
        }

        public bool IsVector()
        {
            if ((this.width == 1) || (this.height == 1))
                return true;
            else
                return false;
        }

        private void Set(double[] oldData)
        {
            Array.Copy(oldData, this.data, this.size);
        }

        public Matrix Add(Matrix m)
        {
            if(!this.Size().Equals(m.Size()))
            {
                throw new ArgumentException($"Cannot add matrixes of size {this.Size()} and {m.Size()}");
            }
            Matrix result = new Matrix(m.Size());
            return result.ApplyToAll((y, x) => { return this[y, x] + m[y, x]; });
        }

        public Matrix Subtract(Matrix m)
        {
            if (!this.Size().Equals(m.Size()))
            {
                throw new ArgumentException($"Cannot add matrixes of size {this.Size()} and {m.Size()}");
            }
            Matrix result = new Matrix(m.Size());
            return result.ApplyToAll((y, x) => { return this[y, x] - m[y, x]; });
        }

        public Matrix Multiply(Matrix m)
        {
            if(this.width != m.height)
                throw new ArgumentException($"Cannot add matrixes of size {this.Size()} and {m.Size()}");
            Matrix result = new Matrix(this.height, m.width);
            result = result.ApplyToAll((y, x) => { return this[y, ""].Dot(m["", x]); });
            return result;
        }

        public double Dot(Matrix m)
        {
            if(!this.IsVector() || !m.IsVector())
                throw new ArgumentException("Can only perform this operation on two vectors");
            if((this.height != m.width)||(this.height != m.width))
                throw new ArgumentException($"Cannot add matrixes of size {this.Size()} and {m.Size()}");

            double sum = 0;
            IEnumerator thisE = this.GetEnumerator();
            IEnumerator mE = m.GetEnumerator();
            while(thisE.MoveNext() && mE.MoveNext())
            {
                sum += ((double)thisE.Current*(double)mE.Current);
            }
            return sum;
        }

        public Matrix Transpose()
        {
            Matrix result = new Matrix(this.width, this.height);
            result = result.ApplyToAll((y, x) => { return this[x, y]; });
            return result;
        }

        public Tuple<int, int> Size()
        {
            return Tuple.Create<int, int>(this.height, this.width);
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
            matrix = matrix.ApplyToAll((y, x) => {if (y == x) return 1; else return 0;});
            return matrix;
        }

        public Matrix Random()
        {
            Matrix matrix = this.Copy();
            Random random = new Random();
            matrix = matrix.ApplyToAll(() => { return random.NextDouble(); });
            return matrix;
        }

        public Matrix CountUp()
        {
            Matrix matrix = this.Copy();
            int count = 0;
            matrix = matrix.ApplyToAll(() => { return (double)++count; });
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
                    matrix[y, x] = action();
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
                    matrix[y, x] = action(this[y, x]);
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
                    matrix[y, x] = action(y, x);
                }
            }
            return matrix;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string tempString;
            foreach (Matrix row in this.Horizontal())
            {
                foreach (double l in row)
                {
                    tempString = String.Format("{0:F3}", l);
                    stringBuilder.Append($"{tempString,-7}");
                }
                stringBuilder.Append("\n");
            }
            return stringBuilder.ToString();
        }

        public IEnumerable<Matrix> Horizontal()
        {
            for (int y = 0; y < this.height; y++)
            {
                yield return this[y, ""];
            }
        }

        public IEnumerable<Matrix> Vertical()
        {
            for (int x = 0; x < this.width; x++)
            {      
                yield return this["",x];
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        public bool Equals(Matrix other)
        {
            return this.data.Equals(other.data);
        }
    }
}

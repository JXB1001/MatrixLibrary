using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MatrixLibrary
{
    public class Matrix : IEnumerable
    {
        private int height = 0;
        private int width;
        private int size;
        private double[] data;

        public Matrix(string intial)
        {
            List<string[]> values = getValues(intial);
            Tuple<int, int> dimensions = getDimenstions(values);
            setMatrixProperties(dimensions.Item1, dimensions.Item2);
            setMatrixValues(values);
        }


        private List<string[]> getValues(string intial)
        {
            string[] rows = intial.Split(';');
            List<string[]> values = new List<string[]>();
            foreach (string row in rows)
            {
                values.Add(row.Split(','));
            }
            return values;
        }

        private Tuple<int, int> getDimenstions(List<string[]> values)
        {
            int width = values[0].Length;
            foreach(string[] row in values)
            {
                if (row.Length != width)
                    throw new ArgumentException("All rows in the matrix must have the same length");
            }
            return Tuple.Create(values.Count, width);
        }

        private void setMatrixValues(List<string[]> values)
        {
            int count = 0;
            foreach(string[] row in values)
            {
                foreach(string stringValue in row)
                {
                    if (!Double.TryParse(stringValue, out this.data[count++]))
                        throw new ArgumentException($"Unable to parse value:{stringValue}");
                }
            }
        }

        public Matrix(Tuple<int, int> dimensions) : this(dimensions.Item1, dimensions.Item2) { }

        public Matrix(int height, int width)
        {
            setMatrixProperties(height, width);
        }

        private void setMatrixProperties(int height, int width)
        {
            this.height = height;
            this.width = width;
            this.size = height * width;
            this.data = new double[this.size];
        }

        public double this[int y, int x]
        {
            get => this.data[y*this.width + x];
            set => this.data[y*this.width + x] = value;
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
                for (int i = 0; i < this.width; i++)
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

        public Matrix Multiply(int scalar)
        {
            Matrix result = this.Copy();
            result = result.ApplyToAll((v) => { return v*scalar; });
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
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
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

        public bool Compare(Matrix matrix)
        {
            return Compare(matrix, 0.00001);
        }

        public bool Compare(Matrix matrix, double precision)
        {
            if (!this.Size().Equals(matrix.Size()))
            {
                throw new ArgumentException($"Cannot compare matrices of size {this.Size()} and {matrix.Size()}");
            }
            IEnumerator thisEnumerator = this.GetEnumerator();
            IEnumerator matrixEnumerator = matrix.GetEnumerator();
            while(thisEnumerator.MoveNext() && matrixEnumerator.MoveNext())
            {
                if (Math.Abs((double)thisEnumerator.Current - (double)matrixEnumerator.Current) > precision)
                    return false;
            }
            return true;
        }
    }
}

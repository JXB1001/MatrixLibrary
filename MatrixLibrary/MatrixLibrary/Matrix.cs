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
        private long[] data;

        public Matrix(int height, int width)
        {
            this.height = height;
            this.width = width;
            this.size = height * width;
            this.data = new long[this.size];
        }


        public long Get(int y, int x)
        {
            return this.data[y * this.height + x];
        }

        public void Set(int y, int x, long value)
        {
            this.data[y * this.height + x] = value;
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
    }
}

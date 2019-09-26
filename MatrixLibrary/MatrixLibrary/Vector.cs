using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MatrixLibrary
{
    public class Vector
    {
        private int size;
        private long[] data;

        public Vector(int size)
        {
            this.size = size;
            this.data = new long[this.size];
        }

        public long Get(int i)
        {
            return this.data[i];
        }

        public void Set(int i, long value)
        {
            this.data[i] = value;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (long l in this.data)
            {
                stringBuilder.Append($"{l:10}");
            }
            return stringBuilder.ToString();
        }
    }
}

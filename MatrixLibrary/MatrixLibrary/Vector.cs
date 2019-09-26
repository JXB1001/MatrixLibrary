using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MatrixLibrary
{
    public class Vector<T>
    {
        private int size;
        private T[] data;

        public Vector(int size)
        {
            this.size = size;
            this.data = new T[this.size];
        }

        public T Get(int i)
        {
            return this.data[i];
        }

        public void Set(int i, T value)
        {
            this.data[i] = value;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (T l in this.data)
            {
                stringBuilder.Append($"{l,10}");
            }
            return stringBuilder.ToString();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MatrixLibrary
{
    public class Vector
    {
        private int size;
        private double[] data;

        public Vector(int size)
        {
            this.size = size;
            this.data = new double[this.size];
        }

        public double Get(int i)
        {
            return this.data[i];
        }

        public void Set(int i, double value)
        {
            this.data[i] = value;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string tempString;
            foreach (double l in this.data)
            {
                tempString = String.Format("{0:F3}", l);
                stringBuilder.Append($"{tempString,10}");
            }
            return stringBuilder.ToString();
        }
    }
}

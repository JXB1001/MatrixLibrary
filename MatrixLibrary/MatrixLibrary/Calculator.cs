using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixLibrary
{
    internal static class Calculator<T>
    {
        private static readonly List<Type> SupportedTypes = new List<Type>()
        {
            typeof(int), typeof(float), typeof(double)
        };

        public static bool TypeSupported(Type type)
        {
            return SupportedTypes.Contains(type);
        }

        public static T One()
        {
            return ToType(1);
        }

        public static T Zero()
        {
            return ToType(0);
        }

        public static T ToType(int number)
        {
            if (!TypeSupported(typeof(T)))
                throw new ArgumentException("Type Not Supported");

            return (T)Convert.ChangeType(number, typeof(T));
        }

        public static T ToType(float number)
        {
            if (!TypeSupported(typeof(T)))
                throw new ArgumentException("Type Not Supported");

            return (T)Convert.ChangeType(number, typeof(T));
        }

        public static T ToType(double number)
        {
            if (!TypeSupported(typeof(T)))
                throw new ArgumentException("Type Not Supported");

            return (T)Convert.ChangeType(number, typeof(T));
        }
    }
}

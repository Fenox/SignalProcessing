using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalGeneration.Common
{
    public class ArrayUtils
    {
        public static double[] Mult(double[] a1, double[] a2)
        {
            double[] result = new double[a1.Length];
            for (int i = 0; i < a1.Length; i++)
            {
                result[i] = a1[i] * a2[i];
            }

            return result;
        }

        public static double[] Add(double[] a, double[] b)
        {
            double[] result = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] + b[i];
            }

            return result;
        }

        public static double[] Sub(double[] a, double[] b)
        {
            double[] result = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] - b[i];
            }

            return result;
        }
    }
}

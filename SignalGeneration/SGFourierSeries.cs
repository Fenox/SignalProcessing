﻿using System;
using System.Collections.Generic;

namespace SignalGeneration
{
    public class SGTimeFourierSeries : ISGTimeContinousSignalSource<PointContinous1D, PointContinous1D, double>
    {
        private double a0;
        private List<double> a;
        private List<double> b;

        public SGTimeFourierSeries(double a0, List<double> a, List<double> b)
        {
            if (a.Count != b.Count)
                throw new ArgumentException("Length of a must be equal to b");

            this.a0 = a0;
            this.a = a;
            this.b = b;
        }

        public PointContinous1D ValueAt(PointContinous1D time)
        {
            double value = a0 / 2;

            for (int i = 0; i < a.Count; i++)
            {
                value += a[i] * Math.Cos((i + 1) * time.X) + b[i] * Math.Sin((i + 1) * time.X);
            }

            return new PointContinous1D() { X = value };
        }
    }
}

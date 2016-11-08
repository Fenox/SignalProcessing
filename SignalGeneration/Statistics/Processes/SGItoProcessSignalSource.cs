using SignalGeneration.Common;
using SignalGeneration.SignalProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalGeneration.Statistics.Processes
{
    public class SGItoProcessSignalSource : ISGContinousSignalSource<Point1DDiscrete, Point<double>, int>
    {
        List<Point<double>> Values = new List<Point<double>>();

        private int dimensions;
        List<SGDForwarderivativeProcessor> wienerProcessDeltas = new List<SGDForwarderivativeProcessor>();
        public double TimeDelta { get; private set; }
        Func<double[], double, double[]> a;
        Func<double[], double, double[]> b;
        

        public SGItoProcessSignalSource(int dim, double timeDelta, Func<double[], double, double[]> a, Func<double[], double, double[]> b, double[] startValue)
        {
            TimeDelta = timeDelta;
            this.a = a;
            this.b = b;

            Values.Add(new Point<double>(dim) { Values = startValue });

            dimensions = dim;
            for (int i = 0; i < dim; i++)
            {
                var wp = new SGWienerProcessSignalSource(timeDelta);
                var wpd = new SGDForwarderivativeProcessor(wp);
                wienerProcessDeltas.Add(wpd);
            }
        }

        public Point<double> ValueAt(Point1DDiscrete position)
        {
            if (position.X >= Values.Count)
                AddValuesTill(position);

            return new Point<double>(dimensions) { Values = Values[position.X].Values };
        }

        public void AddValuesTill(Point1DDiscrete position)
        {
            if (position.X == 0)
                return;

            while (Values.Count <= position.X)
            {
                double[] newDelta = new double[dimensions];

                double[] Xt = Values[position.X - 1].Values;

                for (int i = 0; i < dimensions; i++)
                    newDelta[i] = a(Xt, position.X)[i] * TimeDelta + b(Xt, TimeDelta)[i] * wienerProcessDeltas[i].ValueAt(position).Values[0];
                
                var newPoint = new Point<double>(dimensions) { Values = ArrayUtils.Add(newDelta, Xt) };
                Values.Add(newPoint);
            }
        }
    }

    public class SGBrownianMotionSignalSource : SGItoProcessSignalSource
    {
        public SGBrownianMotionSignalSource(int dim, double timeDelta, double[] drift, double[] vola) : 
            base(dim,
                timeDelta, 
                new Func<double[], double, double[]>((a, b) => ArrayUtils.Mult(drift, a)), 
                new Func<double[], double, double[]>((a, b) => ArrayUtils.Mult(vola, a)),
                new double[] { 10 })
        {

        }
    }
}

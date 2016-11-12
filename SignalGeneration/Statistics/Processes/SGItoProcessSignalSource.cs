using SignalGeneration.Common;
using SignalGeneration.SignalProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalGeneration.Statistics.Processes
{
    public class SGItoProcessSignalSource : ISGDiscreteSignalSource<Point<int>, PointDouble, double>
    {
        private readonly List<Point<double>> _values = new List<Point<double>>();

        private readonly int _dimensions;
        private readonly List<SGDForwarderivativeProcessor> _wienerProcessDeltas = new List<SGDForwarderivativeProcessor>();
        private readonly Func<double[], double, double[]> _a;
        private readonly Func<double[], double, double[]> _b;

        public double TimeDelta { get; }

        public SGItoProcessSignalSource(int dim, double timeDelta, Func<double[], double, double[]> a, Func<double[], double, double[]> b, double[] startValue)
        {
            TimeDelta = timeDelta;
            _a = a;
            _b = b;

            _values.Add(new Point<double>(dim) { Values = startValue });

            _dimensions = dim;
            for (int i = 0; i < dim; i++)
            {
                var wp = new SGWienerProcessSignalSource(timeDelta);
                var wpd = new SGDForwarderivativeProcessor(wp);
                _wienerProcessDeltas.Add(wpd);
            }
        }

        public PointDouble ValueAt(Point<int> position)
        {
            if (position.Values[0] >= _values.Count)
                AddValuesTill(position);

            return new PointDouble(_dimensions) { Values = _values[position.Values[0]].Values };
        }

        public void AddValuesTill(Point<int> position)
        {
            if (position.Values[0] == 0)
                return;

            while (_values.Count <= position.Values[0])
            {
                double[] newDelta = new double[_dimensions];

                double[] Xt = _values[position.Values[0] - 1].Values;

                for (int i = 0; i < _dimensions; i++)
                    newDelta[i] = _a(Xt, position.Values[0])[i] * TimeDelta + _b(Xt, TimeDelta)[i] * _wienerProcessDeltas[i].ValueAt(position).Values[0];
                
                var newPoint = new Point<double>(_dimensions) { Values = ArrayUtils.Add(newDelta, Xt) };
                _values.Add(newPoint);
            }
        }
    }

    public class SGBrownianMotionSignalSource : SGItoProcessSignalSource
    {
        public SGBrownianMotionSignalSource(int dim, double timeDelta, double[] drift, double[] vola) : 
            base(dim,
                timeDelta, 
                (a, b) => ArrayUtils.Mult(drift, a), 
                (a, b) => ArrayUtils.Mult(vola, a),
                new double[] { 10 })
        {

        }
    }
}

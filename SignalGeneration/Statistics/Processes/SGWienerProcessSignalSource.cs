using SignalGeneration.Probability.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalGeneration
{
    public class SGWienerProcessSignalSource : ISGDiscreteSignalSource<Point<int>, PointDouble, double>
    {
        private readonly Random _rand;

        private readonly List<double> _wienerProcess = new List<double>();

        public double TimeResolution { get; set; }

        private SGWienerProcessSignalSource() { }

        public SGWienerProcessSignalSource(double timeResolution, int seed)
        {
            _wienerProcess.Add(0);
            TimeResolution = timeResolution;
            _rand = new Random(seed);
        }

        public SGWienerProcessSignalSource(double timeResolution)
        {
            _wienerProcess.Add(0);
            TimeResolution = timeResolution;
            _rand = new Random();
        }

        public PointDouble ValueAt(Point<int> position)
        {
            if (position.Values[0] >= _wienerProcess.Count)
                AddValuesTill(position.Values[0]);

            return new PointDouble(1) { Values = new double[] { _wienerProcess[position.Values[0]] } };
        }

        private void AddValuesTill(int pos)
        {
            int valuesToAdd = pos - _wienerProcess.Count + 1;
            int currentNum = _wienerProcess.Count;
            
            for(int i = currentNum - 1; i < currentNum + valuesToAdd; i++)
            {
                _wienerProcess.Add(_wienerProcess[i] + _rand.NormalDistributetRandPolarMethod(0, TimeResolution));
            }
        }
    }
}

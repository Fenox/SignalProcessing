using SignalGeneration.Probability.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalGeneration
{
    public class SGWienerProcessSignalSource : ISGContinousSignalSource<Point<int>, PointDouble, int>
    {
        Random rand;

        List<double> wienerProcess = new List<double>();

        public double TimeResolution { get; set; }

        private SGWienerProcessSignalSource() { }

        public SGWienerProcessSignalSource(double timeResolution, int seed)
        {
            wienerProcess.Add(0);
            TimeResolution = timeResolution;
            rand = new Random(seed);
        }

        public SGWienerProcessSignalSource(double timeResolution)
        {
            wienerProcess.Add(0);
            TimeResolution = timeResolution;
            rand = new Random();
        }

        public PointDouble ValueAt(Point<int> position)
        {
            if (position.Values[0] >= wienerProcess.Count)
                AddValuesTill(position.Values[0]);

            return new PointDouble(1) { Values = new double[] { wienerProcess[position.Values[0]] } };
        }

        private void AddValuesTill(int pos)
        {
            int valuesToAdd = pos - wienerProcess.Count + 1;
            int currentNum = wienerProcess.Count;
            
            for(int i = currentNum - 1; i < currentNum + valuesToAdd; i++)
            {
                wienerProcess.Add(wienerProcess[i] + rand.NormalDistributetRandPolarMethod(0, TimeResolution));
            }
        }
    }
}

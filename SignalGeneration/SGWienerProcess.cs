using SignalGeneration.Probability.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalGeneration
{
    public class SGWienerProcess : ISGDiscreteSignalSource<Point1DDiscrete>
    {
        Random rand;

        List<double> wienerProcess = new List<double>();

        public double TimeResolution { get; set; }

        private SGWienerProcess() { }

        public SGWienerProcess(double timeResolution, int seed)
        {
            wienerProcess.Add(0);
            TimeResolution = timeResolution;
            rand = new Random(seed);
        }

        public double ValueAt(Point1DDiscrete position)
        {
            if (position.X >= wienerProcess.Count)
                AddValuesTill(position.X);

            return wienerProcess[position.X];
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

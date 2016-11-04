using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalGeneration.Probability.Distributions
{
    public static class RandomExtensions
    {
        public static double NormalDistributetRandPolarMethod(this Random rand, double mean, double variance)
        {
            double q = 0;
            double rand1;
            do
            {
                rand1 = rand.NextDouble() * 2 - 1;
                double rand2 = rand.NextDouble() * 2 - 1;
                q = rand1 * rand1 + rand2 * rand2;
            } while (q == 0 || q >= 1);

            double p = Math.Sqrt((-2 * Math.Log(q)) / q);
            double x = rand1 * p;
            return Math.Sqrt(variance) * x + mean;
        }
    }
}

using System;
using SignalGeneration.SignalProcessors.Convolution;

namespace SignalGeneration.SignalProcessors
{
    public class SGHistoricVolatilityProcessor : ISignalProcessor<ISGTimeDiscreteSignalSource<Point<int>, PointDouble, double>, double>
    {
        public int NumStepsBack { get; set; }
        public int LastValueIndex { get; set; }

        public SGHistoricVolatilityProcessor(int numStepsBack, int lastValueIndex)
        {
            NumStepsBack = numStepsBack;
            LastValueIndex = lastValueIndex;
        }

        public double Process(ISGTimeDiscreteSignalSource<Point<int>, PointDouble, double> source)
        {
            double vola = 0;

            double average = 0;
            for (int i = 0; i < NumStepsBack; i++)
            {
                int indexInner = LastValueIndex - NumStepsBack + i;
                double valAtPosInner = source.ValueAt(new Point<int>(1) { Values = new[] { indexInner } }).Values[0];
                average += valAtPosInner;
            }

            average = average/(NumStepsBack - 1);

            for (int i = 0; i < NumStepsBack; i++)
            {
                int index = LastValueIndex - NumStepsBack + i;
                double valAtPos = source.ValueAt(new Point<int>(1) { Values = new[] { index } }).Values[0];
                
                vola += (valAtPos - average) * (valAtPos - average);
            }

            return vola / (NumStepsBack - 1);
        }

        public double GetVariance(ISGTimeDiscreteSignalSource<Point<int>, PointDouble, double> source)
        {
            return Process(source) / NumStepsBack;
        }

        public double GetVola(ISGTimeDiscreteSignalSource<Point<int>, PointDouble, double> source, double deltaTime)
        {
            return Math.Sqrt(Process(source) / deltaTime) / NumStepsBack;
        }
    }
}

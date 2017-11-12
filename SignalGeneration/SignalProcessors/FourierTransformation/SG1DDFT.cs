using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SignalGeneration.SignalProcessors.Convolution;

namespace SignalGeneration.SignalProcessors.FourierTransformation
{
    public class SG1DDFT : ISignalProcessor<ISGTimeDiscreteSignalSource<Point1DDiscrete, PointContinous1D, double>, SG1DTimeDiscreteValueContinousSignalSource>
    {
        public int SignalLength { get; set; }

        public SG1DDFT(int signalLength)
        {
            this.SignalLength = signalLength;
        }


        public SG1DTimeDiscreteValueContinousSignalSource Process(ISGTimeDiscreteSignalSource<Point1DDiscrete, PointContinous1D, double>source)
        {
            var output = new SG1DTimeDiscreteValueContinousSignalSource();
            for (int i = 0; i < SignalLength; i++)
            {
                output.PointContinous1Ds.Add(new PointContinous1D( TransformAt(i, source)));
            }

            return output;
        }

        private double TransformAt(int pos, ISGTimeDiscreteSignalSource<Point1DDiscrete, PointContinous1D, double> source)
        {
            double real = 0;
            double img = 0;
            for (int i = 0; i < SignalLength; i++)
            {
                double valAtI = source.ValueAt(new Point1DDiscrete(i)).X;
                real += valAtI * Math.Cos(2*Math.PI*i*pos/SignalLength);
                img -= valAtI * Math.Sin(2*Math.PI*i*pos/SignalLength);
            }

            return real*real + img*img;
        }
    }

    public class SG1DTimeDiscreteValueContinousSignalSource :
        ISGTimeDiscreteSignalSource<Point1DDiscrete, PointContinous1D, double>
    {
        public List<PointContinous1D> PointContinous1Ds { get; set; } = new List<PointContinous1D>();

        public PointContinous1D ValueAt(Point1DDiscrete position)
        {
            return PointContinous1Ds[position.X];
        }
    }
}

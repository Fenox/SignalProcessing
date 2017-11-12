using System;
using GalaSoft.MvvmLight;
using OxyPlot;
using SignalGeneration;
using System.Collections.Generic;
using SignalGeneration.SignalProcessors.FourierTransformation;

namespace SignalGeneratorTestViewer.ViewModel
{
    public sealed class FourierViewModel : ViewModelBase, IControlViewModel
    {
        public string Name => "Fourier View Model";
        
        public IList<DataPoint> Points { get; set; } = new List<DataPoint>();

        public IList<DataPoint> TransformedPoints { get; set; } = new List<DataPoint>();

        public FourierViewModel()
        {
            CreatePoints();
            CreateTransformedPoints();

            RaisePropertyChanged("Points");
        }

        private void CreatePoints()
        {
            var fourierSeries = new SGTimeFourierSeries(0, new List<double> { 1, 0, 0, 0, 0, 0, 0 }, new List<double> { 0, 0, 0, 0, 0, 0, 0 });

            for (int i = 0; i < 2 * Math.PI * 10; i++)
            {
                Points.Add(new DataPoint(i, fourierSeries.ValueAt(new PointContinous1D() { X = i }).X));
            }
        }

        private void CreateTransformedPoints()
        {
            //Convert DataPoint to SignalSource
            var points = ConvertFrom(Points);

            SG1DDFT dft = new SG1DDFT(points.PointContinous1Ds.Count);
            var transformedPoints = dft.Process(points);
            TransformedPoints = Convert(transformedPoints);
        }


        private static IList<DataPoint> Convert(SG1DTimeDiscreteValueContinousSignalSource points)
        {
            IList<DataPoint> newPoints = new List<DataPoint>();

            for (int i = 0; i < points.PointContinous1Ds.Count; i++)
            {
                newPoints.Add(new DataPoint(i, points.ValueAt(new Point1DDiscrete(i)).X));
            }

            return newPoints;
        }

        private static SG1DTimeDiscreteValueContinousSignalSource ConvertFrom(IList<DataPoint> points)
        {
            var output = new SG1DTimeDiscreteValueContinousSignalSource();
            for (int i = 0; i < points.Count; i++)
            {
                output.PointContinous1Ds.Add(new PointContinous1D(points[i].X));
            }
            return output;
        }
    }
}

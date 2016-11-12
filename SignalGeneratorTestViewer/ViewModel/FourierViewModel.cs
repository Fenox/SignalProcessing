using GalaSoft.MvvmLight;
using OxyPlot;
using SignalGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalGeneratorTestViewer.ViewModel
{
    public sealed class FourierViewModel : ViewModelBase, IControlViewModel
    {
        public string Name => "Fourier View Model";
        
        public IList<DataPoint> Points { get; set; } = new List<DataPoint>();

        public FourierViewModel()
        {
            int numPoints = 100;
            var fourierSeries = new SGFourierSeries(0, new List<double> { 1, 0, 0, 0, 0, 0, 0.3 }, new List<double> { 0, 0, 0, 0, 0, 0, 0 });
             
            for (int i = 0; i < numPoints; i++)
            {
                Points.Add(new DataPoint(i * 0.1, fourierSeries.ValueAt(new PointContinous1D() { X = i * 0.1 }).X));
            }

            RaisePropertyChanged("Points");
        }
    }
}

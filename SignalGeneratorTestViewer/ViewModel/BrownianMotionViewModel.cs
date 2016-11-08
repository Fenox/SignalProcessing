using GalaSoft.MvvmLight;
using OxyPlot;
using SignalGeneration;
using SignalGeneration.Statistics.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalGeneratorTestViewer.ViewModel
{
    public class BrownianMotionViewModel : ViewModelBase, IControlViewModel
    {
        public string Name
        {
            get
            {
                return "Brownian Motion";
            }
        }

        public IList<DataPoint> Points { get; set; } = new List<DataPoint>();

        public BrownianMotionViewModel()
        {
            SGBrownianMotionSignalSource wp = new SGBrownianMotionSignalSource(1, 0.1, new double[] { 0.05 }, new double[] { 0.05 });

            for (int i = 0; i < 1000; i++)
            {
                Points.Add(new DataPoint(wp.TimeDelta * i, wp.ValueAt(new Point1DDiscrete() { X = i }).Values[0]));
            }

            RaisePropertyChanged("Points");
        }
    }
}

using GalaSoft.MvvmLight;
using OxyPlot;
using SignalGeneration;
using System.Collections.Generic;

namespace SignalGeneratorTestViewer.ViewModel
{
    public sealed class WienerProzessViewModel : ViewModelBase, IControlViewModel
    {
        public string Name => "Wiener Prozess";
        public IList<DataPoint> Points { get; set; } = new List<DataPoint>();

        public WienerProzessViewModel()
        {
            IsgTimeWienerProcessSignalSource wp = new IsgTimeWienerProcessSignalSource(0.1, 1);

            for (int i = 0; i < 100; i++)
            {
                Points.Add(new DataPoint(0.1 * i, wp.ValueAt(new Point1DDiscrete() { X = i }).Values[0]));
            }

            RaisePropertyChanged("Points");
        }
    }
}

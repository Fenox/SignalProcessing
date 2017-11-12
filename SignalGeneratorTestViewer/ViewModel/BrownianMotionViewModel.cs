using GalaSoft.MvvmLight;
using OxyPlot;
using SignalGeneration;
using SignalGeneration.SignalProcessors;
using SignalGeneration.Statistics.Processes;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;

namespace SignalGeneratorTestViewer.ViewModel
{
    public sealed class BrownianMotionViewModel : ViewModelBase, IControlViewModel
    {
        public string Name => "Brownian Motion";

        private string _volatility;
        public string Volatility
        {
            get { return _volatility; }
            set
            {
                _volatility = value;
                RaisePropertyChanged("Volatility");
            }
        }

        private string _variance;
        public string Variance
        {
            get { return _variance; }
            set
            {
                _variance = value;
                RaisePropertyChanged("Variance");
            }
        }

        private double _vola = 0.2;
        public double Vola
        {
            get { return _vola; }
            set
            {
                _vola = value;
                RaisePropertyChanged("Vola");
            }
        }

        private double _mean = 0.05;
        public double Mean
        {
            get { return _mean; }
            set
            {
                _mean = value;
                RaisePropertyChanged("Mean");
            }
        }

        private int _steps = 100;
        public int Steps
        {
            get { return _steps; }
            set
            {
                _steps = value;
                RaisePropertyChanged("Steps");
            }
        }

        public RelayCommand NewRandomProcess { get; set; }


        public IList<DataPoint> Points { get; set; } = new List<DataPoint>();

        public BrownianMotionViewModel()
        {
            CreateNewProcess();
            NewRandomProcess = new RelayCommand(CreateNewProcess);
        }

        private void CreateNewProcess()
        {
            Points = new List<DataPoint>();
            var wp = new IsgTimeBrownianMotionSignalSource(1, 0.1, new[] { Mean }, new[] { Vola });

            for (int i = 0; i < Steps; i++)
            {
                Points.Add(new DataPoint(wp.TimeDelta * i, wp.ValueAt(new Point1DDiscrete() { X = i }).Values[0]));
            }

            RaisePropertyChanged("Points");

            SGHistoricVolatilityProcessor volaProcessor = new SGHistoricVolatilityProcessor(Steps / 2, Steps);
            Variance = volaProcessor.GetVariance(wp).ToString("F3");
            Volatility = volaProcessor.GetVola(wp, wp.TimeDelta).ToString("F3");
        }
    }
}

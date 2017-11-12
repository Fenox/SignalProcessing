using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight;

namespace SignalGeneratorTestViewer.ViewModel
{
    public class OptionViewModel : ViewModelBase, IControlViewModel
    {
        public string Name => "Options";

        public int Interest { get; set; } = 5;
        public float UnderlyingVola { get; set; } = 0.2f;
        public int UnderlyingStartValue { get; set; } = 100;
        public int OptionExercisePrice { get; set; } = 110;
        public float OptionEndTime { get; set; } = 1;
        private double D1 => (Math.Log((float)UnderlyingStartValue / OptionExercisePrice) +
                             (Interest + (UnderlyingVola * UnderlyingVola) / 2) * (OptionEndTime)) /
                            (UnderlyingVola * Math.Sqrt(OptionEndTime));

        public double OptionPrice => UnderlyingStartValue*StandardNormalDistribution(D1) -
                                     OptionEndTime*Math.Exp(-Interest*OptionEndTime)*
                                     StandardNormalDistribution(D1 - UnderlyingVola*OptionEndTime);

       
        public OptionViewModel()
        {
        }

        private static double StandardNormalDistribution(double x)
        {
            return 1.0/Math.Sqrt(2*Math.PI)*Math.Exp(-0.5*x*x);
        }


    }
}

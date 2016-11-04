using SignalGeneration.SignalProcessors.Convolution;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SignalGeneration.SignalProcessors
{
    public interface ISignalProcessor<T, U>
    {
        T Process(U source);
    }

    

    public class SGGausfilter : SGImageConvolution
    {
        public SGGausfilter()
        {
            double[,] conv = new double[,] { { 1.0/16, 2.0/16, 1.0/16 }, { 2.0/16, 4.0/16, 2.0 /16}, { 1.0/16, 2.0/16, 1.0/16 } };
            ConvolutionMatrix = conv;
        }
    }

    public class SGPrewitXFilter : SGImageConvolution
    {
        public SGPrewitXFilter()
        {
            double[,] conv = new double[,] { { -1.0, 0.0, 1.0 }, { -1.0, 0.0, 1.0 }, { -1.0, 0.0, 1.0 } };
            ConvolutionMatrix = conv;
        }
    }

    public class SGPrewitYFilter : SGImageConvolution
    {
        public SGPrewitYFilter()
        {
            double[,] conv = new double[,] { { -1.0, -1.0, -1.0 }, { 0.0, 0.0, 0.0 }, { 1.0, 1.0, 1.0 } };
            ConvolutionMatrix = conv;
        }
    }

    public class SGPrewit : ISignalProcessor<SGImageSignalSource, SGImageSignalSource>
    {
        public SGImageSignalSource Process(SGImageSignalSource source)
        {
            SGPrewitXFilter prewitX = new SGPrewitXFilter();
            var resultX = prewitX.Process(source);

            SGPrewitYFilter prewitY = new SGPrewitYFilter();
            return prewitY.Process(resultX);
        }
    }

    public class SGSobelX : SGImageConvolution
    {
        public SGSobelX()
        {
            double[,] conv = new double[,] { { -1.0, 0.0, 1.0 }, { -2.0, 0.0, 2.0 }, { -1.0, 0.0, 1.0 } };
            ConvolutionMatrix = conv;
        }
    }


    public class SGSobelY : SGImageConvolution
    {
        public SGSobelY()
        {
            double[,] conv = new double[,] { { -1.0, -2.0, -1.0 }, { 0.0, 0.0, 0.0 }, { 1.0, 2.0, 1.0 } };
            ConvolutionMatrix = conv;
        }
    }


    public class SGSobel : ISignalProcessor<SGImageSignalSource, SGImageSignalSource>
    {
        public SGImageSignalSource Process(SGImageSignalSource source)
        {
            SGSobelX sobelX = new SGSobelX();
            var resultX = sobelX.Process(source);

            SGSobelY sobleY = new SGSobelY();
            return sobleY.Process(resultX);
        }
    }
}

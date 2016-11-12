using System;
using System.Drawing;

namespace SignalGeneration.SignalProcessors.Convolution
{
    public interface ISignalProcessor<in TIn, out TOut>
    {
        TOut Process(TIn source);
    }

    

    public class SGGausfilter : SGImageConvolution
    {
        public SGGausfilter()
        {
            double[,] conv = { { 1.0/16, 2.0/16, 1.0/16 }, { 2.0/16, 4.0/16, 2.0 /16}, { 1.0/16, 2.0/16, 1.0/16 } };
            ConvolutionMatrix = conv;
        }
    }

    public class SGPrewitXFilter : SGImageConvolution
    {
        public SGPrewitXFilter()
        {
            double[,] conv = { { -1.0, 0.0, 1.0 }, { -1.0, 0.0, 1.0 }, { -1.0, 0.0, 1.0 } };
            ConvolutionMatrix = conv;
        }
    }

    public class SGPrewitYFilter : SGImageConvolution
    {
        public SGPrewitYFilter()
        {
            double[,] conv = { { -1.0, -1.0, -1.0 }, { 0.0, 0.0, 0.0 }, { 1.0, 1.0, 1.0 } };
            ConvolutionMatrix = conv;
        }
    }

    public class SGPrewit : ISignalProcessor<SGImageSignalSource, SGImageSignalSource>
    {
        public SGImageSignalSource Process(SGImageSignalSource source)
        {
            var prewitX = new SGPrewitXFilter();
            SGImageSignalSource resultX = prewitX.Process(source);

            var prewitY = new SGPrewitYFilter();
            return prewitY.Process(resultX);
        }
    }

    public class SGSobelX : SGImageConvolution
    {
        public SGSobelX()
        {
            double[,] conv = { { -1.0, 0.0, 1.0 }, { -2.0, 0.0, 2.0 }, { -1.0, 0.0, 1.0 } };
            ConvolutionMatrix = conv;
        }
    }


    public class SGSobelY : SGImageConvolution
    {
        public SGSobelY()
        {
            double[,] conv = { { -1.0, -2.0, -1.0 }, { 0.0, 0.0, 0.0 }, { 1.0, 2.0, 1.0 } };
            ConvolutionMatrix = conv;
        }
    }


    public class SGSobel : ISignalProcessor<SGImageSignalSource, SGImageSignalSource>
    {
        public SGImageSignalSource Process(SGImageSignalSource source)
        {
            var sobelX = new SGSobelX();
            SGImageSignalSource resultX = sobelX.Process(source);

            var sobleY = new SGSobelY();
            return sobleY.Process(resultX);
        }
    }

    public class SGRoberts : ISignalProcessor<SGImageSignalSource, SGImageSignalSource>
    {
        public SGImageSignalSource Process(SGImageSignalSource input)
        {
            var output = new SGImageSignalSource(input.Image.Width, input.Image.Height);
            int width = input.Image.Width;
            int height = input.Image.Height;
            

            for (int i = 0; i < width - 1; i++)
            {
                for (int j = 0; j < height - 1; j++)
                {
                    //Gradient des Pixels
                    double r, g, b;

                    Color xy = input.Image.GetPixel(i, j);
                    Color xP1yP1 = input.Image.GetPixel(i + 1, j + 1);
                    Color xP1y = input.Image.GetPixel(i + 1, j);
                    Color xyP1 = input.Image.GetPixel(i, j + 1);

                    r = Math.Abs(xy.R - xP1yP1.R) + Math.Abs(xP1y.R - xyP1.R);
                    g = Math.Abs(xy.G - xP1yP1.G) + Math.Abs(xP1y.G - xyP1.G);
                    b = Math.Abs(xy.B - xP1yP1.B) + Math.Abs(xP1y.B - xyP1.B);
                    
                    r = r > 255 ? 255 : r;
                    g = g > 255 ? 255 : g;
                    b = b > 255 ? 255 : b;

                    r = r < 0 ? 0 : r;
                    g = g < 0 ? 0 : g;
                    b = b < 0 ? 0 : b;

                    output.Image.SetPixel(i, j, Color.FromArgb((int)r, (int)g, (int)b));
                }
            }

            return output;
        }
    }

    public class SGLaplaceFilter : SGImageConvolution
    {
        public SGLaplaceFilter()
        {
            ConvolutionMatrix = new double[,] { { 0,1,0 }, { 1,-4,1 }, { 0,1,0 } };
        }
    }
}

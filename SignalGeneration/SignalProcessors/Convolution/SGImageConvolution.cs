using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SignalGeneration.SignalProcessors.Convolution
{
    public class SGImageConvolution : ISignalProcessor<SGImageSignalSource, SGImageSignalSource>
    {
        double[,] _convMatrix;
        public double[,] ConvolutionMatrix
        {
            get
            {
                return _convMatrix;
            }
            set
            {
                if (value.GetLength(0) != value.GetLength(1))
                    throw new ArgumentException("Width of convolution matrix must equal height.");

                if (value.GetLength(0) % 2 == 0)
                    throw new ArgumentException("Width and Height of convolution matrix must not be even.");

                _convMatrix = value;
            }
        }

        public SGImageConvolution() { }

        public SGImageConvolution(double[,] convolutionMatrix)
        {
            ConvolutionMatrix = convolutionMatrix;
        }

        public SGImageSignalSource Process(SGImageSignalSource input)
        {
            var output = new SGImageSignalSource(input.Image.Width, input.Image.Height);
            int width = input.Image.Width;
            int height = input.Image.Height;

            int halvConvSize = ConvolutionMatrix.GetLength(0) / 2;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double r = 0, g = 0, b = 0;

                    //Compute Average Pixel
                    for (int k = -halvConvSize; k < halvConvSize + 1; k++)
                    {
                        for (int l = -halvConvSize; l < halvConvSize + 1; l++)
                        {
                            if ((i + k) < 0 || (j + l) < 0)
                                continue;

                            if (i + k >= width - 1 || j + l >= height - 1)
                                continue;

                            r += input.Image.GetPixel(i + k, j + l).R * ConvolutionMatrix[k + 1, l + 1];
                            g += input.Image.GetPixel(i + k, j + l).G * ConvolutionMatrix[k + 1, l + 1];
                            b += input.Image.GetPixel(i + k, j + l).B * ConvolutionMatrix[k + 1, l + 1];
                        }
                    }

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
}

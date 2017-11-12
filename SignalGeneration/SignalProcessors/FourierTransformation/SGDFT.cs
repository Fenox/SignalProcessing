using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using SignalGeneration.SignalProcessors.Convolution;

namespace SignalGeneration.SignalProcessors.FourierTransformation
{
    public class SGDFT : ISignalProcessor<IsgTimeImageSignalSource, IsgTimeImageSignalSource>
    {
        public IsgTimeImageSignalSource Process(IsgTimeImageSignalSource input)
        {
            IsgTimeImageSignalSource output = new IsgTimeImageSignalSource(input.Image.Width, input.Image.Height);

            for (int k = 0; k < input.Image.Width; k++)
            {
                for (int l = 0; l < input.Image.Height; l++)
                {
                    output.Image.SetPixel(k, l, TransformPixel(k, l, input.Image));
                }
            }
           


            return null;
        }

        private Color TransformPixel(int k, int l, Bitmap image)
        {
            byte dftValue = 0;

            for (int m = 0; m < image.Width; m++)
            {
                for (int n = 0; n < image.Height; n++)
                {
                    dftValue = image.GetPixel(m, n).R;

                    //dftValue = dftValue*Math.Exp(-j*Math.PI*2*((m*k)/image.Width + (n*l)/image.Height));
                }
            }

            return Color.FromArgb(0, dftValue, dftValue, dftValue);
        }
    }
}

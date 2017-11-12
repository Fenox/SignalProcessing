using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using SignalGeneration.SignalProcessors.Convolution;

namespace SignalGeneration.SignalProcessors
{
    /// <summary>
    /// Generates images with the minimum value of square fields in the field.
    /// Particular sqaure sizes can be deleted.
    /// Afterwards the Picture gets reassembled
    /// Restraints:
    /// - Only works on 8Bit greyscale images
    /// - Only square images
    /// </summary>
    public class SGQuadraticImageProcessor : ISignalProcessor<IsgTimeImageSignalSource, IsgTimeImageSignalSource>
    {
        private byte[] minimaMap;
        List<byte[]> minimaInSquare = new List<byte[]>();

        public IsgTimeImageSignalSource Process(IsgTimeImageSignalSource source)
        {
            if (source.Image.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new ArgumentException("Only 8 bit greyscale images accepted.");

            if(source.Image.Width != source.Image.Height)
                throw new ArgumentException("Only square images are accepted");
       
            CalculateMinimaMap(source.Image);

            //Loop over all possible square sizes
            //Start with size 2 (Size 1 is the original image, size 0 does not exist)
            for (int currSquareSize = 2; currSquareSize < source.Image.Width; currSquareSize++)
            {
                CalculateMinimaForSquares(currSquareSize, source.Image.Width);
            }

            //Substrakt the Minimas beginning with the biggest Size
            for (int i = source.Image.Width - 3; i > 1 ; i--)
            {
                SubstraktImgs(minimaInSquare[i], minimaInSquare[i - 1]);
            }

            //Reasemble original Image Addition of all sub-images
            byte[] original = new byte[source.Image.Width];
            for (int i = 0; i < minimaInSquare.Count; i++)
            {
                for (int j = 0; j < original.Length; j++)
                {
                    original[j] += minimaInSquare[i][j];
                }
            }

            IsgTimeImageSignalSource imgSignalSource = new IsgTimeImageSignalSource(original);
            return imgSignalSource;
        }

        private void SubstraktImgs(byte[] img, byte[] imgToSubstraktFrom)
        {
            for (int i = 0; i < img.Length; i++)
            {
                imgToSubstraktFrom[i] -= img[i];
            }
        }

        private void CalculateMinimaMap(Bitmap sourceImage)
        {
            minimaMap = new byte[sourceImage.Width];
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    CalculateMinimaAt(i, j, sourceImage, minimaMap);
                }
            }
        }

        private void CalculateMinimaAt(int i, int j, Bitmap sourceImage, byte[] minimaMap)
        {
            int w = sourceImage.Width;
            int minimaHelpIndexP1X = i == 0 ? 0 : i - 1;
            int minimaHelpIndexP1Y = j;
            int minimaHelpIndexP2X = j == 0 ? 0 : j - 1;
            int minimaHelpIndexP2Y = i;

            byte minimaOld = Math.Min(minimaMap[minimaHelpIndexP1X*w + minimaHelpIndexP1Y],
                minimaMap[minimaHelpIndexP2X*w + minimaHelpIndexP2Y]);
            byte minima = Math.Min(minimaOld, minimaMap[i*w + j]);

            minimaMap[i*w + j] = minimaMap[i*w + j] = minima;
        }

        
        private void CalculateMinimaForSquares(int size, int imageWidth)
        {
            byte[] minima = new byte[imageWidth * imageWidth];
            int possibleSquares = imageWidth;
            for (int i = 0; i < possibleSquares; i++)
            {
                for (int j = 0; j < possibleSquares; j++)
                {
                    minima[i*imageWidth + j] = GetMinimaForSquareAt(size, i, j, imageWidth);
                }
            }

            minimaInSquare.Add(minima);
        }

        private byte GetMinimaForSquareAt(int squareSize, int i, int j, int width)
        {
            int x1 = i;
            int y1 = j;
            int x2 = i + squareSize >= width ? width - 1 : i + squareSize;
            int y2 = j + squareSize >= width ? width - 1 : j + squareSize;

            byte minima1 = minimaMap[x2 * width + y1];
            byte minima2 = minimaMap[x1 * width + y2];
            byte minimaAtPos = minimaMap[x1*width + y1];

            byte minimadelta1 = Math.Min(minima1, minima2);
            return Math.Min(minimadelta1, minimaAtPos);
        }
    }
}

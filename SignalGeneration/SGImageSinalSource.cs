using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace SignalGeneration
{
    public class SGImageSignalSource : ISGDiscreteSignalSource<Point2DDiscrete, Point<int>, int>
    {
        public Bitmap Image { get; set; }

        public SGImageSignalSource(string path)
        {
            Image = new Bitmap(path);
        }

        public SGImageSignalSource(int width, int height)
        {
            Image = new Bitmap(width, height);
        }

        public Point<int> ValueAt(Point2DDiscrete position)
        {
            Color col = Image.GetPixel(position.X, position.Y);

            return new Point<int>(3)
            {
                Values = new int[] { col.R, col.G, col.B }
            };        
        }
    }
}

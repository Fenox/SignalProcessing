using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace SignalGeneration
{
    public class SGImageSignalSource : ISGDiscreteSignalSource<Point2DDicsrete, Color>
    {
        public Bitmap Image { get; set; }

        public SGImageSignalSource(string path)
        {
            Image = new Bitmap(path);
        }

        public Color ValueAt(Point2DDicsrete position)
        {
            return Image.GetPixel(position.X, position.Y);
        }
    }
}

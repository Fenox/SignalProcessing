using System.Drawing;
using System.IO;


namespace SignalGeneration
{
    public class IsgTimeImageSignalSource : ISGTimeDiscreteSignalSource<Point2DDiscrete, Point<int>, int>
    {
        public Bitmap Image { get; set; }

        public IsgTimeImageSignalSource(byte[] image)
        {
            using (var ms = new MemoryStream(image))
            {
                Image = new Bitmap(ms);
            }
        }

        public IsgTimeImageSignalSource(Bitmap image)
        {
            this.Image = image;
        }

        public IsgTimeImageSignalSource(string path)
        {
            Image = new Bitmap(path);
        }

        public IsgTimeImageSignalSource(int width, int height)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalGeneration
{
    public class Point { }

    public class Point1D : Point
    {
        public double X { get; set; }
    }

    public class Point2DDiscrete : Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class Point1DDiscrete : Point
    {
        public int X { get; set; }
    }

    public class Point2DDicsrete : Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    interface ISGDiscreteSignalSource<T, U> where T : Point
    {
        U ValueAt(T position);
    }
}

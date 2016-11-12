using SignalGeneration.Common;

namespace SignalGeneration
{
    public class Point<T>
    {
        public T[] Values { get; set; }
        public int Dimensions { get; set; }

        public Point(int dimensions)
        {
            Dimensions = dimensions;
            Values = new T[dimensions];
        }
    }

    public class PointDouble : Point<double>
    {
        public PointDouble(int dimensions) : base(dimensions)
        {

        }

        public static PointDouble operator +(PointDouble p1, PointDouble p2)
        {
            return new PointDouble(p1.Dimensions)
            {
                Values = ArrayUtils.Add(p1.Values, p2.Values)
            };
        }

        public static PointDouble operator -(PointDouble p1, PointDouble p2)
        {
            return new PointDouble(p1.Dimensions)
            {
                Values = ArrayUtils.Sub(p1.Values, p2.Values)
            };
        }
    }

    public class PointInt : Point<int>
    {
        public PointInt(int dimensions) : base(dimensions) {  }
    }

    public class PointContinous1D : Point<double>
    {
        public PointContinous1D() : base(1) { }
        public PointContinous1D(double x) : base(1)
        {
            X = x;
        }


        public double X
        {
            get { return Values[0]; }
            set { Values[0] = value; }
        }
    }

    public class Point2DDiscrete : Point<int>
    {
        public Point2DDiscrete() : base (2) { }

        public Point2DDiscrete(int x, int y) : base(2)
        {
            X = x;
            Y = y;
        }

        public int X
        {
            get { return Values[0]; }
            set { Values[0] = value; }
        }
        public int Y
        {
            get { return Values[1]; }
            set { Values[1] = value; }
        }
    }

    public class Point1DDiscrete : Point<int>
    {
        public Point1DDiscrete() : base(1) { }

        public Point1DDiscrete(int x) : base(1)
        {
            X = x;
        }
        public int X
        {
            get { return Values[0]; }
            set { Values[0] = value; }
        }
    }

    public interface ISGDiscreteSignalSource<TIn, TOut, TPointOut> 
        : ISGSignalSource<TIn, TOut, int, TPointOut> 
            where TOut : Point<TPointOut>
            where TIn : Point<int> 
    {

    }

    public interface ISGContinousSignalSource<TIn, TOut, TPointOut> 
        : ISGSignalSource<TIn, TOut, double, TPointOut> 
            where TOut : Point<TPointOut>
            where TIn : Point<double>
    {

    }

    public interface ISGSignalSource<in TIn, out TOut, TPointIn, TPointOut>
        where TOut : Point<TPointOut>
        where TIn : Point<TPointIn>
    {
        TOut ValueAt(TIn position);
    }
}

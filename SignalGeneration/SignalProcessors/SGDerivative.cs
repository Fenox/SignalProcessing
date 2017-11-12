using System;

namespace SignalGeneration.SignalProcessors
{
    public class SGDForwarderivativeProcessor : ISGTimeDiscreteSignalSource<Point<int>, PointDouble, double>
    {
        private readonly ISGTimeDiscreteSignalSource<Point<int>, PointDouble, double> _inputSignal;

        public SGDForwarderivativeProcessor(ISGTimeDiscreteSignalSource<Point<int>, PointDouble, double> input)
        {
            _inputSignal = input;
        }

        public PointDouble ValueAt(Point<int> position)
        {
            PointDouble valueAtPos = _inputSignal.ValueAt(position);
            var derivative = new PointDouble(position.Dimensions);

            for(int i = 0; i < position.Values.Length; i++)
            {
                var posP1Arr = new int[valueAtPos.Dimensions];
                Array.Copy(position.Values, posP1Arr, valueAtPos.Values.Length);
                posP1Arr[i] += 1;

                PointDouble posPlus1 = _inputSignal.ValueAt(new Point<int>(valueAtPos.Dimensions) { Values = posP1Arr });

                //Calculate Forward Derivative in Direction i
                derivative = posPlus1 - valueAtPos;
            }

            return derivative;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalGeneration.SignalProcessors
{
    public class SGDForwarderivativeProcessor : ISGContinousSignalSource<Point<int>, PointDouble, int>
    {
        ISGContinousSignalSource<Point<int>, PointDouble, int> InputSignal;

        public SGDForwarderivativeProcessor(ISGContinousSignalSource<Point<int>, PointDouble, int> input)
        {
            InputSignal = input;
        }

        public PointDouble ValueAt(Point<int> position)
        {
            PointDouble valueAtPos = InputSignal.ValueAt(position);
            PointDouble derivative = new PointDouble(position.Dimensions);

            for(int i = 0; i < position.Values.Length; i++)
            {
                var posP1Arr = new int[valueAtPos.Dimensions];
                Array.Copy(position.Values, posP1Arr, valueAtPos.Values.Length);
                posP1Arr[i] += 1;

                PointDouble posPlus1 = InputSignal.ValueAt(new Point<int>(valueAtPos.Dimensions) { Values = posP1Arr });

                //Calculate Forward Derivative in Direction i
                derivative = posPlus1 - valueAtPos;
            }

            return derivative;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SignalGeneration.Common
{
    public static class ColorUtils
    {
        public static Color Add(this Color c1, Color c2)
        {
            return Color.FromArgb(c1.A + c2.A, c1.R + c2.R, c1.G + c2.G, c1.B + c2.B);
        }

        public static Color Sub(this Color c1, Color c2)
        {
            return Color.FromArgb(c1.A - c2.A, c1.R - c2.R, c1.G - c2.G, c1.B - c2.B);
        }
    }
}

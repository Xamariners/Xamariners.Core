using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamariners.Core.Common.Helpers
{
    public static class MathHelpers
    {
        public static double ApplyRounding(this double value, int digits)
        {
            // First rounding is to fix rounding errors,
            // by changing things like 0.99499999999999 to 0.995
            value = Math.Round(value, digits + 5, MidpointRounding.AwayFromZero);
            // Round value to specified number of digits
            value = Math.Round(value, digits, MidpointRounding.AwayFromZero);
            return value;
        }

        public static float ToFloat(this double source)
        {
            float result = (float)source;
            if (float.IsPositiveInfinity(result))
            {
                result = float.MaxValue;
            }
            else if (float.IsNegativeInfinity(result))
            {
                result = float.MinValue;
            }

            return result;
        }
    }
}

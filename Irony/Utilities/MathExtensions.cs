#if !SILVERLIGHT && !PORTABLE

using Microsoft.Scripting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Scripting.Math
{
    
    public static class MathExtensions
    {

        public static bool TryToFloat64(this BigInteger self, out double result)
        {
            return StringUtils.TryParseDouble(
                self.ToString(10),
                System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.InvariantCulture.NumberFormat,
                out result
            );
        }

        public static double ToFloat64(this BigInteger self)
        {
            return double.Parse(
                self.ToString(10),
                System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.InvariantCulture.NumberFormat
            );
        }

        public static double ToDouble(this BigInteger self, object culture)
        {
            return self.ToFloat64();
        }

    }

}

#endif
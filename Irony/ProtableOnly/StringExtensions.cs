using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System
{

    public static class StringExtensions
    {

        /// <summary>
        /// Test if the given character is in the string sequence.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool Contains(this string str, char c)
        {
            return str.ToCharArray().Count(item => item == c) > 0;
        }

        public static string ToLower(this string str, CultureInfo culture)
        {
            return str.ToLower();
        }

    }

}
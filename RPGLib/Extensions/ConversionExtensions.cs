using System;
using System.Collections.Generic;
using System.Text;

namespace RPGLib.Extensions
{
    public static class ConversionExtensions
    {
        public static int ToInt(this string str) => int.Parse(str);

        public static bool TryToInt(this string str, out int output) => int.TryParse(str, out output);
    }
}

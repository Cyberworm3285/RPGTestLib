using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGLib.Extensions
{
    public static class Misc
    {
        public static string NormalizeLength(this string str, int maxLength)
        {
            return (str.Length <= maxLength) ? str : str.Substring(0, maxLength - 2) + "..";
        }
    }
}

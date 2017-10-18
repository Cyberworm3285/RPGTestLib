using System;
using System.Collections.Generic;
using System.Text;

namespace RPGLib.Extensions
{
    public static class GenericObjectExtensions
    {
        public static bool IsNull<T>(this T t)
            where T : class
            => t == null;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RPGLib.Extensions
{
    public static class LINQlike
    {
        public static void ForEach<T>(this IEnumerable<T> enu, Action<T> action)
        {
            if (enu.IsNull()) return;
            foreach (var x in enu)
                action(x);
        }

        public static T Find<T>(this T[] t, Predicate<T> predicate) => Array.Find(t, predicate);
    }
}

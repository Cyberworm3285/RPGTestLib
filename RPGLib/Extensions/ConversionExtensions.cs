using System;
using System.Collections.Generic;

using RPGLib.HelperTypes.Collections.Tree;

namespace RPGLib.Extensions
{
    public static class ConversionExtensions
    {
        #region int -> string

        public static int ToInt(this string str) => int.Parse(str);

        public static bool TryToInt(this string str, out int output) => int.TryParse(str, out output);

        #endregion
        #region IEnumerable -> Tree

        public static Tree<TContent, TId> ToTree<TContent, TId>(
            this IEnumerable<TContent> e,
            Func<TContent, TId> idSelector,
            Func<TContent, List<TId>> childrenIdSelector,
            TContent rootContent = default(TContent),
            TId rootId = default(TId)
            ) => new Tree<TContent, TId>(e, idSelector, childrenIdSelector, rootContent, rootId);

        #endregion
    }
}

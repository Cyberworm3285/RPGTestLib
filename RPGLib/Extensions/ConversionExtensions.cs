using System;
using System.Collections.Generic;

using System.Linq;

using RPGLib.HelperTypes.Collections.Tree;
using RPGLib.Commands;

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
        #region string -> KeyValuePair

        public static KeyValuePair<string,string> ToKeyValue(this string str, string seperator)
        {
            string[] temp = str.Split(new[] { seperator }, StringSplitOptions.RemoveEmptyEntries);

            if (temp.Length > 2)
                throw new ArgumentException($"To Many Values/Keys [{temp.Length}]");

            return new KeyValuePair<string, string>(temp[0], temp[1]);
        }

        public static Dictionary<string,string> ToArgDic(this List<string> strings, string seperator)
        {
            string[][] stringPairs = strings
                                        .Select(
                                            s => s.Split(new[] { seperator }, StringSplitOptions.RemoveEmptyEntries))
                                        .Where(
                                            s => s.Count() == 2)
                                        .ToArray();

            return stringPairs.ToDictionary(x => x[0], x => x[1]);
        }

        #endregion
    }
}

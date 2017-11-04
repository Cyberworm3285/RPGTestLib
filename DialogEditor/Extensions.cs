using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;


namespace DialogEditor
{
    public static class Extensions
    {
        public static bool DeleteCurrent(this ListBox box)
        {
            if (box.SelectedIndex == -1)
                return false;
            box.Items.RemoveAt(box.SelectedIndex);
            return true;
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            return pairs.ToDictionary(p => p.Key, p => p.Value);
        }
    }
}

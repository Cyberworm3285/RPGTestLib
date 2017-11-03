using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RPGLib.HelperTypes.Collections.Tree;

namespace RPGLib.HelperTypes.Collections.Tree
{
    public class TreeEnumerator<TContent, TId> : IEnumerator<TContent>
    {
        private List<TContent> _list;
        private int _counter;
        private TContent _curr;

        #region Constructors

        public TreeEnumerator(Tree<TContent, TId> tree)
        {
            _list = tree.Traverse().ToList();
            _counter = -1;
            _curr = default(TContent);
        }

        #endregion

        #region Interface Implementations

        public TContent Current => _curr;

        object IEnumerator.Current => _curr;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (++_counter >= _list.Count)
                return false;
            else
            {
                _curr = _list[_counter];
                return true;
            }
        }

        public void Reset()
        {
            _counter = -1;
        }

        #endregion
    }
}

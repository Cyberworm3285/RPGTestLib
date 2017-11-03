using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGLib.HelperTypes.Collections.Tree
{
    public class IndexCounter
    {
        public int CurrentIndex { get; set; }

        #region Constructors

        public IndexCounter() => CurrentIndex = 0;

        public IndexCounter(int start) => CurrentIndex = start;

        #endregion

        #region Methods

        public int Next()
        {
            CurrentIndex++;
            return CurrentIndex;
        }

        #endregion

        #region Operator

        public static IndexCounter operator ++(IndexCounter counter)
        {
            counter.CurrentIndex++;
            return counter;
        }

        public static IndexCounter operator --(IndexCounter counter)
        {
            counter.CurrentIndex--;
            return counter;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using static RPGLib.Extensions.Misc;

namespace RPGLib.Quest
{
    public class FollowQuest
    {
        public string LinkedID { get; set; }
        public string[] CommandsOnFinish { get; set; }

        #region Overrides

        public override string ToString()
        {
            return $"{LinkedID}";
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using static RPGLib.Extensions.Misc;

namespace RPGLib.Dialog
{
    public class Answer
    {
        public string LinkedID { get; set; }
        public string AnswerText { get; set; }
        public string[] CommandsOnExit { get; set; }
        public string[] RequiredItems { get; set; }
        public string[] ReqiredSkills { get; set; }

        #region Overrides

        public override string ToString()
        {
            return $"{LinkedID} :: {AnswerText.NormalizeLength(10)}";
        }

        #endregion
    }
}

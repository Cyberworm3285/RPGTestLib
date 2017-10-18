using System;
using System.Collections.Generic;
using System.Text;

namespace RPGLib.Dialog
{
    public class Answer
    {
        public string LinkedID { get; set; }
        public string AnswerText { get; set; }
        public string[] CommandsOnExit { get; set; }
    }
}

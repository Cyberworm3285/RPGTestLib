using System;
using System.Collections.Generic;
using System.Text;

namespace RPGLib.Dialog
{
    public class DialogElement
    {
        public string ID { get; set; }
        public string DisplayText { get; set; }
        public Answer[] Answers { get; set; }

        public string[] CommandsAtEnter { get; set; }
    }
}

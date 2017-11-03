using System;
using System.Collections.Generic;
using System.Text;

namespace RPGLib.Quest
{
    public class QuestElement
    {
        public string ID { get; set; }
        public string QuestDescription { get; set; }
        public FollowQuest[] FollowQuests { get; set; }
        public QStatus Status { get; set; }

        public string[] CommandsAtStart { get; set; }
    }
}

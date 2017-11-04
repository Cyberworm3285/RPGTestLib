using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace RPGLib.Quest
{
    [JsonObject(MemberSerialization.OptIn)]
    public class QuestElement
    {
        public string ID { get; set; }
        public string QuestDescription { get; set; }
        [JsonProperty]
        public QStatus Status { get; set; }

        public string[] CommandsAtStart { get; set; }
        public string[] CommandsAtFinish { get; set; }
    }
}

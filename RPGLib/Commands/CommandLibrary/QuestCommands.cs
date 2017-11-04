using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RPGLib.Commands;
using RPGLib.Quest;

using static RPGLib.Extensions.ConversionExtensions;

namespace RPGLib.Commands.CommandLibrary
{
    public class QuestCommandMasterNode : ICommandLibrary
    {
        private CommandNode _node;

        public QuestCommandMasterNode()
        {
            _node = new CommandNode
            {
                Children = new Dictionary<string, CommandNode>
                {
                    { "Start",new CommandNode { Method = StartQuest } },
                    { "Finish",new CommandNode{ Method = FinishQuest } },
                }
            };
        }

        #region Methods

        private static Dictionary<string, string> GetDic(List<string> input) => input.ToArgDic(CommandManager.Instance.ArgSplitChar);

        private static void StartQuest(List<string> input)
        {
            var dic = GetDic(input);
            QuestManager.Instance.StartQuest(dic["id"]);
        }

        private static void FinishQuest(List<string> input)
        {
            var dic = GetDic(input);

            if (dic.Keys.Count == 1)
            {
                QuestManager.Instance.FinishQuest(dic["id"]);
            }
            else
            {
                QuestManager.Instance.FinishQuest(dic["id"], bool.Parse(dic["force_finish"]));
            }
        }

        #endregion

        #region Interface Implementation

        public CommandNode Node => _node;

        public string RootKey => "Quest";

        #endregion
    }
}

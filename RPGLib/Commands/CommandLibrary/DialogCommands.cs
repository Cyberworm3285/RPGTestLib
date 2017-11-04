using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RPGLib.Dialog;

using static RPGLib.Extensions.ConversionExtensions;

namespace RPGLib.Commands.CommandLibrary
{
    public class DialogCommandMasterNode : ICommandLibrary
    {
        private CommandNode _node;

        public DialogCommandMasterNode()
        {
            _node = new CommandNode
            {
                Children =
                {
                    { "Start",  new CommandNode{ Method = StartDialog } }
                }
            };
        }

        #region Methods

        private static Dictionary<string, string> GetDic(List<string> input) => input.ToArgDic(CommandManager.Instance.ArgSplitChar);

        private static void StartDialog(List<string> input)
        {
            var dic = GetDic(input);

            DialogManager.Instance.StartDialog(dic["dialog"]);
        }

        #endregion

        #region Interface Implementation

        public CommandNode Node => _node;

        public string RootKey => "Dialog";
        
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using RPGLib.Commands.CommandLibrary;

using static RPGLib.Extensions.GenericObjectExtensions;

namespace RPGLib.Commands
{
    public class CommandManager
    {
        private static CommandManager _instance;
        public static CommandManager Instance => _instance ?? (_instance = new CommandManager());

        public char[] SplitChars { get; set; } = { ' ' };
        public string ArgSplitChar { get; set; } = "::";

        public CommandNode CommandRoot { get; set; }

        private CommandManager()
        {
            CommandRoot = new CommandNode();
            CommandRoot.Children = CommandMaster.GetAllLibNodes();
        }

        #region Methods

        public void EvalCommand(string command)
        {
            string[] parts = command.Split(SplitChars, StringSplitOptions.RemoveEmptyEntries);
            CommandNode currRoot = CommandRoot;

            List<string> variables = new List<string>();
            foreach (var p in parts)
            {
                if (p[0] == '-')
                {
                    variables.Add(p.Remove(0,1));
                }
                else
                {
                    currRoot = currRoot[p]; //obacht
                }
            }

            currRoot.Method(variables); //obacht
        }

        public void EvalCommands(IEnumerable<string> commands)
        {
            if (commands.IsNull())
                return;
            foreach (var c in commands)
            {
                EvalCommand(c);
            }
        }

        #endregion
    }
}

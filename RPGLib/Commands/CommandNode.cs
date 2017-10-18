using System;
using System.Collections.Generic;
using System.Text;

namespace RPGLib.Commands
{
    public class CommandNode
    {
        public bool IsLeaf => Children == null || Children.Values.Count == 0; 

        public Dictionary<string, CommandNode> Children { get; set; }
        public Action<List<string>> Method { get; set; } 

        public CommandNode this[string id] => Children[id];
    }
}

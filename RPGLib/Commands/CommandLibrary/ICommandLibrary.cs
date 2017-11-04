using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RPGLib.Commands;

namespace RPGLib.Commands.CommandLibrary
{
    public interface ICommandLibrary
    {
        CommandNode Node { get; }
        string RootKey { get; }
    }
}

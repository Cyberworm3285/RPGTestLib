using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

using RPGLib.Commands;

using static RPGLib.Extensions.GenericObjectExtensions;

namespace RPGLib.Commands.CommandLibrary
{
    public static class CommandMaster
    {
        public static (string Key, CommandNode Node)[] GetAllLibNodes(Assembly assembly, string nSpace = null)
        {
            return assembly.GetTypes()
                .Where(
                    t => typeof(ICommandLibrary).IsAssignableFrom(t) 
                        && !t.GetConstructor(new Type[0]).IsNull() 
                        && (nSpace.IsNull() 
                            || t.Namespace == nSpace))
                .Select(
                    t => t.GetConstructor(new Type[0]).Invoke(null, null) as ICommandLibrary)
                .Select(c => (c.RootKey, c.Node))
                .ToArray();
        }

        public static (string Key, CommandNode Node)[] GetAllLibs(string nSpace = null) => GetAllLibNodes(Assembly.GetAssembly(typeof(ICommandLibrary)));
    }
}

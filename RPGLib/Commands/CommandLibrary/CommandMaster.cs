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
        public static Dictionary<string, CommandNode> GetAllLibNodes(Assembly assembly, string nSpace = null)
        {
            return assembly.GetTypes()
                .Where(
                    t => typeof(ICommandLibrary).IsAssignableFrom(t)
                        && !t.GetConstructor(new Type[0]).IsNull()
                        && (nSpace.IsNull()
                            || t.Namespace == nSpace))
                .Select(
                    t => t.GetConstructor(new Type[0]).Invoke(new object[0]) as ICommandLibrary)
                .ToDictionary(
                    c => c.RootKey, c => c.Node);
        }

        public static Dictionary<string, CommandNode> GetAllLibNodes(string nSpace = null) => GetAllLibNodes(Assembly.GetAssembly(typeof(ICommandLibrary)));
    }
}

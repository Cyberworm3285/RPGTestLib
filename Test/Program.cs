using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RPGLib.Commands;
using RPGLib.Dialog;
using RPGLib.HelperTypes.Collections.Tree;

using static RPGLib.Extensions.LINQlike;
using static RPGLib.Extensions.ConversionExtensions;

using Newtonsoft.Json;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandTest();
            DialogTest();
            DialogTreeTest();

            DialogManager.Instance.StartDialog("Test1");
            while (true)
            {
                Console.WriteLine(DialogManager.Instance.CurrentText);
                int i = 0;
                DialogManager.Instance.CurrentAnswers.ForEach(a => Console.WriteLine($"{i++}  {a.AnswerText}"));
                if (DialogManager.Instance.NextDialog(Console.ReadLine().ToInt())) break;
            }

            Console.ReadKey();
        }

        static void CommandTest()
        {
            CommandManager.Instance.CommandRoot = new CommandNode
            {
                Children = new Dictionary<string, CommandNode>
                {
                    {
                        "Say",
                        new CommandNode
                        {
                            Children = new Dictionary<string, CommandNode>
                            {
                                {
                                    "Hi",
                                    new CommandNode
                                    {
                                        Method = a => Console.WriteLine("hi" + string.Join(",", a))
                                    }
                                }
                            }
                        }
                    },
                    {
                        "Sum",
                        new CommandNode
                        {
                            Method = a => Console.WriteLine(JsonConvert.DeserializeObject<int[]>(a[0]).Sum())
                        }
                    },
                    {
                        "Write",
                        new CommandNode
                        {
                            Method = a => a.ForEach(Console.Write)
                        }
                    },
                    {
                        "WriteLine",
                        new CommandNode
                        {
                            Method = a => a.ForEach(Console.WriteLine)
                        }
                    }
                }
            };
        }

        static void DialogTest()
        {
            DialogManager.Instance.DialogElements = new DialogElement[]
            {
                new DialogElement
                {
                    ID = "Test1",
                    DisplayText = "Ey Fuck off",
                    Answers = new Answer[]
                    {
                        new Answer{ AnswerText = "selber", LinkedID = "Test2" },
                        new Answer{ AnswerText = "nice", LinkedID = "Test3"}
                    }
                },
                new DialogElement
                {
                    ID = "Test2",
                    DisplayText = "WUASS?! Pass ma auf wie ich rechnen kann!",
                    Answers = new Answer[]
                    {
                        new Answer{ AnswerText = "exit", LinkedID = null , CommandsOnExit = new[]{ "Write -1+2+3+4=","Sum -[1,2,3,4]" } }
                    }
                },
                new DialogElement
                {
                    ID = "Test3",
                    DisplayText = "Ja, ne?",
                    Answers = new Answer[]
                    {
                        new Answer{ AnswerText = "exit", LinkedID = null }
                    }
                },
                new DialogElement
                {
                    ID = "Test4",
                    DisplayText = "Bla",
                    Answers = new Answer[]
                    {
                        new Answer{ AnswerText = "Bla", LinkedID = "Test3" },
                        new Answer{ AnswerText = "Bla", LinkedID = "Test5" },
                        new Answer{ AnswerText = "exit", LinkedID = null }
                    }
                },
                new DialogElement
                {
                    ID = "Test5",
                    DisplayText = "Bla",
                    Answers = new Answer[]
                    {
                        new Answer{ AnswerText = "Bla", LinkedID = "Test3" },
                        new Answer{ AnswerText = "exit", LinkedID = null }
                    }
                },
            };
        }

        static void DialogTreeTest()
        {
            Tree<DialogElement, string> tree = new Tree<DialogElement, string>(DialogManager.Instance.DialogElements, x => x.ID, x => x.Answers.Select(y => y.LinkedID).ToList(), null, "Root");

            //?
        }
    }
}

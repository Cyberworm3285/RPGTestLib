using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RPGLib.Commands;
using RPGLib.Dialog;
using RPGLib.Quest;

using static RPGLib.Extensions.LINQlike;

namespace TestRange
{
    class Program
    {
        static void Main(string[] args)
        {
            DialogManager.Instance.DialogStarted += (x) => Console.WriteLine($"Dialog started: {x}");
            DialogManager.Instance.NextDialogStarted += (x) => Console.WriteLine($"Next Dialog: {x}");
            DialogManager.Instance.DialogFinished += (x) => Console.WriteLine($"Dialog Finished: {x}");

            QuestManager.Instance.QuestStarted += (x) => Console.WriteLine($"Quest started ID:{x.ID} Description:{x.QuestDescription}");
            QuestManager.Instance.QuestFinished += (x) => Console.WriteLine($"Quest finished ID:{x.ID}");
            QuestManager.Instance.QuestIsDead += (x) => Console.WriteLine($"Quest is already dead ID:{x.ID}");

            InitDialogs();
            InitQuests();

            DialogManager.Instance.StartDialog("D1");
            while (true)
            {
                Console.WriteLine(DialogManager.Instance.CurrentText);
                EnumerateOptions();
                int aIndex;
                while (!int.TryParse(Console.ReadLine(), out aIndex) && aIndex >= 0 && aIndex < DialogManager.Instance.CurrentAnswers.Length)
                {
                    Console.WriteLine("invalid input");
                }
                if (!DialogManager.Instance.NextDialog(aIndex)) break;
            }

            Console.ReadKey();
        }

        static void EnumerateOptions()
        {
            int i = 0;
            DialogManager.Instance.CurrentAnswers.ForEach(a => Console.WriteLine($"{i++}:{a.AnswerText}"));
        }

        static void InitDialogs()
        {
            DialogManager.Instance.DialogElements = new[]
            {
                new DialogElement
                {
                    ID = "D1",
                    DisplayText = "Hallo",
                    CommandsAtEnter = new[] { "Quest Start -id::Q1" },
                    Answers = new[]
                    {
                        new Answer { AnswerText = "Komm ma klar auf dein Leben", LinkedID = "D2", CommandsOnExit = new[]{ "Quest Finish -id::Q1 -force_finish::false" } }    
                    }
                },
                new DialogElement
                {
                    ID = "D2",
                    DisplayText = "Ey man",
                    Answers = new[]
                    {
                        new Answer{ AnswerText = "[end]", CommandsOnExit = new[]{ "Quest Finish -id::Q1" } }
                    }
                }
            };           
        }

        static void InitQuests()
        {
            QuestManager.Instance.QuestElements = new[]
            {
                new QuestElement
                {
                    ID = "Q1",
                    QuestDescription = "Fuck Off",
                }
            };
        }
    }
}

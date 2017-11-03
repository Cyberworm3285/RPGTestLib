using System;
using System.Linq;

using RPGLib.Commands;

using static RPGLib.Extensions.LINQlike;
using static RPGLib.Extensions.GenericObjectExtensions;


namespace RPGLib.Quest
{
    public class QuestManager
    {
        private static QuestManager _instance;
        public static QuestManager Instance => _instance ?? (_instance = new QuestManager());

        public QuestElement[] QuestElements { get; set; }

        public event Action<QuestElement> QuestStarted;
        public event Action<QuestElement> QuestFinished;

        private QuestManager() { }

        public void StartQuest(string id)
        {
            var temp = Array.Find(QuestElements, q => q.ID == id);

            if (temp.Status == QStatus.Inactive)
            {
                temp.Status = QStatus.Active;
                QuestStarted(temp);

                CommandManager.Instance.EvalCommands(temp.CommandsAtStart);
            }
        }

        public void FinishQuest(string id)
        {
            var temp = Array.Find(QuestElements, q => q.ID == id);

            if (temp.Status == QStatus.Active)
            {
                temp.Status = QStatus.Finished;
                QuestFinished(temp);

                CommandManager.Instance.EvalCommands(temp.CommandsAtFinish);
            }
        }
    }
}

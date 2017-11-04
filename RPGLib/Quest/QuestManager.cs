﻿using System;
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
        public event Action<QuestElement> QuestIsDead;

        private QuestManager() { }

        public void StartQuest(string id)
        {
            var temp = Array.Find(QuestElements, q => q.ID == id);

            if (temp.Status == QStatus.Inactive)
            {
                temp.Status = QStatus.Active;
                CommandManager.Instance.EvalCommands(temp.CommandsAtStart);

                QuestStarted(temp);
            }
        }

        public void FinishQuest(string id, bool forceFinish)
        {
            var temp = Array.Find(QuestElements, q => q.ID == id);

            if (temp.Status == QStatus.Active)
            {
                temp.Status = QStatus.Finished;
                CommandManager.Instance.EvalCommands(temp.CommandsAtFinish);

                QuestFinished(temp);
            }
            else if (temp.Status == QStatus.Inactive && forceFinish)
            {
                CommandManager.Instance.EvalCommands(temp.CommandsAtStart);
                QuestStarted(temp);

                temp.Status = QStatus.Finished;
                CommandManager.Instance.EvalCommands(temp.CommandsAtFinish);

                QuestFinished(temp);
            }
            else
            {
                QuestIsDead(temp);
            }
        }
    }
}

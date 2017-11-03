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

        public string CurrentText => _currentElement.QuestDescription;
        public FollowQuest[] CurrentAnswers => _currentElement.FollowQuests;

        private QuestElement _currentElement { get; set; }
        

        private QuestManager() { }

        public void StartQuest(string id)
        {
            _currentElement = QuestElements.Find(e => e.ID == id); //obacht
            _currentElement.CommandsAtStart.ForEach(CommandManager.Instance.EvalCommand);
        }

        public bool NextQuest(string id)
        {
            _currentElement.FollowQuests.Find(array => array.LinkedID == id).CommandsOnFinish.ForEach(CommandManager.Instance.EvalCommand); //obacht
            _currentElement = QuestElements.Find(e => e.ID == id); //obacht

            return _currentElement.IsNull();
        }

        public bool NextQuest(int index)
        {
            _currentElement.FollowQuests[index].CommandsOnFinish.ForEach(CommandManager.Instance.EvalCommand); //obacht
            _currentElement = QuestElements.Find(e => e.ID == _currentElement.FollowQuests[index].LinkedID); //obacht

            return _currentElement.IsNull();
        }
    }
}

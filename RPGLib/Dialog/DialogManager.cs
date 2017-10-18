using System;
using System.Linq;

using RPGLib.Commands;

using static RPGLib.Extensions.LINQlike;
using static RPGLib.Extensions.GenericObjectExtensions;


namespace RPGLib.Dialog
{
    public class DialogManager
    {
        private static DialogManager _instance;
        public static DialogManager Instance => _instance ?? (_instance = new DialogManager());

        public DialogElement[] DialogElements { get; set; }

        public string CurrentText => _currentElement.DisplayText;
        public Answer[] CurrentAnswers => _currentElement.Answers;

        private DialogElement _currentElement { get; set; }
        

        private DialogManager() { }

        public void StartDialog(string id)
        {
            _currentElement = DialogElements.Find(e => e.ID == id); //obacht
            _currentElement.CommandsAtEnter.ForEach(CommandManager.Instance.EvalCommand);
        }

        public bool NextDialog(string id)
        {
            _currentElement.Answers.Find(array => array.LinkedID == id).CommandsOnExit.ForEach(CommandManager.Instance.EvalCommand); //obacht
            _currentElement = DialogElements.Find(e => e.ID == id); //obacht

            return _currentElement.IsNull();
        }

        public bool NextDialog(int index)
        {
            _currentElement.Answers[index].CommandsOnExit.ForEach(CommandManager.Instance.EvalCommand); //obacht
            _currentElement = DialogElements.Find(e => e.ID == _currentElement.Answers[index].LinkedID); //obacht

            return _currentElement.IsNull();
        }
    }
}

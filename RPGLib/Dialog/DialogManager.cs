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

        public event Action<DialogElement> DialogStarted;
        public event Action<DialogElement> NextDialogStarted;
        public event Action<DialogElement> DialogFinished;

        private DialogManager() { }

        public void StartDialog(string id)
        {
            _currentElement = DialogElements.Find(e => e.ID == id); //obacht
            _currentElement.CommandsAtEnter.ForEach(CommandManager.Instance.EvalCommand);

            DialogStarted(_currentElement);
        }

        public bool NextDialog(string id)
        {
            var last = _currentElement;
            _currentElement.Answers.Find(array => array.LinkedID == id).CommandsOnExit.ForEach(CommandManager.Instance.EvalCommand); //obacht
            _currentElement = DialogElements.Find(e => e.ID == id); //obacht

            if (_currentElement.IsNull())
            {
                DialogFinished(last);
                return false;
            }
            else
            {
                NextDialogStarted(_currentElement);
                return true;
            }
        }

        public bool NextDialog(int index)
        {
            var last = _currentElement;
            _currentElement.Answers[index]?.CommandsOnExit.ForEach(CommandManager.Instance.EvalCommand); //obacht
            _currentElement = DialogElements.Find(e => e.ID == _currentElement.Answers[index].LinkedID); //obacht

            if (_currentElement.IsNull())
            {
                DialogFinished(last);
                return false;
            }
            else
            {
                NextDialogStarted(_currentElement);
                return true;
            }
        }
    }
}

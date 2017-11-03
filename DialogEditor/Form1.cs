using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;

using RPGLib.HelperTypes.Collections.Tree;
using RPGLib.Dialog;

using static RPGLib.Extensions.ConversionExtensions;
using static DialogEditor.PopUp;
 
namespace DialogEditor
{
    public partial class Form1 : Form
    {
        private List<DialogElement> _dialogList;
        private Tree<DialogElement, string> _dialogTree;
        private List<string> _Ids;

        public Form1()
        {
            InitializeComponent();

            _dialogList = new List<DialogElement>();
            _Ids = new List<string>();
        }

        private void UpdateTree()
        {
            _dialogTree = _dialogList.ToTree(x => x.ID, x => x.Answers.Select(y => y.LinkedID).ToList(), null, "[Root]");
            listBox1.Items.Clear();
            listBox1.Items.AddRange(_dialogTree.GetStringRepresentation().ToArray());
            _Ids = _dialogList.Select(x => x.ID).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "json files (*.json)|*.txt|All files (*.*)|*.*";
            open.InitialDirectory = Directory.GetCurrentDirectory();
            open.Multiselect = false;
            if (open.ShowDialog() == DialogResult.OK)
            {
                string json = File.ReadAllText(open.FileName);

                _dialogList = JsonConvert.DeserializeObject<List<DialogElement>>(json);
                UpdateTree();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "json files (*.json)|*.txt|All files (*.*)|*.*";
            save.InitialDirectory = Directory.GetCurrentDirectory();

            if (save.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(save.FileName, JsonConvert.SerializeObject(_dialogList, Formatting.Indented));
            }
        }

        private void EvalResult(DialogElement d)
        {
            _dialogList.Add(d);
            UpdateTree();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NewDialogForm input = new NewDialogForm(_Ids);
            input.Finished += EvalResult;
            input.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            _dialogList.Remove(_dialogTree.AtIndex(listBox1.SelectedIndex));
            UpdateTree();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                return;

            TreeNode<DialogElement, string> temp = null;
            if (!_dialogTree.TryFind(x => x.ID == textBox1.Text, out temp))
                return;

            listBox1.SelectedIndex = temp.Index.Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            var edit = _dialogTree.AtIndex(listBox1.SelectedIndex);

            NewDialogForm editForm = new NewDialogForm(edit, _Ids);
            editForm.Finished += EvalResult;
            _dialogList.Remove(edit);
            editForm.Show();

            UpdateTree();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
                return;

            var matches = _dialogList
                .Where(
                    x => x.DisplayText.ToUpper().Contains(textBox2.Text.ToUpper()) || 
                    x.Answers.Any(
                        y => y.AnswerText.ToUpper().Contains(textBox2.Text.ToUpper())))
                .Select(x => x.ID);

            ShowInfo((matches.Any())?$"Matches : [{string.Join(",", matches)}]":"No Matches Found");
        }
    }
}

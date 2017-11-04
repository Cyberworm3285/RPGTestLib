using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RPGLib.Dialog;

using static DialogEditor.PopUp;
using static RPGLib.Extensions.GenericObjectExtensions;
using static RPGLib.Extensions.LINQlike;

namespace DialogEditor
{
    public partial class NewDialogForm : Form
    {
        public event Action<DialogElement> Finished;
        private List<string> occupiedIds;
        private bool isOk = false;

        public NewDialogForm(List<string> ids)
        {
            InitializeComponent();
            occupiedIds = ids;
        }

        public NewDialogForm(DialogElement edit, List<string> ids)
        {
            InitializeComponent();
            textBox1.Text = edit.ID;
            textBox2.Text = edit.DisplayText;
            listBox1.Items.AddRange(edit.Answers);

            occupiedIds = ids;
            ids.Remove(edit.ID);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                ShowInfo("At least one input is empty");
                return;
            }

            if (occupiedIds.Contains(textBox1.Text))
            {
                ShowInfo("Id is already in use");
                return;
            }

            DialogElement result = new DialogElement();
            result.ID = textBox1.Text;
            result.DisplayText = textBox2.Text;
            result.Answers = listBox1.Items.Cast<Answer>().ToArray();
            result.CommandsAtEnter = (textBox9.Text == "") ? null : textBox9.Text.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            Finished(result);
            isOk = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                return;
            }
            string[] script = (textBox7.Text == "")?null:textBox7.Text.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            listBox1.Items.Add(
                new Answer
                {
                    AnswerText = textBox3.Text,
                    LinkedID = textBox4.Text,
                    CommandsOnExit = script,
                    ReqiredSkills = listBox2.Items.Cast<KeyValuePair<string, int>>().ToDictionary(),
                    RequiredItems = listBox3.Items.Cast<object>().Select(x => x.ToString()).ToArray()
                }
                );

            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();

            listBox2.Items.Clear();
            listBox3.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.DeleteCurrent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                PopUp.ShowInfo("textbox is empty");
                return;
            }
            if (listBox3.Items.Contains(textBox6.Text))
            {
                PopUp.ShowInfo($"{textBox6.Text} is already added");
                return;
            }

            listBox3.Items.Add(textBox6.Text);
            textBox6.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox3.DeleteCurrent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox8.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                PopUp.ShowInfo("One or more TextBox is empty");
                return;
            }

            int level;
            if (!int.TryParse(textBox5.Text, out level))
            {
                PopUp.ShowInfo("Level has an invalid format");
            }

            KeyValuePair<string, int> keyValue = new KeyValuePair<string, int>(textBox8.Text, level);
            if (listBox2.Items.Contains(keyValue))
            {
                PopUp.ShowInfo("Skill is already in place");
            }

            textBox8.Clear();
            textBox5.Clear();
            listBox2.Items.Add(keyValue);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.DeleteCurrent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var temp = listBox1.SelectedItem as Answer;

            textBox4.Text = temp.LinkedID;
            if (!temp.CommandsOnExit.IsNull())
                textBox7.Text = string.Join(";", temp.CommandsOnExit);
            textBox3.Text = temp.AnswerText;

            temp.ReqiredSkills?.ToList().ForEach(x => listBox2.Items.Add(x));
            temp.RequiredItems?.ForEach(x => listBox3.Items.Add(x));

            listBox1.Items.Remove(temp);
        }

        private void NewDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isOk)
                return;

            DialogElement result = new DialogElement();
            result.ID = textBox1.Text;
            result.DisplayText = textBox2.Text;
            result.Answers = listBox1.Items.Cast<Answer>().ToArray();
            result.CommandsAtEnter = (textBox9.Text == "") ? null : textBox9.Text.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            Finished(result);
        }
    }
}

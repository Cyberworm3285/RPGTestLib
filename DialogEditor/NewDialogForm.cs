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

namespace DialogEditor
{
    public partial class NewDialogForm : Form
    {
        public event Action<DialogElement> Finished;
        private List<string> occupiedIds;

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

            Finished(result);

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                return;
            }

            listBox1.Items.Add(new Answer { AnswerText = textBox3.Text, LinkedID = textBox4.Text });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }
    }
}

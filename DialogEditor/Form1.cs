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


using RPGLib.HelperTypes.Collections.Tree;
 
namespace DialogEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

            }
        }
    }
}

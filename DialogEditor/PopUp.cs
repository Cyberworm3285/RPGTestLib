using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace DialogEditor
{
    static class PopUp
    {
        public static void ShowInfo(string message)
        {
            MessageBox.Show(message, "info");
        }
    }
}

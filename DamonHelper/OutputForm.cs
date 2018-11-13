using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DamonHelper.Helper;

namespace DamonHelper
{
    public partial class OutputForm : Form
    {
        public OutputForm()
        {
            InitializeComponent();
        }

        public void TextEventHandler(object sender, EventArgs e)
        {
            CustomEventArgs e2 = e as CustomEventArgs;
            StringBuilder text = new StringBuilder(rtb_Output.Text);
            text.Append($"{e2?.Time:yyyy-MM-dd HH:mm:ss} {e2?.Text}\r\n");

            rtb_Output.Text = text.ToString();

            rtb_Output.SelectionStart = rtb_Output.TextLength;
            rtb_Output.Focus();
        }
    }
}

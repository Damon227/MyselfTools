using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CertificateHelper
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void 加解密ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            TalosEncrytForm talosEncrytForm = new TalosEncrytForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None
            };

            panel1.Controls.Add(talosEncrytForm);
            talosEncrytForm.Show();
        }
    }
}

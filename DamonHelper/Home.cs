using System;
using System.Drawing;
using System.Windows.Forms;
using DamonHelper.Settings;

namespace DamonHelper
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

            SetText();
        }

        private void SetText()
        {
            string env = "正式版";
            if (Config.Environment == Settings.Environment.Test.ToString())
            {
                env = "测试版";
            }
            if (Config.Environment == Settings.Environment.Development.ToString())
            {
                env = "开发版";
            }

            Text = Text + env;
        }

        private void 加解密ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            TalosEncrytForm talosEncrytForm = new TalosEncrytForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None
            };

            talosEncrytForm.Top = (panel1.Height - talosEncrytForm.Height) / 2;
            talosEncrytForm.Left = (panel1.Width - talosEncrytForm.Width) / 2;

            panel1.Controls.Add(talosEncrytForm);
            talosEncrytForm.Show();
        }

        private void 取消违约金ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            CancelOverdueFeeForm cancelOverdueFeeForm = new CancelOverdueFeeForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None
            };

            cancelOverdueFeeForm.Top = (panel1.Height - cancelOverdueFeeForm.Height) / 2;
            cancelOverdueFeeForm.Left = (panel1.Width - cancelOverdueFeeForm.Width) / 2;

            panel1.Controls.Add(cancelOverdueFeeForm);
            cancelOverdueFeeForm.Show();
        }

        private void 注册商户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            RegisterTenancyForm registerTenancyForm = new RegisterTenancyForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None
            };

            registerTenancyForm.Top = (panel1.Height - registerTenancyForm.Height) / 2;
            registerTenancyForm.Left = (panel1.Width - registerTenancyForm.Width) / 2;

            panel1.Controls.Add(registerTenancyForm);
            registerTenancyForm.Show();
        }

        private void 用户解锁ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            UnlockForm unlockForm = new UnlockForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None
            };

            unlockForm.Top = (panel1.Height - unlockForm.Height) / 2;
            unlockForm.Left = (panel1.Width - unlockForm.Width) / 2;

            panel1.Controls.Add(unlockForm);
            unlockForm.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle,
                Color.DarkGoldenrod, 1, ButtonBorderStyle.Solid, //左边
                Color.DarkGoldenrod, 1, ButtonBorderStyle.Solid, //上边
                Color.DarkGoldenrod, 1, ButtonBorderStyle.Solid, //右边
                Color.DarkGoldenrod, 1, ButtonBorderStyle.Solid);//底边
        }

        private void 配置违约金ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            PenaltySettingForm form = new PenaltySettingForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None
            };

            form.Top = (panel1.Height - form.Height) / 2;
            form.Left = (panel1.Width - form.Width) / 2;

            panel1.Controls.Add(form);
            form.Show();
        }

        private void 用户权限管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            PermissionManagerForm form = new PermissionManagerForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None
            };

            form.Top = (panel1.Height - form.Height) / 2;
            form.Left = (panel1.Width - form.Width) / 2;

            panel1.Controls.Add(form);
            form.Show();
        }

        private void 公寓迁移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            ApartmentMegrateForm form = new ApartmentMegrateForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None
            };

            form.Top = (panel1.Height - form.Height) / 2;
            form.Left = (panel1.Width - form.Width) / 2;

            panel1.Controls.Add(form);
            form.Show();
        }
    }
}

namespace DamonHelper
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.蜂鸟屋ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加解密ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.注册商户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消违约金ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.蜂鸟屋ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 蜂鸟屋ToolStripMenuItem
            // 
            this.蜂鸟屋ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加解密ToolStripMenuItem,
            this.注册商户ToolStripMenuItem,
            this.取消违约金ToolStripMenuItem});
            this.蜂鸟屋ToolStripMenuItem.Name = "蜂鸟屋ToolStripMenuItem";
            this.蜂鸟屋ToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.蜂鸟屋ToolStripMenuItem.Text = "蜂鸟屋";
            // 
            // 加解密ToolStripMenuItem
            // 
            this.加解密ToolStripMenuItem.Name = "加解密ToolStripMenuItem";
            this.加解密ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.加解密ToolStripMenuItem.Text = "加解密";
            this.加解密ToolStripMenuItem.Click += new System.EventHandler(this.加解密ToolStripMenuItem_Click);
            // 
            // 注册商户ToolStripMenuItem
            // 
            this.注册商户ToolStripMenuItem.Name = "注册商户ToolStripMenuItem";
            this.注册商户ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.注册商户ToolStripMenuItem.Text = "注册商户";
            this.注册商户ToolStripMenuItem.Click += new System.EventHandler(this.注册商户ToolStripMenuItem_Click);
            // 
            // 取消违约金ToolStripMenuItem
            // 
            this.取消违约金ToolStripMenuItem.Name = "取消违约金ToolStripMenuItem";
            this.取消违约金ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.取消违约金ToolStripMenuItem.Text = "取消违约金";
            this.取消违约金ToolStripMenuItem.Click += new System.EventHandler(this.取消违约金ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1160, 715);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 753);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "偷懒神器";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 蜂鸟屋ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加解密ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem 注册商户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消违约金ToolStripMenuItem;
    }
}


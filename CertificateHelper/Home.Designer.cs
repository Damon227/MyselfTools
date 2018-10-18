namespace CertificateHelper
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.蜂鸟屋ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加解密ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.加解密ToolStripMenuItem});
            this.蜂鸟屋ToolStripMenuItem.Name = "蜂鸟屋ToolStripMenuItem";
            this.蜂鸟屋ToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.蜂鸟屋ToolStripMenuItem.Text = "蜂鸟屋";
            // 
            // 加解密ToolStripMenuItem
            // 
            this.加解密ToolStripMenuItem.Name = "加解密ToolStripMenuItem";
            this.加解密ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.加解密ToolStripMenuItem.Text = "加解密";
            this.加解密ToolStripMenuItem.Click += new System.EventHandler(this.加解密ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1160, 715);
            this.panel1.TabIndex = 1;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Home";
            this.Text = "Home";
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
    }
}


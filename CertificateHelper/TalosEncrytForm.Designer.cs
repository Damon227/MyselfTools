namespace CertificateHelper
{
    partial class TalosEncrytForm
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
            this.btn_TalosEncryt_GenerateToken_Dev = new System.Windows.Forms.Button();
            this.btn_TalosEncryt_GenerateToken_Pro = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_TalosEncryt_GenerateToken_Dev
            // 
            this.btn_TalosEncryt_GenerateToken_Dev.Location = new System.Drawing.Point(45, 29);
            this.btn_TalosEncryt_GenerateToken_Dev.Name = "btn_TalosEncryt_GenerateToken_Dev";
            this.btn_TalosEncryt_GenerateToken_Dev.Size = new System.Drawing.Size(104, 23);
            this.btn_TalosEncryt_GenerateToken_Dev.TabIndex = 0;
            this.btn_TalosEncryt_GenerateToken_Dev.Text = "生成Token-Dev";
            this.btn_TalosEncryt_GenerateToken_Dev.UseVisualStyleBackColor = true;
            this.btn_TalosEncryt_GenerateToken_Dev.Click += new System.EventHandler(this.btn_TalosEncryt_GenerateToken_Dev_Click);
            // 
            // btn_TalosEncryt_GenerateToken_Pro
            // 
            this.btn_TalosEncryt_GenerateToken_Pro.Location = new System.Drawing.Point(45, 72);
            this.btn_TalosEncryt_GenerateToken_Pro.Name = "btn_TalosEncryt_GenerateToken_Pro";
            this.btn_TalosEncryt_GenerateToken_Pro.Size = new System.Drawing.Size(104, 23);
            this.btn_TalosEncryt_GenerateToken_Pro.TabIndex = 0;
            this.btn_TalosEncryt_GenerateToken_Pro.Text = "生成Token-Pro";
            this.btn_TalosEncryt_GenerateToken_Pro.UseVisualStyleBackColor = true;
            // 
            // TalosEncrytForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 676);
            this.Controls.Add(this.btn_TalosEncryt_GenerateToken_Pro);
            this.Controls.Add(this.btn_TalosEncryt_GenerateToken_Dev);
            this.Name = "TalosEncrytForm";
            this.Text = "TalosEncryt";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_TalosEncryt_GenerateToken_Dev;
        private System.Windows.Forms.Button btn_TalosEncryt_GenerateToken_Pro;
    }
}
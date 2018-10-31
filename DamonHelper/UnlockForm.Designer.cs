namespace DamonHelper
{
    partial class UnlockForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txb_Cellphone = new System.Windows.Forms.TextBox();
            this.btn_Unlock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(324, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "手机号";
            // 
            // txb_Cellphone
            // 
            this.txb_Cellphone.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txb_Cellphone.Location = new System.Drawing.Point(389, 54);
            this.txb_Cellphone.Name = "txb_Cellphone";
            this.txb_Cellphone.Size = new System.Drawing.Size(220, 24);
            this.txb_Cellphone.TabIndex = 1;
            // 
            // btn_Unlock
            // 
            this.btn_Unlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Unlock.Location = new System.Drawing.Point(630, 54);
            this.btn_Unlock.Name = "btn_Unlock";
            this.btn_Unlock.Size = new System.Drawing.Size(75, 24);
            this.btn_Unlock.TabIndex = 0;
            this.btn_Unlock.Text = "解锁";
            this.btn_Unlock.Click += new System.EventHandler(this.btn_Unlock_Click);
            // 
            // UnlockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 676);
            this.Controls.Add(this.btn_Unlock);
            this.Controls.Add(this.txb_Cellphone);
            this.Controls.Add(this.label1);
            this.Name = "UnlockForm";
            this.Text = "UnlockForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_Cellphone;
        private System.Windows.Forms.Button btn_Unlock;
    }
}
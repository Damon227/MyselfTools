namespace DamonHelper
{
    partial class ConfirmContractForm
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
            this.rtb_ContractIds = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chb_Yes = new System.Windows.Forms.CheckBox();
            this.btn_Submit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtb_ContractIds
            // 
            this.rtb_ContractIds.Location = new System.Drawing.Point(282, 88);
            this.rtb_ContractIds.Name = "rtb_ContractIds";
            this.rtb_ContractIds.Size = new System.Drawing.Size(651, 244);
            this.rtb_ContractIds.TabIndex = 0;
            this.rtb_ContractIds.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(154, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "合同Id（Json数组）：";
            // 
            // chb_Yes
            // 
            this.chb_Yes.AutoSize = true;
            this.chb_Yes.Location = new System.Drawing.Point(282, 374);
            this.chb_Yes.Name = "chb_Yes";
            this.chb_Yes.Size = new System.Drawing.Size(110, 17);
            this.chb_Yes.TabIndex = 3;
            this.chb_Yes.Text = "不发送门锁密码";
            this.chb_Yes.UseVisualStyleBackColor = true;
            // 
            // btn_Submit
            // 
            this.btn_Submit.Location = new System.Drawing.Point(858, 459);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(75, 23);
            this.btn_Submit.TabIndex = 4;
            this.btn_Submit.Text = "提交";
            this.btn_Submit.UseVisualStyleBackColor = true;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // ConfirmContractForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 676);
            this.Controls.Add(this.btn_Submit);
            this.Controls.Add(this.chb_Yes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtb_ContractIds);
            this.Name = "ConfirmContractForm";
            this.Text = "ConfirmContractForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_ContractIds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chb_Yes;
        private System.Windows.Forms.Button btn_Submit;
    }
}
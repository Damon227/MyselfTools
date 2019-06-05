namespace DamonHelper
{
    partial class ApartmentMegrateForm
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
            this.rtb_ApartmentIds = new System.Windows.Forms.RichTextBox();
            this.lable1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txb_TenancyId = new System.Windows.Forms.TextBox();
            this.btn_Megrate = new System.Windows.Forms.Button();
            this.rtb_Output = new System.Windows.Forms.RichTextBox();
            this.txb_Cellphone = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rtb_ApartmentIds
            // 
            this.rtb_ApartmentIds.Location = new System.Drawing.Point(344, 43);
            this.rtb_ApartmentIds.Name = "rtb_ApartmentIds";
            this.rtb_ApartmentIds.Size = new System.Drawing.Size(526, 119);
            this.rtb_ApartmentIds.TabIndex = 0;
            this.rtb_ApartmentIds.Text = "";
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Location = new System.Drawing.Point(274, 43);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(64, 13);
            this.lable1.TabIndex = 1;
            this.lable1.Text = "公寓Id列表";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(293, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "商户Id";
            // 
            // txb_TenancyId
            // 
            this.txb_TenancyId.Location = new System.Drawing.Point(344, 186);
            this.txb_TenancyId.Name = "txb_TenancyId";
            this.txb_TenancyId.Size = new System.Drawing.Size(287, 20);
            this.txb_TenancyId.TabIndex = 3;
            // 
            // btn_Megrate
            // 
            this.btn_Megrate.Location = new System.Drawing.Point(795, 271);
            this.btn_Megrate.Name = "btn_Megrate";
            this.btn_Megrate.Size = new System.Drawing.Size(75, 23);
            this.btn_Megrate.TabIndex = 4;
            this.btn_Megrate.Text = "迁移";
            this.btn_Megrate.UseVisualStyleBackColor = true;
            this.btn_Megrate.Click += new System.EventHandler(this.btn_Megrate_Click);
            // 
            // rtb_Output
            // 
            this.rtb_Output.Location = new System.Drawing.Point(344, 321);
            this.rtb_Output.Name = "rtb_Output";
            this.rtb_Output.Size = new System.Drawing.Size(526, 323);
            this.rtb_Output.TabIndex = 5;
            this.rtb_Output.Text = "";
            // 
            // txb_Cellphone
            // 
            this.txb_Cellphone.Location = new System.Drawing.Point(344, 236);
            this.txb_Cellphone.Name = "txb_Cellphone";
            this.txb_Cellphone.Size = new System.Drawing.Size(287, 20);
            this.txb_Cellphone.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "管理员手机号";
            // 
            // ApartmentMegrateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 676);
            this.Controls.Add(this.txb_Cellphone);
            this.Controls.Add(this.rtb_Output);
            this.Controls.Add(this.btn_Megrate);
            this.Controls.Add(this.txb_TenancyId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lable1);
            this.Controls.Add(this.rtb_ApartmentIds);
            this.Name = "ApartmentMegrateForm";
            this.Text = "ApartmentMegrateForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_ApartmentIds;
        private System.Windows.Forms.Label lable1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_TenancyId;
        private System.Windows.Forms.Button btn_Megrate;
        private System.Windows.Forms.RichTextBox rtb_Output;
        private System.Windows.Forms.TextBox txb_Cellphone;
        private System.Windows.Forms.Label label2;
    }
}
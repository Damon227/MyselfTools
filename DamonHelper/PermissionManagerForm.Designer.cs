namespace DamonHelper
{
    partial class PermissionManagerForm
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
            this.btn_AddToAdmin = new System.Windows.Forms.Button();
            this.btn_AddToAll = new System.Windows.Forms.Button();
            this.rtb_PermissionIds = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_RemoveFromAdmin = new System.Windows.Forms.Button();
            this.btn_RemoveFromAll = new System.Windows.Forms.Button();
            this.rtb_Output = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(299, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "权限Id列表";
            // 
            // btn_AddToAdmin
            // 
            this.btn_AddToAdmin.Location = new System.Drawing.Point(542, 198);
            this.btn_AddToAdmin.Name = "btn_AddToAdmin";
            this.btn_AddToAdmin.Size = new System.Drawing.Size(132, 23);
            this.btn_AddToAdmin.TabIndex = 2;
            this.btn_AddToAdmin.Text = "添加到商户管理员";
            this.btn_AddToAdmin.UseVisualStyleBackColor = true;
            this.btn_AddToAdmin.Click += new System.EventHandler(this.btn_AddToAdmin_Click);
            // 
            // btn_AddToAll
            // 
            this.btn_AddToAll.Location = new System.Drawing.Point(695, 197);
            this.btn_AddToAll.Name = "btn_AddToAll";
            this.btn_AddToAll.Size = new System.Drawing.Size(132, 23);
            this.btn_AddToAll.TabIndex = 2;
            this.btn_AddToAll.Text = "添加到所有用户";
            this.btn_AddToAll.UseVisualStyleBackColor = true;
            this.btn_AddToAll.Click += new System.EventHandler(this.btn_AddToAll_Click);
            // 
            // rtb_PermissionIds
            // 
            this.rtb_PermissionIds.Location = new System.Drawing.Point(369, 42);
            this.rtb_PermissionIds.Name = "rtb_PermissionIds";
            this.rtb_PermissionIds.Size = new System.Drawing.Size(458, 96);
            this.rtb_PermissionIds.TabIndex = 3;
            this.rtb_PermissionIds.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(366, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(246, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "注：权限Id用逗号隔开，不要加引号";
            // 
            // btn_RemoveFromAdmin
            // 
            this.btn_RemoveFromAdmin.Location = new System.Drawing.Point(542, 248);
            this.btn_RemoveFromAdmin.Name = "btn_RemoveFromAdmin";
            this.btn_RemoveFromAdmin.Size = new System.Drawing.Size(132, 23);
            this.btn_RemoveFromAdmin.TabIndex = 5;
            this.btn_RemoveFromAdmin.Text = "从商户管理员移除";
            this.btn_RemoveFromAdmin.UseVisualStyleBackColor = true;
            this.btn_RemoveFromAdmin.Click += new System.EventHandler(this.btn_RemoveFromAdmin_Click);
            // 
            // btn_RemoveFromAll
            // 
            this.btn_RemoveFromAll.Location = new System.Drawing.Point(695, 247);
            this.btn_RemoveFromAll.Name = "btn_RemoveFromAll";
            this.btn_RemoveFromAll.Size = new System.Drawing.Size(132, 23);
            this.btn_RemoveFromAll.TabIndex = 6;
            this.btn_RemoveFromAll.Text = "从所有用户移除";
            this.btn_RemoveFromAll.UseVisualStyleBackColor = true;
            this.btn_RemoveFromAll.Click += new System.EventHandler(this.btn_RemoveFromAll_Click);
            // 
            // rtb_Output
            // 
            this.rtb_Output.Location = new System.Drawing.Point(369, 354);
            this.rtb_Output.Name = "rtb_Output";
            this.rtb_Output.Size = new System.Drawing.Size(458, 272);
            this.rtb_Output.TabIndex = 7;
            this.rtb_Output.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "输出内容";
            // 
            // PermissionManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 676);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rtb_Output);
            this.Controls.Add(this.btn_RemoveFromAll);
            this.Controls.Add(this.btn_RemoveFromAdmin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtb_PermissionIds);
            this.Controls.Add(this.btn_AddToAll);
            this.Controls.Add(this.btn_AddToAdmin);
            this.Controls.Add(this.label1);
            this.Name = "PermissionManagerForm";
            this.Text = "PermissionManagerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_AddToAdmin;
        private System.Windows.Forms.Button btn_AddToAll;
        private System.Windows.Forms.RichTextBox rtb_PermissionIds;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_RemoveFromAdmin;
        private System.Windows.Forms.Button btn_RemoveFromAll;
        private System.Windows.Forms.RichTextBox rtb_Output;
        private System.Windows.Forms.Label label3;
    }
}
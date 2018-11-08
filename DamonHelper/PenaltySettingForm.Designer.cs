namespace DamonHelper
{
    partial class PenaltySettingForm
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
            this.lable1 = new System.Windows.Forms.Label();
            this.cmb_Tenancies = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_Apartments = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txb_Radio = new System.Windows.Forms.TextBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.rbtn_Tenancy = new System.Windows.Forms.RadioButton();
            this.rbtn_Apartment = new System.Windows.Forms.RadioButton();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Location = new System.Drawing.Point(421, 219);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(31, 13);
            this.lable1.TabIndex = 0;
            this.lable1.Text = "商户";
            // 
            // cmb_Tenancies
            // 
            this.cmb_Tenancies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Tenancies.FormattingEnabled = true;
            this.cmb_Tenancies.Location = new System.Drawing.Point(480, 216);
            this.cmb_Tenancies.Name = "cmb_Tenancies";
            this.cmb_Tenancies.Size = new System.Drawing.Size(220, 21);
            this.cmb_Tenancies.TabIndex = 1;
            this.cmb_Tenancies.SelectedIndexChanged += new System.EventHandler(this.cmb_Tenancies_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(421, 258);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(421, 328);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "公寓";
            // 
            // cmb_Apartments
            // 
            this.cmb_Apartments.FormattingEnabled = true;
            this.cmb_Apartments.Location = new System.Drawing.Point(480, 325);
            this.cmb_Apartments.Name = "cmb_Apartments";
            this.cmb_Apartments.Size = new System.Drawing.Size(220, 21);
            this.cmb_Apartments.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(421, 370);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "费率";
            // 
            // txb_Radio
            // 
            this.txb_Radio.Location = new System.Drawing.Point(480, 367);
            this.txb_Radio.Name = "txb_Radio";
            this.txb_Radio.Size = new System.Drawing.Size(220, 20);
            this.txb_Radio.TabIndex = 3;
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(480, 418);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(94, 23);
            this.btn_Add.TabIndex = 4;
            this.btn_Add.Text = "添加";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // rbtn_Tenancy
            // 
            this.rbtn_Tenancy.AutoSize = true;
            this.rbtn_Tenancy.Location = new System.Drawing.Point(480, 256);
            this.rbtn_Tenancy.Name = "rbtn_Tenancy";
            this.rbtn_Tenancy.Size = new System.Drawing.Size(73, 17);
            this.rbtn_Tenancy.TabIndex = 5;
            this.rbtn_Tenancy.TabStop = true;
            this.rbtn_Tenancy.Text = "商户级别";
            this.rbtn_Tenancy.UseVisualStyleBackColor = true;
            this.rbtn_Tenancy.Click += new System.EventHandler(this.rbtn_Tenancy_Click);
            // 
            // rbtn_Apartment
            // 
            this.rbtn_Apartment.AutoSize = true;
            this.rbtn_Apartment.Location = new System.Drawing.Point(480, 289);
            this.rbtn_Apartment.Name = "rbtn_Apartment";
            this.rbtn_Apartment.Size = new System.Drawing.Size(73, 17);
            this.rbtn_Apartment.TabIndex = 5;
            this.rbtn_Apartment.TabStop = true;
            this.rbtn_Apartment.Text = "公寓级别";
            this.rbtn_Apartment.UseVisualStyleBackColor = true;
            this.rbtn_Apartment.MouseCaptureChanged += new System.EventHandler(this.rbtn_Apartment_MouseCaptureChanged);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(606, 418);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(94, 23);
            this.btn_Delete.TabIndex = 4;
            this.btn_Delete.Text = "删除";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(553, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "配置违约金";
            // 
            // PenaltySettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 676);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rbtn_Apartment);
            this.Controls.Add(this.rbtn_Tenancy);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.txb_Radio);
            this.Controls.Add(this.cmb_Apartments);
            this.Controls.Add(this.cmb_Tenancies);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lable1);
            this.Name = "PenaltySettingForm";
            this.Text = "PenaltySettingForm";
            this.Load += new System.EventHandler(this.PenaltySettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lable1;
        private System.Windows.Forms.ComboBox cmb_Tenancies;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_Apartments;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txb_Radio;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.RadioButton rbtn_Tenancy;
        private System.Windows.Forms.RadioButton rbtn_Apartment;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Label label4;
    }
}
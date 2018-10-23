namespace DamonHelper
{
    partial class CancelOverdueFeeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CancelOverdueFeeForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txb_CancelOverdueFeeForm_TenantName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txb_CancelOverdueFeeForm_RoomNumber = new System.Windows.Forms.TextBox();
            this.btn_CancelOverdueFeeForm_Query = new System.Windows.Forms.Button();
            this.dgv_CancelOverdueFeeForm_OrderList = new System.Windows.Forms.DataGridView();
            this.btn_CancelOverdueFee = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_Tenancies = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CancelOverdueFeeForm_OrderList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(419, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "租客姓名";
            // 
            // txb_CancelOverdueFeeForm_TenantName
            // 
            this.txb_CancelOverdueFeeForm_TenantName.Location = new System.Drawing.Point(480, 15);
            this.txb_CancelOverdueFeeForm_TenantName.Name = "txb_CancelOverdueFeeForm_TenantName";
            this.txb_CancelOverdueFeeForm_TenantName.Size = new System.Drawing.Size(182, 20);
            this.txb_CancelOverdueFeeForm_TenantName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(730, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "房间编号";
            // 
            // txb_CancelOverdueFeeForm_RoomNumber
            // 
            this.txb_CancelOverdueFeeForm_RoomNumber.Location = new System.Drawing.Point(791, 15);
            this.txb_CancelOverdueFeeForm_RoomNumber.Name = "txb_CancelOverdueFeeForm_RoomNumber";
            this.txb_CancelOverdueFeeForm_RoomNumber.Size = new System.Drawing.Size(182, 20);
            this.txb_CancelOverdueFeeForm_RoomNumber.TabIndex = 1;
            // 
            // btn_CancelOverdueFeeForm_Query
            // 
            this.btn_CancelOverdueFeeForm_Query.Location = new System.Drawing.Point(1046, 13);
            this.btn_CancelOverdueFeeForm_Query.Name = "btn_CancelOverdueFeeForm_Query";
            this.btn_CancelOverdueFeeForm_Query.Size = new System.Drawing.Size(75, 23);
            this.btn_CancelOverdueFeeForm_Query.TabIndex = 3;
            this.btn_CancelOverdueFeeForm_Query.Text = "查询";
            this.btn_CancelOverdueFeeForm_Query.UseVisualStyleBackColor = true;
            this.btn_CancelOverdueFeeForm_Query.Click += new System.EventHandler(this.btn_CancelOverdueFeeForm_Query_Click);
            // 
            // dgv_CancelOverdueFeeForm_OrderList
            // 
            this.dgv_CancelOverdueFeeForm_OrderList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_CancelOverdueFeeForm_OrderList.Location = new System.Drawing.Point(25, 56);
            this.dgv_CancelOverdueFeeForm_OrderList.Name = "dgv_CancelOverdueFeeForm_OrderList";
            this.dgv_CancelOverdueFeeForm_OrderList.ReadOnly = true;
            this.dgv_CancelOverdueFeeForm_OrderList.Size = new System.Drawing.Size(1096, 579);
            this.dgv_CancelOverdueFeeForm_OrderList.TabIndex = 4;
            this.dgv_CancelOverdueFeeForm_OrderList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CancelOverdueFeeForm_OrderList_CellContentClick);
            // 
            // btn_CancelOverdueFee
            // 
            this.btn_CancelOverdueFee.Location = new System.Drawing.Point(1046, 645);
            this.btn_CancelOverdueFee.Name = "btn_CancelOverdueFee";
            this.btn_CancelOverdueFee.Size = new System.Drawing.Size(75, 23);
            this.btn_CancelOverdueFee.TabIndex = 5;
            this.btn_CancelOverdueFee.Text = "取消违约金";
            this.btn_CancelOverdueFee.UseVisualStyleBackColor = true;
            this.btn_CancelOverdueFee.Click += new System.EventHandler(this.btn_CancelOverdueFee_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(119, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "商户列表";
            // 
            // cmb_Tenancies
            // 
            this.cmb_Tenancies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Tenancies.FormattingEnabled = true;
            this.cmb_Tenancies.Location = new System.Drawing.Point(180, 15);
            this.cmb_Tenancies.Name = "cmb_Tenancies";
            this.cmb_Tenancies.Size = new System.Drawing.Size(182, 21);
            this.cmb_Tenancies.TabIndex = 6;
            // 
            // CancelOverdueFeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1144, 676);
            this.Controls.Add(this.cmb_Tenancies);
            this.Controls.Add(this.btn_CancelOverdueFee);
            this.Controls.Add(this.dgv_CancelOverdueFeeForm_OrderList);
            this.Controls.Add(this.btn_CancelOverdueFeeForm_Query);
            this.Controls.Add(this.txb_CancelOverdueFeeForm_RoomNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txb_CancelOverdueFeeForm_TenantName);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CancelOverdueFeeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CancelOverdueFeeForm";
            this.Load += new System.EventHandler(this.CancelOverdueFeeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CancelOverdueFeeForm_OrderList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_CancelOverdueFeeForm_TenantName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_CancelOverdueFeeForm_RoomNumber;
        private System.Windows.Forms.Button btn_CancelOverdueFeeForm_Query;
        private System.Windows.Forms.DataGridView dgv_CancelOverdueFeeForm_OrderList;
        private System.Windows.Forms.Button btn_CancelOverdueFee;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_Tenancies;
    }
}
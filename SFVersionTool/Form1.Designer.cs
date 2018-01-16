namespace SFVersionTool
{
    partial class Form1
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
            this.txb_ProjectPath = new System.Windows.Forms.TextBox();
            this.btn_SelectProjectPath = new System.Windows.Forms.Button();
            this.txb_ApplicationTypeVersion = new System.Windows.Forms.TextBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.chb_PkgVersion = new System.Windows.Forms.CheckBox();
            this.chb_CodeVersion = new System.Windows.Forms.CheckBox();
            this.chb_ConfigVersion = new System.Windows.Forms.CheckBox();
            this.chb_chooseAll = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Preview = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_ActorCount = new System.Windows.Forms.Label();
            this.lbl_ChoosedActorCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目路径：";
            // 
            // txb_ProjectPath
            // 
            this.txb_ProjectPath.Location = new System.Drawing.Point(113, 26);
            this.txb_ProjectPath.Name = "txb_ProjectPath";
            this.txb_ProjectPath.ReadOnly = true;
            this.txb_ProjectPath.Size = new System.Drawing.Size(459, 20);
            this.txb_ProjectPath.TabIndex = 1;
            // 
            // btn_SelectProjectPath
            // 
            this.btn_SelectProjectPath.Location = new System.Drawing.Point(591, 25);
            this.btn_SelectProjectPath.Name = "btn_SelectProjectPath";
            this.btn_SelectProjectPath.Size = new System.Drawing.Size(148, 23);
            this.btn_SelectProjectPath.TabIndex = 2;
            this.btn_SelectProjectPath.Text = "选择项目";
            this.btn_SelectProjectPath.UseVisualStyleBackColor = true;
            this.btn_SelectProjectPath.Click += new System.EventHandler(this.btn_SelectProjectPath_Click);
            // 
            // txb_ApplicationTypeVersion
            // 
            this.txb_ApplicationTypeVersion.Location = new System.Drawing.Point(169, 682);
            this.txb_ApplicationTypeVersion.Name = "txb_ApplicationTypeVersion";
            this.txb_ApplicationTypeVersion.Size = new System.Drawing.Size(166, 20);
            this.txb_ApplicationTypeVersion.TabIndex = 4;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(1219, 679);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 5;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // chb_PkgVersion
            // 
            this.chb_PkgVersion.AutoSize = true;
            this.chb_PkgVersion.Location = new System.Drawing.Point(357, 684);
            this.chb_PkgVersion.Name = "chb_PkgVersion";
            this.chb_PkgVersion.Size = new System.Drawing.Size(83, 17);
            this.chb_PkgVersion.TabIndex = 7;
            this.chb_PkgVersion.Text = "Pkg Version";
            this.chb_PkgVersion.UseVisualStyleBackColor = true;
            // 
            // chb_CodeVersion
            // 
            this.chb_CodeVersion.AutoSize = true;
            this.chb_CodeVersion.Location = new System.Drawing.Point(455, 684);
            this.chb_CodeVersion.Name = "chb_CodeVersion";
            this.chb_CodeVersion.Size = new System.Drawing.Size(89, 17);
            this.chb_CodeVersion.TabIndex = 7;
            this.chb_CodeVersion.Text = "Code Version";
            this.chb_CodeVersion.UseVisualStyleBackColor = true;
            // 
            // chb_ConfigVersion
            // 
            this.chb_ConfigVersion.AutoSize = true;
            this.chb_ConfigVersion.Location = new System.Drawing.Point(550, 684);
            this.chb_ConfigVersion.Name = "chb_ConfigVersion";
            this.chb_ConfigVersion.Size = new System.Drawing.Size(94, 17);
            this.chb_ConfigVersion.TabIndex = 7;
            this.chb_ConfigVersion.Text = "Config Version";
            this.chb_ConfigVersion.UseVisualStyleBackColor = true;
            // 
            // chb_chooseAll
            // 
            this.chb_chooseAll.AutoSize = true;
            this.chb_chooseAll.Location = new System.Drawing.Point(47, 64);
            this.chb_chooseAll.Name = "chb_chooseAll";
            this.chb_chooseAll.Size = new System.Drawing.Size(50, 17);
            this.chb_chooseAll.TabIndex = 8;
            this.chb_chooseAll.Text = "全选";
            this.chb_chooseAll.UseVisualStyleBackColor = true;
            this.chb_chooseAll.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chb_chooseAll_MouseClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(47, 82);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1247, 573);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 685);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "应用程序版本号：";
            // 
            // btn_Preview
            // 
            this.btn_Preview.Location = new System.Drawing.Point(1124, 679);
            this.btn_Preview.Name = "btn_Preview";
            this.btn_Preview.Size = new System.Drawing.Size(75, 23);
            this.btn_Preview.TabIndex = 5;
            this.btn_Preview.Text = "预览";
            this.btn_Preview.UseVisualStyleBackColor = true;
            this.btn_Preview.Click += new System.EventHandler(this.btn_Preview_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1096, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "已选择项目数量：";
            // 
            // lbl_ActorCount
            // 
            this.lbl_ActorCount.AutoSize = true;
            this.lbl_ActorCount.Location = new System.Drawing.Point(1257, 63);
            this.lbl_ActorCount.Name = "lbl_ActorCount";
            this.lbl_ActorCount.Size = new System.Drawing.Size(13, 13);
            this.lbl_ActorCount.TabIndex = 12;
            this.lbl_ActorCount.Text = "0";
            // 
            // lbl_ChoosedActorCount
            // 
            this.lbl_ChoosedActorCount.AutoSize = true;
            this.lbl_ChoosedActorCount.Location = new System.Drawing.Point(1216, 63);
            this.lbl_ChoosedActorCount.Name = "lbl_ChoosedActorCount";
            this.lbl_ChoosedActorCount.Size = new System.Drawing.Size(13, 13);
            this.lbl_ChoosedActorCount.TabIndex = 12;
            this.lbl_ChoosedActorCount.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1237, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "/";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1345, 726);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbl_ChoosedActorCount);
            this.Controls.Add(this.lbl_ActorCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chb_chooseAll);
            this.Controls.Add(this.chb_ConfigVersion);
            this.Controls.Add(this.chb_CodeVersion);
            this.Controls.Add(this.chb_PkgVersion);
            this.Controls.Add(this.btn_Preview);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.txb_ApplicationTypeVersion);
            this.Controls.Add(this.btn_SelectProjectPath);
            this.Controls.Add(this.txb_ProjectPath);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service Fabric Version Tool";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_ProjectPath;
        private System.Windows.Forms.Button btn_SelectProjectPath;
        private System.Windows.Forms.TextBox txb_ApplicationTypeVersion;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.CheckBox chb_PkgVersion;
        private System.Windows.Forms.CheckBox chb_CodeVersion;
        private System.Windows.Forms.CheckBox chb_ConfigVersion;
        private System.Windows.Forms.CheckBox chb_chooseAll;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Preview;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_ActorCount;
        private System.Windows.Forms.Label lbl_ChoosedActorCount;
        private System.Windows.Forms.Label label5;
    }
}


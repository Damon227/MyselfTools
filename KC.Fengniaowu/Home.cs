// ***********************************************************************
// Solution         : MyselfTools
// Project          : KC.Fengniaowu
// File             : Home.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2017 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Windows.Forms;

namespace KC.Fengniaowu
{
    public partial class Home : Form
    {
        private SFVersionToolWindow _sfVersionToolWindow;

        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            _sfVersionToolWindow = new SFVersionToolWindow();
        }

        /// <summary>
        ///     显示 Service Fabric Version Tool 界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sFVersionToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sfVersionToolWindow.Show();

            panel1.Controls.Clear();
            panel1.Controls.Add(_sfVersionToolWindow);
        }

       
    }
}
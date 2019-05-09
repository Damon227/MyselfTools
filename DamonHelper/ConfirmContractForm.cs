using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DamonHelper
{
    /// <summary>
    ///     确认合同
    /// </summary>
    public partial class ConfirmContractForm : Form
    {
        public ConfirmContractForm()
        {
            InitializeComponent();
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            // 通过contractId查询userId

            string contractIdString = rtb_ContractIds.Text;
            bool unsendLockInfo = chb_Yes.Checked;

            if (string.IsNullOrEmpty(contractIdString))
            {
                MessageBox.Show("合同Id不能为空");
                return;
            }

            string[] contractIds = contractIdString.Split(',');
            if (contractIds.Length == 0)
            {
                MessageBox.Show("合同Id格式不正确，需以逗号隔开，不加引号");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DamonHelper.Config;
using DamonHelper.Models;
using DamonHelper.sys;
using Dapper;
using Newtonsoft.Json.Linq;

namespace DamonHelper
{
    public partial class CancelOverdueFeeForm : Form
    {
        private static (string SessionId, DateTimeOffset ExpiryTime) s_sessionIdCache = (null, Time.Now);
        private readonly IList<int> _selectedRowIndexs  = new List<int>();

        public CancelOverdueFeeForm()
        {
            InitializeComponent();
        }

        private void CancelOverdueFeeForm_Load(object sender, EventArgs e)
        {
            _selectedRowIndexs.Clear();

            InitTenancies();
        }

        private void InitTenancies()
        {
            string sql = "select TenancyId, TenancyName from dbo.[KC.Fengniaowu.Talos.Tenancies] where Enabled = 1";
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                List<SimpleTenancy> result = connection.Query<SimpleTenancy>(sql).ToList();

                cmb_Tenancies.DataSource = result;
                cmb_Tenancies.DisplayMember = "TenancyName";
                cmb_Tenancies.ValueMember = "TenancyId";
            }
        }

        private void btn_CancelOverdueFeeForm_Query_Click(object sender, EventArgs e)
        {
            QueryOrders();
        }

        /// <summary>
        ///     查询逾期账单
        /// </summary>
        private void QueryOrders()
        {
            //  清空DataGridView
            dgv_CancelOverdueFeeForm_OrderList.DataSource = null;
            dgv_CancelOverdueFeeForm_OrderList.Columns.Clear();

            string tenantName = txb_CancelOverdueFeeForm_TenantName.Text;
            string roomNumber = txb_CancelOverdueFeeForm_RoomNumber.Text;
            string tenancyId = cmb_Tenancies.SelectedValue.ToString();

            string whereString = null;
            if (!string.IsNullOrEmpty(tenancyId))
            {
                whereString = $"and orders.AssetTenancyId = '{tenancyId}' ";
            }

            if (!string.IsNullOrEmpty(tenantName))
            {
                whereString += $"and orders.PayeeDraweeRealName like '%{tenantName}%'";
            }

            if (!string.IsNullOrEmpty(roomNumber))
            {
                whereString += $" and rooms.RoomNumber like '%{roomNumber}%'";
            }

            string sql = $@"
            select 
            apartments.ApartmentId,
            apartments.ApartmentName,
            orders.PayeeDraweeRealName,
            orders.TenantId,
            orders.OrderId,
            orders.OrderState,
            orders.Amount,
            orders.PropertyManagementAmount,
            orders.PenaltyAmount,
            orders.OrderStartTime,
            orders.OrderEndTime,
            orders.PaymentTime,
            (select isnull(sum(Amount),0) from dbo.[KC.Fengniaowu.Talos.Transactions] where Enabled = 1 and TransactionState = 'Succeed' and OrderId = orders.OrderId) as PaidAmount,
            rooms.RoomNumber

            from dbo.[KC.Fengniaowu.Talos.Orders] as orders

            left join dbo.[KC.Fengniaowu.Talos.Apartments] as apartments 
	            on orders.ApartmentId = apartments.ApartmentId and apartments.Enabled = 1
            left join dbo.[KC.Fengniaowu.Talos.Rooms] as rooms
	            on orders.RoomId = rooms.RoomId and rooms.Enabled = 1

            where orders.Enabled = 1 and orders.OrderState = 'Overdue' and orders.PenaltyAmount > 0 {whereString} 
            order by apartments.ApartmentName asc, orders.PayeeDraweeRealName asc";

            SqlConnection connection = SqlConnectionExtensions.GetConnection();
            List<Order> orders = connection.Query<Order>(sql).ToList();

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn { HeaderText = "选择", Name = "check" };

            dgv_CancelOverdueFeeForm_OrderList.Columns.Insert(0, checkBoxColumn);

            dgv_CancelOverdueFeeForm_OrderList.DataSource = orders;

            SetDataGridViewStyle();
        }

        /// <summary>
        ///     设置表格样式
        /// </summary>
        private void SetDataGridViewStyle()
        {
            SetDataGridView();

            dgv_CancelOverdueFeeForm_OrderList.DefaultCellStyle.Font = new Font("UTF-8", 10);
            dgv_CancelOverdueFeeForm_OrderList.RowHeadersVisible = false;
            dgv_CancelOverdueFeeForm_OrderList.AllowUserToResizeRows = false;
            dgv_CancelOverdueFeeForm_OrderList.ReadOnly = true;
            dgv_CancelOverdueFeeForm_OrderList.Columns[0].Width = 50;
            dgv_CancelOverdueFeeForm_OrderList.Columns[1].Visible = false;
            dgv_CancelOverdueFeeForm_OrderList.Columns[2].Visible = false;
            dgv_CancelOverdueFeeForm_OrderList.Columns[4].Width = 150;
            dgv_CancelOverdueFeeForm_OrderList.Columns[7].Visible = false;
            dgv_CancelOverdueFeeForm_OrderList.Columns[8].Width = 80;
            dgv_CancelOverdueFeeForm_OrderList.Columns[9].Width = 80;
            dgv_CancelOverdueFeeForm_OrderList.Columns[10].Width = 80;
            dgv_CancelOverdueFeeForm_OrderList.Columns[11].Width = 80;
            dgv_CancelOverdueFeeForm_OrderList.Columns[12].Width = 80;
            dgv_CancelOverdueFeeForm_OrderList.Columns[13].Width = 160;
            dgv_CancelOverdueFeeForm_OrderList.Columns[14].Width = 160;
            dgv_CancelOverdueFeeForm_OrderList.Columns[15].Width = 160;

            dgv_CancelOverdueFeeForm_OrderList.Columns[13].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            dgv_CancelOverdueFeeForm_OrderList.Columns[14].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            dgv_CancelOverdueFeeForm_OrderList.Columns[15].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
        }

        /// <summary>
        ///     优化控件，下拉时不闪
        /// </summary>
        private void SetDataGridView()
        {
            Type type = dgv_CancelOverdueFeeForm_OrderList.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(dgv_CancelOverdueFeeForm_OrderList, true, null);
            }
        }

        /// <summary>
        ///     取消违约金
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_CancelOverdueFee_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("您确定要取消违约金吗", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            await CancelOverdueFeeAsync();

            QueryOrders();
        }

        private async Task CancelOverdueFeeAsync()
        {
            if (_selectedRowIndexs.Count == 0)
            {
                MessageBox.Show("请选择逾期账单");
                return;
            }

            foreach (int index in _selectedRowIndexs)
            {
                DataGridViewRow selectedRow = dgv_CancelOverdueFeeForm_OrderList.Rows[index];

                string orderId = selectedRow.Cells["OrderId"].Value.ToString();
                long totalAmount = Convert.ToInt64(selectedRow.Cells["TotalAmount"].Value);
                long paidAmount = Convert.ToInt64(selectedRow.Cells["PaidAmount"].Value);

                SqlConnection connection = SqlConnectionExtensions.GetConnection();

                if (paidAmount >= totalAmount)
                {
                    // 取消违约金，修改账单已支付

                    string sql = $@"
                    update dbo.[KC.Fengniaowu.Talos.Orders] 
                    set OrderState = 'Paid',
                    PenaltyAmount = 0,
                    ActualPaymentTime = (select top 1 PaymentTime from dbo.[KC.Fengniaowu.Talos.Transactions] where Enabled = 1 and OrderId = '{orderId}' order by PaymentTime desc) 
                    where OrderId = '{orderId}'";

                    int result = connection.Execute(sql);
                    if (result > 0)
                    {
                        // 清除缓存
                        await ClearCacheOfOrderAsync(orderId);
                    }
                    else
                    {
                        MessageBox.Show("取消违约金SQL语句执行失败");
                    }
                }
                else
                {
                    // 取消违约金
                    string sql = $@"
                    update dbo.[KC.Fengniaowu.Talos.Orders] 
                    set PenaltyAmount = 0 
                    where OrderId = '{orderId}'";

                    int result = connection.Execute(sql);
                    if (result > 0)
                    {
                        // 清除缓存
                        await ClearCacheOfOrderAsync(orderId);
                    }
                    else
                    {
                        MessageBox.Show("取消违约金SQL语句执行失败");
                    }
                }
            }
        }

        private static async Task ClearCacheOfOrderAsync(string orderId)
        {
            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(Config.Config.TalosBaseAddress) };

            string sessionId;
            if (!string.IsNullOrEmpty(s_sessionIdCache.SessionId) && s_sessionIdCache.ExpiryTime < Time.Now)
            {
                sessionId = s_sessionIdCache.SessionId;
            }
            else
            {
                var content0 = new
                {
                    TenancyId = "28342C66E8EE408E9070452799CC8F6E",
                    LoginInfoAccount = "10100000000",
                    Password = "KC@2018"
                };

                HttpResponseMessage response0 = await httpClient.PostAsJsonAsync("api/PasswordLogin/Account/Login", content0);
                if (!response0.IsSuccessStatusCode)
                {
                    MessageBox.Show("管理员登录失败");
                    return;
                }

                string responseString0 = await response0.Content.ReadAsStringAsync();
                JObject obj0 = JObject.Parse(responseString0);
                if (!(bool)obj0["succeeded"])
                {
                    MessageBox.Show("管理员登录失败" + obj0["message"]);
                    return;
                }

                sessionId = obj0["headers"]["x-KC-SID"].ToString();
            }


            httpClient.DefaultRequestHeaders.Add("X-KC-SID", sessionId);

            var content1 = new
            {
                OrderId = orderId
            };

            HttpResponseMessage response1 = await httpClient.PostAsJsonAsync("api/Order/ClearCache", content1);
            if (!response1.IsSuccessStatusCode)
            {
                MessageBox.Show("账单缓存清除失败");
                return;
            }

            string responseString1 = await response1.Content.ReadAsStringAsync();
            JObject obj1 = JObject.Parse(responseString1);
            if (!(bool)obj1["succeeded"])
            {
                MessageBox.Show("账单缓存清除失败" + obj1["message"]);
            }

            //MessageBox.Show("账单缓存清除成功");
        }

        /// <summary>
        ///     单选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_CancelOverdueFeeForm_OrderList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dgv_CancelOverdueFeeForm_OrderList.Rows[e.RowIndex].Cells["check"];
                bool flag = Convert.ToBoolean(checkBoxCell.Value);
                checkBoxCell.Value = !flag;

                if (!flag)
                {
                    _selectedRowIndexs.Add(e.RowIndex);
                    //dgv_CancelOverdueFeeForm_OrderList.Rows[e.RowIndex].Selected = true;
                    dgv_CancelOverdueFeeForm_OrderList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
                else
                {
                    _selectedRowIndexs.Remove(e.RowIndex);
                    //dgv_CancelOverdueFeeForm_OrderList.Rows[e.RowIndex].Selected = false;
                    dgv_CancelOverdueFeeForm_OrderList.Rows[e.RowIndex].DefaultCellStyle.BackColor = DefaultBackColor;
                }
            }
        }
    }
}

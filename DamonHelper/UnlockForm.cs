using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DamonHelper.Helper;
using DamonHelper.Settings;
using Dapper;
using Newtonsoft.Json.Linq;

namespace DamonHelper
{
    public partial class UnlockForm : Form
    {
        public UnlockForm()
        {
            InitializeComponent();
        }

        private async void btn_Unlock_Click(object sender, EventArgs e)
        {
            string cellphone = txb_Cellphone.Text.Trim();
            if (string.IsNullOrEmpty(cellphone))
            {
                MessageBox.Show("手机号不能为空");
                return;
            }

            if (cellphone == "10100000000")
            {
                MessageBox.Show("该手机号无法解锁");
                return;
            }

            // 通过手机号查询UserId
            string userId;
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                string sql = $"select UserId from dbo.[KC.Fengniaowu.Talos.Users] where UserName = '{cellphone}' and Enabled = 1";
                userId = connection.QueryFirstOrDefault<string>(sql);
            }

            if (userId == null)
            {
                MessageBox.Show("解锁失败，手机号不存在");
                return;
            }

            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(Config.TalosBaseAddress) };
            httpClient.DefaultRequestHeaders.Add("X-KC-SID", SessionIdHelper.SessionId);

            var content = new
            {
                UserId = userId
            };
            HttpResponseMessage response = httpClient.PostAsJsonAsync("api/Password/Unlock", content).Result;
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show($"解锁失败，解锁接口调用失败，HttpStatusCode：{response.StatusCode}");
                return;
            }

            string responseString1 = await response.Content.ReadAsStringAsync();
            JObject obj = JObject.Parse(responseString1);
            if ((bool)obj["succeeded"])
            {
                MessageBox.Show("解锁成功");
                return;
            }


            MessageBox.Show($"解锁失败，code：{obj["code"]}，message：{obj["message"]}");
        }
    }
}

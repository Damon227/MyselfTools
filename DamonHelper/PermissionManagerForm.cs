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
using DamonHelper.sys;
using DamonHelper.Settings;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DamonHelper
{
    public partial class PermissionManagerForm : Form
    {
        public PermissionManagerForm()
        {
            InitializeComponent();
        }

        private async void btn_AddToAdmin_Click(object sender, EventArgs e)
        {
            string permissionIdString = rtb_PermissionIds.Text.Trim();
            if (string.IsNullOrEmpty(permissionIdString))
            {
                this.Show("权限Id不能为空");
                return;
            }

            List<string> permissionIds = permissionIdString.Split(',').ToList();
            if (permissionIds.Count == 0)
            {
                this.Show("权限Id不能为空");
                return;
            }

            DialogResult dialogResult = this.Show("确定添加到商户管理员吗", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                foreach (string permissionId in permissionIds)
                {
                    string sql1 = $"select * from dbo.[KC.Fengniaowu.Talos.Permissions] where Enabled = 1 and PermissionId = '{permissionId}'";

                    dynamic result = connection.QueryFirstOrDefault<dynamic>(sql1);
                    if (result == null)
                    {
                        this.Show($"权限Id({permissionId})不存在");
                        return;
                    }
                }
            }

            // 查询所有商户管理员
            string sql2 = "select * from dbo.[KC.Fengniaowu.Talos.Users] where Enabled = 1 and Permissions like (select '%'+PermissionId+'%' from dbo.[KC.Fengniaowu.Talos.Permissions] where PermissionCode = 'TenancyAdministrator@fengniaowu.tenancy')";

            dynamic users;
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                users = connection.Query<dynamic>(sql2);
            }

            if (users == null)
            {
                this.Show("未找到商户管理员");
                return;
            }

            await AddPermissionsAsync(users, permissionIds);
        }

        private async void btn_AddToAll_Click(object sender, EventArgs e)
        {
            string permissionIdString = rtb_PermissionIds.Text.Trim();
            if (string.IsNullOrEmpty(permissionIdString))
            {
                this.Show("权限Id不能为空");
                return;
            }

            List<string> permissionIds = permissionIdString.Split(',').ToList();
            if (permissionIds.Count == 0)
            {
                this.Show("权限Id不能为空");
                return;
            }

            DialogResult dialogResult = this.Show("确定添加到所有用户吗", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                foreach (string permissionId in permissionIds)
                {
                    string sql1 = $"select * from dbo.[KC.Fengniaowu.Talos.Permissions] where Enabled = 1 and PermissionId = '{permissionId}'";

                    dynamic result = connection.QueryFirstOrDefault<dynamic>(sql1);
                    if (result == null)
                    {
                        this.Show($"权限Id({permissionId})不存在");
                        return;
                    }
                }
            }

            // 查询所有用户
            string sql2 = "select * from dbo.[KC.Fengniaowu.Talos.Users] where Enabled = 1 and Permissions like (select '%'+PermissionId+'%' from dbo.[KC.Fengniaowu.Talos.Permissions] where PermissionCode = 'Account@fengniaowu.tenancy')";

            dynamic users;
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                users = connection.Query<dynamic>(sql2);
            }

            if (users == null)
            {
                this.Show("未找到用户");
                return;
            }

            await AddPermissionsAsync(users, permissionIds);
        }

        private async Task AddPermissionsAsync(dynamic users, List<string> permissionIds)
        {
            List<string> failedUsers = new List<string>();

            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                foreach (dynamic user in users)
                {
                    // 若已有该权限，跳过
                    string addPermissionIds = null;
                    foreach (string permissionId in permissionIds)
                    {
                        if (!user.Permissions.Contains(permissionId))
                        {
                            addPermissionIds += "\"" + permissionId + "\",";
                        }
                    }

                    if (addPermissionIds == null)
                    {
                        continue;
                    }

                    addPermissionIds = addPermissionIds.Remove(addPermissionIds.Length - 1, 1);

                    string permissions = user.Permissions;
                    string newPermissions = permissions.Remove(permissions.Length - 1, 1) + "," + addPermissionIds + "]";
                    user.Permissions = newPermissions;

                    string sql4 = $"update dbo.[KC.Fengniaowu.Talos.Users] set Permissions = '{newPermissions}' where UserId = '{user.UserId}'";

                    int row = connection.Execute(sql4);
                    if (row < 0)
                    {
                        failedUsers.Add(user.UserId);
                        AppendMessage($"{user.UserId}，更新数据库失败。");
                    }
                    else
                    {
                        // 刷新用户缓存
                        await ClearCacheOfUserAsync(user, failedUsers);
                    }
                }
            }

            AppendMessage($"执行结束，失败数量：{failedUsers.Count}。");
        }

        private async Task DeletePermissionsAsync(dynamic users, List<string> permissionIds)
        {
            List<string> failedUsers = new List<string>();

            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                foreach (dynamic user in users)
                {
                    List<string> permissions = JsonConvert.DeserializeObject<List<string>>(user.Permissions);
                    int oldCount = permissions.Count;

                    foreach (string permissionId in permissionIds)
                    {
                        if (permissions.Contains(permissionId))
                        {
                            permissions.Remove(permissionId);
                        }
                    }

                    // user 权限没有任何变化时，跳过
                    if (oldCount == permissions.Count)
                    {
                        continue;
                    }

                    string newPermissions = JsonConvert.SerializeObject(permissions);
                    user.Permissions = newPermissions;

                    string sql4 = $"update dbo.[KC.Fengniaowu.Talos.Users] set Permissions = '{newPermissions}' where UserId = '{user.UserId}'";

                    int row = connection.Execute(sql4);
                    if (row < 0)
                    {
                        failedUsers.Add(user.UserId);
                        AppendMessage($"{user.UserId}，更新数据库失败。");
                    }
                    else
                    {
                        // 刷新用户缓存
                        await ClearCacheOfUserAsync(user, failedUsers);
                    }
                }
            }

            AppendMessage($"执行结束，失败数量：{failedUsers.Count}。");
        }

        private async Task ClearCacheOfUserAsync(dynamic user, List<string> failedUsers)
        {
            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(Config.TalosBaseAddress) };

            string sessionId = SessionIdHelper.SessionId;
            if (string.IsNullOrEmpty(sessionId))
            {
                return;
            }

            httpClient.DefaultRequestHeaders.Add("X-KC-SID", sessionId);

            var content = new
            {
                userId = user.UserId,
                userName = user.UserName,
                description = user.Description,
                enabled = user.Enabled,
                roles = JsonConvert.DeserializeObject<List<string>>(user.Roles),
                permissions = JsonConvert.DeserializeObject<List<string>>(user.Permissions),
                data = JsonConvert.DeserializeObject<Dictionary<string, string>>(user.Data)
            };

            HttpResponseMessage response1 = await httpClient.PostAsJsonAsync("api/Identities/Users/Update", content);
            if (!response1.IsSuccessStatusCode)
            {
                failedUsers.Add(user.UserId);
                AppendMessage($"{user.UserId}，清除缓存失败");
                return;
            }

            string responseString1 = await response1.Content.ReadAsStringAsync();
            JObject obj1 = JObject.Parse(responseString1);
            if (!(bool)obj1["succeeded"])
            {
                failedUsers.Add(user.UserId);
                AppendMessage( $"{user.UserId}，清除缓存失败");
            }
            else
            {
                AppendMessage($"{user.UserId}，成功");
            }
        }

        private async void btn_RemoveFromAdmin_Click(object sender, EventArgs e)
        {
            string permissionIdString = rtb_PermissionIds.Text.Trim();
            if (string.IsNullOrEmpty(permissionIdString))
            {
                this.Show("权限Id不能为空");
                return;
            }

            List<string> permissionIds = permissionIdString.Split(',').ToList();
            if (permissionIds.Count == 0)
            {
                this.Show("权限Id不能为空");
                return;
            }

            DialogResult dialogResult = this.Show("确定从商户管理员移除吗", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                foreach (string permissionId in permissionIds)
                {
                    string sql1 = $"select * from dbo.[KC.Fengniaowu.Talos.Permissions] where Enabled = 1 and PermissionId = '{permissionId}'";

                    dynamic result = connection.QueryFirstOrDefault<dynamic>(sql1);
                    if (result == null)
                    {
                        this.Show($"权限Id({permissionId})不存在");
                        return;
                    }
                }
            }

            // 查询所有商户管理员
            string sql2 = "select * from dbo.[KC.Fengniaowu.Talos.Users] where Enabled = 1 and Permissions like (select '%'+PermissionId+'%' from dbo.[KC.Fengniaowu.Talos.Permissions] where PermissionCode = 'TenancyAdministrator@fengniaowu.tenancy')";

            dynamic users;
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                users = connection.Query<dynamic>(sql2);
            }

            if (users == null)
            {
                this.Show("未找到商户管理员");
                return;
            }

            await DeletePermissionsAsync(users, permissionIds);
        }

        private async void btn_RemoveFromAll_Click(object sender, EventArgs e)
        {
            string permissionIdString = rtb_PermissionIds.Text.Trim();
            if (string.IsNullOrEmpty(permissionIdString))
            {
                this.Show("权限Id不能为空");
                return;
            }

            List<string> permissionIds = permissionIdString.Split(',').ToList();
            if (permissionIds.Count == 0)
            {
                this.Show("权限Id不能为空");
                return;
            }

            DialogResult dialogResult = this.Show("确定从所有用户移除吗", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                foreach (string permissionId in permissionIds)
                {
                    string sql1 = $"select * from dbo.[KC.Fengniaowu.Talos.Permissions] where Enabled = 1 and PermissionId = '{permissionId}'";

                    dynamic result = connection.QueryFirstOrDefault<dynamic>(sql1);
                    if (result == null)
                    {
                        this.Show($"权限Id({permissionId})不存在");
                        return;
                    }
                }
            }

            // 查询所有用户
            string sql2 = "select * from dbo.[KC.Fengniaowu.Talos.Users] where Enabled = 1 and Permissions like (select '%'+PermissionId+'%' from dbo.[KC.Fengniaowu.Talos.Permissions] where PermissionCode = 'Account@fengniaowu.tenancy')";

            dynamic users;
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                users = connection.Query<dynamic>(sql2);
            }

            if (users == null)
            {
                this.Show("未找到用户");
                return;
            }

            await DeletePermissionsAsync(users, permissionIds);
        }

        private void AppendMessage(string text)
        {
            StringBuilder oldText = new StringBuilder();
            oldText.Append(rtb_Output.Text);
            oldText.Append($"{Time.Now:yyyy-MM-dd HH:mm:ss} {text}\r\n");

            rtb_Output.Text = oldText.ToString();

            rtb_Output.SelectionStart = rtb_Output.TextLength;
            rtb_Output.Focus();
        }
    }
}

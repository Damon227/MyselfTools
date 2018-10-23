using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DamonHelper.Config;
using DamonHelper.Models;
using DamonHelper.sys;
using Dapper;

namespace DamonHelper
{
    public partial class RegisterTenancyForm : Form
    {
        /// <summary>
        ///     财务拥有的权限
        /// </summary>
        private readonly List<string> _permissionCodeCaiwu = new List<string>
        {
            "RoomStatusPage@fengniaowu.tenancy.asset","ContractsPage@fengniaowu.tenancy.asset","OrdersPage@fengniaowu.tenancy.asset","TransactionsPage@fengniaowu.tenancy.asset","UtilityExpensesPage@fengniaowu.tenancy.asset","ReservationsPage@fengniaowu.tenancy.asset","CreateRoomReservation@fengniaowu.tenancy.asset","UpdateRoomReservation@fengniaowu.tenancy.asset","CreateOrder@fengniaowu.tenancy.asset","UpdateOrder@fengniaowu.tenancy.asset","DisableOrder@fengniaowu.tenancy.asset","DownloadContracts@fengniaowu.tenancy.asset","DownloadOrders@fengniaowu.tenancy.asset","CreateTransaction@fengniaowu.tenancy.asset","DownloadTransactions@fengniaowu.tenancy.asset","CancelRoomReservation@fengniaowu.tenancy.asset","RevokeRoomReservation@fengniaowu.tenancy.asset"
        };

        public RegisterTenancyForm()
        {
            InitializeComponent();
        }

        private void RegisterTenancyForm_Load(object sender, EventArgs e)
        {
            // 设置默认值
            txb_License.Text = "@xxx.com";
            txb_Domain.Text = "@fengniaowu.tenancy.asset.xxx";
            txb_TenancyType.Text = "Asset";
            txb_AccountName.Text = "系统管理员";
            txb_RealName.Text = "系统管理员";
            txb_CredentialType.Text = "IDCard";
            txb_CredentialNo.Text = "000000000000000000";

            Init();
            //InitForTest();
        }

        /// <summary>
        ///     初始化部分值
        /// </summary>
        private void Init()
        {
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                string sql = "select CityName from dbo.[KC.Fengniaowu.Talos.Cities] where Enabled = 1";
                List<string> cities = connection.Query<string>(sql).ToList();
                if (cities.Count > 0)
                {
                    cmb_CityName.DataSource = cities;
                }

                string sql2 = "select top 1 Cellphone from dbo.[KC.Fengniaowu.Talos.Accounts] where Enabled = 1 and Cellphone like '%10500000%' order by Cellphone desc";
                string cellphone = connection.QueryFirst<string>(sql2);

                string numberString = cellphone.Substring(8, 3);
                int number = Convert.ToInt32(numberString);
                number++;

                string newCellphone = cellphone.Substring(0, 8);
                if (number < 100)
                {
                    newCellphone += $"0{number}";
                }
                else if (number >= 100 && number < 1000)
                {
                    newCellphone += number;
                }
                else
                {
                    MessageBox.Show("手机号已超过999个");
                }

                txb_Cellphone.Text = newCellphone;
            }
        }

        /// <summary>
        ///     注册商户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [SuppressMessage("ReSharper", "FunctionComplexityOverflow")]
        private void btn_Register_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("您确定要注册商户吗", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            string cellphone = txb_Cellphone.Text.Trim();
            string license = txb_License.Text.Trim();
            string domain = txb_Domain.Text.Trim();
            string tenancyType = txb_TenancyType.Text.Trim();
            string tenancyName = txb_TenancyName.Text.Trim();
            string companyName = txb_CompanyName.Text.Trim();
            string cityName = cmb_CityName.SelectedValue.ToString();
            string districtName = txb_DistrictName.Text.Trim();
            string cuscc = txb_Cuscc.Text.Trim();
            string accountName = txb_AccountName.Text.Trim();
            string realName = txb_RealName.Text.Trim();
            string credentialType = txb_CredentialType.Text.Trim();
            string credentialNo = txb_CredentialNo.Text.Trim();
            string userName = domain.Split('.').Last() + "Administrator";
            string passwordHash = "AQAAAAEAACcQAAAAEMfdU3dK+R1uC7YkQ2exN/hJ7g6WJ+PN2Nvbr76BdVIVr69uVp85NgY1fVdNZ9dJXA==";

            if (string.IsNullOrEmpty(cellphone))
            {
                MessageBox.Show("Cellphone can not be null or empty.");
                return;
            }
            if (string.IsNullOrEmpty(license))
            {
                MessageBox.Show("License can not be null or empty.");
                return;
            }
            if (string.IsNullOrEmpty(domain))
            {
                MessageBox.Show("Domain can not be null or empty.");
                return;
            }
            if (string.IsNullOrEmpty(tenancyType))
            {
                MessageBox.Show("TenancyType can not be null or empty.");
                return;
            }
            if (string.IsNullOrEmpty(tenancyName))
            {
                MessageBox.Show("TenancyName can not be null or empty.");
                return;
            }
            if (string.IsNullOrEmpty(companyName))
            {
                MessageBox.Show("CompanyName can not be null or empty.");
                return;
            }
            if (string.IsNullOrEmpty(cityName))
            {
                MessageBox.Show("CityName can not be null or empty.");
                return;
            }
            if (string.IsNullOrEmpty(districtName))
            {
                MessageBox.Show("DistrictName can not be null or empty.");
                return;
            }
            if (string.IsNullOrEmpty(cuscc))
            {
                MessageBox.Show("Cuscc can not be null or empty.");
                return;
            }
            if (string.IsNullOrEmpty(accountName))
            {
                MessageBox.Show("AccountName can not be null or empty.");
                return;
            }
            if (string.IsNullOrEmpty(realName))
            {
                MessageBox.Show("RealName can not be null or empty.");
                return;
            }
            if (string.IsNullOrEmpty(credentialType))
            {
                MessageBox.Show("CredentialType can not be null or empty.");
                return;
            }
            if (string.IsNullOrEmpty(credentialNo))
            {
                MessageBox.Show("CredentialNo can not be null or empty.");
                return;
            }
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("UserName can not be null or empty.");
                return;
            }

            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                string sql1 = $"select Cellphone from dbo.[KC.Fengniaowu.Talos.Accounts] where Cellphone = '{cellphone}'";
                string cellphone1 = connection.QueryFirstOrDefault<string>(sql1);
                if (!string.IsNullOrEmpty(cellphone1))
                {
                    MessageBox.Show($"Cellphone {cellphone} is exist.");
                    return;
                }

                string sql2 = $"select TenancyId from dbo.[KC.Fengniaowu.Talos.Tenancies] where Enabled = 1 and (Domain = '{domain}' or License = '{license}')";
                string tenancyId2 = connection.QueryFirstOrDefault<string>(sql2);
                if (!string.IsNullOrEmpty(tenancyId2))
                {
                    MessageBox.Show($"Domain {domain} or License {license} is exist.");
                    return;
                }

                string sql3 = $"select CityId from dbo.[KC.Fengniaowu.Talos.Cities] where Enabled = 1 and CityName = '{cityName}'";
                string existCityId = connection.QueryFirstOrDefault<string>(sql3);
                
                string sql5 = "select '\"' + PermissionId + '\"' as PermissionId, PermissionCode from dbo.[KC.Fengniaowu.Talos.Permissions] " +
                              "where Enabled = 1";
                List<Permission> permissions = connection.Query<Permission>(sql5).ToList();
                List<string>  permissionIds1 = permissions.FindAll(t => _permissionCodeCaiwu.Contains(t.PermissionCode))
                    .Select(t => t.PermissionId).ToList();
                List<string>  permissionIds2 = permissions.FindAll(t => t.PermissionCode != "AlphaAdministrator@fengniaowu" && 
                                                                        t.PermissionCode != "Tenant@fengniaowu.tenant" && 
                                                                        t.PermissionCode != "Settings@fengniaowu.tenancy.asset")
                    .Select(t => t.PermissionId).ToList();
                List<string>  permissionIds3 = permissions.FindAll(t => t.PermissionCode != "AlphaAdministrator@fengniaowu" && 
                                                                        t.PermissionCode != "Tenant@fengniaowu.tenant")
                    .Select(t => t.PermissionId).ToList();


                DateTimeOffset time = Time.Now;
                string cityId = ID.NewSequentialGuid().ToGuidString();
                string tenancyId = ID.NewSequentialGuid().ToGuidString();
                string linkAddress = "http://www.fengniaowu.com/mobile/housessource.html?houselistId=" + tenancyId;
                string districtId = ID.NewSequentialGuid().ToGuidString();
                string departmentId1 = ID.NewSequentialGuid().ToGuidString();
                string departmentId2 = ID.NewSequentialGuid().ToGuidString();
                string departmentId3 = ID.NewSequentialGuid().ToGuidString();
                string departmentId4 = ID.NewSequentialGuid().ToGuidString();
                string positionId1 = ID.NewSequentialGuid().ToGuidString();
                string positionId2 = ID.NewSequentialGuid().ToGuidString();
                string positionId3 = ID.NewSequentialGuid().ToGuidString();
                string positionId4 = ID.NewSequentialGuid().ToGuidString();
                string positionId5 = ID.NewSequentialGuid().ToGuidString();
                string positionId6 = ID.NewSequentialGuid().ToGuidString();
                string positionId7 = ID.NewSequentialGuid().ToGuidString();
                string roleIdd1 = ID.NewSequentialGuid().ToGuidString();
                string roleIdd2 = ID.NewSequentialGuid().ToGuidString();
                string roleIdd3 = ID.NewSequentialGuid().ToGuidString();
                string roleIdd4 = ID.NewSequentialGuid().ToGuidString();
                string userId = ID.NewSequentialGuid().ToGuidString();
                string loginInfoId = ID.NewSequentialGuid().ToGuidString();
                string accountId = ID.NewSequentialGuid().ToGuidString();
                string digitalId = ID.NewSequentialGuid().ToGuidString();
                string digitalConfigId = ID.NewSequentialGuid().ToGuidString();
                

                SqlTransaction tran = connection.BeginTransaction();

                string s0 = null;
                if (string.IsNullOrEmpty(existCityId))
                {
                    s0 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Cities]([ActorId], [CityId], [CityName], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{cityId.ToGuid()}', '{cityId}', '{cityName}', '1', '{time}', '{time}', N'{{}}')";
                }

                string s1 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Tenancies]([ActorId], [TenancyId], [TenancyName], [Domain], [TenancyType], [CompanyName], [CredentialNo], [Enabled], [CreateTime], [UpdateTime], [Data], [License], [RoomSourcePromotionLinkAddress]) 
VALUES('{tenancyId.ToGuid()}', '{tenancyId}', '{tenancyName}', '{domain}', '{tenancyType}', '{companyName}', NULL, '1', '{time}', '{time}', N'{{}}', '{license}', '{linkAddress}')";

                string s2 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Districts]([ActorId], [DistrictId], [TenancyId], [CityName], [DistrictName], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{districtId.ToGuid()}',  '{districtId}', '{tenancyId}', '{cityName}', '{districtName}', '1', '{time}', '{time}', N'{{}}')";

                string s3 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Departments]([ActorId], [DepartmentId], [TenancyId], [DepartmentName], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{departmentId1.ToGuid()}', '{departmentId1}', '{tenancyId}', N'IT', '1', '{time}', '{time}', N'{{}}')";

                string s4 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Departments]([ActorId], [DepartmentId], [TenancyId], [DepartmentName], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{departmentId2.ToGuid()}', '{departmentId2}', '{tenancyId}', N'总经办', '1', '{time}', '{time}', N'{{}}')";

                string s5 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Departments]([ActorId], [DepartmentId], [TenancyId], [DepartmentName], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{departmentId3.ToGuid()}', '{departmentId3}', '{tenancyId}', N'运营部', '1', '{time}', '{time}', N'{{}}')";

                string s6 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Departments]([ActorId], [DepartmentId], [TenancyId], [DepartmentName], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{departmentId4.ToGuid()}', '{departmentId4}', '{tenancyId}', N'财务部', '1', '{time}', '{time}', N'{{}}')";

                string s7 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Positions]([ActorId], [PositionId], [TenancyId], [PositionName], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{positionId1.ToGuid()}', '{positionId1}', '{tenancyId}', N'系统管理员', '1', '{time}', '{time}', N'{{}}')";

                string s8 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Positions]([ActorId], [PositionId], [TenancyId], [PositionName], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{positionId2.ToGuid()}', '{positionId2}', '{tenancyId}', N'董事长', '1', '{time}', '{time}', N'{{}}')";

                string s9 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Positions]([ActorId], [PositionId], [TenancyId], [PositionName], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{positionId3.ToGuid()}', '{positionId3}', '{tenancyId}', N'总经理', '1', '{time}', '{time}', N'{{}}')";

                string s10 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Positions]([ActorId], [PositionId], [TenancyId], [PositionName], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{positionId4.ToGuid()}', '{positionId4}', '{tenancyId}', N'财务', '1', '{time}', '{time}', N'{{}}')";

                string s11 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Positions]([ActorId], [PositionId], [TenancyId], [PositionName], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{positionId5.ToGuid()}', '{positionId5}', '{tenancyId}', N'店长管家', '1', '{time}', '{time}', N'{{}}')";

                string s12 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Positions]([ActorId], [PositionId], [TenancyId], [PositionName], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{positionId6.ToGuid()}', '{positionId6}', '{tenancyId}', N'运营经理', '1', '{time}', '{time}', N'{{}}')";

                string s13 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Positions]([ActorId], [PositionId], [TenancyId], [PositionName], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{positionId7.ToGuid()}', '{positionId7}', '{tenancyId}', N'其他', '1', '{time}', '{time}', N'{{}}')";

                string s14 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Roles]([ActorId], [RoleId], [RoleName], [Description], [Enabled], [Permissions], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{roleIdd1.ToGuid()}', '{roleIdd1}', N'财务' + '{domain}', N'财务日常操作，财务包含数据导出权限', '1', N'[{string.Join(",", permissionIds1)}]', '{time}', '{time}', N'{{}}')";

                string s15 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Roles]([ActorId], [RoleId], [RoleName], [Description], [Enabled], [Permissions], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{roleIdd2.ToGuid()}', '{roleIdd2}', N'店长' + '{domain}', N'管家日常管理，包括签约、续租、退租、账单', '1', N'[{string.Join(",", permissionIds2)}]', '{time}', '{time}', N'{{}}')";

                string s16 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Roles]([ActorId], [RoleId], [RoleName], [Description], [Enabled], [Permissions], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{roleIdd3.ToGuid()}', '{roleIdd3}', N'管理层' + '{domain}', N'方便管理层数据监控、可以浏览所有权限', '1', N'[{string.Join(",", permissionIds2)}]', '{time}', '{time}', N'{{}}')";

                string s17 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Roles]([ActorId], [RoleId], [RoleName], [Description], [Enabled], [Permissions], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{roleIdd4.ToGuid()}', '{roleIdd4}', N'IT管理' + '{domain}', N'负责平时系统管理，拥有大部分权限', '1', N'[{string.Join(",", permissionIds2)}]', '{time}', '{time}', N'{{}}')";

                string s18 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Users] ([ActorId], [UserId], [UserName], [Description], [Enabled], [Roles], [Permissions], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{userId.ToGuid()}', '{userId}', '{userName}', N'Administrator', '1', N'[]', N'[{string.Join(",", permissionIds3)}]', '{time}', '{time}', N'{{}}')";

                string s19 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.LoginInfos] ([LoginInfoId], [LoginInfoAccount], [LoginInfoType], [UserId], [Enabled], [CreateTime]) 
VALUES ('{loginInfoId.ToGuid()}', '{cellphone}', 'Cellphone', '{userId}', '1', '{time}')";

                string s20 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.Accounts]([ActorId], [UserId], [AccountId], [TenancyId], [Cellphone], [AccountName], [AccountState], [AccountPhotoUrl], [CredentialType], [CredentialNo], [RealName], [Email], [DistrictId], [DepartmentId], [PositionId], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{accountId.ToGuid()}', '{userId}', '{accountId}', '{tenancyId}', '{cellphone}', '{accountName}', N'Normal', N'https://kolibrestore.blob.core.chinacloudapi.cn/fengniaowu/talos/default_avatars/1.png', '{credentialType}', '{credentialNo}', '{realName}', NULL, '{districtId}', '{departmentId1}', '{positionId1}', '1', '{time}', '{time}', N'{{}}')";

                string s21 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.PasswordLoginProviders] ([ActorId], [UserId], [PasswordHash], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{userId.ToGuid()}', '{userId}', '{passwordHash}', NULL, '1', '0', '1', '{time}', '{time}', N'{{}}')";

                string temp22 = "{\"tenancyName\":\""+companyName+"\",\"tenancyOrganizationNumber\":\""+cuscc+"\"}";
                string s22 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.DigitalContractConfigs]([DigitalContractConfigId], [Name], [TemplateType], [Description], [RequestParameters], [HtmlContentUrl], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{digitalId}', N'居住房屋租赁合同', N'TongYongZuFang', N'', '{temp22}', N'https://kolibrestore.blob.core.chinacloudapi.cn/public/uploads/apartment/digitalcontract/templates/tongyongzufang_20180505.html', '1', '{time}', '{time}', N'{{}}')";

                string temp23 = "[[\"" + digitalId + "\"]]";
                string s23 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.DigitalContractRelationConfigs]([DigitalContractRelationConfigId], [EntityType], [EntityId], [ConfigContent], [Enabled], [CreateTime], [UpdateTime], [Data], [ContractProcess]) 
VALUES ('{digitalConfigId}', N'Tenancy', '{tenancyId}', '{temp23}', '1', '{time}', '{time}', N'{{}}', N'Sign')";

                Dictionary<string, string> tempDic = new Dictionary<string, string>
                {
                    {"s0",s0},{"s1",s1 },{"s2",s2 },{"s3",s3 },{"s4",s4 },{"s5",s5 },{"s6",s6 },{"s7",s7 },{"s8",s8 },{"s9",s9 },{"s10",s10 },{"s11",s11 },{"s12",s12 },{"s13",s13 },{"s14",s14 },{"s15",s15 },{"s16",s16 },{"s17",s17 },{"s18",s18 },{"s19",s19 },{"s20",s20 },{"s21",s21 },{"s22",s22 },{"s23",s23 }
                };

                // 执行SQL语句

                try
                {
                    foreach (KeyValuePair<string, string> kv in tempDic)
                    {
                        if (string.IsNullOrEmpty(kv.Value))
                        {
                            continue;
                        }

                        int rows = connection.Execute(kv.Value, null, tran);
                        if (rows <= 0)
                        {
                            tran.Rollback();
                            ShowRegisterErrorMsg(kv.Key);
                            return;
                        }
                    }
                }
                catch (Exception)
                {
                    tran.Rollback();
                    MessageBox.Show("注册商户失败，SQL执行异常");
                    return;
                }

                tran.Commit();

                MessageBox.Show("注册成功");
            }
        }

        /// <summary>
        ///     刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Init();
        }

        private static void ShowRegisterErrorMsg(string s)
        {
            MessageBox.Show($"注册商户失败，SQL语句 {s} 执行失败");
        }

        private void InitForTest()
        {
            txb_Cellphone.Text = "10500000050";
            txb_License.Text = "@ceshi1.com";
            txb_Domain.Text = "@fengniaowu.tenancy.asset.ceshi1";
            txb_TenancyType.Text = "Asset";
            txb_TenancyName.Text = "测试商户1";
            txb_CompanyName.Text = "上海测试公司";
            cmb_CityName.SelectedIndex = 0;
            txb_DistrictName.Text = "浦东新区";
            txb_Cuscc.Text = "12301";
            txb_AccountName.Text = "系统管理员";
            txb_RealName.Text = "系统管理员";
            txb_CredentialType.Text = "IDCard";
            txb_CredentialNo.Text = "000000000000000000";
        }
    }
}

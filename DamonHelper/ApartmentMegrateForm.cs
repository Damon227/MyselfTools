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
    public partial class ApartmentMegrateForm : Form
    {
        private static List<string> s_errorList;

        public ApartmentMegrateForm()
        {
            InitializeComponent();
        }

        private async void btn_Megrate_Click(object sender, EventArgs e)
        {
            s_errorList = new List<string>();

            string apartmentIdString = rtb_ApartmentIds.Text;
            if (string.IsNullOrEmpty(apartmentIdString))
            {
                MessageBox.Show("公寓Id不能为空");
                return;
            }

            string[] apartmentIds = apartmentIdString.Split(',');
            if (apartmentIds.Length == 0)
            {
                MessageBox.Show("未检测到有效的公寓Id");
                return;
            }

            string tenancyId = txb_TenancyId.Text;
            if (string.IsNullOrEmpty(tenancyId))
            {
                MessageBox.Show("商户Id不能为空");
                return;
            }

            string cellphone = txb_Cellphone.Text;
            if (string.IsNullOrEmpty(cellphone))
            {
                MessageBox.Show("管理员手机号不能为空");
                return;
            }

            foreach (string apartmentId in apartmentIds)
            {
                await MegrateAsync(apartmentId, tenancyId, cellphone);
            }
        }

        private async Task MegrateAsync(string apartmentId, string tenancyId, string cellphone)
        {
            DateTimeOffset time = Time.Now;
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                await connection.OpenAsync();
                // 查询公寓和商户是否有效
                string sql1 = "select count(ApartmentId) from dbo.[KC.Fengniaowu.Talos.Apartments] where ApartmentId = @apartmentId and Enabled = 1";
                long count1 = await connection.QueryFirstAsync<long>(sql1, new { ApartmentId = apartmentId });
                if (count1 == 0)
                {
                    MessageBox.Show($"公寓Id({apartmentId})不存在");
                    return;
                }

                string sql2 = "select count(TenancyId) from dbo.[KC.Fengniaowu.Talos.Tenancies] where TenancyId = @tenancyId and Enabled = 1";
                long count2 = await connection.QueryFirstAsync<long>(sql2, new { TenancyId = tenancyId });
                if (count2 == 0)
                {
                    MessageBox.Show($"商户Id({tenancyId})不存在");
                    return;
                }

                string sql3 = $"select * from dbo.[KC.Fengniaowu.Talos.Apartments] where ApartmentId = '{apartmentId}' and Enabled = 1";
                dynamic apartment = await connection.QueryFirstAsync<dynamic>(sql3);
                if (apartment.AssetTenancyId == tenancyId)
                {
                    MessageBox.Show("该公寓不需要迁移");
                    return;
                }

                string sql4 = $"select * from dbo.[KC.Fengniaowu.Talos.Districts] where DistrictId = '{apartment.DistrictId}'";
                dynamic district = await connection.QueryFirstAsync<dynamic>(sql4);

                string sql5 = $"select * from dbo.[KC.Fengniaowu.Talos.Accounts] where Enabled = 1 and TenancyId = '{tenancyId}' and Cellphone = '{cellphone}'";
                dynamic account = await connection.QueryFirstAsync<dynamic>(sql5);

                string sql6 = $"select VillageId from dbo.[KC.Fengniaowu.Ladon.Villages] where Enabled = 1 and TenancyId = '{tenancyId}' and (ApartmentId = '{apartmentId}' or RelationIds = '{apartmentId}')";
                IEnumerable<string> villageIds = await connection.QueryAsync<string>(sql6);

                // 修改公寓表商户Id
                string s1 = $"update dbo.[KC.Fengniaowu.Talos.Apartments] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}'";

                // 修改公寓DistrictId
                Guid districtGuid = ID.NewSequentialGuid();
                string s2 = $@"if not exists(
select * from dbo.[KC.Fengniaowu.Talos.Districts] where 
TenancyId = '{tenancyId}'
and CityName = '{district.CityName}'
and DistrictName = '{district.DistrictName}'
)
INSERT INTO [dbo].[KC.Fengniaowu.Talos.Districts]([ActorId], [DistrictId], [TenancyId], [CityName], [DistrictName], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{districtGuid}', '{districtGuid.ToGuidString()}', '{tenancyId}', N'{district.CityName}', N'{district.DistrictName}', '1', '{time}', '{time}', N'{{}}')";
                string s3 = $@"update dbo.[KC.Fengniaowu.Talos.Apartments] set DistrictId = 
(select DistrictId from dbo.[KC.Fengniaowu.Talos.Districts] where TenancyId = '{tenancyId}' and CityName = '{district.CityName}' and DistrictName = '{district.DistrictName}') 
where ApartmentId = '{apartmentId}'";

                // 修改房间表商户Id
                string s4 = $"update dbo.[KC.Fengniaowu.Talos.Rooms] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改合同表商户Id
                string s5 = $"update dbo.[KC.Fengniaowu.Talos.Contracts] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改账单表商户Id
                string s6 = $"update dbo.[KC.Fengniaowu.Talos.Orders] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改流水表商户Id
                string s7 = $"update dbo.[KC.Fengniaowu.Talos.Transactions] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改流水明细表商户Id
                string s8 = $"update dbo.[KC.Fengniaowu.Talos.TransactionDetails] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改Cleaning表商户Id
                string s9 = $"update dbo.[KC.Fengniaowu.Talos.Cleanings] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改ComplaintSuggestions表商户Id
                string s10 = $"update dbo.[KC.Fengniaowu.Talos.ComplaintSuggestions] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改ContractCheckoutInfos表商户Id
                string s11 = $"update dbo.[KC.Fengniaowu.Talos.ContractCheckoutInfos] set TenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改DomesticServiceConfig表商户Id
                string s12 = $"update dbo.[KC.Fengniaowu.Talos.DomesticServiceConfigs] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改IntelligentDeviceConfigs表商户Id
                string s13 = $"update dbo.[KC.Fengniaowu.Talos.IntelligentDeviceConfigs] set TenancyId = '{tenancyId}' where EntityType = 'Apartment' and EntityId = '{apartmentId}' and Enabled = 1";

                // 修改LoanRecords表商户Id
                string s14 = $"update dbo.[KC.Fengniaowu.Talos.LoanRecords] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改PromotionRecords表商户Id
                string s15 = $"update dbo.[KC.Fengniaowu.Talos.PromotionRecords] set TenancyId = '{tenancyId}', Content = json_modify(Content,'$.AssetTenancyId','{tenancyId}') where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改Repairs表商户Id
                string s16 = $"update dbo.[KC.Fengniaowu.Talos.Repairs] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改RoomMessages表商户Id
                string s17 = $"update dbo.[KC.Fengniaowu.Talos.RoomMessages] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改RoomReservations表商户Id
                string s18 = $"update dbo.[KC.Fengniaowu.Talos.RoomReservations] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改RoomSourceConfigs表商户Id
                string s19 = $"update dbo.[KC.Fengniaowu.Talos.RoomSourceConfigs] set AssetTenancyId = '{tenancyId}' where EntityType = 'Room' and EntityId in (select RoomId from dbo.[KC.Fengniaowu.Talos.Rooms] where ApartmentId = '{apartmentId}') and Enabled = 1";

                // 修改RoomSourceReservations表商户Id
                string s20 = $"update dbo.[KC.Fengniaowu.Talos.RoomSourceReservations] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改RoomTypes表商户Id
                string s21 = $"update dbo.[KC.Fengniaowu.Talos.RoomTypes] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改StageOrders表商户Id
                string s22 = $"update dbo.[KC.Fengniaowu.Talos.StageOrders] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改StageOrdersFee表商户Id
                //string s23 = $"update dbo.[KC.Fengniaowu.Talos.StageOrdersFee] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改UtilityExpenses表商户Id
                string s24 = $"update dbo.[KC.Fengniaowu.Talos.UtilityExpenses] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 删除AccountApartments表记录
                string s25 = $"update dbo.[KC.Fengniaowu.Talos.AccountApartments] set Enabled = 0 where ApartmentId = '{apartmentId}'";

                // 修改ApplyMonthlyPayConfigs表商户Id
                string s26 = $"update dbo.[KC.Fengniaowu.Talos.ApplyMonthPayConfigs] set TenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改DeviceConsumptionInfos表商户Id
                string s27 = $"update dbo.[KC.Fengniaowu.Hebe.DeviceConsumptionInfos] set TenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改DeviceRechargeRecords表商户Id 
                string s28 = $"update dbo.[KC.Fengniaowu.Hebe.DeviceRechargeRecords] set TenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 修改Devices表商户Id
                string s29 = $"update dbo.[KC.Fengniaowu.Hebe.Devices] set AssetTenancyId = '{tenancyId}' where ApartmentId = '{apartmentId}' and Enabled = 1";

                // 给新商户管理添加公寓
                Guid accountApartmentActorId = ID.NewSequentialGuid();
                string s30 = $@"INSERT INTO [dbo].[KC.Fengniaowu.Talos.AccountApartments]([ActorId], [AccountApartmentId], [AccountId], [TenancyId], [ApartmentId], [Enabled], [CreateTime], [UpdateTime], [Data]) 
VALUES ('{accountApartmentActorId}', '{accountApartmentActorId.ToGuidString()}', '{account.AccountId}', '{tenancyId}', '{apartmentId}', '1', '{time}', '{time}', N'{{}}')";

                // 修改房源表商户Id
                string s31 = $"update dbo.[KC.Fengniaowu.Ladon.Villages] set TenancyId = '{tenancyId}' where Enabled = 1 and ApartmentId = '{apartmentId}' or RelationIds = '{apartmentId}'";

                // 修改VillageOrders表商户Id
                // 修改VillageContracts表商户Id
                string villageidsString = string.Empty;
                foreach (string villageId in villageIds)
                {
                    villageidsString += $"'{villageId}',";
                }

                string s32 = string.Empty;
                string s33 = string.Empty;
                if (villageidsString.Length > 0)
                {
                    villageidsString.Remove(villageidsString.Length - 1);
                    s32 = $"update dbo.[KC.Fengniaowu.Ladon.LandlordOrders] set TenancyId = '{tenancyId}' where Enabled = 1 and VillageId in ({villageidsString})";
                    s33 = $"update dbo.[KC.Fengniaowu.Ladon.LandlordContracts] set TenancyId = '{tenancyId}' where Enabled = 1 and VillageId in ({villageidsString})";
                }

                List<string> sqls = new List<string>
                {
                    s1,s2,s3,s4,s5,s6,s7,s8,s9,s10,s11,s12,s13,s14,s15,s16,s17,s18,s19,s20,s21,s22,s24,s25,s26,s27,s28,s29,
                    s30, s31, s32, s33
                };

                // 执行SQL语句
                SqlTransaction tran = connection.BeginTransaction();
                try
                {
                    foreach (string sql in sqls)
                    {
                        if (string.IsNullOrEmpty(sql))
                        {
                            continue;
                        }

                        int rows = await connection.ExecuteAsync(sql, null, tran);
                        //if (rows <= 0)
                        //{
                        //    tran.Rollback();
                        //    return;
                        //}
                    }
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    MessageBox.Show($"公寓迁移失败，SQL执行异常。异常：{e.ToString()}");
                    return;
                }

                tran.Commit();
            }


            //MessageBox.Show("公寓迁移成功");

            /* 清除缓存 */

            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(Config.TalosBaseAddress) };

            string sessionId = SessionIdHelper.SessionId;
            if (string.IsNullOrEmpty(sessionId))
            {
                return;
            }

            httpClient.DefaultRequestHeaders.Add("X-KC-SID", sessionId);

            // 清除公寓表缓存
            await ClearCacheAsync(httpClient, "api/Apartment/ClearCache", new { ApartmentId = apartmentId });

            // 清除房间表缓存
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                await connection.OpenAsync();
                string ss1 = $"select RoomId from dbo.[KC.Fengniaowu.Talos.Rooms] where AssetTenancyId = '{tenancyId}' and Enabled = 1";
                IEnumerable<string> roomIds = await connection.QueryAsync<string>(ss1);
                foreach (string roomId in roomIds)
                {
                    await ClearCacheAsync(httpClient, "api/Room/ClearRoomCache", new { RoomId = roomId });
                }
            }


            // 清除合同表缓存
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                await connection.OpenAsync();
                string ss2 = $"select ContractId from dbo.[KC.Fengniaowu.Talos.Contracts] where AssetTenancyId = '{tenancyId}' and Enabled = 1";
                IEnumerable<string> contractIds = await connection.QueryAsync<string>(ss2);
                foreach (string contractId in contractIds)
                {
                    await ClearCacheAsync(httpClient, "api/Contract/ClearCache", new { ContractId = contractId });
                }
            }

            // 清除账单表缓存
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                await connection.OpenAsync();
                string ss3 = $"select OrderId from dbo.[KC.Fengniaowu.Talos.Orders] where AssetTenancyId = '{tenancyId}' and Enabled = 1";
                IEnumerable<string> orderIds = await connection.QueryAsync<string>(ss3);
                List<object> contents = new List<object>();
                foreach (string orderId in orderIds)
                {
                    contents.Add(new { OrderId = orderId });
                    //await ClearCacheAsync(httpClient, "api/Order/ClearCache", new { OrderId = orderId });
                }

                await ClearCachesAsync(httpClient, "api/Order/ClearCache", contents);
            }

            // 清除流水表缓存
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                await connection.OpenAsync();
                string ss4 = $"select TransactionId from dbo.[KC.Fengniaowu.Talos.Transactions] where AssetTenancyId = '{tenancyId}' and Enabled = 1";
                IEnumerable<string> transactionIds = await connection.QueryAsync<string>(ss4);
                foreach (string transactionId in transactionIds)
                {
                    await ClearCacheAsync(httpClient, $"api/Transaction/ClearCache?transactionId={transactionId}", null, "Get");
                }
            }

            // 清除RoomReservations表缓存
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                await connection.OpenAsync();
                string ss5 = $"select RoomReservationId from dbo.[KC.Fengniaowu.Talos.RoomReservations] where AssetTenancyId = '{tenancyId}' and Enabled = 1";
                IEnumerable<string> roomReservationIds = await connection.QueryAsync<string>(ss5);
                foreach (string roomReservationId in roomReservationIds)
                {
                    await ClearCacheAsync(httpClient, "api/RoomReservation/ClearReservationCache", new { RoomReservationId = roomReservationId });
                }
            }

            // 清除RoomSourceReservations表缓存
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                await connection.OpenAsync();
                string ss6 = $"select RoomSourceReservationId from dbo.[KC.Fengniaowu.Talos.RoomSourceReservations] where AssetTenancyId = '{tenancyId}' and Enabled = 1";
                IEnumerable<string> roomSourceReservationIds = await connection.QueryAsync<string>(ss6);
                foreach (string roomSourceReservationId in roomSourceReservationIds)
                {
                    await ClearCacheAsync(httpClient, "api/RoomSource/ClearReservationCache", new { RoomSourceReservationId = roomSourceReservationId });
                }
            }

            // 清除UtilityExpenses表缓存
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                await connection.OpenAsync();
                string ss7 = $"select UtilityExpenseId from dbo.[KC.Fengniaowu.Talos.UtilityExpenses] where AssetTenancyId = '{tenancyId}' and Enabled = 1";
                IEnumerable<string> utilityExpenseIds = await connection.QueryAsync<string>(ss7);
                foreach (string utilityExpenseId in utilityExpenseIds)
                {
                    await ClearCacheAsync(httpClient, $"api/UtilityExpense/ClearUtilityExpenseCache?utilityExpenseId={utilityExpenseId}", null, "Get");
                }
            }

            // 清除StageOrders表缓存
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                await connection.OpenAsync();
                string ss8 = $"select StageOrderId from dbo.[KC.Fengniaowu.Talos.StageOrders] where AssetTenancyId = '{tenancyId}' and Enabled = 1";
                IEnumerable<string> stageOrderIds = await connection.QueryAsync<string>(ss8);
                foreach (string stageOrderId in stageOrderIds)
                {
                    await ClearCacheAsync(httpClient, "api/StageOrder/ClearCache", new { StageOrderId = stageOrderId });
                }
            }

            // 清除Devices表缓存
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                await connection.OpenAsync();
                string ss9 = $"select DeviceId from dbo.[KC.Fengniaowu.Hebe.Devices] where AssetTenancyId = '{tenancyId}' and Enabled = 1";
                IEnumerable<string> deviceIds = await connection.QueryAsync<string>(ss9);
                foreach (string deviceId in deviceIds)
                {
                    await ClearCacheAsync(httpClient, "api/IntelligentDevice/ClearDeviceCache", new { DeviceId = deviceId });
                }
            }

            if (s_errorList.Count == 0)
            {
                MessageBox.Show("迁移成功");
            }
            else
            {
                MessageBox.Show($"迁移失败，失败内容：{JsonConvert.SerializeObject(s_errorList)}");
            }
        }

        private async Task ClearCachesAsync(HttpClient httpClient, string url, List<object> contents, string httpMethod = "Post")
        {
            IEnumerable<List<object>> contentLists = contents.SplitToSmallList();
            foreach (List<object> contentList in contentLists)
            {
                await Task.WhenAll(contentList.Select(async content => await ClearCacheAsync(httpClient, url, content, httpMethod)));
            }
        }

        private async Task<bool> ClearCacheAsync(HttpClient httpClient, string url, object content, string httpMethod = "Post")
        {
            try
            {
                HttpResponseMessage response;
                if (httpMethod == "Post")
                {
                    response = await httpClient.PostAsJsonAsync(url, content);
                }
                else
                {
                    response = await httpClient.GetAsync(url);
                }

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    JObject obj = JObject.Parse(responseString);
                    if ((bool)obj["succeeded"])
                    {
                        AppendMessage($"{url} {content} 成功");
                        return true;
                    }

                    AppendMessage($"{url} {content} 失败。原因：{responseString}");
                    s_errorList.Add($"{url} {content} 失败。原因：{responseString}");
                    return false;
                }

                AppendMessage($"{url} {content} 失败。原因：HttpStatusCode {response.StatusCode}");
                s_errorList.Add($"{url} {content} 失败。原因：HttpStatusCode {response.StatusCode}");
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
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

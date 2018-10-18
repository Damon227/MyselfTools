// ***********************************************************************
// Solution         : MyselfTools
// Project          : TableMegrateConsole
// File             : Program.cs
// Updated          : 2018-05-22 20:20
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2017 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace TableMegrateConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<string> tables = new List<string>
            {
                //"dbo.[KC.Fengniaowu.Talos.AccountApartments]",
                //"dbo.[KC.Fengniaowu.Talos.Accounts]",
                //"dbo.[KC.Fengniaowu.Talos.Apartments]",
                //"dbo.[KC.Fengniaowu.Talos.ApartmentStageConfigs]",
                //"dbo.[KC.Fengniaowu.Talos.ApartmentTenancyRelations]",
                //"dbo.[KC.Fengniaowu.Talos.AuthorizationCodes]",
                //"dbo.[KC.Fengniaowu.Talos.ChangeCellphones]",
                //"dbo.[KC.Fengniaowu.Talos.CheckoutApplies]",
                //"dbo.[KC.Fengniaowu.Talos.Cities]",
                //"dbo.[KC.Fengniaowu.Talos.ConfirmContractProcess]",
                //"dbo.[KC.Fengniaowu.Talos.ConfirmContractProcessReminders]",
                //"dbo.[KC.Fengniaowu.Talos.ContractAuditRecords]",
                //"dbo.[KC.Fengniaowu.Talos.ContractAuditReminders]",
                //"dbo.[KC.Fengniaowu.Talos.ContractAudits]",
                //"dbo.[KC.Fengniaowu.Talos.ContractConfirmInfos]",
                //"dbo.[KC.Fengniaowu.Talos.ContractReviews]",
                //"dbo.[KC.Fengniaowu.Talos.Contracts]",
                //"dbo.[KC.Fengniaowu.Talos.CreditReportReminders]",
                //"dbo.[KC.Fengniaowu.Talos.CreditReports]",
                //"dbo.[KC.Fengniaowu.Talos.Departments]",
                //"dbo.[KC.Fengniaowu.Talos.DigitalContractConfigs]",
                //"dbo.[KC.Fengniaowu.Talos.DigitalContractRelationConfigs]",
                //"dbo.[KC.Fengniaowu.Talos.Districts]",
                //"dbo.[KC.Fengniaowu.Talos.EnvelopeReminders]",
                //"dbo.[KC.Fengniaowu.Talos.Envelopes]",
                //"dbo.[KC.Fengniaowu.Talos.LoanRecords]",
                //"dbo.[KC.Fengniaowu.Talos.LoginInfos]",
                //"dbo.[KC.Fengniaowu.Talos.Messages]",
                //"dbo.[KC.Fengniaowu.Talos.Orders]",
                //"dbo.[KC.Fengniaowu.Talos.PasswordLoginProviders]",
                //"dbo.[KC.Fengniaowu.Talos.Penalties]",
                //"dbo.[KC.Fengniaowu.Talos.Permissions]",
                //"dbo.[KC.Fengniaowu.Talos.Positions]",
                //"dbo.[KC.Fengniaowu.Talos.RiskLables]",
                //"dbo.[KC.Fengniaowu.Talos.Roles]",
                //"dbo.[KC.Fengniaowu.Talos.RoomReservations]",
                //"dbo.[KC.Fengniaowu.Talos.Rooms]",
                //"dbo.[KC.Fengniaowu.Talos.RoomSourceConfigs]",
                //"dbo.[KC.Fengniaowu.Talos.RoomSourceReservations]",
                //"dbo.[KC.Fengniaowu.Talos.RoomTypes]",
                //"dbo.[KC.Fengniaowu.Talos.ScheduleRecords]",
                //"dbo.[KC.Fengniaowu.Talos.ScheduleReminders]",
                //"dbo.[KC.Fengniaowu.Talos.Schedules]",
                //"dbo.[KC.Fengniaowu.Talos.ScheduleSmsRecords]"

                //"dbo.[KC.Fengniaowu.Talos.Sessions]",
                //"dbo.[KC.Fengniaowu.Talos.SignContractProcess]",
                //"dbo.[KC.Fengniaowu.Talos.SignContractProcessReminders]",
                //"dbo.[KC.Fengniaowu.Talos.StageOrders]",
                //"dbo.[KC.Fengniaowu.Talos.Tenancies]",
                //"dbo.[KC.Fengniaowu.Talos.TenancyApartments]",
                //"dbo.[KC.Fengniaowu.Talos.TenancyRelations]",
                //"dbo.[KC.Fengniaowu.Talos.Tenants]",
                //"dbo.[KC.Fengniaowu.Talos.TransactionReminders]",
                //"dbo.[KC.Fengniaowu.Talos.Transactions]",
                //"dbo.[KC.Fengniaowu.Talos.TwoFactorVerifyRecords]",
                //"dbo.[KC.Fengniaowu.Talos.Users]",
                //"dbo.[KC.Fengniaowu.Talos.UtilityExpenses]",
                //"dbo.[KC.Fengniaowu.Talos.ValidateCodes]",
                //"dbo.[KC.Fengniaowu.Talos.WechatLoginProviders]"
                "dbo.[KC.Fengniaowu.Clotho.AuthorizationCodes]",
                "dbo.[KC.Fengniaowu.Clotho.LoginManagerReminders]",
                "dbo.[KC.Fengniaowu.Clotho.PasswordLoginAccounts]",
                "dbo.[KC.Fengniaowu.Clotho.Permissions]",
                "dbo.[KC.Fengniaowu.Clotho.Roles]",
                "dbo.[KC.Fengniaowu.Clotho.Users]",
                "dbo.[KC.Fengniaowu.Clotho.ValidateCodeLoginAccounts]",
                "dbo.[KC.Fengniaowu.Clotho.WeChatLoginAccounts]"
            };

            foreach (string table in tables)
            {
                 Megrate(table);
            }

            Console.ReadLine();

            Console.WriteLine("Hello World!");
        }

        private static void Megrate(string table)
        {
            //要复制的表名  
            //string table = "dbo.[KC.Fengniaowu.Talos.Cities]";

            //构造连接字符串  
            SqlConnectionStringBuilder builder1 = new SqlConnectionStringBuilder("Server=tcp:kc-fengniaowu-dev.database.chinacloudapi.cn;Database=KC.Fengniaowu-LOCAL;User ID=KC@kc-fengniaowu-dev;Password=V245ZGxbEhn3Sakk;Trusted_Connection=False;Encrypt=True;MultipleActiveResultSets=True;Max Pool Size=2000;");
           

            SqlConnectionStringBuilder builder2 = new SqlConnectionStringBuilder("Server=tcp:kc-fengniaowu-staging.database.chinacloudapi.cn;Database=KC.Fengniaowu-STAGING;User ID=KC@kc-fengniaowu-staging;Password=v9duUGu$O9yeVOVi;Trusted_Connection=False;Encrypt=True;MultipleActiveResultSets=True;Max Pool Size=2000;");

            InsertTable(builder1.ConnectionString, builder2.ConnectionString, table);
        }

        //参数为两个数据库的连接字符串  
        private static void InsertTable(string conString1, string conString2, string tabStr)
        {
            //连接数据库  
            SqlConnection conn1 = new SqlConnection();
            conn1.ConnectionString = conString1;
            conn1.Open();

            SqlConnection conn2 = new SqlConnection();
            conn2.ConnectionString = conString2;
            conn2.Open();

            //填充DataSet1  
            SqlDataAdapter adapter1 = new SqlDataAdapter("select * from " + tabStr, conn1);
            DataSet dataSet1 = new DataSet();

            adapter1.Fill(dataSet1, tabStr);

            SqlDataAdapter adapter2 = new SqlDataAdapter("select * from " + tabStr, conn2);
            DataSet dataSet2 = new DataSet();
            adapter2.Fill(dataSet2, tabStr);

            int rows = dataSet1.Tables[0].Rows.Count;
            int times = rows / 1000;
            for (int k = 0; k < times + 1; k++)
            {
                int a = (k + 1) * 1000;
                if (k == times)
                {
                    a = rows;
                }

                //复制数据  
                string sql = $"INSERT INTO {tabStr} VALUES";

                string values = string.Empty;
                for (int j = k * 1000; j < a; j++)
                {
                    int length = dataSet1.Tables[0].Rows[j].ItemArray.Length;
                    if (string.IsNullOrEmpty(values))
                    {
                        values += "(";
                    }
                    else
                    {
                        values += ",(";
                    }

                    for (int i = 1; i < length; i++)
                    {
                        values += $"'{dataSet1.Tables[0].Rows[j][i]}',";
                    }

                    values = values.Remove(values.Length - 1);
                    values += ")";

                    //dataSet2.Tables[0].LoadDataRow(dataSet1.Tables[0].Rows[j].ItemArray, false);
                }

                if (!string.IsNullOrEmpty(values))
                {
                    sql = sql + values;

                    //将DataSet变换显示在与其关联的目标数据库  
                    adapter2.InsertCommand = new SqlCommand(sql, conn2);
                    adapter2.InsertCommand.ExecuteNonQuery();
                }
            }

            Console.WriteLine("表" + tabStr + "复制成功！");

            conn1.Close();
            conn2.Close();
        }
    }
}
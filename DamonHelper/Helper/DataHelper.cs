// ***********************************************************************
// Solution         : MyselfTools
// Project          : DamonHelper
// File             : DataHelper.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DamonHelper.Models;
using DamonHelper.Settings;
using Dapper;
using Newtonsoft.Json;

namespace DamonHelper.Helper
{
    public static class DataHelper
    {
        public static List<SimpleTenancy> GetTenancies()
        {
            string sql = "select TenancyId, TenancyName from dbo.[KC.Fengniaowu.Talos.Tenancies] where Enabled = 1";
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                List<SimpleTenancy> result = connection.Query<SimpleTenancy>(sql).ToList();
                return result;
            }
        }

        public static List<SimpleApartment> GetApartmentsByTenancyId(string tenancyId)
        {
            string sql = $"select ApartmentId, ApartmentName from dbo.[KC.Fengniaowu.Talos.Apartments] where Enabled = 1 and AssetTenancyId = '{tenancyId}'";
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                List<SimpleApartment> result = connection.Query<SimpleApartment>(sql).ToList();
                return result;
            }
        }

        public static List<PenaltySetting> GetPenaltySettings()
        {
            string sql = "select PenaltyConfig from dbo.[KC.Fengniaowu.Talos.Schedules] where Enabled = 1 and ScheduleId = '5337DCC80FEE45D39EAB76ACD2BF20A8'";
            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                string result = connection.QueryFirstOrDefault<string>(sql);
                return JsonConvert.DeserializeObject<List<PenaltySetting>>(result);
            }
        }
    }
}
// ***********************************************************************
// Solution         : MyselfTools
// Project          : DamonHelper
// File             : SqlConnectionExtensions.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Data.SqlClient;

namespace DamonHelper.Config
{
    public class SqlConnectionExtensions
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(Config.TalosDbConnectionString_Pro);
        }
    }
}
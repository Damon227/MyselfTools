using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Options;
using Polly;

namespace CQ.Dapper
{
    public class DbConnectionProvider
    {
        private static readonly Policy s_sqlRetryPolicy = Policy
            .Handle<TimeoutException>()
            .Or<SqlException>(AnyRetryableError)
            .WaitAndRetry(2, ExponentialBackoff);

        private readonly DapperOptions _dataOptions;

        public DbConnectionProvider(IOptions<DapperOptions> options)
        {
            _dataOptions = options?.Value;
        }

        /// <summary>
        ///     获取数据库连接。
        /// </summary>
        public IDbConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(_dataOptions.ConnectionString);

            //SqlRetryPolicy.Execute(connection.Open);
            return connection;
        }

        /// <summary>
        ///     打开数据为连接，在连接失败时重试。
        /// </summary>
        /// <param name="connection">数据库连接</param>
        public void OpenWithRetry(IDbConnection connection)
        {
            s_sqlRetryPolicy.Execute(() => connection.Open());
        }

        private static bool RetryableError(SqlError error)
        {
            return true;
        }

        private static bool AnyRetryableError(SqlException exception)
        {
            return exception.Errors.OfType<SqlError>().Any(RetryableError);
        }

        private static TimeSpan ExponentialBackoff(int attempt)
        {
            return TimeSpan.FromSeconds(Math.Pow(2, attempt));
        }
    }
}

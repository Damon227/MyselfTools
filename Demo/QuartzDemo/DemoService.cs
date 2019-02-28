using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace QuartzDemo
{
    public class DemoService : IDemoService
    {
        private readonly ILogger _logger;
        private readonly DataOptions _dataOptions;

        public DemoService(ILoggerFactory loggerFactory, IOptions<DataOptions> optionsAccessor)
        {
            _logger = loggerFactory?.CreateLogger<DemoService>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _dataOptions = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
        }

        public void Work()
        {
            Console.WriteLine("This is demo work.");
            _logger.LogInformation("This is info log.");
        }

        public async Task QueryStationMsgAsync()
        {
            string sql = "";
            using (IDbConnection connection = new SqlConnection(_dataOptions.ConnectionString))
            {
                int result = await connection.QueryFirstOrDefaultAsync<int>(sql);
            }
        }
    }
}

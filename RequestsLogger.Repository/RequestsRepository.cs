using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace RequestsLogger.Repository
{
    public class RequestsRepository : IRequestsRepository
    {
        private readonly string _connectionString;

        public RequestsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnectionString");
        }

        public async Task InsertAsync(string content, string clientIp)
        {
            const string sql = "INSERT INTO [dbo].[Requests] (TimeStamp, Content, ClientIp) Values (@TimeStamp, @Content, @ClientIp);";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, new { TimeStamp = DateTime.Now, Content = content, ClientIp = clientIp });
            }
        }
    }
}

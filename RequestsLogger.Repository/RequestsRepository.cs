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
        private readonly IConfiguration _configuration;

        public RequestsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InsertAsync(string content)
        {
            var sql = "INSERT INTO [dbo].[Requests] (TimeStamp, Content) Values (@TimeStamp, @Content);";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("RequestsDB")))
            {
                await connection.ExecuteAsync(sql, new { TimeStamp = DateTime.Now, Content = content });
            }
        }
    }
}

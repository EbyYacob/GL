using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LQMSApplication.CommonServices
{
    public class DapperService
    {
        private readonly string _connectionString;
        public DapperService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LQMSDBContext");
        }
        public async Task<(IEnumerable<T> Data, string Result)> ExecuteStoredProcedure<T>(string storedProcedure, object parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                using var multi = await connection.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                var resultData = await multi.ReadAsync<T>();
                var result = await multi.ReadFirstAsync<string>();

                return (resultData, result);
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
        }
    }
}

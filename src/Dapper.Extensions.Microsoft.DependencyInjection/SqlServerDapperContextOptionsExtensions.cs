using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Dapper.Extensions.Microsoft.DependencyInjection
{
    public static class SqlServerDapperContextOptionsExtensions
    {
        public static DapperContextOptionsBuilder UseSqlServerUserSecrets(this DapperContextOptionsBuilder pOptionsBuilder, IConfiguration pConfiguration, string pConnection = "DefaultConnection",
            Action<SqlConnectionStringBuilder> optionsAction = null)
        {
            var xConnectionString = pConfiguration.GetConnectionString(pConnection);
            if (string.IsNullOrWhiteSpace(xConnectionString))
                throw new KeyNotFoundException(pConnection + " em IConfiguration não encontrada");

            var xConnection = pConnection + "Password";
            var xPassword = pConfiguration[xConnection];

            if (string.IsNullOrWhiteSpace(xPassword))
                throw new KeyNotFoundException(xConnection + " em IConfiguration não encontrada");

            var xConnectionStringBuilder = new SqlConnectionStringBuilder(xConnectionString)
            {
                Password = xPassword,
                MultipleActiveResultSets = true
            };
            return pOptionsBuilder.UseSqlServer(xConnectionStringBuilder, optionsAction);
        }
    }
}
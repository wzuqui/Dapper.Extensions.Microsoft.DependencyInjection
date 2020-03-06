using System;
using System.Data.SqlClient;

namespace Dapper.Extensions.Microsoft.DependencyInjection
{
    public class DapperContextOptionsBuilder
    {
        public readonly DapperContextOptions DapperContextOptions;

        public DapperContextOptionsBuilder(DapperContextOptions pDapperContextOptions)
        {
            DapperContextOptions = pDapperContextOptions;
        }

        public DapperContextOptionsBuilder UseSqlServer(SqlConnectionStringBuilder pSqlConnectionStringBuilder, Action<SqlConnectionStringBuilder> pAction)
        {
            pAction?.Invoke(pSqlConnectionStringBuilder);
            DapperContextOptions.Extensions.Add(typeof(SqlConnectionStringBuilder), pSqlConnectionStringBuilder);
            return this;
        }
    }
}
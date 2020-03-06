using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Dapper.Extensions.Microsoft.DependencyInjection
{
    public abstract class DapperContext : IDisposable
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;
        public IDbConnection Connection => new SqlConnection(_connectionStringBuilder.ConnectionString);

        protected DapperContext(DapperContextOptions pDapperContextOptions)
        {
            if (pDapperContextOptions.Extensions.TryGetValue(typeof(SqlConnectionStringBuilder), out var xConnectionStringBuilder))
                _connectionStringBuilder = xConnectionStringBuilder;
            else
                throw new ArgumentException(nameof(_connectionStringBuilder) + " não foi encontrado");


            foreach (var xDapperDbSet in GetType().GetProperties().Where(p => p.PropertyType.BaseType == typeof(DapperDbSet)))
                xDapperDbSet.SetValue(this, Activator.CreateInstance(xDapperDbSet.PropertyType, this));
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
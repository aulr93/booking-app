using Booking.Application.Commons.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data.Common;

namespace Booking.Presistence
{
    public class DapperContext : IDapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AuthConnection") ?? string.Empty;
        }

        public DbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    }
}

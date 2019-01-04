using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Wtl.EntityFramework;

namespace Wtl.EntityFrameworkCore
{
    public static class SmdZeroTemplateDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<WtlDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<WtlDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
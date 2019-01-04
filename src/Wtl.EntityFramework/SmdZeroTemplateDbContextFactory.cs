using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Wtl.Configuration;
using Wtl.EntityFramework;
using Wtl.Web;

namespace Wtl.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class SmdZeroTemplateDbContextFactory : IDesignTimeDbContextFactory<WtlDbContext>
    {
        public WtlDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WtlDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            SmdZeroTemplateDbContextConfigurer.Configure(builder, configuration.GetConnectionString("DefaultConnection"));

            return new WtlDbContext(builder.Options);
        }
    }
}
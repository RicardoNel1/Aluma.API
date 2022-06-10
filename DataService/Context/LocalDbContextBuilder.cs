using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace DataService.Context
{
    internal class LocalDbContextBuilder : IDesignTimeDbContextFactory<AlumaDBContext>
    {
        //    --Install NuGet Packages:
        //    Microsoft.EntityFrameworkCore.SqlServer
        //    Microsoft.EntityFrameworkCore.Design
        //
        //    --Migration Commands
        //    dotnet ef migrations add InitialCreate
        //    dotnet ef database update
        public AlumaDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AlumaDBContext>();
            builder.UseSqlServer(GetConnectionString(),
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(AlumaDBContext).GetTypeInfo().Assembly.GetName().Name));

            return new AlumaDBContext(builder.Options);
        }

        private static string GetConnectionString()
        {
            var config = new ConfigurationBuilder();
            config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", false);
            var configuration = config.Build();
            return configuration.GetValue("ConnectionStrings:DefaultConnection", string.Empty);
        }
    }
}

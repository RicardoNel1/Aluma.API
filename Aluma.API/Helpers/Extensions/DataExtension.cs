using DataService.Context;
using DataService.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aluma.API.Helpers.Extensions
{
    public static class DataExtension
    {
        #region Public Methods

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connections = config.GetSection("ConnectionStrings").Get<ConnectionStringsDto>();

            services.AddDbContext<AlumaDBContext>(o =>
            {
                o.UseSqlServer(connections.DefaultConnection,
                    sqlServerOptionsAction: so =>
                    {
                        so.EnableRetryOnFailure();
                        so.CommandTimeout(150);
                    }

                );
            });
        }

        #endregion Public Methods
    }
}
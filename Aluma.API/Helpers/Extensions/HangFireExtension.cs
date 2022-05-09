using DataService.Dto;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Aluma.API.Helpers.Extensions
{
    public static class HangFireExtension
    {
        #region Public Methods

        public static void ConfigureHangfireContext(this IServiceCollection services,
            IConfiguration config)
        {
            var connections = config.GetSection("ConnectionStrings").Get<ConnectionStringsDto>();
            services.AddHangfire(c => c.UseSqlServerStorage(connections.DefaultConnection));
            services.AddHangfireServer();
        }

        #endregion Public Methods

        #region Public Classes

        public class MyAuthorizationFilter : IDashboardAuthorizationFilter
        {
            #region Public Methods

            public bool Authorize(DashboardContext context)
            {
                var httpContext = context.GetHttpContext();

                // Only authenticated admins should be able to view the
                // hangfire dashboard

                return httpContext.User.Identity.IsAuthenticated &&
                       httpContext.User.Claims.Any(c => c.Value == "Admin") ? true : false;
            }

            #endregion Public Methods
        }

        #endregion Public Classes
    }
}
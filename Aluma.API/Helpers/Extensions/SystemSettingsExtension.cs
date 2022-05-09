using DataService.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aluma.API.Helpers.Extensions
{
    public static class SystemSettingsExtension
    {
        #region Public Methods

        public static void ConfigureSystemSettings(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddSingleton(config.GetSection("SystemSettings").Get<SystemSettingsDto>());
        }

        #endregion Public Methods
    }
}
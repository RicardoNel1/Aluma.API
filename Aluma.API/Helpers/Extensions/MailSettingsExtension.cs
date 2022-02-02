using DataService.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aluma.API.Helpers.Extensions
{
    public static class MailSettingsExtension
    {
        public static void ConfigureMailSettings(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddSingleton(config.GetSection("MailServerSettings").Get<MailServerSettingsDto>());
        }
    }
}
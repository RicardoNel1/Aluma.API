using Azure.Storage.Files.Shares;
using DataService.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aluma.API.Helpers.Extensions
{
    public static class FileStorageExtension
    {
        #region Public Methods

        public static void ConfigureAzureFileStorage(this IServiceCollection services, IConfiguration config)
        {
            var connections = config.GetSection("AzureSettings").Get<AzureSettingsDto>();

            services.AddScoped(s =>
           {
               return new ShareServiceClient(connections.AzureFileStorageConnection);
           });
        }

        #endregion Public Methods
    }
}
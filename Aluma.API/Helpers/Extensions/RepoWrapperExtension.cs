using Aluma.API.RepoWrapper;
using Microsoft.Extensions.DependencyInjection;

namespace Aluma.API.Helpers.Extensions
{
    public static class RepoWrapperExtension
    {
        #region Public Methods

        public static void ConfigureRepoWrapper(this IServiceCollection services)
        {
            services.AddScoped<IWrapper, Wrapper>();
        }

        #endregion Public Methods
    }
}
using Aluma.API.Helpers.Extensions;
using DataService.Context;
using FileStorageService;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Globalization;
using DataService;

namespace Aluma.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureJwtAuthentication(Configuration);
            services.ConfigureSqlContext(Configuration);
            services.ConfigureAzureFileStorage(Configuration);
            services.AddScoped<IFileStorageRepo, FileStorageRepo>();
            services.ConfigureRepoWrapper();
            services.AddAutoMapper(typeof(MappingProfile));
           //services.AddAutoMapper();
            services.ConfigureHangfireContext(Configuration);
            services.ConfigureSystemSettings(Configuration);
            services.ConfigureMailSettings(Configuration);
            services.AddHsts(opt =>
            {
                opt.IncludeSubDomains = true;
            });
            services.AddControllers().AddJsonOptions(c =>
            {
                c.JsonSerializerOptions.MaxDepth = 0;
                c.JsonSerializerOptions.AllowTrailingCommas = true;
                c.JsonSerializerOptions.WriteIndented = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aluma.API", Version = "v1.000" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AlumaDBContext>();
                context.Database.EnsureCreated();
            }

            var cultureInfo = new CultureInfo("en-ZA");
            cultureInfo.NumberFormat.CurrencySymbol = "R";
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                

                app.UseHangfireDashboard();

            }
            else
            {
                app.UseHsts();
            }
            //dev only move back
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aluma.API v1.0.1"));

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //loggerFactory.AddFile("Logs/alumaAPI-information-{Date}.txt", LogLevel.Information);
            //loggerFactory.AddFile("Logs/alumaAPI-errors-{Date}.txt", LogLevel.Error);
        }
    }
}
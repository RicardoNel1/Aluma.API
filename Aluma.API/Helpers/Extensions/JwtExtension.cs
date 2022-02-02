using DataService.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Aluma.API.Helpers.Extensions
{
    public static class JwtExtension
    {
        public static void ConfigureJwtAuthentication(this IServiceCollection services,
            IConfiguration config)
        {
            var jwtSettings = config.GetSection("JwtSettings").Get<JwtSettingsDto>();

            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret.ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(c =>
            {
                c.SaveToken = true;
                c.RequireHttpsMetadata = false;
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true,

                    //ValidIssuer = jwtSettings.Issuer,
                    //ValidAudience = jwtSettings.Audience
                };
            });
        }
    }
}
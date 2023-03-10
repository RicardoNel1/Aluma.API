using DataService.Dto;
using DataService.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JwtService
{
    public interface IJwtRepo
    {
        /// <summary>
        /// Creates jwt token used to authenticate a user
        /// </summary>
        string CreateJwtToken(int userId, RoleEnum Role, int minutes = 0);

        /// <summary>
        /// Returns the JWT Token Claimes (userId & Role)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        ClaimsDto GetUserClaims(string token);

        string CreateJwtTokenB64(string Base64);

        string GetUserClaimsB64(string token);

        void CreateJwtToken(LoginDto dto);

        bool IsTokenValid(string token);
    }

    public class JwtRepo : IJwtRepo
    {
        public readonly JwtSettingsDto _settings;

        public JwtRepo()
        {
            var config = new ConfigurationBuilder();
            // Get current directory will return the root dir of Base app as that is the running application
            var path = Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _settings = root.GetSection("JwtSettings").Get<JwtSettingsDto>();
        }

        public JwtSettingsDto settings { get => _settings; }

        public string CreateJwtToken(int userId, RoleEnum role, int minutes)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_settings.Secret);
            minutes = minutes > 0 ? minutes : _settings.LifeSpan;

            // we define our token descriptor
            // We need to utilise claims which are properties in our token which gives information about the token
            // which belong to the specific user who it belongs to
            // so it could contain their id, name, email the good part is that these information
            // are generated by our server and identity framework which is valid and trusted

            var claims = new[] {
             new Claim("UserId", userId.ToString()),
                    new Claim(ClaimTypes.Role, role.ToString())
        };

            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(_settings.Issuer, _settings.Audience, claims,
                expires: DateTime.UtcNow.AddMinutes(minutes), signingCredentials: credentials);

            return handler.WriteToken(tokenDescriptor);
        }

        public bool IsTokenValid(string token)
        {
            string[] broken_str = token.Split(' ');

            var key = Encoding.UTF8.GetBytes(_settings.Secret);
            var mySecurityKey = new SymmetricSecurityKey(key);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(broken_str[1],
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidIssuer = issuer,
                    //ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public ClaimsDto GetUserClaims(string token)
        {
            string[] broken_str = token.Split(' ');

            var handler = new JwtSecurityTokenHandler();
            var tokenDetails = handler.ReadToken(broken_str[1]) as JwtSecurityToken;

            var id = Int32.Parse(tokenDetails.Claims.First(c => c.Type == "UserId").Value);
            //var role = tokenDetails.Claims.First(c => c.Type == ClaimTypes.Role).Value.ToString();

            return new ClaimsDto()
            {
                UserId = id,
                //Role = role
            };
        }

        public string CreateJwtTokenB64(string Base64)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var descriptor = new SecurityTokenDescriptor
            {
                //Subject = new ClaimsIdentity(new Claim[]
                //{
                //    new Claim(ClaimTypes.Name, userId.ToString()),
                //    new Claim(ClaimTypes.Role, role)
                //}),
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim("Base64", Base64),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_settings.LifeSpan),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                //Issuer = _settings.Issuer,
                //Audience = _settings.Audience
            };
            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }

        public string GetUserClaimsB64(string token)
        {
            string[] broken_str = token.Split(' ');

            var handler = new JwtSecurityTokenHandler();
            var tokenDetails = handler.ReadToken(broken_str[1]) as JwtSecurityToken;

            return tokenDetails.Claims.First(c => c.Type == "Base64").Value.ToString();
        }

        public void CreateJwtToken(LoginDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
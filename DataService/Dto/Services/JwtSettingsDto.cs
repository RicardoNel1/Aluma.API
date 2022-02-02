using System;

namespace DataService.Dto
{
    public class JwtSettingsDto
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int LifeSpan { get; set; }
    }

    public class ClaimsDto
    {
        public int UserId { get; set; }
        public string Role { get; set; }
    }
}
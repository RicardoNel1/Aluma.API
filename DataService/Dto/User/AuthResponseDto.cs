using DataService.Model;

namespace DataService.Dto
{
    public class AuthResponseDto
    {
        public UserDto User { get; set; }

        public int ClientId { get; set; }
        public string Token { get; set; }
        public string TokenExpiry { get; set; }
        public string Message { get; set; }
    }
}
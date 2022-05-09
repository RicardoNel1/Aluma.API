namespace DataService.Dto
{
    public class AuthResponseDto : ApiResponseDto
    {
        public UserDto User { get; set; }

        public int ClientId { get; set; }
        public int AdvisorId { get; set; }
        public string Token { get; set; }
        public string TokenExpiry { get; set; }

    }
}
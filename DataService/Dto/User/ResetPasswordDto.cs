namespace DataService.Dto
{
    public class ResetPasswordDto:ApiResponseDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string Otp { get; set; }
    }
}
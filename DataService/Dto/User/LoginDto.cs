namespace DataService.Dto
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Otp { get; set; }
        public string SocialId { get; set; }
    }
}
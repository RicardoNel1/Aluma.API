namespace DataService.Dto
{
    public class LoginDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Otp { get; set; }
        public string SocialId { get; set; }
    }
}
namespace DataService.Dto
{
    public class EmploymentDetailsDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string EmploymentStatus { get; set; }
        public string Employer { get; set; }
        public string Industry { get; set; }
        public string Occupation { get; set; }
        public string WorkNumber { get; set; }
    }
}
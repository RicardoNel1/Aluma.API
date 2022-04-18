using System;

namespace DataService.Dto
{
    public class PassportDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string CountryOfIssue { get; set; }
        public string PassportNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
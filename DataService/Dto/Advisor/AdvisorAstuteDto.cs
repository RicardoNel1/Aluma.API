using DataService.Model;

namespace DataService.Dto.Advisor
{
    public class AdvisorAstuteDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int AdvisorId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

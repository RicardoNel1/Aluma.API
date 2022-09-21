using System;

namespace DataService.Dto
{
    public class ClientFNADto : ApiResponseDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int AdvisorId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}

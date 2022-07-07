using System;

namespace DataService.Dto
{
    public class DocumentListDto : ApiResponseDto
    {
        public int DocumentId { get; set; }
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public DocumentListDto()
        {
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }
    }
}
namespace DataService.Dto
{
    public class DocumentListDto
    {
        public int DocumentId { get; set; }
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
       
    }
}
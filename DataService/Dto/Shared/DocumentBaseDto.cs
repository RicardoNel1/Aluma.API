using System;

namespace DataService.Dto
{
    public class DocumentBaseDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string FileType { get; set; }
        public string Name { get; set; }
        public string DocumentType { get; set; }
        public byte[] DocumentData { get; set; }
        //public DocumentTypesEnum DocumentType { get; set; }
    }
}
namespace DataService.Dto
{
    public class FileStorageDto
    {
        public string BaseDocumentPath { get; set; }
        public string FileDirectory { get; set; }
        public string BaseShare { get; set; }
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }
    }
}
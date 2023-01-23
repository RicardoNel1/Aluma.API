using System;

namespace DataService.Dto
{
    public class ClientNotesDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Title { get; set; }
        public string NoteBody { get; set; }
        public DateTime DateCaptured { get; set; }
    }
}
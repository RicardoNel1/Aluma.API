using System;
using System.Collections.Generic;

namespace DataService.Dto
{
    public class ClientNotesDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Title { get; set; }
        public string NoteBody { get; set; }
    }
}
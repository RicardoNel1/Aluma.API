using System;
using System.Collections.Generic;

namespace DataService.Dto
{
    public class RecordOfAdviceDto
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int AdvisorId { get; set; }
        public string BdaNumber { get; set; }
        public string Introduction { get; set; }
        public string MaterialInformation { get; set; }
        public bool Replacement_A { get; set; }
        public bool Replacement_B { get; set; }
        public bool Replacement_C { get; set; }
        public bool Replacement_D { get; set; }
        public string ReplacementReason { get; set; }
        public ICollection<RecordOfAdviceItemsDto> SelectedProducts { get; set; }
    }
}
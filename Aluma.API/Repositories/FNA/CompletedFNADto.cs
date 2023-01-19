using System;

namespace Aluma.API.Repositories
{
    public class CompletedFNADto
    {
        public string Client { get; set; }
        public string Advisor { get; set; }
        public DateTime Created { get; set; }
    }

    public class CompletedFNACountDto
    {
        public string Advisor { get; set; }
        public int FNAsCompleted { get; set; }
    }
}
namespace Aluma.API.Repositories
{
    public class ProvidingDeathSummaryDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double TotalNeeds { get; set; }
        public double TotalAvailable { get; set; }

    }
}
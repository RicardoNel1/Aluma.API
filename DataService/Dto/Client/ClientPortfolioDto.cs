using System.Collections.Generic;

namespace DataService.Dto
{
    public class ClientPortfolioDto : ApiResponseDto
    {
        public ClientDto Client { get; set; }
        public ClientFNADto FNA { get; set; }
        public List<InvestmentsDto> Investments { get; set; }
        public RetirementSummaryDto Retirement { get; set; }
        public RetirementPlanningDto RetirementPlanning { get; set; }
        public ProvidingDisabilitySummaryDto ProvidingDisability { get; set;}
        public ProvidingDeathSummaryDto ProvidingDeath { get; set; }
        public ProvidingOnDreadDiseaseDto ProvidingDread { get; set; }
        public List<ShortTermInsuranceDTO> ShortTermInsurance { get; set; }
        public MedicalAidDTO MedicalAid { get; set; }
        public List<InsuranceDto> Insurance { get; set; }
        public AssumptionsDto Assumptions { get; set; }
        public List<DocumentListDto> DocumentList { get; set; }
        public List<ClientNotesDto> ClientNotes { get; set; }
    }
}
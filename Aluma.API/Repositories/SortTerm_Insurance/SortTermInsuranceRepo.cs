using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Model;

namespace Aluma.API.Repositories
{
    public interface ISortTermInsuranceRepo : IRepoBase<SortTermInsuranceModel>
    {
        List<SortTermInsuranceDTO> GetSortTermInsurance(int fnaId);
        List<SortTermInsuranceDTO> CreateSortTermInsurance(List<SortTermInsuranceDTO> dtoArray);
        List<SortTermInsuranceDTO> UpdateSortTermInsurance(List<SortTermInsuranceDTO> dtoArray);

        bool DeleteSortTermInsurance(int id);
    }

    public class SortTermInsuranceRepo
    {
        
    }
}
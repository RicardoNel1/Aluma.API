using Aluma.API.RepoWrapper;
using DataService.Context;
using DataService.Model;

namespace Aluma.API.Repositories
{
    public interface IPurposeAndFundingRepo : IRepoBase<PurposeAndFundingModel>
    {
    }

    public class PurposeAndFundingRepo : RepoBase<PurposeAndFundingModel>, IPurposeAndFundingRepo
    {
        public PurposeAndFundingRepo(AlumaDBContext databaseContext) : base(databaseContext)
        {
        }
    }
}
namespace Aluma.API.Controllers
{
    //[ApiController, Route("api/v1/dividend/tax"), Authorize]
    //public class DividendTaxController : ControllerBase
    //{
    //    private readonly IWrapper _repo;
    //    private readonly IMapper _mapper;

    //    public DividendTaxController(IWrapper repo, IMapper mapper)
    //    {
    //        _repo = repo;
    //        _mapper = mapper;
    //    }

    //    [HttpPut]
    //    public IActionResult CreateOrUpdate(DividendTaxDto dto)
    //    {
    //        try
    //        {
    //            // check if a dividend for given application ID exists
    //            var dividendExist = _repo.DividendTax
    //                .FindByCondition(
    //                    c => c.ApplicationId == dto.ApplicationId);

    //            if (dto.NatureOfEntity == "Individual")
    //            {
    //                var schedule = _repo.PrimaryIndividual
    //                    .FindByCondition(c => c.ApplicationId == dto.ApplicationId)
    //                    .Include(c => c.ClientDetails)
    //                    .First();

    //                dto.NameSurname = schedule.ClientDetails.FirstNames + " " + schedule.ClientDetails.Surname;
    //            }
    //            else if (dto.NatureOfEntity == "Company")
    //            {
    //                var schedule = _repo.PrimarySaCompany
    //                    .FindByCondition(c => c.ApplicationId == dto.ApplicationId)
    //                    .Include(c => c.SaCompanyDetails)
    //                    .First();

    //                dto.NameSurname = schedule.SaCompanyDetails.RegisteredName;
    //                dto.TradingName = schedule.SaCompanyDetails.TradingName;
    //            }
    //            else if (dto.NatureOfEntity == "Trust")
    //            {
    //                var schedule = _repo.PrimaryTrust
    //                    .FindByCondition(c => c.ApplicationId == dto.ApplicationId)
    //                    .Include(c => c.TrustDetails)
    //                    .First();

    //                dto.NameSurname = schedule.TrustDetails.Name;

    //            }
    //            else if (dto.NatureOfEntity == "CC")
    //            {
    //                var schedule = _repo.PrimaryCC
    //                    .FindByCondition(c => c.ApplicationId == dto.ApplicationId)
    //                    .Include(c => c.CCDetails)
    //                    .First();

    //                dto.NameSurname = schedule.CCDetails.RegisteredName;
    //                dto.TradingName = schedule.CCDetails.TradingName;
    //            }
    //            else if (dto.NatureOfEntity == "Partnership")
    //            {
    //                var schedule = _repo.PrimaryPartnership
    //                   .FindByCondition(c => c.ApplicationId == dto.ApplicationId)
    //                   .Include(c => c.PartnershipDetails)
    //                   .First();

    //                dto.NameSurname = schedule.PartnershipDetails.Name;
    //            }

    //            if (dividendExist.Any())
    //            {
    //                // get current step
    //                var thisStep = _repo.ApplicationSteps
    //                    .FindByCondition(
    //                        c => c.ApplicationId == dto.ApplicationId &&
    //                        c.StepType == ApplicationStepTypesEnum.Dividends)
    //                    .First();

    //                // update existing
    //                var dividend = dividendExist.First();
    //                dividend = _mapper.Map<DividendTaxModel>(dto);
    //                dividend.StepId = thisStep.Id;

    //                _repo.DividendTax.Update(dividend);
    //                _repo.Save();
    //            }
    //            else
    //            {
    //                // Get current application step item
    //                var checkStep = _repo.ApplicationSteps
    //                    .FindByCondition(
    //                        c => c.ApplicationId == dto.ApplicationId &&
    //                        c.StepType == ApplicationStepTypesEnum.Dividends);

    //                if (checkStep.Any())
    //                {
    //                    var currentStep = checkStep.First();
    //                    // create new dividends
    //                    var dividend = _mapper.Map<DividendTaxModel>(dto);
    //                    dividend.StepId = currentStep.Id;
    //                    _repo.DividendTax.Create(dividend);

    //                    currentStep.DataId = dividend.Id;
    //                    currentStep.Complete = true;
    //                    currentStep.ActiveStep = false;
    //                    _repo.ApplicationSteps.Update(currentStep);

    //                    // set next step as active
    //                    var nextStep = _repo.ApplicationSteps
    //                        .ReturnNextStep(dividend.ApplicationId, currentStep.Order);
    //                    nextStep.ActiveStep = true;
    //                    _repo.ApplicationSteps.Update(nextStep);
    //                }
    //                else
    //                {
    //                    var dividend = _mapper.Map<DividendTaxModel>(dto);
    //                    dividend.StepId = new Guid("00000000-0000-0000-0000-000000000000");
    //                    _repo.DividendTax.Create(dividend);
    //                }

    //                _repo.Save();
    //            }

    //            return Ok();
    //        }
    //        catch (Exception e)
    //        {
    //            return StatusCode(500, e.Message);
    //        }
    //    }
    //}
}
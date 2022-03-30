using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using DataService.Dto;
using DataService.Model;
using System;
using System.Linq;
using Aluma.API.Helpers;

namespace Aluma.API.Repositories
{
    public interface IBankDetailsRepo : IRepoBase<BankDetailsModel>
    {
        void CheckBankValidationStatusByJobId(string jobId);

        bool DoesBankDetailsExist(BankDetailsDto dto);

        BankDetailsDto CreateClientBankDetails(BankDetailsDto dto);

        BankDetailsDto UpdateClientBankDetails(BankDetailsDto dto);

        BankDetailsDto GetBankDetails(int clientId);

        bool DeleteBankDetails(BankDetailsDto dto);
    }

    public class BankDetailsRepo : RepoBase<BankDetailsModel>, IBankDetailsRepo
    {
        private AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public BankDetailsRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;       //added
            _config = config;   //added
            _mapper = mapper;   //added
        }

        public void CheckBankValidationStatusByJobId(string jobId)
        {
            //BankValidationRepo PbVerifyBankValidation = new BankValidationRepo();

            //// get the bank validation object where jobId matches
            //var bav = _context.BankDetails.First(e => e.JobID == jobId);

            //var bavStatus = PbVerifyBankValidation.GetBankValidationStatus(jobId);

            //if (bavStatus.Status.ToLower() == "success")
            //{
            //    var result = bavStatus.AVS;
            //    bav.Reference = "Verification";
            //    bav.BranchCode = result.BranchCode;
            //    bav.AccountNumber = result.AccountNumber;
            //    bav.IdNumber = result.IdNumber;
            //    bav.Initials = result.Initials;
            //    bav.Surname = result.Surname;
            //    bav.FoundAtBank = result.FoundAtBank;
            //    bav.AccOpen = result.AccOpen;
            //    bav.OlderThan3Months = result.OlderThan3Months;
            //    bav.TypeCorrect = result.TypeCorrect;
            //    bav.IdNumberMatch = result.IdNumberMatch;
            //    bav.NamesMatch = result.SurnameMatch;
            //    bav.AcceptDebits = result.AcceptDebits;
            //    bav.AcceptCredits = result.AcceptCredits;

            //    _context.BankDetails.Update(bav);

            //    // alse set the step entry as completed
            //    var step = _context.ApplicationSteps
            //        .First(c => c.ApplicationId == bav.ApplicationId && c.StepType == ApplicationStepTypesEnum.BankValidation);
            //    step.Complete = true;
            //    _context.ApplicationSteps.Update(step);

            //    // set the application bank validation to complete
            //    var application = _context.Applications.Find(step.ApplicationId);
            //    application.BankValidationComplete = true;
            //    _context.Applications.Update(application);

            //    _context.SaveChanges();
            //}
            //else
            //{
            //    bav.Reference = "Bank Verification Failed";
            //    _context.BankVerifications.Update(bav);

            //    // alse set the step entry as completed
            //    var step = _context.ApplicationSteps
            //        .First(c => c.ApplicationId == bav.ApplicationId && c.StepType == ApplicationStepTypesEnum.BankValidation);
            //    step.Complete = true;
            //    _context.ApplicationSteps.Update(step);

            //    // set the application bank validation to complete
            //    var application = _context.Applications.Find(step.ApplicationId);
            //    application.BankValidationComplete = true;
            //    _context.Applications.Update(application);

            //    _context.SaveChanges();

            //}
        }

        public BankDetailsDto CreateClientBankDetails(BankDetailsDto dto)
        {
            //Todo: create bankdetails record, check if skipped, send through for AVS to pbverify

            BankDetailsModel details = _mapper.Map<BankDetailsModel>(dto);
            UtilityHelper uh = new UtilityHelper();
            details.BranchCode = uh.BanksDictionary[details.BankName].ToString();

            _context.BankDetails.Add(details);
            _context.SaveChanges();
            dto = _mapper.Map<BankDetailsDto>(details);
            return dto;

            //[HttpPut]
            ////[AutomaticRetry(Attempts = 30, DelaysInSeconds = new int[] { 60 })]
            //public IActionResult PutBankVerification([FromBody] BankDetailsDto dto)
            //{
            //    try
            //    {
            //        // get banking step for the given bankverification
            //        var step = _repo.ApplicationSteps
            //            .FindByCondition(
            //                c => c.ApplicationId == applicationId &&
            //                c.StepType == ApplicationStepTypesEnum.BankValidation)
            //            .First();

            //        // check if bankValidation for this application exists
            //        var bankValidationExist = _repo.BankVerification
            //            .FindByCondition(c => c.ApplicationId == applicationId);

            //        // append required clientDetails to dto so that it's present when mapping to bav object
            //        // first check if we have data from digital KYC
            //        // else use user registration data
            //        var application = _repo.Applications
            //            .FindByCondition(c => c.Id == applicationId)
            //            .Include(c => c.User)
            //            .Include(c => c.KycMetaData)
            //            .First();

            //        if (application.KycMetaData == null)
            //        {
            //            dto.IdNumber = application.User.IdNumber;
            //            dto.Initials = Initials(application.User.FirstName);
            //            dto.Surname = application.User.LastName;
            //            //dto.Reference = $"{application.User.FirstName} {application.User.LastName}";
            //            string verificationRef = $"{dto.Initials} {application.User.LastName}";
            //            dto.Reference = verificationRef.Length > 20 ? verificationRef.Substring(0, 20) : verificationRef;
            //        }
            //        else
            //        {
            //            dto.IdNumber = application.KycMetaData.IdNumber;
            //            dto.Initials = Initials(application.KycMetaData.FirstNames);
            //            dto.Surname = application.KycMetaData.SurName;
            //            //dto.Reference = $"{application.KycMetaData.FirstNames} {application.KycMetaData.SurName}";
            //            string verificationRef = $"{dto.Initials} {application.KycMetaData.SurName}";
            //            dto.Reference = verificationRef.Length > 20 ? verificationRef.Substring(0, 20) : verificationRef;
            //        }

            //        Dictionary<string, int> Banks = new Dictionary<string, int>()
            //    {
            //        {"FNB",250655},
            //        {"NEDBANK",198765},
            //        {"STANDARDBANK",051001},
            //        {"CAPITEC",470010},
            //        {"AFRICANBANK",430000},
            //        {"ABSA",632005},
            //        {"INVESTEC",580105},
            //        {"BIDVESTBANK",462005},
            //        {"SASFINBANK",683000},
            //        {"DISCOVERYBANK",679000},
            //        {"GRINDRODBANK",584000},
            //        {"TYMEBANK",678910},
            //    };

            //        dto.BranchCode = Banks[dto.BankName].ToString();

            //        var jobID = string.Empty;

            //        if (bankValidationExist.Any())
            //        {
            //            // update existing
            //            var bav = bankValidationExist.First();
            //            bav = _mapper.Map<BankVerificationsModel>(dto);

            //            // start bank validation
            //            var validation = _repo.BankValidationService.StartBankValidation(dto);

            //            if (validation.Status == "Failure")
            //            {
            //                TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                return BadRequest(txtInfo.ToTitleCase(validation.Error));
            //            }
            //            else
            //            {
            //                //if (validation.Message.Contains("1055"))
            //                //{
            //                //    return BadRequest("Invalid Account Number and/or Account Type Combination");
            //                //}

            //                if (validation.Message.Contains("1054"))
            //                {
            //                    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                    return BadRequest(txtInfo.ToTitleCase("Invalid branch number"));

            //                }

            //                if (validation.Message.Contains("1055"))
            //                {
            //                    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                    return BadRequest(txtInfo.ToTitleCase("Invalid account number"));

            //                }

            //                if (validation.Message.Contains("1056"))
            //                {
            //                    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                    return BadRequest(txtInfo.ToTitleCase("Account type invalid"));

            //                }

            //                if (validation.Message.Contains("1057"))
            //                {
            //                    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                    return BadRequest(txtInfo.ToTitleCase("Institution not on master file"));

            //                }

            //                if (validation.Message.Contains("1059"))
            //                {
            //                    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                    return BadRequest(txtInfo.ToTitleCase("Account number length is not valid"));

            //                }

            //                if (validation.Message.Contains("1084"))
            //                {
            //                    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                    return BadRequest(txtInfo.ToTitleCase("Bond account type not allowed for this account"));

            //                }

            //                else if (validation.JobID == null)
            //                {
            //                    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                    return BadRequest(txtInfo.ToTitleCase(validation.Message));

            //                }
            //            }

            //            bav.JobID = validation.JobID;
            //            bav.ApplicationId = applicationId;
            //            jobID = validation.JobID;

            //            _repo.BankVerification.Update(bav);
            //        }
            //        else
            //        {
            //            var bav = _mapper.Map<BankVerificationsModel>(dto);
            //            bav.StepId = step.Id;
            //            bav.ApplicationId = applicationId;

            //            // start bank validation
            //            var validation = _repo.BankValidationService.StartBankValidation(dto);

            //            if (validation.Status == "Failure")
            //            {
            //                TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                return BadRequest(txtInfo.ToTitleCase(validation.Error));
            //            }
            //            else
            //            {
            //                //Catches Invalid account combinations and invalid account numbers:   1055(invalid combo),  1056(invalid type for this account number),  1084 (invalid account type)

            //                //if (validation.Message.Contains("105") || validation.Message.Contains("108"))
            //                //{
            //                //    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                //    return BadRequest(txtInfo.ToTitleCase("invalid account number and/or account type combination"));
            //                //}

            //                if (validation.Message.Contains("1054"))
            //                {
            //                    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                    return BadRequest(txtInfo.ToTitleCase("Invalid branch number"));

            //                }

            //                if (validation.Message.Contains("1055"))
            //                {
            //                    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                    return BadRequest(txtInfo.ToTitleCase("Invalid account number"));

            //                }

            //                if (validation.Message.Contains("1056"))
            //                {
            //                    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                    return BadRequest(txtInfo.ToTitleCase("Account type invalid"));

            //                }

            //                if (validation.Message.Contains("1057"))
            //                {
            //                    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                    return BadRequest(txtInfo.ToTitleCase("Institution not on master file"));

            //                }

            //                if (validation.Message.Contains("1059"))
            //                {
            //                    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                    return BadRequest(txtInfo.ToTitleCase("Account number length is not valid"));

            //                }

            //                if (validation.Message.Contains("1084"))
            //                {
            //                    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                    return BadRequest(txtInfo.ToTitleCase("Bond account type not allowed for this account"));

            //                }

            //                else if (validation.JobID == null)
            //                {
            //                    TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            //                    return BadRequest(txtInfo.ToTitleCase(validation.Message));

            //                }

            //            }
            //            //success 0000,   transmitted to bank 00001

            //            jobID = validation.JobID;
            //            bav.JobID = jobID;
            //            _repo.BankVerification.Create(bav);

            //            // we cannot yet set this step to complete.
            //            // It's only complete once we've received the bank validation from PbVerify
            //            step.DataId = bav.Id;
            //            step.Complete = false;
            //            step.ActiveStep = false;
            //            _repo.ApplicationSteps.Update(step);

            //            // set next step as the active step
            //            var nextStep = _repo.ApplicationSteps
            //                .ReturnNextStep(applicationId, step.Order);

            //            nextStep.ActiveStep = true;
            //            _repo.ApplicationSteps.Update(nextStep);
            //        }

            //        // add check status update to hanfire que - it should start in 3 minutes from now
            //        BackgroundJob.Schedule(() => _repo.BankVerification
            //            .CheckBankValidationStatusByJobId(jobID), TimeSpan.FromMinutes(2));

            //        _repo.Save();

            //        return StatusCode(201);
            //    }
            //    catch (Exception e)
            //    {
            //        return StatusCode(500, e.Message);
            //    }
            //}
        }

        public bool DeleteBankDetails(BankDetailsDto dto)
        {
            throw new NotImplementedException();
        }

        public bool DoesBankDetailsExist(BankDetailsDto dto)
        {
            bool bankDetailsExist = false;
            bankDetailsExist = _context.BankDetails.Where(a => a.ClientId == dto.ClientId).Any();
            return bankDetailsExist;
        }

        public BankDetailsDto GetBankDetails(int clientId)
        {
            BankDetailsModel bankDetails = _context.BankDetails.Where(c => c.ClientId == clientId).First();
            return _mapper.Map<BankDetailsDto>(bankDetails);
        }

        public BankDetailsDto UpdateClientBankDetails(BankDetailsDto dto)
        {
            //BankDetailsModel details = _mapper.Map<BankDetailsModel>(dto);
            BankDetailsModel details = _context.BankDetails.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();
            UtilityHelper uh = new UtilityHelper();


            //set user fields to be updated
            details.BankName = dto.BankName;
            details.AccountType = dto.AccountType;
            details.AccountNumber = dto.AccountNumber;
            details.BranchCode = uh.BanksDictionary[dto.BankName].ToString();

            _context.BankDetails.Update(details);
            _context.SaveChanges();
            dto = _mapper.Map<BankDetailsDto>(details);
            return dto;

        }
    }
}
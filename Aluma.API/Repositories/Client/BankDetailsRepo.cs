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
using Hangfire;
using BankValidationService;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Aluma.API.Repositories
{
    public interface IBankDetailsRepo : IRepoBase<BankDetailsModel>
    {
        #region Public Methods

        void CheckBankValidationStatusByJobId(string jobId);

        BankDetailsDto CreateClientBankDetails(BankDetailsDto dto);

        bool DeleteBankDetails(BankDetailsDto dto);

        bool DoesBankDetailsExist(BankDetailsDto dto);
        BankDetailsDto GetBankDetails(int clientId);

        BankDetailsDto UpdateClientBankDetails(BankDetailsDto dto);

        #endregion Public Methods
    }

    public class BankDetailsRepo : RepoBase<BankDetailsModel>, IBankDetailsRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly IWebHostEnvironment _host;

        private readonly IMapper _mapper;

        private AlumaDBContext _context;
        UtilityHelper uh;

        #endregion Private Fields

        #region Public Constructors

        public BankDetailsRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;       //added
            _config = config;   //added
            _mapper = mapper;   //added
            uh = new UtilityHelper();
        }

        #endregion Public Constructors

        #region Public Methods

        public void CheckBankValidationStatusByJobId(string jobId)
        {
            BankValidationServiceRepo bvr = new BankValidationServiceRepo();

            // get the bank validation object where jobId matches
            var bav = _context.BankDetails.First(e => e.JobID == jobId);

            var bavStatus = bvr.GetBankValidationStatus(jobId);

            if (bavStatus.Status.ToLower() == "success")
            {
                var result = bavStatus.AVS;
                bav.Reference = "Verification";
                bav.BranchCode = result.BranchCode;
                bav.AccountNumber = result.AccountNumber;
                bav.IdNumber = result.IdNumber;
                bav.Initials = result.Initials;
                bav.Surname = result.Surname;
                bav.FoundAtBank = result.FoundAtBank;
                bav.AccOpen = result.AccOpen;
                bav.OlderThan3Months = result.OlderThan3Months;
                bav.TypeCorrect = result.TypeCorrect;
                bav.IdNumberMatch = result.IdNumberMatch;
                bav.NamesMatch = result.SurnameMatch;
                bav.AcceptDebits = result.AcceptDebits;
                bav.AcceptCredits = result.AcceptCredits;

                _context.BankDetails.Update(bav);

                _context.SaveChanges();
            }
            else
            {
                bav.Reference = "Bank Verification Failed";
                _context.BankDetails.Update(bav);             

                _context.SaveChanges();

            }
        }

        public BankDetailsDto CreateClientBankDetails(BankDetailsDto dto)
        {
            TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            try
            {
                BankDetailsModel details = _mapper.Map<BankDetailsModel>(dto);
                
                details.BranchCode = uh.BanksDictionary[details.BankName].ToString();

                _context.BankDetails.Add(details);
                _context.SaveChanges();
                dto = _mapper.Map<BankDetailsDto>(details);

                ClientModel client = _context.Clients.Include(c => c.User).First(c => c.Id == dto.ClientId);
                dto.IdNumber = client.User.RSAIdNumber;
                BankValidationServiceRepo bvr = new BankValidationServiceRepo();

                var jobID = string.Empty;
                var validation = bvr.StartBankValidation(dto);

                if (validation.Status == "Failure")
                {
                    dto.Status = "Failed";
                    dto.Message = txtInfo.ToTitleCase(validation.Error);
                    return dto;
                }
                else
                {
                    if (validation.Message.Contains("1054"))
                    {
                        dto.Status = "Failed";
                        dto.Message = txtInfo.ToTitleCase("Invalid branch number");
                        return dto;

                    }

                    if (validation.Message.Contains("1055"))
                    {
                        dto.Status = "Failed";
                        dto.Message = txtInfo.ToTitleCase("Invalid account number");
                        return dto;

                    }

                    if (validation.Message.Contains("1056"))
                    {
                        dto.Status = "Failed";
                        dto.Message = txtInfo.ToTitleCase("Account type invalid");
                        return dto;

                    }

                    if (validation.Message.Contains("1057"))
                    {
                        dto.Status = "Failed";
                        dto.Message = txtInfo.ToTitleCase("Institution not on master file");
                        return dto;

                    }

                    if (validation.Message.Contains("1059"))
                    {
                        dto.Status = "Failed";
                        dto.Message = txtInfo.ToTitleCase("Account number length is not valid");
                        return dto;

                    }

                    if (validation.Message.Contains("1084"))
                    {
                        dto.Status = "Failed";
                        dto.Message = txtInfo.ToTitleCase("Bond account type not allowed for this account");
                        return dto;

                    }

                    else if (validation.JobID == null)
                    {
                        dto.Status = "Failed";
                        dto.Message = txtInfo.ToTitleCase(validation.Message);
                        return dto;
                    }
                }

                details.JobID = validation.JobID;
                BackgroundJob.Schedule(() => CheckBankValidationStatusByJobId(validation.JobID), TimeSpan.FromMinutes(2));

                _context.BankDetails.Update(details);
                _context.SaveChanges();
                dto = _mapper.Map<BankDetailsDto>(details);
                dto.Status = "Success";
                return dto;
            }
            catch (Exception ex)
            {
                dto.Status = "Failed";
                dto.Message = txtInfo.ToTitleCase(ex.Message);
                return dto;
            }
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
            var bankDetails = _context.BankDetails.Where(c => c.ClientId == clientId);

            var result = bankDetails.Any() ? _mapper.Map<BankDetailsDto>(bankDetails.First()) : new BankDetailsDto();

            return result;
        }

        public BankDetailsDto UpdateClientBankDetails(BankDetailsDto dto)
        {
            TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            try
            {
                bool reValidate = false;
                BankDetailsModel oldDetails = _context.BankDetails.Where(a => a.Id == dto.Id).FirstOrDefault();
                BankDetailsModel newDetails = _mapper.Map<BankDetailsModel>(dto);

                if (oldDetails.BankName != newDetails.BankName || oldDetails.AccountNumber != newDetails.AccountNumber)
                {
                    reValidate = true;
                }

                //set user fields to be updated
                oldDetails.BankName = dto.BankName;
                oldDetails.AccountType = dto.AccountType;
                oldDetails.AccountNumber = dto.AccountNumber;
                oldDetails.BranchCode = uh.BanksDictionary[dto.BankName].ToString();

                _context.BankDetails.Update(oldDetails);
                _context.SaveChanges();
                dto = _mapper.Map<BankDetailsDto>(oldDetails);
                dto.Status = "Success";
                if (reValidate)
                {
                    ClientModel client = _context.Clients.Include(c => c.User).First(c => c.Id == dto.ClientId);
                    dto.IdNumber = client.User.RSAIdNumber;
                    BankValidationServiceRepo bvr = new BankValidationServiceRepo();

                    var jobID = string.Empty;
                    var validation = bvr.StartBankValidation(dto);

                    if (validation.Status == "Failure")
                    {
                        dto.Status = "Failed";
                        dto.Message = txtInfo.ToTitleCase(validation.Error);
                        return dto;
                    }
                    else
                    {
                        if (validation.Message.Contains("1054"))
                        {
                            dto.Status = "Failed";
                            dto.Message = txtInfo.ToTitleCase("Invalid branch number");
                            return dto;

                        }

                        if (validation.Message.Contains("1055"))
                        {
                            dto.Status = "Failed";
                            dto.Message = txtInfo.ToTitleCase("Invalid account number");
                            return dto;

                        }

                        if (validation.Message.Contains("1056"))
                        {
                            dto.Status = "Failed";
                            dto.Message = txtInfo.ToTitleCase("Account type invalid");
                            return dto;

                        }

                        if (validation.Message.Contains("1057"))
                        {
                            dto.Status = "Failed";
                            dto.Message = txtInfo.ToTitleCase("Institution not on master file");
                            return dto;

                        }

                        if (validation.Message.Contains("1059"))
                        {
                            dto.Status = "Failed";
                            dto.Message = txtInfo.ToTitleCase("Account number length is not valid");
                            return dto;

                        }

                        if (validation.Message.Contains("1084"))
                        {
                            dto.Status = "Failed";
                            dto.Message = txtInfo.ToTitleCase("Bond account type not allowed for this account");
                            return dto;

                        }

                        else if (validation.JobID == null)
                        {
                            dto.Status = "Failed";
                            dto.Message = txtInfo.ToTitleCase(validation.Message);
                            return dto;
                        }
                    }

                    oldDetails.JobID = validation.JobID;
                    BackgroundJob.Schedule(() => CheckBankValidationStatusByJobId(validation.JobID), TimeSpan.FromMinutes(2));
                    
                    _context.BankDetails.Update(oldDetails);
                    _context.SaveChanges();

                }
                return dto;
            }
            catch (Exception ex)
            {
                dto.Status = "Failed";
                dto.Message = txtInfo.ToTitleCase(ex.Message);
                return dto;
            }
        }

        #endregion Public Methods
    }
}
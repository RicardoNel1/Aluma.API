using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Dto.Client;
using DataService.Enum;
using DataService.Model;
using DataService.Model.Client;
using FileStorageService;
using iText.Layout.Element;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace Aluma.API.Repositories
{
    public interface IOtpRepo : IRepoBase<OtpModel>
    {
        string VerifyOTP(string otp, int userId, int applicationId = 0);

        string SendOTP(UserDto user, OtpTypesEnum otpType, int applicationId = 0);

        string ResendOTP(UserDto user);
        string GetOtpTypeMessage(UserDto user);

        Task SendOTPEmail(UserDto user, OtpTypesEnum otpType, int applicationId = 0);
    }

    public class OtpRepo : RepoBase<OtpModel>, IOtpRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IFileStorageRepo _fileStorage;
        MailSender _ms;

        public OtpRepo(AlumaDBContext context, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(context)
        {
            _context = context;
            _host = host;
            _config = config;
            _mapper = mapper;
            _ms = new MailSender(_context, _config, _fileStorage, _host);
        }

        public string SendOTP(UserDto user, OtpTypesEnum otpType, int applicationId = 0)
        {
            SmsService.SmsRepo smsService = new();
            string result = string.Empty;
            try
            {

                var userOtpList = _context.Otp.Where(o => o.UserId == user.Id && o.isExpired == false && o.isValidated == false).ToList();

                if (userOtpList.Count > 5)
                {
                    UserModel um = _context.Users.Where(u => u.Id == user.Id).First();
                    um.isOtpLocked = true;

                    _context.Users.Update(um);
                    _context.SaveChanges();

                    result = "Too many attempts. Please contact support.";

                    return result;
                }
                            

                string newOtpNumber = smsService.CreateOtp();
                string otpMessage = otpType == OtpTypesEnum.Login ? "Aluma Capital: Herewith your OTP for signing in - " + newOtpNumber
                    : otpType == OtpTypesEnum.Registration ? "Aluma Capital: Herewith your OTP for registration - " + newOtpNumber
                    : otpType == OtpTypesEnum.SignDocument ? "Aluma Capital: Herewith your OTP for authorization of signing the application documents - " + newOtpNumber
                    : otpType == OtpTypesEnum.Consent ? "Aluma Capital: Herewith your OTP to obtain your personal information from agreed upon institutions - " + newOtpNumber
                    : "Aluma Capital: Herewith your OTP for resetting your password - " + newOtpNumber;


                if (otpType == OtpTypesEnum.Consent)
                {
                    var list = new List<string>();
                    var client = _context.Clients.Where(u => u.UserId == user.Id).First();
                    ClientConsentModel consentedProviders = _context.ClientConsentModels.Include(a => a.ConsentedProviders).Where(u => u.ClientId == client.Id).OrderByDescending(c => c.Created).First();

                    List<ClientConsentProviderDto> consentedProviderListDto = _mapper.Map<List<ClientConsentProviderDto>>(consentedProviders.ConsentedProviders);

                    foreach (ClientConsentProviderDto item in consentedProviderListDto)
                    {
                       List<FinancialProviderModel> financialProviderList = _context.FinancialProviders.Where(x => x.Id == item.FinancialProviderId).ToList();

                        foreach (FinancialProviderModel provider in financialProviderList)
                        {
                            otpMessage += ", " + provider.Name;
                        }
                    }

                    
                }


                OtpModel newOtp = new()
                {
                    Otp = newOtpNumber,
                    OtpType = otpType,
                    Created = DateTime.Now,
                    CreatedBy = 00,
                    isExpired = false,
                    isValidated = false,
                    UserId = user.Id
                };

                if (applicationId > 0)
                {
                    newOtp.ApplicationId = applicationId;
                }

                _context.Otp.Add(newOtp);
                _context.SaveChanges();

                bool sent = smsService.SendOtp(user.MobileNumber, otpMessage);

                if (!sent)
                {
                    result = "Could not send sms at this time. Please try again later, or contact support.";
                }
                else
                {
                    result = "Success";
                }
            }
            catch (Exception ex)
            {
                //log error
                result = "Could not send sms at this time.Please try again later, or contact support.";
            }
            return result;
        }

        public string ResendOTP(UserDto user)
        {
            SmsService.SmsRepo smsService = new();
            string result = string.Empty;
            try
            {
                var userOtp = _context.Otp.Where(o => o.UserId == user.Id && o.isExpired == false && o.isValidated == false).OrderByDescending(d => d.Created).First();

                string otpMessage = userOtp.OtpType == OtpTypesEnum.Login ? "Aluma Capital: Herewith your OTP for signing in - " + userOtp.Otp
                     : userOtp.OtpType == OtpTypesEnum.Registration ? "Aluma Capital: Herewith your OTP for registration - " + userOtp.Otp
                     : userOtp.OtpType == OtpTypesEnum.SignDocument ? "Aluma Capital: Herewith your OTP for authorization of signing the application documents - " + userOtp.Otp
                     : userOtp.OtpType == OtpTypesEnum.Consent ? "Aluma Capital: Herewith your OTP to obtain your personal information from agreed upon institutions - " + userOtp.Otp
                     : "Aluma Capital: Herewith your OTP for resetting your password - " + userOtp.Otp;

                OtpModel newOtp = new()
                {
                    Otp = userOtp.Otp,
                    OtpType = userOtp.OtpType,
                    Created = DateTime.Now,
                    CreatedBy = 00,
                    isExpired = false,
                    isValidated = false,
                    UserId = user.Id
                };

                bool sent = smsService.SendOtp(user.MobileNumber, otpMessage);

                if (!sent)
                {
                    result = "Could not send sms at this time. Please try again later, or contact support.";
                }
                else
                {
                    result = "Success";
                }
            }
            catch (Exception ex)
            {
                //log error
                result = "Could not send sms at this time.Please try again later, or contact support.";
            }
            return result;
        }

        public string GetOtpTypeMessage(UserDto user)
        {
            var message = "";
            var userOtp = _context.Otp.Where(o => o.UserId == user.Id && o.isExpired == false && o.isValidated == false).OrderByDescending(d => d.Created).First();
            if(userOtp.OtpType == OtpTypesEnum.Registration)
            {
                message = "verifyRegistration";
            }
            if (userOtp.OtpType == OtpTypesEnum.Login)
            {
                message = "verifyLogin";
            }
            if (userOtp.OtpType == OtpTypesEnum.ResetPassword)
            {
                message = "verifyResetPassword";
            }
            if (userOtp.OtpType == OtpTypesEnum.SignDocument)
            {
                message = "verifySignature";
            }
            if (userOtp.OtpType == OtpTypesEnum.Consent)
            {
                message = "verifyConsent";
            }
            return message;
        }

        public string VerifyOTP(string userOtp, int userId, int applicationId = 0)
        {
            string result = "Invalid";

            try
            {
                //check if there is an otp for that user that has not been used and that has not expired
                var hasOtp = _context.Otp.Where(o => o.UserId == userId && o.isValidated == false).OrderByDescending(o => o.Created);

                if (hasOtp.Any())
                {
                    if (applicationId > 0)
                    {
                        if (hasOtp.Where(o => o.OtpType == OtpTypesEnum.SignDocument).Any() == false)
                        {
                            result = "No Otp for signing.";
                            return result;
                        };
                    }

                    List<OtpModel> otpList = hasOtp.ToList();


                    foreach (var otpModel in otpList)
                    {
                        if (otpModel.Otp == userOtp)
                        {
                            if (!otpModel.isExpired)
                            {
                                DateTime expiryDate = otpModel.Created.AddHours(24);
                                if (expiryDate < DateTime.Now)
                                {
                                    otpModel.isExpired = true;
                                    otpModel.Modified = DateTime.UtcNow;
                                    otpModel.ModifiedBy = 00;
                                    result = "Expired";
                                }
                                else
                                {
                                    otpModel.isValidated = true;
                                    otpModel.Modified = DateTime.UtcNow;
                                    otpModel.ModifiedBy = 00;
                                    result = "Validated";
                                }
                                _context.Otp.Update(otpModel);
                                _context.SaveChanges();
                            }
                            else
                            {
                                result = "Expired";
                            }
                        }
                        //else
                        //{
                        //    otpModel.isExpired = true;
                        //    otpModel.Modified = DateTime.UtcNow;
                        //    otpModel.ModifiedBy = 00;

                        //    _context.Otp.Update(otpModel);
                        //    _context.SaveChanges();
                        //}
                    }
                }
            }
            catch (System.Exception ex)
            {
                result = ex.Message;
                //log error                
            }

            return result;

        }

        public async Task SendOTPEmail(UserDto user, OtpTypesEnum otpType, int applicationId = 0)
        {
            SmsService.SmsRepo smsService = new();
            string result = string.Empty;
            try
            {

                var userOtpList = _context.Otp.Where(o => o.UserId == user.Id && o.isExpired == false && o.isValidated == false).ToList();

                if (userOtpList.Count > 5)
                {
                    UserModel um = _context.Users.Where(u => u.Id == user.Id).First();
                    um.isOtpLocked = true;

                    _context.Users.Update(um);
                    _context.SaveChanges();

                    result = "Too many attempts. Please contact support.";
                }

                string newOtpNumber = smsService.CreateOtp();

                OtpModel newOtp = new()
                {
                    Otp = newOtpNumber,
                    OtpType = otpType,
                    Created = DateTime.Now,
                    CreatedBy = 00,
                    isExpired = false,
                    isValidated = false,
                    UserId = user.Id
                };

                if (applicationId > 0)
                {
                    newOtp.ApplicationId = applicationId;
                }

                _context.Otp.Add(newOtp);
                _context.SaveChanges();

                //bool sent = smsService.SendOtp(user.MobileNumber, otpMessage);
                await _ms.SendOTPEmail(user, newOtpNumber);


            }
            catch (Exception ex)
            {
                //log error
                result = "Could not send sms at this time.Please try again later, or contact support.";
            }

        }

    }
}
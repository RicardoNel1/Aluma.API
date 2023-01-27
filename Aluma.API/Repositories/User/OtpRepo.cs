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

                string adv = "Aluma";
                string client = user.FirstName + " " + user.LastName;
                string newOtpNumber = smsService.CreateOtp();
                string otpMessage = otpType == OtpTypesEnum.Login ? "Aluma Capital: Herewith your OTP for signing in - " + newOtpNumber
                    : otpType == OtpTypesEnum.Registration ? "Aluma Capital: Herewith your OTP for registration - " + newOtpNumber
                    : otpType == OtpTypesEnum.SignDocument ? "Aluma Capital: Herewith your OTP for authorization of signing the application documents - " + newOtpNumber
                    //: otpType == OtpTypesEnum.Consent ? "Aluma Capital: Herewith your OTP - " + newOtpNumber + " to obtain your personal information from the following institutions: " 
                    //: otpType == OtpTypesEnum.Consent ? "Aluma Capital: Herewith your OTP to obtain your personal information from agreed upon institutions - " + newOtpNumber
                    : otpType == OtpTypesEnum.Consent ? "Aluma Capital: Herewith your OTP " + newOtpNumber + " to approve consent for " + adv + " to obtain your portfolio details from Astute. " +
                      "By providing the OTP to "+ adv + ", you, " + client + ", give consent to collect your personal information through Astute or any other Financial Services Provider relating to: long-term insurance; collective investment schemes; pension funds; any other financial product or service which may be relevant to you."
                      + "Astute may retain your ID no, Cell no and email address to prevent fraudulent or unauthorised access to your information. This consent will remain effective for 12 months unless cancelled by you in writing directly with the broker. You indemnify Astute (including all its directors, partners, officers, employees, representatives and agents) and the Financial Product Provider providing information and keep them harmless against any claim."
                      + "PROTECTION OF PERSONAL INFORMATION ACT (POPIA) NOTICE We may use your information or obtain information about you provided by you on a voluntary basis for the following purposes: Underwriting of financial or medical products; Assessment and processing of claims; Credit searches and/or verification of personal information; Claims checks (ASISA Life and Claims Register); Tracing persons entitled to benefits, including beneficiaries; Fraud prevention and detection; Market research and statistical analysis by using anonymised personal data; Audit and record-keeping purposes as required by applicable retention schedules of legal or regulatory requirements, after which such records will be de-identified; Compliance with legal and regulatory requirements; Verifying your identity; Protecting yours and our legitimate interests; Any purposes related to the above; Sharing information with service providers we engage to process such information on our behalf or who render services to us. These service providers may be abroad, but we will not share your information with them unless we are satisfied that they have adequate security measures in place to protect your personal information. We will not send your information to a country that does not have information protection legislation similar to that of the RSA, unless we have a binding agreement with the service provider which ensures that it effectively adheres to the principles for processing of information in accordance with the Protection of Personal Information Act No 4 of 2013."
                    : "Aluma Capital: Herewith your OTP for resetting your password - " + newOtpNumber;


                //if (otpType == OtpTypesEnum.Consent)
                //{
                //    var list = new List<string>();
                //    var client = _context.Clients.Where(u => u.UserId == user.Id).First();
                //    ClientConsentModel consentedProviders = _context.ClientConsentModels.Include(a => a.ConsentedProviders).Where(u => u.ClientId == client.Id).OrderByDescending(c => c.Created).First();

                //    List<ClientConsentProviderDto> consentedProviderListDto = _mapper.Map<List<ClientConsentProviderDto>>(consentedProviders.ConsentedProviders);

                //    foreach (ClientConsentProviderDto item in consentedProviderListDto)
                //    {
                //       List<FinancialProviderModel> financialProviderList = _context.FinancialProviders.Where(x => x.Id == item.FinancialProviderId).ToList();

                //        foreach (FinancialProviderModel provider in financialProviderList)
                //        {
                //            otpMessage += " " + provider.Name + ", ";                             

                //        }
                //    }
                    
                //}


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
                string adv = "Aluma";
                string client = user.FirstName + " " + user.LastName;
                var userOtp = _context.Otp.Where(o => o.UserId == user.Id && o.isExpired == false && o.isValidated == false).OrderByDescending(d => d.Created).First();

                string otpMessage = userOtp.OtpType == OtpTypesEnum.Login ? "Aluma Capital: Herewith your OTP for signing in - " + userOtp.Otp
                     : userOtp.OtpType == OtpTypesEnum.Registration ? "Aluma Capital: Herewith your OTP for registration - " + userOtp.Otp
                     : userOtp.OtpType == OtpTypesEnum.SignDocument ? "Aluma Capital: Herewith your OTP for authorization of signing the application documents - " + userOtp.Otp
                     //: userOtp.OtpType == OtpTypesEnum.Consent ? "Aluma Capital: Herewith your OTP - " + userOtp.Otp + " to obtain your personal information from the following institutions: "
                     : userOtp.OtpType == OtpTypesEnum.Consent ? "Aluma Capital: Herewith your OTP " + userOtp.Otp + " to approve consent for " + adv + " to obtain your portfolio details from Astute. " +
                      "By providing the OTP to " + adv + ", you, " + client + ", give consent to collect your personal information through Astute or any other Financial Services Provider relating to: long-term insurance; collective investment schemes; pension funds; any other financial product or service which may be relevant to you."
                      + "Astute may retain your ID no, Cell no and email address to prevent fraudulent or unauthorised access to your information. This consent will remain effective for 12 months unless cancelled by you in writing directly with the broker. You indemnify Astute (including all its directors, partners, officers, employees, representatives and agents) and the Financial Product Provider providing information and keep them harmless against any claim."
                      + "PROTECTION OF PERSONAL INFORMATION ACT (POPIA) NOTICE We may use your information or obtain information about you provided by you on a voluntary basis for the following purposes: Underwriting of financial or medical products; Assessment and processing of claims; Credit searches and/or verification of personal information; Claims checks (ASISA Life and Claims Register); Tracing persons entitled to benefits, including beneficiaries; Fraud prevention and detection; Market research and statistical analysis by using anonymised personal data; Audit and record-keeping purposes as required by applicable retention schedules of legal or regulatory requirements, after which such records will be de-identified; Compliance with legal and regulatory requirements; Verifying your identity; Protecting yours and our legitimate interests; Any purposes related to the above; Sharing information with service providers we engage to process such information on our behalf or who render services to us. These service providers may be abroad, but we will not share your information with them unless we are satisfied that they have adequate security measures in place to protect your personal information. We will not send your information to a country that does not have information protection legislation similar to that of the RSA, unless we have a binding agreement with the service provider which ensures that it effectively adheres to the principles for processing of information in accordance with the Protection of Personal Information Act No 4 of 2013."
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
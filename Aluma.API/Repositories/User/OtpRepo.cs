using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Enum;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IOtpRepo : IRepoBase<OtpModel>
    {
        #region Public Methods

        string SendOTP(UserDto user, OtpTypesEnum otpType, int applicationId = 0);

        string VerifyOTP(string otp, int userId, int applicationId = 0);

        #endregion Public Methods
    }

    public class OtpRepo : RepoBase<OtpModel>, IOtpRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public OtpRepo(AlumaDBContext context, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(context)
        {
            _context = context;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public string SendOTP(UserDto user, OtpTypesEnum otpType, int applicationId = 0)
        {
            SmsService.SmsRepo smsService = new SmsService.SmsRepo();
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
                    : "Aluma Capital: Herewith your OTP for resetting your password - " + newOtpNumber;

                OtpModel newOtp = new OtpModel()
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
                    result = "Could not send sms at this time.Please try again later, or contact support.";
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
                        else
                        {
                            otpModel.isExpired = true;
                            otpModel.Modified = DateTime.UtcNow;
                            otpModel.ModifiedBy = 00;

                            _context.Otp.Update(otpModel);
                            _context.SaveChanges();
                        }
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

        #endregion Public Methods
    }
}
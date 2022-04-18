using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Enum;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StringHasher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Aluma.API.Repositories
{
    public interface IAdvisorRepo : IRepoBase<AdvisorModel>
    {
        public bool DoesAdvisorExist(UserDto dto);

        public AdvisorDto GetAdvisor(AdvisorDto dto);
        public AdvisorDto GetAdvisorByUserId(int userId);

        public AdvisorDto CreateAdvisor(AdvisorDto dto);

        public bool DeleteAdvisor(AdvisorDto dto);

        public AdvisorDto UpdateAdvisor(AdvisorDto dto);
        public List<AdvisorDto> GetAllAdvisors();
    }

    public class AdvisorRepo : RepoBase<AdvisorModel>, IAdvisorRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AdvisorRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public bool DoesAdvisorExist(UserDto dto)
        {
            bool advisorExists = false;
            UserRepo ur = new UserRepo(_context, _host, _config, _mapper);
            bool userExists = ur.DoesUserExist(dto);

            if (userExists)
            {
                UserDto user = ur.GetUser(dto);
                advisorExists = _context.Advisors.Where(a => a.UserId == user.Id).Any();
            }

            return advisorExists;
        }

        public AdvisorDto GetAdvisor(AdvisorDto dto)
        {
            AdvisorModel advisor = _context.Advisors.Include(a => a.User).Where(a => a.Id == dto.Id).First();
            dto = _mapper.Map<AdvisorDto>(advisor);
            return dto;
        }

        public AdvisorDto GetAdvisorByUserId(int userId)
        {
            AdvisorModel advisor = _context.Advisors.Where(a => a.UserId == userId).First();
            return _mapper.Map<AdvisorDto>(advisor);
        }


        public AdvisorDto CreateAdvisor(AdvisorDto dto)
        {
            try
            {
                StringHasherRepo str = new StringHasherRepo();
                UserRepo ur = new UserRepo(_context, _host, _config, _mapper);
                MailSender mailSender = new MailSender();

                //Create Advisor
                AdvisorModel advisor = _mapper.Map<AdvisorModel>(dto);
                advisor.User.Password = str.CreateHash("Aluma" + advisor.User.FirstName.Trim());
                advisor.isActive = true;
                _context.Advisors.Add(advisor);
                _context.SaveChanges();

                AddressModel resAddress = new AddressModel()
                {
                    UnitNumber = null,
                    ComplexName = "FinTech Campus",
                    StreetNumber = null,
                    StreetName = "Cnr Illanga and Botterklapper",
                    Suburb = "The Willows",
                    City = "Pretoria",
                    PostalCode = "0081",
                    Country = "South Africa",
                    Type = AddressTypesEnum.Residential,
                    UserId = advisor.User.Id,
                    YearsAtAddress = 1
                };

                _context.Address.Add(resAddress);
                _context.SaveChanges();

                AddressModel postalAddress = new AddressModel()
                {
                    UnitNumber = null,
                    ComplexName = "Postnet Suite 33",
                    StreetNumber = null,
                    StreetName = "Private Bag X 26",
                    Suburb = "Sunninghill",
                    City = "Johannesburg",
                    PostalCode = "2157",
                    Country = "South Africa",
                    Type = AddressTypesEnum.Postal,
                    UserId = advisor.User.Id,
                };
                _context.Address.Add(postalAddress);
                _context.SaveChanges();


                //Done
                //SendWelcomeEmail(advisor);

                return _mapper.Map<AdvisorDto>(advisor);
            }
            catch (Exception ex)
            {
                //log error
                return null;
            }
        }

        private async void SendWelcomeEmail(AdvisorModel advisor)
        {
            var mailSettings = _config.GetSection("MailServerSettings").Get<MailServerSettingsDto>();

            UserMail um = new UserMail()
            {
                Email = advisor.User.Email,
                Name = advisor.User.FirstName + " " + advisor.User.LastName,
                Subject = "Aluma Capital: Welcome letter for " + advisor.User.FirstName + " " + advisor.User.LastName,
                Template = "AdvisorWelcome"
            };

            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(mailSettings.Username),
                    Subject = um.Subject,
                    IsBodyHtml = true
                };

                message.To.Add(new MailAddress(advisor.User.Email));
                //message.Bcc.Add(new MailAddress("johan@fintegratetech.co.za"));
                message.Bcc.Add(new MailAddress("system@aluma.co.za"));

                message.Body = "Application Completed: " + um.Name;

                var smtpClient = new SmtpClient
                {
                    Host = "mail.administr8it.co.za",
                    Port = 25,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                    Timeout = 1000000
                };


                smtpClient.Send(message);

                return;

            }
            catch (System.Exception ex)
            {
                return;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public bool DeleteAdvisor(AdvisorDto dto)
        {
            try
            {
                AdvisorModel advisor = _context.Advisors.Where(a => a.Id == dto.Id).First();
                advisor.isActive = false;
                _context.Advisors.Update(advisor);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                //log error
                return false;
            }
        }

        public AdvisorDto UpdateAdvisor(AdvisorDto dto)
        {
            UserModel user = _context.Users.Where(x => x.Id == dto.UserId).FirstOrDefault();
            AdvisorModel advisor = _mapper.Map<AdvisorModel>(dto);

            user.FirstName = dto.User.FirstName.Trim();
            user.LastName = dto.User.LastName.Trim();
            user.MobileNumber = dto.User.MobileNumber.Trim();
            user.Email = dto.User.Email.Trim();
            user.RSAIdNumber = dto.User.RSAIdNumber;

            advisor.User = user;

            _context.Advisors.Update(advisor);
            _context.SaveChanges();

            dto = _mapper.Map<AdvisorDto>(advisor);
            return dto;
        }

        public List<AdvisorDto> GetAllAdvisors()
        {
            List<AdvisorModel> advisors = _context.Advisors.Include(c => c.User).Where(c => c.isActive == true).ToList();
            List<AdvisorDto> response = _mapper.Map<List<AdvisorDto>>(advisors);
            return response;
        }
    }
}
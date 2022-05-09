using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Enum;
using DataService.Model;
using FileStorageService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StringHasher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Aluma.API.Repositories
{
    public interface IAdvisorRepo : IRepoBase<AdvisorModel>
    {
        #region Public Methods

        public Task<AdvisorDto> CreateAdvisor(AdvisorDto dto);

        public bool DeleteAdvisor(AdvisorDto dto);

        public bool DoesAdvisorExist(UserDto dto);

        public AdvisorDto GetAdvisor(AdvisorDto dto);
        public AdvisorDto GetAdvisorByUserId(int userId);
        public List<AdvisorDto> GetAllAdvisors();

        public AdvisorDto UpdateAdvisor(AdvisorDto dto);

        #endregion Public Methods
    }

    public class AdvisorRepo : RepoBase<AdvisorModel>, IAdvisorRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IFileStorageRepo _fileStorage;

        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;
        MailSender _ms;

        #endregion Private Fields

        #region Public Constructors

        public AdvisorRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
            _ms = new MailSender(_context, _config, _fileStorage, _host);
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<AdvisorDto> CreateAdvisor(AdvisorDto dto)
        {
            try
            {
                StringHasherRepo str = new StringHasherRepo();
                UserRepo ur = new UserRepo(_context, _host, _config, _fileStorage, _mapper);


                //Create Advisor
                AdvisorModel advisor = _mapper.Map<AdvisorModel>(dto);
                advisor.User.Password = str.CreateHash("Aluma" + advisor.User.FirstName.Trim());
                advisor.User.Signature = null;
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
                await _ms.SendAdvisorWelcomeEmail(advisor);

                return _mapper.Map<AdvisorDto>(advisor);
            }
            catch (Exception ex)
            {
                //log error
                return null;
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

        public bool DoesAdvisorExist(UserDto dto)
        {
            bool advisorExists = false;
            UserRepo ur = new UserRepo(_context, _host, _config, _fileStorage, _mapper);
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
        public List<AdvisorDto> GetAllAdvisors()
        {
            List<AdvisorModel> advisors = _context.Advisors.Include(c => c.User).Where(c => c.isActive == true).ToList();
            List<AdvisorDto> response = _mapper.Map<List<AdvisorDto>>(advisors);
            return response;
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

        #endregion Public Methods
    }
}
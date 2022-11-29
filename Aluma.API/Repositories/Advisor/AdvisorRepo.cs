using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Dto.Advisor;
using DataService.Enum;
using DataService.Model;
using DataService.Model.Advisor;
using FileStorageService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StringHasher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Aluma.API.Repositories
{
    public interface IAdvisorRepo : IRepoBase<AdvisorModel>
    {
        public bool DoesAdvisorExist(UserDto dto);
        public bool DoesAdvisorAstuteExist(UserDto dto);
        public AdvisorDto GetAdvisor(AdvisorDto dto);
        public AdvisorAstuteDto GetAdvisorAstute(AdvisorAstuteDto dto);
        public AdvisorDto GetAdvisorByUserId(int userId);
        public AdvisorAstuteDto GetAstuteAdvisorCredential(int advisorId);
        public AdvisorAstuteDto GetAstuteAdvisorCredentialByUserId(int userId);

        public Task<AdvisorDto> CreateAdvisor(AdvisorDto dto);
        public Task<AdvisorAstuteDto> CreateAdvisorAstute(AdvisorAstuteDto dto);

        public bool DeleteAdvisor(AdvisorDto dto);

        public AdvisorDto UpdateAdvisor(AdvisorDto dto);
        public AdvisorAstuteDto UpdateAdvisorAstute(AdvisorAstuteDto dto);
        public List<AdvisorDto> GetAllAdvisors();
    }

    public class AdvisorRepo : RepoBase<AdvisorModel>, IAdvisorRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IFileStorageRepo _fileStorage;
        MailSender _ms;
        public AdvisorRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
            _ms = new MailSender(_context, _config, _fileStorage, _host);
        }

        public bool DoesAdvisorExist(UserDto dto)
        {
            bool advisorExists = false;
            UserRepo ur = new(_context, _host, _config, _fileStorage, _mapper);
            bool userExists = ur.DoesUserExist(dto);

            if (userExists)
            {
                UserDto user = ur.GetUser(dto);
                advisorExists = _context.Advisors.Where(a => a.UserId == user.Id).Any();
            }

            return advisorExists;
        }

        public bool DoesAdvisorAstuteExist(UserDto dto)
        {
            bool advisorExists = false;
            UserRepo ur = new(_context, _host, _config, _fileStorage, _mapper);
            bool userExists = ur.DoesUserExist(dto);

            if (userExists)
            {
                UserDto user = ur.GetUser(dto);
                advisorExists = _context.AdvisorsAstute.Where(a => a.AdvisorId == user.Id).Any();
            }

            return advisorExists;
        }

        public AdvisorDto GetAdvisor(AdvisorDto dto)
        {
            AdvisorModel advisor = _context.Advisors.Include(a => a.User).Where(a => a.Id == dto.Id).First();

            if (advisor == null)
            {
                return new AdvisorDto();
            }
            else
            {
                dto = _mapper.Map<AdvisorDto>(advisor);
            }

            dto = _mapper.Map<AdvisorDto>(advisor);
            return dto;
        }

        public AdvisorAstuteDto GetAdvisorAstute(AdvisorAstuteDto dto)
        {
            AdvisorAstuteModel advisor = _context.AdvisorsAstute.Include(a => a.Advisor.User).Where(a => a.Id == dto.AdvisorId).FirstOrDefault();

            if (advisor == null)
            {
                return new AdvisorAstuteDto();
            }
            else
            {
                dto = _mapper.Map<AdvisorAstuteDto>(advisor);
            }

            return dto;
        }

        public AdvisorDto GetAdvisorByUserId(int userId)
        {
            AdvisorModel advisor = _context.Advisors.Where(a => a.UserId == userId).First();
            return _mapper.Map<AdvisorDto>(advisor);
        }


        public async Task<AdvisorDto> CreateAdvisor(AdvisorDto dto)
        {
            try
            {
                StringHasherRepo str = new();
                UserRepo ur = new(_context, _host, _config, _fileStorage, _mapper);


                //Create Advisor
                AdvisorModel advisor = _mapper.Map<AdvisorModel>(dto);
                advisor.User.Password = str.CreateHash("Aluma" + advisor.User.FirstName.Trim());
                advisor.User.Signature = null;
                advisor.isActive = true;
                _context.Advisors.Add(advisor);
                _context.SaveChanges();

                AddressModel resAddress = new()
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

                AddressModel postalAddress = new()
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

        public async Task<AdvisorAstuteDto> CreateAdvisorAstute(AdvisorAstuteDto dto)
        {
            try
            {
                StringHasherRepo str = new();
                CredentialCrypt credentialCrypt = new CredentialCrypt();

                //Create Advisor
                AdvisorAstuteModel advisorAstute = _mapper.Map<AdvisorAstuteModel>(dto);
                advisorAstute.Advisor = _context.Advisors.Where(a => a.Id == dto.AdvisorId).First();

                advisorAstute.Password = credentialCrypt.EncryptToHash(dto.Password);
                _context.AdvisorsAstute.Update(advisorAstute);
                _context.SaveChanges();

                return _mapper.Map<AdvisorAstuteDto>(advisorAstute);
            }
            catch (Exception ex)
            {
                //log error
                return null;
            }
        }

        public AdvisorAstuteDto UpdateAdvisorAstute(AdvisorAstuteDto dto)
        {
            UserModel user = _context.Users.Where(x => x.Id == dto.Id).FirstOrDefault();
            AdvisorAstuteModel advisorAstute = _mapper.Map<AdvisorAstuteModel>(dto);
            CredentialCrypt credentialCrypt = new CredentialCrypt();

            advisorAstute.UserName = dto.UserName.Trim();
            advisorAstute.Password = credentialCrypt.EncryptToHash(dto.Password);

            _context.AdvisorsAstute.Update(advisorAstute);
            _context.SaveChanges();

            dto = _mapper.Map<AdvisorAstuteDto>(advisorAstute);
            return dto;
        }

        public List<AdvisorDto> GetAllAdvisors()
        {
            List<AdvisorModel> advisors = _context.Advisors.Include(c => c.User).Where(c => c.isActive == true).ToList();
            List<AdvisorDto> response = _mapper.Map<List<AdvisorDto>>(advisors);
            return response;
        }

        public AdvisorAstuteDto GetAstuteAdvisorCredential(int advisorId)
        {
            CredentialCrypt credentialCrypt = new CredentialCrypt();

            AdvisorAstuteModel advisor = _context.AdvisorsAstute.Include(a => a.Advisor.User).Where(a => a.Id == advisorId).FirstOrDefault();
            if (advisor == null)
            {
                return new AdvisorAstuteDto();
            }
            else
            {
                advisor.Password = credentialCrypt.DecryptFromHash(advisor.Password);
            }


            return _mapper.Map<AdvisorAstuteDto>(advisor);
        }

        public AdvisorAstuteDto GetAstuteAdvisorCredentialByUserId(int userId)
        {
            CredentialCrypt credentialCrypt = new CredentialCrypt();

            AdvisorAstuteModel advisor = _context.AdvisorsAstute.Include(a => a.Advisor.User).Where(a => a.Advisor.UserId == userId).FirstOrDefault();
            advisor.Password = credentialCrypt.DecryptFromHash(advisor.Password);

            return _mapper.Map<AdvisorAstuteDto>(advisor);
        }
    }
}
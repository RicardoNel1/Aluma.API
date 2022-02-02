using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using StringHasher;
using System;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IAdvisorRepo : IRepoBase<AdvisorModel>
    {
        public bool DoesAdvisorExist(UserDto dto);

        public AdvisorDto GetAdvisor(AdvisorDto dto);

        public AdvisorDto CreateAdvisor(AdvisorDto dto);

        public bool DeleteAdvisor(AdvisorDto dto);

        object UpdateAdvisor(AdvisorDto dto);
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
            AdvisorModel advisor = _context.Advisors.Where(a => a.Id == dto.Id).First();
            return _mapper.Map<AdvisorDto>(advisor);
        }

        
        public AdvisorDto CreateAdvisor(AdvisorDto dto)
        {
            try
            {
                StringHasherRepo str = new StringHasherRepo();
                UserRepo ur = new UserRepo(_context, _host, _config, _mapper);
                MailSender mailSender = new MailSender();

                //Create User
                UserModel user = _mapper.Map<UserModel>(dto.User);
                user.Password = str.CreateHash("Aluma" + user.FirstName.Trim());
                ur.Create(user);
                _context.SaveChanges();

                //Create Advisor
                AdvisorModel advisor = _mapper.Map<AdvisorModel>(dto);
                _context.Advisors.Add(advisor);
                _context.SaveChanges();

                //Done
                //mailSender.SendWelcomeEmail(advisor.User);

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

        public object UpdateAdvisor(AdvisorDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
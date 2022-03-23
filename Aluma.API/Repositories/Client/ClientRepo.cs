using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IClientRepo : IRepoBase<ClientModel>
    {
        public ClientDto GetClient(int userId);
        public List<ClientDto> GetClients();

        public List<ClientDto> GetClientsByAdvisor(int advisorId);

        public bool DeleteClient(ClientDto dto);

        bool DoesClientExist(RegistrationDto dto);
        bool DoesClientExist(ClientDto dto);

        ClientDto CreateClient(ClientDto dto);

        ClientDto UpdateClient(ClientDto dto);

        void GenerateClientDocuments(int clientId);
    }

    public class ClientRepo : RepoBase<ClientModel>, IClientRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public ClientRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public List<ClientDto> GetClients()
        {
            List<ClientModel> clients = _context.Clients.Where(c => c.isDeleted == true).ToList();
            return _mapper.Map<List<ClientDto>>(clients);
        }

        public List<ClientDto> GetClientsByAdvisor(int advisorId)
        {
            List<ClientModel> clients = _context.Clients.Include(c => c.User).Where(c => c.AdvisorId == advisorId).ToList();
            return _mapper.Map<List<ClientDto>>(clients);
        }

        public bool DeleteClient(ClientDto dto)
        {
            try
            {
                ClientModel client = _context.Clients.Where(a => a.Id == dto.Id).First();
                client.isDeleted = false;
                _context.Clients.Update(client);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                //log error
                return false;
            }
        }

        public ClientDto GetClient(int userId)
        {
            ClientModel client = _context.Clients.Where(c => c.UserId == userId).First();
            return _mapper.Map<ClientDto>(client);
        }

        public bool DoesClientExist(RegistrationDto dto)
        {
            bool clientExists = false;
            UserRepo ur = new UserRepo(_context, _host, _config, _mapper);
            bool userExists = ur.DoesUserExist(dto);

            if (userExists)
            {
                UserModel user = ur.GetUser(dto);
                clientExists = _context.Clients.Where(a => a.UserId == user.Id).Any();
            }

            return clientExists;
        }

        public bool DoesClientExist(ClientDto dto)
        {
            bool clientExists = false;
            
            clientExists = _context.Clients.Where(a => a.Id == dto.Id).Any();
            
            return clientExists;
        }

        public ClientDto CreateClient(ClientDto dto)
        {
            ClientModel client = _mapper.Map<ClientModel>(dto);
            _context.Clients.Add(client);
            _context.SaveChanges();
            dto = _mapper.Map<ClientDto>(client);
            return dto;
        }

        public ClientDto UpdateClient(ClientDto dto)
        {
            UserModel user  = _context.Users.Where(x => x.Id == dto.UserId).FirstOrDefault();
            ClientModel client = _mapper.Map<ClientModel>(dto);           //can't map address
            //ClientModel client = _context.Clients.Where(x => x.Id == dto.Id).FirstOrDefault();

            //set user fields to be updated
            user.FirstName = dto.User.FirstName;
            user.LastName = dto.User.LastName;
            user.RSAIdNumber = dto.User.RSAIdNumber;
            user.DateOfBirth = dto.User.DateOfBirth;

            ////set address fields to be updated
            //if (dto.User.Address != null) { 
            //foreach (var item in dto.User.Address)
            //{

            //    bool existingItem = _context.Address.Where(a => a.Id == item.Id).Any();

            //    if (existingItem)
            //    {
            //        AddressModel updateItem = _context.Address.Where(a => a.Id == item.Id).FirstOrDefault();
            //        Enum.TryParse(item.Type, true, out DataService.Enum.AddressTypesEnum parsedType);

            //        updateItem.UnitNumber = item.UnitNumber;
            //        updateItem.ComplexName = item.ComplexName;
            //        updateItem.StreetNumber = item.StreetNumber;
            //        updateItem.StreetName = item.StreetName;
            //        updateItem.Suburb = item.Suburb;
            //        updateItem.City = item.City;
            //        updateItem.PostalCode = item.PostalCode;
            //        //updateItem.Country = item.Country;
            //        updateItem.Type = parsedType;
            //        updateItem.InCareAddress = item.InCareAddress;
            //        updateItem.InCareName = item.InCareName;
            //        updateItem.YearsAtAddress = item.YearsAtAddress;
            //        updateItem.AddressSameAs = item.AddressSameAs;

            //        _context.Address.Update(updateItem);

            //    }
            //    else
            //    {
            //        AddressModel newItem = new AddressModel();
            //        Enum.TryParse(item.Type, true, out DataService.Enum.AddressTypesEnum parsedType);

            //        newItem.UserId = dto.UserId;
            //        newItem.UnitNumber = item.UnitNumber;
            //        newItem.ComplexName = item.ComplexName;
            //        newItem.StreetNumber = item.StreetNumber;
            //        newItem.StreetName = item.StreetName;
            //        newItem.Suburb = item.Suburb;
            //        newItem.City = item.City;
            //        newItem.PostalCode = item.PostalCode;
            //        //newItem.Country = item.Country;
            //        newItem.Type = parsedType;
            //        newItem.InCareAddress = item.InCareAddress;
            //        newItem.InCareName = item.InCareName;
            //        newItem.YearsAtAddress = item.YearsAtAddress;
            //        newItem.AddressSameAs = item.AddressSameAs;                    

            //        _context.Address.Add(newItem);

            //    }
            //}
            //}




            //set client fields to be updated
            client.User = user;

            _context.Clients.Update(client);
            _context.SaveChanges();
            dto = _mapper.Map<ClientDto>(client);
            return dto;
        }

        public void GenerateClientDocuments(int clientId)
        {
            ClientModel client = _context.Clients.Include(c => c.User).Include(c => c.BankDetails).Include(c => c.TaxResidency).SingleOrDefault(c => c.Id == clientId);
            AdvisorModel advisor = _context.Advisors.Include(a => a.User).SingleOrDefault(ad => ad.Id == client.AdvisorId);
            
            RiskProfileModel risk = _context.RiskProfiles.SingleOrDefault(r => r.ClientId == client.Id);
            FSPMandateModel fsp = _context.FspMandates.SingleOrDefault(r => r.ClientId == client.Id);
            FNAModel fna = _context.FNA.SingleOrDefault(r => r.ClientId == client.Id);


            //Risk Profile
            RiskProfileRepo riskRepo = new RiskProfileRepo(_context, _host, _config, _mapper);
            riskRepo.GenerateRiskProfile(client, advisor, risk);

            //FSP Mandate
            FspMandateRepo fspRepo = new FspMandateRepo(_context, _host, _config, _mapper);
            fspRepo.GenerateFSP(client,advisor,fsp);

            //FNA
            FNARepo fnaRepo = new FNARepo(_context, _host, _config, _mapper);
            fnaRepo.GenerateFNA(client,advisor,fna);

        }

    }
}
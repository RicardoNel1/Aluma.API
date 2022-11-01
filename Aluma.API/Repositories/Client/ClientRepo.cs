using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using BankValidationService;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using FileStorageService;
using IDVService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StringHasher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluma.API.Repositories
{
    public interface IClientRepo : IRepoBase<ClientModel>
    {
        public ClientDto GetClientByUserId(int userId);
        public ClientDto GetClient(ClientDto dto);

        public List<ClientDto> GetClients();

        public List<ClientDto> GetClientsByAdvisor(int advisorId);

        public bool DeleteClient(ClientDto dto);

        bool DoesClientExist(RegistrationDto dto);
        bool DoesClientExist(ClientDto dto);
        bool IsAccountActive(ClientDto dto);

        Task<ClientDto> CreateClient(ClientDto dto);

        ClientDto UpdateClient(ClientDto dto);

        void GenerateClientDocuments(int clientId);
        void UpdateClientPassports(List<PassportDto> dto);
        bool DoesIDExist(ClientDto dto);

        bool IDVerification(ClientDto dto);

        ClientDto CheckForFNA(ClientDto client);

        Task UploadConsentForm(byte[] fileData, int clientId);
        Task UploadOtherDocuments(byte[] fileData, string fileName, DataService.Enum.DocumentTypesEnum documentType, int clientId);
        Task ActivateClient(ClientDto dto);
    }

    public class ClientRepo : RepoBase<ClientModel>, IClientRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IFileStorageRepo _fileStorage;
        MailSender _ms;
        public ClientRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper, IFileStorageRepo fileStorage) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
            _fileStorage = fileStorage;
            _ms = new MailSender(_context, _config, _fileStorage, _host);
        }

        public List<ClientDto> GetClients()
        {
            List<ClientModel> clients = _context.Clients.Include(a => a.User).ThenInclude(c => c.Address).Include(c => c.EmploymentDetails).Include(c => c.MaritalDetails).Where(c => c.isDeleted == false).ToList();
            List<ClientDto> response = _mapper.Map<List<ClientDto>>(clients);
            foreach (ClientDto dto in response)
            {
                dto.User.MobileNumber = "0" + dto.User.MobileNumber;
                if (dto.AdvisorId != null)
                {
                    var advisor = _context.Advisors.Include(a => a.User).Where(a => a.Id == dto.AdvisorId).First();

                    dto.AdvisorName = advisor.User.FirstName + " " + advisor.User.LastName;
                }

                dto.ApplicationCount = _context.Applications.Where(a => a.ClientId == dto.Id).Count();

                var discExists = _context.Disclosures.Where(d => d.ClientId == dto.Id);
                if (discExists.Any())
                {
                    dto.DisclosureDate = discExists.First().Created;
                }

                dto.hasDisclosure = discExists.Any();

                var fnaExists = _context.clientFNA.Where(d => d.ClientId == dto.Id);
                if (fnaExists.Any())
                {
                    dto.FNADate = fnaExists.First().Created;
                }

                dto.hasFNA = fnaExists.Any();
            }


            return response;
        }

        public List<ClientDto> GetClientsByAdvisor(int advisorId)
        {
            List<ClientModel> clients = _context.Clients.Include(c => c.User).ThenInclude(c => c.Address).Include(c => c.EmploymentDetails).Include(c => c.MaritalDetails).Where(c => c.AdvisorId == advisorId).ToList();
            List<ClientDto> response = _mapper.Map<List<ClientDto>>(clients);
            foreach (var dto in response)
            {
                dto.User.MobileNumber = "0" + dto.User.MobileNumber;
                if (dto.AdvisorId != null)
                {
                    var advisor = _context.Advisors.Include(a => a.User).Where(a => a.Id == dto.AdvisorId).First();

                    dto.AdvisorName = advisor.User.FirstName + " " + advisor.User.LastName;
                }

                dto.ApplicationCount = _context.Applications.Where(a => a.ClientId == dto.Id).Count();

                var discExists = _context.Disclosures.Where(d => d.ClientId == dto.Id);
                if (discExists.Any())
                {
                    dto.DisclosureDate = discExists.First().Created;
                }

                dto.hasDisclosure = discExists.Any();

                var fnaExists = _context.clientFNA.Where(d => d.ClientId == dto.Id);
                if (fnaExists.Any())
                {
                    dto.FNADate = fnaExists.First().Created;
                }

                dto.hasFNA = fnaExists.Any();
            }
            return response;
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

        public ClientDto GetClient(ClientDto dto)
        {
            ClientModel client = _context.Clients.Include(c => c.User).ThenInclude(c => c.Address).Include(c => c.EmploymentDetails).Include(c => c.MaritalDetails).Where(c => c.Id == dto.Id).First();
            dto = _mapper.Map<ClientDto>(client);

            dto.User.MobileNumber = "0" + dto.User.MobileNumber;
            //dto.User.MobileNumber = dto.User.MobileNumber;

            if (dto.AdvisorId != null)
            {
                var advisor = _context.Advisors.Include(a => a.User).Where(a => a.Id == dto.AdvisorId).First();

                dto.AdvisorName = advisor.User.FirstName + " " + advisor.User.LastName;
            }

            dto.ApplicationCount = _context.Applications.Where(a => a.ClientId == dto.Id).Count();

            var discExists = _context.Disclosures.Where(d => d.ClientId == dto.Id);
            if (discExists.Any())
            {
                dto.DisclosureDate = discExists.First().Created;
            }


            dto.hasDisclosure = discExists.Any();

            return dto;
        }

        public ClientDto GetClientByUserId(int userId)
        {
            ClientModel client = _context.Clients.Include(c => c.User).ThenInclude(c => c.Address).Include(c => c.EmploymentDetails).Include(c => c.MaritalDetails).Where(c => c.UserId == userId).First();
            ClientDto response = _mapper.Map<ClientDto>(client);
            response.User.MobileNumber = "0" + response.User.MobileNumber;
            response.ApplicationCount = _context.Applications.Where(a => a.ClientId == response.Id).Count();

            response = CheckForDisclosures(response);
            response = CheckForFNA(response);

            return response;
        }

        private ClientDto CheckForDisclosures(ClientDto client)
        {

            var disclosureExists = _context.Disclosures.Where(d => d.ClientId == client.Id);

            if (disclosureExists.Any())
            {
                client.DisclosureDate = disclosureExists.First().Created;
            }

            client.hasDisclosure = disclosureExists.Any();

            return client;
        }

        public ClientDto CheckForFNA(ClientDto client)
        {

            var fnaExists = _context.clientFNA.Where(d => d.ClientId == client.Id);

            if (fnaExists.Any())
            {
                client.FNADate = fnaExists.First().Created;
            }

            client.hasFNA = fnaExists.Any();

            return client;
        }

        public bool DoesClientExist(RegistrationDto dto)
        {
            bool clientExists = false;
            UserRepo ur = new(_context, _host, _config, _fileStorage, _mapper);
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

        public bool IsAccountActive(ClientDto dto)
        {
            bool active = false;

            active = dto.User.Password != null;

            return active;
        }

        public async Task<ClientDto> CreateClient(ClientDto dto)
        {
            dto.ClientType = "Primary";
            //dto.AdvisorId = null;
            ClientModel client = _mapper.Map<ClientModel>(dto);
            _context.Clients.Add(client);
            _context.SaveChanges();

            dto.User.RSAIdNumber = client.User.RSAIdNumber;
            IDVServiceRepo idv = new();
            var jobID = string.Empty;
            //var validation = idv.StartIDVerification(dto);

            dto = _mapper.Map<ClientDto>(client);

            //await _ms.SendClientWelcomeEmail(client);
            return dto;
        }

        public ClientDto UpdateClient(ClientDto dto)
        {
            UserModel user = _context.Users.Where(x => x.Id == dto.UserId).FirstOrDefault();
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

            //dto.User.RSAIdNumber = client.User.RSAIdNumber;
            //IDVServiceRepo idv = new();
            //var jobID = string.Empty;
            //var validation = idv.StartIDVerification(dto);

            dto = _mapper.Map<ClientDto>(client);
            return dto;
        }

        public void GenerateClientDocuments(int clientId)
        {
            ClientModel client = _context.Clients.Include(c => c.User).Include(c => c.BankDetails).Include(c => c.TaxResidency).SingleOrDefault(c => c.Id == clientId);
            AdvisorModel advisor = _context.Advisors.Include(a => a.User).SingleOrDefault(ad => ad.Id == client.AdvisorId);

            RiskProfileModel risk = _context.RiskProfiles.SingleOrDefault(r => r.ClientId == client.Id);
            FSPMandateModel fsp = _context.FspMandates.SingleOrDefault(r => r.ClientId == client.Id);
            ClientFNAModel fna = _context.clientFNA.SingleOrDefault(r => r.ClientId == client.Id);



            //Risk Profile
            RiskProfileRepo riskRepo = new(_context, _host, _config, _mapper, _fileStorage);
            riskRepo.GenerateRiskProfile(client, advisor, risk);

            //FSP Mandate
            FspMandateRepo fspRepo = new(_context, _host, _config, _mapper, _fileStorage);
            fspRepo.GenerateMandate(client, advisor, fsp);

            //FNA
            //FNARepo fnaRepo = new FNARepo(_context, _host, _config, _mapper, _fileStorage);
            //fnaRepo.GenerateFNA(client, advisor, fna);

        }

        public void UpdateClientPassports(List<PassportDto> dto)
        {
            foreach (PassportDto passport in dto)
            {
                var pModel = _mapper.Map<PassportModel>(passport);
                _context.Passports.Add(pModel);
            }
            _context.SaveChanges();


        }

        public bool DoesIDExist(ClientDto dto)
        {
            try
            {
                bool idExists = false;

                if (dto.User.Id != 0)
                {

                    idExists = _context.Users.Where(a => a.Id != dto.User.Id && a.RSAIdNumber == dto.User.RSAIdNumber).Any();
                }
                else
                {
                    idExists = _context.Users.Where(a => a.RSAIdNumber == dto.User.RSAIdNumber).Any();
                }

                return idExists;
            }
            catch (Exception ex)
            {
                //log error
                return true;
            }
        }

        public bool IDVerification(ClientDto dto)
        {
            try
            {
                bool idVerified = false;

                if (dto.User.Id != 0)
                {

                    idVerified = _context.Users.Where(a => a.Id != dto.User.Id && a.RSAIdNumber == dto.User.RSAIdNumber).Any();
                }
                else
                {
                    idVerified = _context.Users.Where(a => a.RSAIdNumber == dto.User.RSAIdNumber).Any();
                }

                return idVerified;
            }
            catch (Exception ex)
            {
                //log error
                return true;
            }
        }

        public async Task UploadConsentForm(byte[] fileData, int clientId)
        {
            try
            {
                if (fileData != null && fileData.Length > 0)
                {
                    ClientModel client = _context.Clients.Include(c => c.User).SingleOrDefault(c => c.Id == clientId);
                    IDocumentHelper documentHelper = new DocumentHelper(_context, _config, _fileStorage, _host);

                    await documentHelper.SaveDocument(fileData, DataService.Enum.DocumentTypesEnum.ClientConsent, client.User);
                }
                else
                {
                    throw new Exception("There is no consent form to upload");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task UploadOtherDocuments(byte[] fileData, string fileName, DataService.Enum.DocumentTypesEnum documentType, int clientId)
        {
            try
            {
                if (fileData != null && fileData.Length > 0)
                {
                    ClientModel client = _context.Clients.Include(c => c.User).SingleOrDefault(c => c.Id == clientId);
                    IDocumentHelper documentHelper = new DocumentHelper(_context, _config, _fileStorage, _host);

                    await documentHelper.SaveDocument(fileData, documentType, client.User, null, fileName);
                }
                else
                {
                    throw new Exception("There is no file to upload");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ActivateClient(ClientDto client)
        {
            UserModel um = _context.Users.Where(u => u.Id == client.User.Id).FirstOrDefault();
            ClientModel cm = _context.Clients.Where(c => c.Id == client.Id).FirstOrDefault();
            MailSender ms = new(_context, _config, _fileStorage, _host);
            StringHasherRepo str = new();

            um.Password = str.CreateHash("Aluma" + client.User.FirstName.Trim());
            _context.Users.Update(um);
            _context.SaveChanges();

            await ms.SendInvestNowClientWelcomeEmail(cm);

        }
    }
}
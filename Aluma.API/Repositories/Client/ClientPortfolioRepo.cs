using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using FileStorageService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluma.API.Repositories
{
    public interface IClientPortfolioRepo : IRepoBase<ClientPortfolioDto>
    {
        Task<ClientPortfolioDto> GetClientPortfolio(int clientId);
        List<ClientNotesDto> CreateClientNote(List<ClientNotesDto> dtoArray);
        string DeleteClientNote(int Id);

    }

    public class ClientPortfolioRepo : RepoBase<ClientPortfolioDto>, IClientPortfolioRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IFileStorageRepo _filestorage;
        private readonly IMapper _mapper;

        public ClientPortfolioRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IFileStorageRepo filestorage, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _filestorage = filestorage;
            _mapper = mapper;
        }



        public async Task<ClientPortfolioDto> GetClientPortfolio(int clientId)
        {
            ClientPortfolioDto dto = new();
            dto.Client = GetClient(clientId);
            dto.Client.User = await GetUserWithAddress(dto.Client.UserId);
            dto.FNA = GetClientFNA(dto.Client.Id);
            dto.Investments = GetInvestments(dto.FNA.Id);
            dto.Retirement = GetRetirement(dto.FNA.Id);
            dto.RetirementPlanning = GetRetirementPlanning(dto.FNA.Id);
            dto.ProvidingDisability = GetProvidingDisability(dto.FNA.Id);
            dto.ProvidingDeath = GetProvidingDeath(dto.FNA.Id);
            dto.ProvidingDread = GetProvidingDread(dto.FNA.Id);
            dto.ShortTermInsurance = GetShortTerm(dto.Client.Id);
            dto.MedicalAid = GetMedical(dto.Client.Id);
            dto.Assumptions = GetAssumptions(dto.FNA.Id);
            dto.Insurance = GetInsurances(dto.FNA.Id);

            dto.DocumentList = new List<DocumentListDto>();
            dto.DocumentList = AddDocuments(await GetUserDocuments(dto.Client.UserId), dto.DocumentList);
            dto.DocumentList= AddDocuments(await GetAppDocuments(dto.Client.UserId), dto.DocumentList);

            dto.ClientNotes = GetClientNotes(clientId);
            return dto;
        }

        private async Task<UserDto> GetUserWithAddress(int userId)
        {
            try
            {
                UserRepo _user = new UserRepo(_context, _host, _config, _filestorage, _mapper);
                UserDto user = await _user.GetUserWithAddress(new() { Id = userId });

                if (user == null)
                    return new();

                return user;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private ClientFNADto GetClientFNA(int clientId)
        {
            try
            {
                FNARepo _fna = new FNARepo(_context, _host, _config, _mapper, null);
                ClientFNADto clientFNA = _fna.GetClientFNA(clientId);

                if (clientFNA == null)
                    return new();

                return clientFNA;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private ClientDto GetClient(int clientId)
        {
            try
            {
                ClientRepo _clientRepo = new ClientRepo(_context, _host, _config, _mapper, null);
                ClientDto client = _clientRepo.GetClient(new ClientDto() { Id = clientId });

                if (client == null)
                    return new();

                return client;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private List<InvestmentsDto> GetInvestments(int fnaId)
        {
            try
            {
                InvestmentsRepo _investments = new InvestmentsRepo(_context, _host, _config, _mapper);
                List<InvestmentsDto> investments = _investments.GetInvestments(fnaId);

                if (investments == null)
                    return new();

                return investments;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private RetirementSummaryDto GetRetirement(int fnaId)
        {
            try
            {
                RetirementSummaryRepo _retirement = new RetirementSummaryRepo(_context, _host, _config, _mapper);
                RetirementSummaryDto retirementSummary = _retirement.GetRetirementSummary(fnaId);

                if (retirementSummary == null)
                    return new();

                return retirementSummary;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private RetirementPlanningDto GetRetirementPlanning(int fnaId)
        {
            try
            {
                RetirementPlanningRepo _retirement = new RetirementPlanningRepo(_context, _host, _config, _mapper);
                RetirementPlanningDto retirement = _retirement.GetRetirementPlanning(fnaId);

                if (retirement == null)
                    return new();

                return retirement;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private ProvidingDisabilitySummaryDto GetProvidingDisability(int fnaId)
        {
            try
            {
                ProvidingDisabilitySummaryRepo _disability = new ProvidingDisabilitySummaryRepo(_context, _host, _config, _mapper);
                ProvidingDisabilitySummaryDto providingOnDisability = _disability.GetProvidingDisabilitySummary(fnaId);

                if (providingOnDisability == null)
                    return new();

                return providingOnDisability;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private ProvidingDeathSummaryDto GetProvidingDeath(int fnaId)
        {
            try
            {
                ProvidingDeathSummaryRepo _death = new ProvidingDeathSummaryRepo(_context, _host, _config, _mapper);
                ProvidingDeathSummaryDto providingOnDeath = _death.GetProvidingDeathSummary(fnaId);

                if (providingOnDeath == null)
                    return new();

                return providingOnDeath;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private ProvidingOnDreadDiseaseDto GetProvidingDread(int fnaId)
        {
            try
            {
                ProvidingOnDreadDiseaseRepo _dread = new ProvidingOnDreadDiseaseRepo(_context, _host, _config, _mapper);
                ProvidingOnDreadDiseaseDto providingOnDreadDisease = _dread.GetProvidingOnDreadDisease(fnaId);

                if (providingOnDreadDisease == null)
                    return new();

                return providingOnDreadDisease;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private List<ShortTermInsuranceDTO> GetShortTerm(int cliendId)
        {
            try
            {
                ShortTermInsuranceRepo _shortTerm = new ShortTermInsuranceRepo(_context, _host, _config, _mapper);
                List<ShortTermInsuranceDTO> shortTermInsurances = _shortTerm.GetSortTermInsurance(cliendId);

                if (shortTermInsurances == null)
                    return new();

                return shortTermInsurances;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private MedicalAidDTO GetMedical(int clientId)
        {
            try
            {
                MedicalAidRepo _medical = new MedicalAidRepo(_context, _host, _config, _mapper);
                MedicalAidDTO medicalAid = _medical.GetMedicalAid(clientId);

                if (medicalAid == null)
                    return new();

                return medicalAid;
            }
            catch (Exception)
            {
                return new();
            }
        }
               

        private List<InsuranceDto> GetInsurances(int fnaId)
        {
            try
            {
                InsuranceRepo _insurance = new InsuranceRepo(_context, _host, _config, _mapper);
                List<InsuranceDto> insurances = _insurance.GetInsurance(fnaId);

                if (insurances == null)
                    return new();

                return insurances;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private AssumptionsDto GetAssumptions(int fnaId)
        {
            try
            {
                AssumptionsRepo _assumptions = new AssumptionsRepo(_context, _host, _config, _mapper);
                AssumptionsDto assumptions = _assumptions.GetAssumptions(fnaId);

                if (assumptions == null)
                    return new();

                return assumptions;
            }
            catch (Exception)
            {
                return new();
            }
        }


        private async Task<List<DocumentListDto>> GetUserDocuments(int userId)
        {
            try
            {
                DocumentHelper _doc = new(_context, _config, _filestorage, _host);
                List<DocumentListDto> docs = await _doc.GetUserDocListAsync(userId);

                if (docs == null)
                    return new();

                return docs;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private async Task<List<DocumentListDto>> GetAppDocuments(int userId)
        {
            try
            {
                DocumentHelper _doc = new(_context, _config, _filestorage, _host);
                List<DocumentListDto> docs = await _doc.GetUserDocListAsync(userId);

                if (docs == null)
                    return new();

                return docs;
            }
            catch (Exception)
            {
                return new();
            }
        }

        private List<DocumentListDto> AddDocuments(List<DocumentListDto> addDocuments, List<DocumentListDto> currentDocuments)
        {
            if (currentDocuments == null)
                currentDocuments = new List<DocumentListDto>();

            if (addDocuments != null && addDocuments.Count > 0)
            {
                foreach (DocumentListDto doc in addDocuments)
                {
                    if (currentDocuments.FindAll(x => x.DocumentId == doc.DocumentId && x.UserId == doc.UserId && x.DocumentType == doc.DocumentType) == null ||
                        currentDocuments.FindAll(x => x.DocumentId == doc.DocumentId && x.UserId == doc.UserId && x.DocumentType == doc.DocumentType).Count == 0)
                        currentDocuments.Add(doc);
                }
            }

            return currentDocuments;
        }

        private List<ClientNotesDto> GetClientNotes(int clientId)
        {
            try
            {
                ICollection<ClientNotesModel> data = _context.ClientNotes.Where(c => c.ClientId == clientId).ToList();

                var notes = _mapper.Map<List<ClientNotesDto>>(data);

                foreach (var note in notes)
                {
                    note.dateCaptured = data.Where(n => n.Id == note.Id).First().Created;
                }

                return notes;
            }
            catch (Exception)
            {
                return new();
            }
        }

        public List<ClientNotesDto> CreateClientNote(List<ClientNotesDto> dtoArray)
        {

            foreach (var note in dtoArray)
            {
                try
                {                    
                    using (AlumaDBContext db = new())
                    {
                        var pModel = _mapper.Map<ClientNotesModel>(note);

                        if (_context.ClientNotes.Where(a => a.Id == pModel.Id).Any())
                        {
                            _context.Entry(pModel).State = EntityState.Modified;
                            if (_context.SaveChanges() > 0)
                            {
                                note.Status = "Success";
                                note.Message = "Client Notes Updated";
                            }
                        }
                        else
                        {
                            _context.ClientNotes.Add(pModel);
                            if (_context.SaveChanges() > 0)
                            {
                                note.Id = _mapper.Map<ClientNotesDto>(pModel).Id;
                                note.Status = "Success";
                                note.Message = "Client Note Created";
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    note.Status = "Server Error";
                    note.Message = ex.Message;
                }
            }

            return dtoArray;

        }

        public string DeleteClientNote(int Id)
        {
            try
            {
                using (AlumaDBContext db = new())
                {

                    ClientNotesModel note = _context.ClientNotes.Where(a => a.Id == Id).First();

                    _context.ClientNotes.Remove(note);

                    if (_context.SaveChanges() > 0)
                    {
                        return "Client Note Deleted Successfully";
                    }
                    else
                    {
                        return "Unsuccesful";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
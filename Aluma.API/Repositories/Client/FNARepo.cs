using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using FileStorageService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Aluma.API.Repositories
{
    public interface IFNARepo : IRepoBase<FNAModel>
    {
        void GenerateFNA(ClientModel user, AdvisorModel advisor, FNAModel fna);
        Task<ClientFNADto> CreateFNA(ClientFNADto dto);
        ClientFNADto GetClientFNA(int clientId);
    }

    public class FNARepo : RepoBase<FNAModel>, IFNARepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IFileStorageRepo _fileStorage;
        public FNARepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper, IFileStorageRepo fileStorage) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        public async Task<ClientFNADto> CreateFNA(ClientFNADto dto)
        {
            
                FNAModel newFna = _mapper.Map<FNAModel>(dto);
                _context.FNA.Add(newFna);
                _context.SaveChanges();
                dto = _mapper.Map<ClientFNADto>(newFna);

                return dto;
            
        }

        public void GenerateFNA(ClientModel user, AdvisorModel advisor, FNAModel fna)
        {
            //var data = new Dictionary<string, string>();


            //foreach (var prod in roa.SelectedProducts)
            //{
            //    if (prod.DeveationReason == null)
            //        data[$"prod{c}"] = prod.RecommendedLumpSum > 0 ?
            //            $"{prod.ProductName}: R {prod.RecommendedLumpSum}, R {prod.RecommendedRecurringPremium}" :
            //            $"{prod.ProductName}: R {prod.RecommendedLumpSum}";
            //    else
            //        data[$"prod{c}"] = prod.AcceptedRecurringPremium > 0 ?
            //            $"{prod.ProductName}: R {prod.AcceptedLumpSum}, R {prod.AcceptedRecurringPremium}" :
            //            $"{prod.ProductName}: R {prod.AcceptedLumpSum}";
            //}

            //data["reason"] = $"After discussions with the client, and according to the risk profile " +
            //    $"the above investment/s were decided and agreed on.";
            //data["signedAt"] = "Pretoria"; //advisor.BrokerDetails.City;
            //data["day"] = DateTime.Today.Day.ToString();
            //data["monthYear"] = $"{DateTime.Today.Month}-{DateTime.Today.Year}";
            //data["client"] = $"{firstName} {lastname}";
            //data["clientId"] = $"{idNumber}";
            //data["advisor"] = $"{advisor.FirstName} {advisor.LastName}";
            //data["advisorId"] = $"{advisor.IdNumber}";

            //byte[] doc = dh.PopulateDocument("RiskProfile.pdf", d, _host);

            //UserDocumentModel udm = new UserDocumentModel()
            //{
            //    DocumentType = DataService.Enum.DocumentTypesEnum.RiskProfile,
            //    FileType = DataService.Enum.FileTypesEnum.Pdf,
            //    Name = $"Aluma Capital Risk Profile - {user.FirstName + " " + user.LastName} .pdf",
            //    URL = "data:application/pdf;base64," + Convert.ToBase64String(doc, 0, doc.Length),
            //    UserId = user.Id
            //};

            //_context.UserDocuments.Add(udm);
            //_context.SaveChanges();
        }

        public ClientFNADto GetClientFNA(int clientId)
        {
            var fnaModel = _context.FNA.Where(r => r.ClientId == clientId);

            if (fnaModel.Any())
            {
                return _mapper.Map<ClientFNADto>(fnaModel.First());
            }
            return null;
        }
    }
}
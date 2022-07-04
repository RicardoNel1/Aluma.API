

using System;
using System.Collections.Generic;
using System.Linq;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Aluma.API.Repositories
{
    public interface IMedicalAidRepo : IRepoBase<MedicalAidModel>
    {
        MedicalAidDTO GetMedicalAid(int clientId);
        MedicalAidDTO UpdateMedicalAid(MedicalAidDTO dtoArray);

        bool DeleteMedicalAid(int id);
    }

    public class MedicalAidRepo : RepoBase<MedicalAidModel>, IMedicalAidRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public MedicalAidRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public MedicalAidDTO GetMedicalAid(int clientId)
        {
            MedicalAidModel data = _context.MedicalAid.Where(c => c.ClientId == clientId).FirstOrDefault();

            if (data != null)
                return _mapper.Map<MedicalAidDTO>(data);

            return new();
        }

        public MedicalAidDTO UpdateMedicalAid(MedicalAidDTO dto)
        {
            try
            {
                using (AlumaDBContext db = new())
                {
                    var pModel = _mapper.Map<MedicalAidModel>(dto);

                    if (_context.MedicalAid.Where(a => a.Id == pModel.Id).Any())
                    {
                        _context.Entry(pModel).State = EntityState.Modified;
                        if (_context.SaveChanges() > 0)
                        {
                            dto.Status = "Success";
                            dto.Message = "Medical Aid Updated";
                        }
                    }
                    else
                    {
                        _context.MedicalAid.Add(pModel);
                        if (_context.SaveChanges() > 0)
                        {
                            dto.Id = _mapper.Map<MedicalAidDTO>(pModel).Id;
                            dto.Status = "Success";
                            dto.Message = "Medical Aid Created";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                dto.Status = "Server Error";
                dto.Message = ex.Message;
            }

            return dto;
        }

        public bool DeleteMedicalAid(int id)
        {
            MedicalAidModel item = _context.MedicalAid.Where(a => a.Id == id).First();

            _context.MedicalAid.Remove(item);
            return _context.SaveChanges() > 0;

        }
    }
}
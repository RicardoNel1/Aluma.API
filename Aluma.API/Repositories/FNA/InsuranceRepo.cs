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
    public interface IInsuranceRepo : IRepoBase<InsuranceModel>
    {
        List<InsuranceDto> GetInsurance(int fnaId);
        List<InsuranceDto> UpdateInsurance(List<InsuranceDto> dtoArray);
        string DeleteInsurance(int Id);

    }

    public class InsuranceRepo : RepoBase<InsuranceModel>, IInsuranceRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public InsuranceRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }
               

        public List<InsuranceDto> GetInsurance(int fnaId)
        {
            List<InsuranceModel> data = _context.Insurance.Where(c => c.FNAId == fnaId).ToList();
            var insurance = _mapper.Map<List<InsuranceDto>>(data);

            return insurance;
            //ICollection<InsuranceModel> data = _context.Insurance.Where(c => c.FNAId == fnaId).ToList();
            //List<InsuranceDto> insurance = new();

            //foreach (var item in data)
            //{
            //    InsuranceDto insured = new()
            //    {
            //        Id = item.Id,
            //        FNAId = item.FNAId,
            //        Description = item.Description,
            //        Owner = item.Owner,
            //        Beneficiary = item.Beneficiary,
            //        LifeCover = item.LifeCover,
            //        Disability = item.Disability,
            //        DreadDisease = item.DreadDisease,
            //        AbsoluteIpPm = item.AbsoluteIpPm,
            //        ExtendedIpPm = item.ExtendedIpPm,
            //        AllocateTo = Enum.GetName(typeof(DataService.Enum.EstateAllocationEnum), item.AllocateTo)
            //    };

            //    insurance.Add(insured);

            //}

            //return insurance;
        }

        public List<InsuranceDto> UpdateInsurance(List<InsuranceDto> dtoArray)
        {

            foreach (var asset in dtoArray)
            {
                try
                {
                    using (AlumaDBContext db = new())
                    {
                        var pModel = _mapper.Map<InsuranceModel>(asset);
                        InsuranceModel originalModel = _context.Insurance.AsNoTracking().Where(a => a.Id == pModel.Id).FirstOrDefault();

                        if (_context.Insurance.Where(a => a.Id == pModel.Id).Any())
                        {
                            pModel.Created = originalModel.Created; //this keeps getting updated for some reason

                            // Compare the properties of the DTO and model to check for changes
                            if (                               
                                originalModel.Description != pModel.Description ||
                                originalModel.Owner != pModel.Owner ||
                                originalModel.Beneficiary != pModel.Beneficiary ||
                                originalModel.LifeCover != pModel.LifeCover ||
                                originalModel.Disability != pModel.Disability ||
                                originalModel.DreadDisease != pModel.DreadDisease ||
                                originalModel.AbsoluteIpPm != pModel.AbsoluteIpPm ||
                                originalModel.ExtendedIpPm != pModel.ExtendedIpPm

                                )
                            {

                                asset.Modified = DateTime.Now;
                                pModel.Modified = DateTime.Now;

                                if (pModel.DataSource != DataService.Enum.DataSourceEnum.Manual)
                                {
                                    asset.DataSource = "Manual";
                                    pModel.DataSource = DataService.Enum.DataSourceEnum.Manual;
                                }

                            }
                            else pModel.Modified = originalModel.Modified;


                            _context.Entry(pModel).State = EntityState.Modified;
                            if (_context.SaveChanges() > 0)
                            {
                                asset.Status = "Success";
                                asset.Message = "Asset Insurance Updated";
                            }
                        }
                        else
                        {
                            _context.Insurance.Add(pModel);
                            if (_context.SaveChanges() > 0)
                            {
                                asset.Id = _mapper.Map<InsuranceDto>(pModel).Id;
                                asset.Status = "Success";
                                asset.Message = "Asset Insurance Created";
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    asset.Status = "Server Error";
                    asset.Message = ex.Message;
                }
            }

            return dtoArray;

            //foreach (var item in dtoArray)
            //{

            //    bool existingItem = _context.Insurance.Where(a => a.Id == item.Id).Any();

            //    if (existingItem)
            //    {
            //        InsuranceModel updateItem = _context.Insurance.Where(a => a.Id == item.Id).FirstOrDefault();
            //        Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
            //        updateItem.Description = item.Description;
            //        updateItem.Owner = item.Owner;
            //        updateItem.LifeCover = item.LifeCover;
            //        updateItem.Disability = item.Disability;
            //        updateItem.DreadDisease = item.DreadDisease;
            //        updateItem.AbsoluteIpPm = item.AbsoluteIpPm;
            //        updateItem.ExtendedIpPm = item.ExtendedIpPm;
            //        updateItem.AllocateTo = parsedAllocation;

            //        _context.Insurance.Update(updateItem);

            //    }
            //    else
            //    {
            //        InsuranceModel newItem = new();

            //        Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
            //        newItem.FNAId = item.FNAId;
            //        newItem.Description = item.Description;
            //        newItem.Owner = item.Owner;
            //        newItem.LifeCover = item.LifeCover;
            //        newItem.Disability = item.Disability;
            //        newItem.DreadDisease = item.DreadDisease;
            //        newItem.AbsoluteIpPm = item.AbsoluteIpPm;
            //        newItem.ExtendedIpPm = item.ExtendedIpPm;
            //        newItem.AllocateTo = parsedAllocation;

            //        _context.Insurance.Add(newItem);

            //    }
            //}

            //_context.SaveChanges();
            //return null;

        }

        public string DeleteInsurance(int Id)
        {
            try
            {
                using (AlumaDBContext db = new())
                {

                    InsuranceModel item = _context.Insurance.Where(a => a.Id == Id).First();

                    _context.Insurance.Remove(item);

                    if (_context.SaveChanges() > 0)
                    {
                        return "Insurance Deleted Successfully";
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
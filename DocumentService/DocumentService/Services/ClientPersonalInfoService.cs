using AutoMapper;
using DataService.Dto.FNA.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DocumentService.Services
{
    public class ClientPersonalInfoService
    {
        public ClientPersonalInfoService()
        {

        }

        public PersonalDetailDto getUsers(int fnaId)
        {
            return getUsers_p(fnaId);
        }

        private PersonalDetailDto getUsers_p(int fnaId)
        {
            return null;
            //return _repo.User.GetUserByApplicationID(applicationId);
        }

        public string PopulatePersonalDetail(PersonalDetailDto dto)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-personal-details.html");

            string result = File.ReadAllText(path);

            result = result.Replace("[clientSurname]", "Tiago");
            return result;

        }
    }
}

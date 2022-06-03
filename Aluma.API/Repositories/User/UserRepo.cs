using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using StringHasher;
using DataService.Enum;
using Microsoft.EntityFrameworkCore;
using Aluma.API.Helpers;
using FileStorageService;
using Microsoft.IdentityModel.Tokens;

namespace Aluma.API.Repositories
{
    public interface IUserRepo : IRepoBase<UserModel>
    {
        //Task<string> SendForgotPasswordMail(UserMail user);
        public bool DoesUserExist(UserDto dto);
        public bool DoesUserExist(RegistrationDto dto);
        public UserDto GetUser(UserDto dto);
        public UserDto GetUser(LoginDto dto);

        //public string CreateOTP(UserModel user, OtpTypesEnum otpType, Guid applicationID = new Guid());
        bool DoesUserNameExist(LoginDto dto);

        public bool ValidateID(string idNumber);

        bool IsRegistrationVerified(LoginDto dto);

        bool IsSocialLoginVerified(LoginDto dto);

        bool IsPasswordVerified(LoginDto dto);

        bool IsUserAdvisor(LoginDto dto);

        public UserDto CreateClientUser(RegistrationDto dto);
        public UserDto CreateAdvisorUser(RegistrationDto dto);

        bool DoesAddressExist(AddressDto dto);

        public AddressDto GetUserAddress(int userId, string type);
        public AddressDto CreateUserAddress(AddressDto dto);
        public AddressDto UpdateUserAddress(AddressDto dto);

        //string GetUserSignature(int userId);
        public UserDto GetUserSignature(int userId);

        public UserDto EditUserSignature(UserDto dto);//(UserDto dto);

        void ResetPassword(int userId, string password);

        void ForgotPassword(LoginDto dto);
        void VerifyUser(UserDto user);
        UserDto GetUserByApplicationID(int applicationId);
        int DecryptUserId(string userId);
    }

    public class UserRepo : RepoBase<UserModel>, IUserRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IFileStorageRepo _fileStorage;
        private readonly IMapper _mapper;
        MailSender _ms;
        public UserRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IFileStorageRepo filestorage, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _fileStorage = filestorage;
            _mapper = mapper;
            _ms = new MailSender(_context, _config, _fileStorage, _host);
        }

        public bool DoesUserExist(UserDto dto)
        {
            bool exists = _context.Users.Where(c => (c.Email == dto.Email || c.RSAIdNumber == dto.RSAIdNumber || c.MobileNumber == dto.MobileNumber)).Any();

            return exists;
        }

        public bool ValidateID(string idNumber)
        {
            //check for valid ID length
            if (idNumber.Length != 13)

            { return false; }

            //check if first six digits return a valid date
            if (!DateTime.TryParseExact(idNumber.Substring(0, 6), "yyMMdd", System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None, out DateTime result))

            { return false; }

            //check if 11th is a 0 or 1
            if (System.Int32.Parse(idNumber.Substring(10, 1)) > 1)

            { return false; }

            //Luhn algorithm
            string oddString = "";
            string evenString = "";
            int doubleEven;

            for (int i = 0; i < 12; i += 2)
            {
                oddString += idNumber[i];
            }

            char[] oddArray = oddString.ToCharArray();
            int[] oddInt = Array.ConvertAll(oddArray, c => (int)char.GetNumericValue(c));
            int sumOdd = oddInt.Sum();

            for (int i = 1; i < 12; i += 2)
            {
                evenString += idNumber[i];
            }
            doubleEven = int.Parse(evenString) * 2;

            char[] doubleEvenArray = doubleEven.ToString().ToCharArray();
            int[] doubleEvenInt = Array.ConvertAll(doubleEvenArray, c => (int)char.GetNumericValue(c));
            int sumDoubleEven = doubleEvenInt.Sum();
            int sumResults = sumOdd + sumDoubleEven;
            int check = 10 - sumResults % 10;

            if (check == 10)
            {
                check = 0;
            }

            //last digit of ID should match Luhn algorithm result
            if (idNumber.Substring(12, 1) != check.ToString())
            { return false; }
            else
                return true;
        }

        public UserDto GetUser(UserDto dto)
        {
            //if (DoesUserExist(dto))
            //{
                var user = _context.Users.Where(u => (u.Id == dto.Id)).FirstOrDefault();
                return _mapper.Map<UserDto>(user);
            //}

            //return null;
        }

        public UserDto GetUser(LoginDto dto)
        {
            if (DoesUserNameExist(dto))
            {
                var user = _context.Users.Where(u => (u.Email == dto.UserName)).FirstOrDefault();
                return _mapper.Map<UserDto>(user);
            }

            return null;
        }

        public UserModel GetUser(RegistrationDto dto)
        {
            if (DoesUserExist(dto))
            {
                var user = _context.Users.Where(u => (u.Email == dto.Email || u.MobileNumber == dto.MobileNumber)).FirstOrDefault();
                return user;
            }

            return null;
        }

        //public string GetUserSignature(int userId)               
        //{
        //    byte[] signature = _context.Users.Where(u => u.Id == userId).First().Signature;

        //    return Convert.ToBase64String(signature);

        //}

        public UserDto GetUserSignature(int userId)
        {
            UserModel user = _context.Users.Where(a => a.Id == userId).First();

            //byte[] signature = _context.Users.Where(u => u.Id == userId).First().Signature;

            return _mapper.Map<UserDto>(user);  //Convert.ToBase64String(signature);

        }

        //public bool EditUserSignature(int userId, string signature)
        //{
        //    UserModel user = _context.Users.Where(a => a.Id == userId).FirstOrDefault();

        //    byte[] newSignature = Encoding.ASCII.GetBytes(signature);

        //    //UserModel user = _mapper.Map<UserModel>(dto);
        //    user.Signature = newSignature;            
        //    _context.Users.Update(user);
        //    _context.SaveChanges();

        //    return true;
        //}

        public UserDto EditUserSignature(UserDto dto)
        {
            UserModel user = _context.Users.Where(a => a.Id == dto.Id).FirstOrDefault();

            //byte[] newSignature = Encoding.ASCII.GetBytes(dto.Signature);
            byte[] newSignature = dto.Signature;

            //UserModel user = _mapper.Map<UserModel>(dto);
            user.Signature = newSignature;
            _context.Users.Update(user);
            _context.SaveChanges();

            return dto;
        }

        public bool DoesUserNameExist(LoginDto dto)
        {
            bool exists = false;
            if (dto.UserId > 0)
            {
                exists = _context.Users.Where(c => (c.Id == dto.UserId)).Any();

            }
            else
            {
                exists = _context.Users.Where(c => (c.Email == dto.UserName)).Any();

            }

            return exists;
        }

        public bool IsRegistrationVerified(LoginDto dto)
        {
            bool exists = _context.Users.Where(c => c.Email == dto.UserName && c.isRegistrationVerified == true && c.RegistrationVerifiedDate > DateTime.MinValue).Any();

            return exists;
        }

        public bool IsSocialLoginVerified(LoginDto dto)
        {
            bool result = _context.Users.Where(u => u.Email == dto.UserName && u.isSocialLogin == true && u.SocialId == dto.SocialId).Any();

            return result;
        }

        public bool IsPasswordVerified(LoginDto dto)
        {
            StringHasherRepo str = new();
            UserModel user = _context.Users.Where(c => c.Email == dto.UserName).First();
            bool match = str.ValidateHash(user.Password, dto.Password);

            return match;
        }

        public bool IsUserAdvisor(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        public bool DoesUserExist(RegistrationDto dto)
        {

            bool exists = _context.Users.Where(c => (c.Email == dto.Email || c.MobileNumber == dto.MobileNumber)).Any();

            return exists;

            throw new NotImplementedException();
        }

        public UserDto CreateClientUser(RegistrationDto dto)
        {
            //create user            
            StringHasherRepo str = new();

            //Create User
            UserModel user = _mapper.Map<UserModel>(dto);

            if (dto.SocialId != null)
            {
                user.isSocialLogin = true;
                user.isRegistrationVerified = true;
                user.RegistrationVerifiedDate = DateTime.UtcNow;
            }

            user.Role = RoleEnum.Client;
            user.Password = dto.Password != null ? str.CreateHash(dto.Password) : null;
            _context.Users.Add(user);
            _context.SaveChanges();

            return _mapper.Map<UserDto>(user);
        }
        public UserDto CreateAdvisorUser(RegistrationDto dto)
        {
            StringHasherRepo str = new();

            //Create User
            UserModel user = _mapper.Map<UserModel>(dto);
            user.Role = RoleEnum.Advisor;
            user.Password = str.CreateHash(dto.Password);
            _context.Users.Add(user);
            _context.SaveChanges();

            return _mapper.Map<UserDto>(user);
        }

        //Address
        public bool DoesAddressExist(AddressDto dto)
        {
            bool addressExist = false;
            Enum.TryParse(dto.Type, true, out AddressTypesEnum parsedType);

            addressExist = _context.Address.Where(a => a.UserId == dto.UserId && a.Type == parsedType).Any();
            return addressExist;
        }
        public AddressDto CreateUserAddress(AddressDto dto)
        {
            AddressModel details = _mapper.Map<AddressModel>(dto);

            _context.Address.Add(details);
            _context.SaveChanges();

            dto = _mapper.Map<AddressDto>(details);

            return dto;
        }

        public AddressDto GetUserAddress(int userId, string type)

        {
            Enum.TryParse(type, true, out AddressTypesEnum parsedType);
            AddressModel addressModel = _context.Address.Where(a => a.UserId == userId && a.Type == parsedType).First();

            return _mapper.Map<AddressDto>(addressModel);

        }

        public AddressDto UpdateUserAddress(AddressDto dto)
        {
            Enum.TryParse(dto.Type, true, out AddressTypesEnum parsedType);
            AddressModel details = _context.Address.Where(a => a.UserId == dto.UserId && a.Type == parsedType).FirstOrDefault();

            //set fields to be updated
            details.UnitNumber = dto.UnitNumber;
            details.ComplexName = dto.ComplexName;
            details.StreetNumber = dto.StreetNumber;
            details.StreetName = dto.StreetName;
            details.Suburb = dto.Suburb;
            details.City = dto.City;
            details.PostalCode = dto.PostalCode;
            details.Country = dto.Country;
            details.Type = parsedType;
            details.InCareAddress = dto.InCareAddress;
            details.InCareName = dto.InCareName;
            details.YearsAtAddress = dto.YearsAtAddress;
            details.AddressSameAs = dto.AddressSameAs;

            _context.Address.Update(details);
            _context.SaveChanges();
            dto = _mapper.Map<AddressDto>(dto);
            return dto;
        }

        public void ResetPassword(int userId, string password)
        {
            StringHasherRepo str = new();
            UserModel user = _context.Users.Where(u => u.Id == userId).First();

            user.Password = str.CreateHash(password);

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void ForgotPassword(LoginDto dto)
        {
            UserModel user = _context.Users.Where(u => u.Email == dto.UserName).First();
            _ms.SendForgotPasswordMail(user);

        }

        public void VerifyUser(UserDto user)
        {
            UserModel um = _context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            if (!um.isRegistrationVerified)
            {
                um.isRegistrationVerified = true;
                um.RegistrationVerifiedDate = DateTime.UtcNow;

                _context.Users.Update(um);
                _context.SaveChanges();
            }

        }

        public UserDto GetUserByApplicationID(int applicationId)
        {
            ApplicationModel app = _context.Applications.Include(a => a.Client).ThenInclude(c => c.User).SingleOrDefault(a => a.Id == applicationId);
            UserDto result = _mapper.Map<UserDto>(app.Client.User);
            return result;

        }

        public int DecryptUserId(string userId)
        {
            string key = _config.GetSection("SystemSettings").Get<SystemSettingsDto>().EncryptionKey;
            string decryption = UtilityHelper.DecryptString(key, Base64UrlEncoder.Decode(userId));
            return Int32.Parse(decryption);
        }
    }
}
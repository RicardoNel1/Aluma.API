using DataService.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StringHasher;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("address")]
    public class AddressModel : BaseModel
    {
        public UserModel User { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UnitNumber { get; set; }
        public string ComplexName { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public AddressTypesEnum Type { get; set; }

        public bool InCareAddress { get; set; }
        public string InCareName { get; set; }
        public int YearsAtAddress { get; set; }
        public bool AddressSameAs { get; set; }
    }

    public class AddressModelBuilder : IEntityTypeConfiguration<AddressModel>
    {
        private IStringHasher _hasher = new StringHasherRepo();

        public void Configure(EntityTypeBuilder<AddressModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasData(new AddressModel()
            {
                Id = 1,
                UnitNumber = null,
                ComplexName = "FinTech Campus", 
                StreetNumber = null,
                StreetName = "Cnr Illanga and Botterklapper",
                Suburb = "The Willows", 
                City = "Pretoria", 
                PostalCode = "0081",
                Country = "South Africa", 
                Type = AddressTypesEnum.Residential, 
                UserId = 1,
                YearsAtAddress = 1
            });
            mb.HasData(new AddressModel()
            {
                Id = 2,
                UnitNumber = null,
                ComplexName = "Postnet Suite 33",
                StreetNumber = null,
                StreetName = "Private Bag X 26",
                Suburb = "Sunninghill",
                City = "Johannesburg",
                PostalCode = "2157",
                Country = "South Africa",
                Type = AddressTypesEnum.Postal,
                UserId = 1,
            });
            //mb.HasData(new UserModel()
            //{
            //    Id = Guid.Parse("9a5db3e7-7ec6-4e30-8384-20a20046658e"),
            //    Email = "uat@aluma.co.za",
            //    FirstName = "UAT",
            //    RSAIdNumber = "9012245555088",
            //    LastName = "Tester",
            //    MobileNumber = "843334444",
            //    isRegistrationVerified = true,
            //    Role = Roles.Admin,
            //    Password = _hasher.CreateHash("AlumaUAT"),
            //});
        }
    }
}
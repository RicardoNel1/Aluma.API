using DataService.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StringHasher;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("users")]
    public class UserModel : BaseModel
    {
        public ICollection<OtpModel> Otp { get; set; }
        public ICollection<UserDocumentModel> Documents { get; set; }
        public ICollection<AddressModel> Addresses { get; set; }

        public AdvisorModel Advisor { get; set; }
        public ClientModel Client { get; set; }

        public UserModel()
        {
            isRegistrationVerified = false;
            isSocialLogin = false;
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(60), MinLength(4)]
        public string FirstName { get; set; }

        [Required, StringLength(60), MinLength(4)]
        public string LastName { get; set; }
                
        public string RSAIdNumber { get; set; }

        public string DateOfBirth { get; set; }

        [Required, StringLength(60), MinLength(6), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string MobileNumber { get; set; }

        [Required]
        public string Password { get; set; }


        [Required]
        public RoleEnum Role { get; set; }

        public byte[] Signature { get; set; } 
        public string ProfileImage { get; set; }

        public bool isRegistrationVerified { get; set; }
        public DateTime RegistrationVerifiedDate { get; set; }
        public bool isSocialLogin { get; set; }
        public string SocialId { get; set; }
        public bool isOtpLocked { get; set; }

    }

    public class UserModelBuilder : IEntityTypeConfiguration<UserModel>
    {
        private IStringHasher _hasher = new StringHasherRepo();

        public void Configure(EntityTypeBuilder<UserModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.RSAIdNumber).IsUnique();
            //mb.HasIndex(c => c.Email).IsUnique();
            //mb.HasIndex(c => c.MobileNumber).IsUnique();
            mb.HasOne(c => c.Advisor)
                .WithOne(c => c.User)
                .HasForeignKey<AdvisorModel>(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            mb.HasOne(c => c.Client)
                .WithOne(c => c.User)
                .HasForeignKey<ClientModel>(c => c.UserId)                
                .OnDelete(DeleteBehavior.NoAction);

            mb.HasMany(c => c.Otp)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                //.IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            mb.HasMany(c => c.Documents)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            mb.HasMany(c => c.Addresses)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            //mb.HasData(new UserModel()
            //{
            //    Id = Guid.Parse("9a5db3e7-7ec6-4e30-8384-20a20046658e"),
            //    Email = "stefan@aluma.co.za",
            //    FirstName = "Stefan",
            //    RSAIdNumber = "8804225100087",
            //    LastName = "Griesel",
            //    MobileNumber = "842403357",
            //    isRegistrationVerified = true,
            //    Role = RoleEnum.Admin,
            //    Password = _hasher.CreateHash("AlumaStefan"),
            //});
            mb.HasData(new UserModel()
            {
                Id = 1,
                Email = "dev@aluma.co.za",
                FirstName = "Dev",
                RSAIdNumber = "9012245555088",
                LastName = "Tester",
                MobileNumber = "843334444",
                isRegistrationVerified = true,
                Role = RoleEnum.Admin,
                Password = _hasher.CreateHash("AlumaDev"),
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

    public class UserMail
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Url { get; set; }
        public string UrlToken { get; set; }
        public Guid UserId { get; set; }
        public string Template { get; set; }
    }
}
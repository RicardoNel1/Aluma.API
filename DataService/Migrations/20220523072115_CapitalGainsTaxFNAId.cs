using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class CapitalGainsTaxFNAId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "capital_gains_tax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FnaId = table.Column<int>(type: "int", nullable: false),
                    PreviousCapitalLosses = table.Column<double>(type: "float", nullable: false),
                    TotalCGTPayable = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_capital_gains_tax", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fna_tax_lumpsum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FnaId = table.Column<int>(type: "int", nullable: false),
                    PreviouslyDisallowed = table.Column<double>(type: "float", nullable: false),
                    RetirementReceived = table.Column<double>(type: "float", nullable: false),
                    WithdrawalReceived = table.Column<double>(type: "float", nullable: false),
                    SeverenceReceived = table.Column<double>(type: "float", nullable: false),
                    TaxPayable = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_tax_lumpsum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Institute = table.Column<string>(type: "varchar(100)", nullable: true),
                    ProductType = table.Column<int>(type: "int", nullable: false),
                    ProductCategory = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AssociatedRisk = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", nullable: true),
                    MaidenName = table.Column<string>(type: "varchar(100)", nullable: true),
                    RSAIdNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    DateOfBirth = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    MobileNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    Password = table.Column<string>(type: "varchar(100)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Signature = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ProfileImage = table.Column<string>(type: "varchar(100)", nullable: true),
                    isRegistrationVerified = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationVerifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isSocialLogin = table.Column<bool>(type: "bit", nullable: false),
                    SocialId = table.Column<string>(type: "varchar(100)", nullable: true),
                    isOtpLocked = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "advisors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", nullable: true),
                    BusinessTel = table.Column<string>(type: "varchar(100)", nullable: true),
                    HomeTel = table.Column<string>(type: "varchar(100)", nullable: true),
                    Fax = table.Column<string>(type: "varchar(100)", nullable: true),
                    AdviceLTSubCatA = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedLTSubCatA = table.Column<bool>(type: "bit", nullable: false),
                    AdviceLTSubCatB1 = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedLTSubCatB1 = table.Column<bool>(type: "bit", nullable: false),
                    AdviceLTSubCatB1A = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedLTSubCatB1A = table.Column<bool>(type: "bit", nullable: false),
                    AdviceLTSubCatB2 = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedLTSubCatB2 = table.Column<bool>(type: "bit", nullable: false),
                    AdviceLTSubCatB2A = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedLTSubCatB2A = table.Column<bool>(type: "bit", nullable: false),
                    AdviceLTSubCatC = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedLTSubCatC = table.Column<bool>(type: "bit", nullable: false),
                    AdviceSTPersonal = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedSTPersonal = table.Column<bool>(type: "bit", nullable: false),
                    AdviceSTPersonalA1 = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedSTPersonalA1 = table.Column<bool>(type: "bit", nullable: false),
                    AdviceSTCommercial = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedSTCommercial = table.Column<bool>(type: "bit", nullable: false),
                    AdviceLTDeposits = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedLTDeposits = table.Column<bool>(type: "bit", nullable: false),
                    AdviceSTDeposits = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedSTDeposits = table.Column<bool>(type: "bit", nullable: false),
                    AdviceStructuredDeposits = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedStructuredDeposits = table.Column<bool>(type: "bit", nullable: false),
                    AdviceRetailPension = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedRetailPension = table.Column<bool>(type: "bit", nullable: false),
                    AdvicePensionFunds = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedPensionFunds = table.Column<bool>(type: "bit", nullable: false),
                    AdviceShares = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedShares = table.Column<bool>(type: "bit", nullable: false),
                    AdviceMoneyMarket = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedMoneyMarket = table.Column<bool>(type: "bit", nullable: false),
                    AdviceDebentures = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedDebentures = table.Column<bool>(type: "bit", nullable: false),
                    AdviceWarrants = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedWarrants = table.Column<bool>(type: "bit", nullable: false),
                    AdviceBonds = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedBonds = table.Column<bool>(type: "bit", nullable: false),
                    AdviceDerivatives = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedDerivatives = table.Column<bool>(type: "bit", nullable: false),
                    AdviceParticipatoryInterestCollective = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedParticipatoryInterestCollective = table.Column<bool>(type: "bit", nullable: false),
                    AdviceSecurities = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedSecurities = table.Column<bool>(type: "bit", nullable: false),
                    AdviceParticipatoryInterestHedge = table.Column<bool>(type: "bit", nullable: false),
                    SupervisedParticipatoryInterestHedge = table.Column<bool>(type: "bit", nullable: false),
                    isExternalBroker = table.Column<bool>(type: "bit", nullable: false),
                    isSupervised = table.Column<bool>(type: "bit", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_advisors_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "user_addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UnitNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    ComplexName = table.Column<string>(type: "varchar(100)", nullable: true),
                    StreetNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    StreetName = table.Column<string>(type: "varchar(100)", nullable: true),
                    Suburb = table.Column<string>(type: "varchar(100)", nullable: true),
                    City = table.Column<string>(type: "varchar(100)", nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(100)", nullable: true),
                    Country = table.Column<string>(type: "varchar(100)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    InCareAddress = table.Column<bool>(type: "bit", nullable: false),
                    InCareName = table.Column<string>(type: "varchar(100)", nullable: true),
                    YearsAtAddress = table.Column<int>(type: "int", nullable: false),
                    AddressSameAs = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_addresses_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    URL = table.Column<string>(type: "varchar(100)", nullable: true),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    IsSigned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_documents_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_otp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OtpType = table.Column<int>(type: "int", nullable: false),
                    Otp = table.Column<string>(type: "varchar(100)", nullable: true),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    isValidated = table.Column<bool>(type: "bit", nullable: false),
                    isExpired = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_otp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_otp_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AdvisorId = table.Column<int>(type: "int", nullable: true),
                    LeadId = table.Column<int>(type: "int", nullable: true),
                    ClientType = table.Column<string>(type: "varchar(100)", nullable: true),
                    Title = table.Column<string>(type: "varchar(100)", nullable: true),
                    CountryOfResidence = table.Column<string>(type: "varchar(100)", nullable: true),
                    CountryOfBirth = table.Column<string>(type: "varchar(100)", nullable: true),
                    CityOfBirth = table.Column<string>(type: "varchar(100)", nullable: true),
                    Nationality = table.Column<string>(type: "varchar(100)", nullable: true),
                    EmploymentDetailsId = table.Column<int>(type: "int", nullable: false),
                    MaritalDetailsId = table.Column<int>(type: "int", nullable: false),
                    NonResidentialAccount = table.Column<bool>(type: "bit", nullable: false),
                    isSmoker = table.Column<bool>(type: "bit", nullable: false),
                    Education = table.Column<string>(type: "varchar(100)", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_clients_advisors_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "advisors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_clients_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "advisor_disclosures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvisorId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    AuthorisedUsers = table.Column<string>(type: "varchar(100)", nullable: true),
                    BrokerAppointment = table.Column<bool>(type: "bit", nullable: false),
                    AdvisorAuthority = table.Column<bool>(type: "bit", nullable: false),
                    AdvisorAuthorityProducts = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advisor_disclosures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_advisor_disclosures_advisors_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "advisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_advisor_disclosures_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvisorId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ApplicationType = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ApplicationStatus = table.Column<int>(type: "int", nullable: false),
                    DocumentsCreated = table.Column<bool>(type: "bit", nullable: false),
                    SignatureConsent = table.Column<bool>(type: "bit", nullable: false),
                    SignatureConsentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentsSigned = table.Column<bool>(type: "bit", nullable: false),
                    BdaNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    PaymentConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_applications_advisors_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "advisors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_applications_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_bank_details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    JobID = table.Column<string>(type: "varchar(100)", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    SearchData = table.Column<string>(type: "varchar(100)", nullable: true),
                    Reference = table.Column<string>(type: "varchar(100)", nullable: true),
                    BankName = table.Column<string>(type: "varchar(100)", nullable: true),
                    AccountType = table.Column<string>(type: "varchar(100)", nullable: true),
                    VerificationType = table.Column<string>(type: "varchar(100)", nullable: true),
                    BranchCode = table.Column<string>(type: "varchar(100)", nullable: true),
                    AccountNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    AccountId = table.Column<string>(type: "varchar(100)", nullable: true),
                    IdNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    Initials = table.Column<string>(type: "varchar(100)", nullable: true),
                    InitialsMatch = table.Column<string>(type: "varchar(100)", nullable: true),
                    Surname = table.Column<string>(type: "varchar(100)", nullable: true),
                    FoundAtBank = table.Column<string>(type: "varchar(100)", nullable: true),
                    AccOpen = table.Column<string>(type: "varchar(100)", nullable: true),
                    OlderThan3Months = table.Column<string>(type: "varchar(100)", nullable: true),
                    TypeCorrect = table.Column<string>(type: "varchar(100)", nullable: true),
                    IdNumberMatch = table.Column<string>(type: "varchar(100)", nullable: true),
                    NamesMatch = table.Column<string>(type: "varchar(100)", nullable: true),
                    AcceptDebits = table.Column<string>(type: "varchar(100)", nullable: true),
                    AcceptCredits = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_bank_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_bank_details_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_consumer_protection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    InformProducts = table.Column<bool>(type: "bit", nullable: false),
                    InformOffers = table.Column<bool>(type: "bit", nullable: false),
                    RequestResearch = table.Column<bool>(type: "bit", nullable: false),
                    PreferredComm = table.Column<int>(type: "int", nullable: false),
                    OtherCommMethods = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_consumer_protection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_consumer_protection_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_employment_details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    EmploymentStatus = table.Column<string>(type: "varchar(100)", nullable: true),
                    Employer = table.Column<string>(type: "varchar(100)", nullable: true),
                    Industry = table.Column<string>(type: "varchar(100)", nullable: true),
                    Occupation = table.Column<string>(type: "varchar(100)", nullable: true),
                    WorkNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_employment_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_employment_details_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_fna",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    AdvisorId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_fna", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_fna_advisors_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "advisors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_client_fna_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "client_kyc_data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    FactoryId = table.Column<int>(type: "int", nullable: false),
                    IdNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    FirstNames = table.Column<string>(type: "varchar(100)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(100)", nullable: true),
                    DateOfBrith = table.Column<string>(type: "varchar(100)", nullable: true),
                    Age = table.Column<string>(type: "varchar(100)", nullable: true),
                    Gender = table.Column<string>(type: "varchar(100)", nullable: true),
                    Citizenship = table.Column<string>(type: "varchar(100)", nullable: true),
                    DeceasedStatus = table.Column<string>(type: "varchar(100)", nullable: true),
                    CellNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    Status = table.Column<string>(type: "varchar(100)", nullable: true),
                    KycMethod = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_kyc_data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_kyc_data_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_lead",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    AdvisorId = table.Column<int>(type: "int", nullable: false),
                    CRMId = table.Column<int>(type: "int", nullable: false),
                    LeadType = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_lead", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_lead_advisors_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "advisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_client_lead_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_mandate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    DiscretionType = table.Column<string>(type: "varchar(100)", nullable: true),
                    InvestmentObjective = table.Column<string>(type: "varchar(100)", nullable: true),
                    LimitedInstruction = table.Column<string>(type: "varchar(100)", nullable: true),
                    VoteInstruction = table.Column<string>(type: "varchar(100)", nullable: true),
                    PortfolioManagementFee = table.Column<string>(type: "varchar(100)", nullable: true),
                    InitialFee = table.Column<string>(type: "varchar(100)", nullable: true),
                    AdditionalAdvisorFee = table.Column<string>(type: "varchar(100)", nullable: true),
                    ForeignInvestmentInitialFee = table.Column<string>(type: "varchar(100)", nullable: true),
                    ForeignInvestmentAnnualFee = table.Column<string>(type: "varchar(100)", nullable: true),
                    AdminFee = table.Column<string>(type: "varchar(100)", nullable: true),
                    DividendInstruction = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_mandate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_mandate_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_marital_details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    MaritalStatus = table.Column<string>(type: "varchar(100)", nullable: true),
                    FullName = table.Column<string>(type: "varchar(100)", nullable: true),
                    MaidenName = table.Column<string>(type: "varchar(100)", nullable: true),
                    DateOfMarriage = table.Column<string>(type: "varchar(100)", nullable: true),
                    ForeignMarriage = table.Column<bool>(type: "bit", nullable: false),
                    CountryOfMarriage = table.Column<string>(type: "varchar(100)", nullable: true),
                    SpouseDateOfBirth = table.Column<string>(type: "varchar(100)", nullable: true),
                    PowerOfAttorney = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_marital_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_marital_details_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_passports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CountryOfIssue = table.Column<string>(type: "varchar(100)", nullable: true),
                    PassportNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_passports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_passports_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_products_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_client_products_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_risk_profile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Goal = table.Column<string>(type: "varchar(100)", nullable: true),
                    LibertyRequiredRisk = table.Column<string>(type: "varchar(100)", nullable: true),
                    LibertyInvestmentTerm = table.Column<string>(type: "varchar(100)", nullable: true),
                    LibertyRiskTolerance = table.Column<string>(type: "varchar(100)", nullable: true),
                    LibertyRiskCapacity = table.Column<string>(type: "varchar(100)", nullable: true),
                    RiskAge = table.Column<string>(type: "varchar(100)", nullable: true),
                    RiskTerm = table.Column<string>(type: "varchar(100)", nullable: true),
                    RiskInflation = table.Column<string>(type: "varchar(100)", nullable: true),
                    RiskReaction = table.Column<string>(type: "varchar(100)", nullable: true),
                    RiskExample = table.Column<string>(type: "varchar(100)", nullable: true),
                    DerivedProfile = table.Column<string>(type: "varchar(100)", nullable: true),
                    AgreeWithOutcome = table.Column<bool>(type: "bit", nullable: false),
                    DisagreeReason = table.Column<string>(type: "varchar(100)", nullable: true),
                    AdvisorNotes = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_risk_profile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_risk_profile_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_tax_residency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    TaxNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    TaxObligations = table.Column<bool>(type: "bit", nullable: false),
                    UsCitizen = table.Column<bool>(type: "bit", nullable: false),
                    UsRelinquished = table.Column<bool>(type: "bit", nullable: false),
                    UsOther = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_tax_residency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_tax_residency_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    URL = table.Column<string>(type: "varchar(100)", nullable: true),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    IsSigned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_application_documents_applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_purpose_and_funding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    fundsEmployedSalary = table.Column<bool>(type: "bit", nullable: false),
                    fundsEmployedCommission = table.Column<bool>(type: "bit", nullable: false),
                    fundsEmployedBonus = table.Column<bool>(type: "bit", nullable: false),
                    fundsSelfTurnover = table.Column<bool>(type: "bit", nullable: false),
                    fundsRetiredAnnuity = table.Column<bool>(type: "bit", nullable: false),
                    fundsRetiredOnceOff = table.Column<bool>(type: "bit", nullable: false),
                    fundsDirectorSalary = table.Column<bool>(type: "bit", nullable: false),
                    fundsDirectorDividend = table.Column<bool>(type: "bit", nullable: false),
                    fundsDirectorInterest = table.Column<bool>(type: "bit", nullable: false),
                    fundsDirectorBonus = table.Column<bool>(type: "bit", nullable: false),
                    fundsOther = table.Column<string>(type: "varchar(100)", nullable: true),
                    wealthIncome = table.Column<bool>(type: "bit", nullable: false),
                    wealthInvestments = table.Column<bool>(type: "bit", nullable: false),
                    wealthShares = table.Column<bool>(type: "bit", nullable: false),
                    wealthProperty = table.Column<bool>(type: "bit", nullable: false),
                    wealthCompany = table.Column<bool>(type: "bit", nullable: false),
                    wealthInheritance = table.Column<bool>(type: "bit", nullable: false),
                    wealthLoan = table.Column<bool>(type: "bit", nullable: false),
                    wealthGift = table.Column<bool>(type: "bit", nullable: false),
                    wealthOther = table.Column<string>(type: "varchar(100)", nullable: true),
                    InvestmentGoal = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_purpose_and_funding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_application_purpose_and_funding_applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_roa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    AdvisorId = table.Column<int>(type: "int", nullable: false),
                    BdaNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    Introduction = table.Column<string>(type: "varchar(100)", nullable: true),
                    MaterialInformation = table.Column<string>(type: "varchar(100)", nullable: true),
                    Replacement_A = table.Column<bool>(type: "bit", nullable: false),
                    Replacement_B = table.Column<bool>(type: "bit", nullable: false),
                    Replacement_C = table.Column<bool>(type: "bit", nullable: false),
                    Replacement_D = table.Column<bool>(type: "bit", nullable: false),
                    ReplacementReason = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_roa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_application_roa_advisors_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "advisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_application_roa_applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_accrual",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    ClientAssetsCommencement = table.Column<double>(type: "float", nullable: false),
                    SpouseAssetsCommencement = table.Column<double>(type: "float", nullable: false),
                    ClientEstateCurrent = table.Column<double>(type: "float", nullable: false),
                    SpouseEstateCurrent = table.Column<double>(type: "float", nullable: false),
                    ClientLiabilities = table.Column<double>(type: "float", nullable: false),
                    SpouseLiabilities = table.Column<double>(type: "float", nullable: false),
                    ClientExcludedValue = table.Column<double>(type: "float", nullable: false),
                    SpouseExcludedValue = table.Column<double>(type: "float", nullable: false),
                    Offset = table.Column<double>(type: "float", nullable: false),
                    AllocateTo = table.Column<int>(type: "int", nullable: false),
                    TotalAsAsset = table.Column<double>(type: "float", nullable: false),
                    TotalAsLiability = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_accrual", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_accrual_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_administration_costs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    OtherConveyanceCosts = table.Column<double>(type: "float", nullable: false),
                    AdvertisingCosts = table.Column<double>(type: "float", nullable: false),
                    RatesAndTaxes = table.Column<double>(type: "float", nullable: false),
                    OtherAdminDescription = table.Column<string>(type: "varchar(100)", nullable: true),
                    OtherAdminCosts = table.Column<double>(type: "float", nullable: false),
                    TotalEstimatedCosts = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_administration_costs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_administration_costs_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_assets_attracting_cgt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    RecurringPremium = table.Column<double>(type: "float", nullable: false),
                    EscPercent = table.Column<double>(type: "float", nullable: false),
                    Growth = table.Column<double>(type: "float", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    AllocateTo = table.Column<int>(type: "int", nullable: false),
                    BaseCost = table.Column<double>(type: "float", nullable: false),
                    DisposedAtRetirement = table.Column<bool>(type: "bit", nullable: false),
                    DisposedOnDisability = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_assets_attracting_cgt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_assets_attracting_cgt_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_assets_exempt_from_cgt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Growth = table.Column<double>(type: "float", nullable: false),
                    DisposedAtRetirement = table.Column<bool>(type: "bit", nullable: false),
                    DisposedOnDisability = table.Column<bool>(type: "bit", nullable: false),
                    AllocateTo = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_assets_exempt_from_cgt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_assets_exempt_from_cgt_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_assumptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    RetirementAge = table.Column<double>(type: "float", nullable: false),
                    CurrentGrossIncome = table.Column<double>(type: "float", nullable: false),
                    RetirementInvestmentRisk = table.Column<int>(type: "int", nullable: false),
                    DeathInvestmentRisk = table.Column<int>(type: "int", nullable: false),
                    DisabilityInvestmentRisk = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_assumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_assumptions_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_estate_duty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    Section4pValue = table.Column<double>(type: "float", nullable: false),
                    LimitedRights = table.Column<double>(type: "float", nullable: false),
                    ResidueToSpouse = table.Column<bool>(type: "bit", nullable: false),
                    Abatement = table.Column<double>(type: "float", nullable: false),
                    TotalDutyPayable = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_estate_duty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_estate_duty_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_estate_expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    AdminCosts = table.Column<double>(type: "float", nullable: false),
                    FuneralExpenses = table.Column<double>(type: "float", nullable: false),
                    CashBequests = table.Column<double>(type: "float", nullable: false),
                    Other = table.Column<double>(type: "float", nullable: false),
                    ExecutorsFees = table.Column<double>(type: "float", nullable: false),
                    TotalEstateExpenses = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_estate_expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_estate_expenses_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_insurance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Owner = table.Column<string>(type: "varchar(100)", nullable: true),
                    LifeCover = table.Column<double>(type: "float", nullable: false),
                    Disability = table.Column<double>(type: "float", nullable: false),
                    DreadDisease = table.Column<double>(type: "float", nullable: false),
                    AbsoluteIpPm = table.Column<double>(type: "float", nullable: false),
                    ExtendedIpPm = table.Column<double>(type: "float", nullable: false),
                    AllocateTo = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_insurance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_insurance_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_liabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_liabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_liabilities_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_liquid_assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Growth = table.Column<double>(type: "float", nullable: false),
                    AllocateTo = table.Column<int>(type: "int", nullable: false),
                    DisposedAtRetirement = table.Column<bool>(type: "bit", nullable: false),
                    DisposedOnDisability = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_liquid_assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_liquid_assets_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_primary_residence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    AllocateTo = table.Column<int>(type: "int", nullable: false),
                    BaseCost = table.Column<double>(type: "float", nullable: false),
                    Growth = table.Column<double>(type: "float", nullable: false),
                    DisposedAtRetirement = table.Column<bool>(type: "bit", nullable: false),
                    DisposedOnDisability = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_primary_residence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_primary_residence_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_providing_on_death",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    IncomeNeeds = table.Column<double>(type: "float", nullable: false),
                    IncomeTerm_Years = table.Column<int>(type: "int", nullable: false),
                    CapitalNeeds = table.Column<double>(type: "float", nullable: false),
                    Available_InsuranceDescription = table.Column<string>(type: "varchar(100)", nullable: true),
                    Available_Insurance_Amount = table.Column<double>(type: "float", nullable: false),
                    RetirementFunds = table.Column<double>(type: "float", nullable: false),
                    Available_PreTaxIncome_Amount = table.Column<double>(type: "float", nullable: false),
                    Available_PreTaxIncome_Term = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_providing_on_death", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_providing_on_death_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_providing_on_disability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    ShortTermProtection = table.Column<double>(type: "float", nullable: false),
                    IncomeProtectionTerm_Months = table.Column<int>(type: "int", nullable: false),
                    LongTermProtection = table.Column<double>(type: "float", nullable: false),
                    IncomeNeeds = table.Column<double>(type: "float", nullable: false),
                    NeedsTerm_Years = table.Column<int>(type: "int", nullable: false),
                    LiabilitiesToClear = table.Column<double>(type: "float", nullable: false),
                    CapitalNeeds = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_providing_on_disability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_providing_on_disability_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_providing_on_dread_disease",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    Needs_CapitalNeeds = table.Column<double>(type: "float", nullable: false),
                    Needs_GrossAnnualSalary = table.Column<double>(type: "float", nullable: false),
                    Available_DreadDiseaseDescription = table.Column<string>(type: "varchar(100)", nullable: true),
                    Available_DreadDiseaseAmount = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_providing_on_dread_disease", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_providing_on_dread_disease_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_retirement_pension_funds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    MonthlyContributions = table.Column<double>(type: "float", nullable: false),
                    EscPercent = table.Column<double>(type: "float", nullable: false),
                    Growth = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_retirement_pension_funds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_retirement_pension_funds_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_retirement_planning",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    MonthlyIncome = table.Column<double>(type: "float", nullable: false),
                    TermPostRetirement_Years = table.Column<int>(type: "int", nullable: false),
                    IncomeEscalation = table.Column<double>(type: "float", nullable: false),
                    IncomeNeeds = table.Column<double>(type: "float", nullable: false),
                    NeedsTerm_Years = table.Column<int>(type: "int", nullable: false),
                    IncomeNeedsEscalation = table.Column<double>(type: "float", nullable: false),
                    CapitalNeeds = table.Column<double>(type: "float", nullable: false),
                    OutstandingLiabilities = table.Column<double>(type: "float", nullable: false),
                    SavingsEscalation = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_retirement_planning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_retirement_planning_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_retirement_preservation_funds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Growth = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_retirement_preservation_funds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_retirement_preservation_funds_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_foreign_tax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxResidencyId = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "varchar(100)", nullable: true),
                    TinNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    TinUnavailableReason = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_foreign_tax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_foreign_tax_client_tax_residency_TaxResidencyId",
                        column: x => x.TaxResidencyId,
                        principalTable: "client_tax_residency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_roa_products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordOfAdviceId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RecommendedLumpSum = table.Column<double>(type: "float", nullable: false),
                    AcceptedLumpSum = table.Column<double>(type: "float", nullable: false),
                    RecommendedRecurringPremium = table.Column<double>(type: "float", nullable: false),
                    AcceptedRecurringPremium = table.Column<double>(type: "float", nullable: false),
                    DeviationReason = table.Column<string>(type: "varchar(100)", nullable: true),
                    CapitalProtection = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_roa_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_application_roa_products_application_roa_RecordOfAdviceId",
                        column: x => x.RecordOfAdviceId,
                        principalTable: "application_roa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "AssociatedRisk", "Created", "CreatedBy", "Description", "Institute", "IsActive", "Modified", "ModifiedBy", "Name", "PaymentType", "ProductCategory", "ProductType" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(6898), 0, "The Minimum Return Multi-Asset Global Note (the “Note”) is a five year 100% ZAR capital protected investment linked to the CITI Flexible Multi Asset V15% Index (the “Index”). The investment objective of Note is to provide no minimum ZAR return, with the maximum possible full uncapped participation in the Index, with a 100% ZAR capital protected investment, thus also providing full USD / ZAR return exposure. It is Aluma’s view that this investment could be suitable for investors who require exposure to a low risk USD Multi-Asset Balanced Portfolio and 100% ZAR capital protection.", "Standard Bank", true, new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(6906), 0, "Structured Note", 0, 1, 0 },
                    { 2, 3, new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9027), 0, "Our Local Share Portfolio is an investment product for discretionary money, which allows you to access to all Local Shares listed on the JSE as well as EFTs.It is subject to CGT, income tax on interest, dividends tax, and Real Estate Investment Trust (REIT) tax. Income tax is due whether interest is earned by your investment. A CGT event will occur when you do a withdrawal or a switch from an investment portfolio. A 20% withholdings tax on local dividends applies. We will deduct any dividend and REIT tax which you may owe from an income distribution before it’s invested into your investment account. Contributions, withdrawals and drawing a regular withdrawal\r\n                                are allowable at any point in time without incurring penalties.You may change,\r\n                                stop and resume your ad hoc or regular contributions at any time without incurring any penalties.", "Standard Bank", false, new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9031), 0, "Local Share Portfolio", 0, 1, 0 },
                    { 3, 3, new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9037), 0, "Our International Share Portfolio is an investment product for discretionary money, which allows you to access to all International Shares listed on International Stock exchanges as well as ETFs. It is subject to CGT, income tax on interest, dividends tax, and Real Estate Investment Trust (REIT) tax. Income tax is due whenever interest is earned by your investment. A CGT event will occur when you do a withdrawal or a switch from an investment portfolio. A 20% withholdings tax on local dividends applies. We will deduct any dividend and REIT tax which you may owe from an income distribution before it’s invested into your investment account Contributions, withdrawals and drawing a regular withdrawal are allowable at any point in time without incurring penalties. You may change, stop and resume your ad hoc or regular contributions at any time without incurring any penalties.", "Standard Bank", false, new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9038), 0, "International Share Portfolio", 1, 1, 0 },
                    { 4, 3, new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9041), 0, "Trade the JSE and International Equities, CFDs, Indices, ETFs, Forex & Commodities from a single trading account at very competitive rates. Aluma clients receive cost- effective trading via a Multi Asset Direct Market Access (DMA) class-leading trading platform. Clients have the option to manage their own trading account and execute their own trades, or to have their account managed on a discretionary basis.", "Standard Bank", false, new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9042), 0, "Self Managed Account", 1, 1, 0 },
                    { 5, 3, new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9045), 0, "Limited partner interests (the 'Interests') in The Aluma Capital Private Equity Fund I Partnership (the 'Partnership') are being offered to qualified investors.\r\n                                The Interests are offered subject to the right of Aluma Capital General Partner(Proprietary) Limited(the 'General Partner'), in its capacity as the ultimate\r\n                                general partner of the Partnership, to reject any application in whole or in part.", "Aluma Capital", true, new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9046), 0, "Private Equity Fund - Growth", 0, 1, 0 },
                    { 6, 3, new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9051), 0, "Limited partner interests (the 'Interests') in The Aluma Capital Private Equity Fund I Partnership (the 'Partnership') are being offered to qualified investors.\r\n                                The Interests are offered subject to the right of Aluma Capital General Partner(Proprietary) Limited(the 'General Partner'), in its capacity as the ultimate\r\n                                general partner of the Partnership, to reject any application in whole or in part.", "Aluma Capital", true, new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9051), 0, "Private Equity Fund - Income", 0, 1, 0 },
                    { 7, 3, new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9054), 0, " ", "Vanguard", false, new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9055), 0, "Fixed Income", 0, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Created", "CreatedBy", "DateOfBirth", "Email", "FirstName", "LastName", "MaidenName", "MobileNumber", "Modified", "ModifiedBy", "Password", "ProfileImage", "RSAIdNumber", "RegistrationVerifiedDate", "Role", "Signature", "SocialId", "isOtpLocked", "isRegistrationVerified", "isSocialLogin" },
                values: new object[] { 1, new DateTime(2022, 5, 23, 9, 21, 15, 58, DateTimeKind.Local).AddTicks(7260), 0, null, "dev@aluma.co.za", "Dev", "Tester", null, "0843334444", new DateTime(2022, 5, 23, 9, 21, 15, 58, DateTimeKind.Local).AddTicks(7282), 0, "9406.KUQoqM6LMz4opG7fNCY5KA==.zaSHlPGQoASmXFzJB4dQI2+XiJ59/gaDbxltaGzDzA8=", null, "9012245555088", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, false, true, false });

            migrationBuilder.InsertData(
                table: "advisors",
                columns: new[] { "Id", "AdviceBonds", "AdviceDebentures", "AdviceDerivatives", "AdviceLTDeposits", "AdviceLTSubCatA", "AdviceLTSubCatB1", "AdviceLTSubCatB1A", "AdviceLTSubCatB2", "AdviceLTSubCatB2A", "AdviceLTSubCatC", "AdviceMoneyMarket", "AdviceParticipatoryInterestCollective", "AdviceParticipatoryInterestHedge", "AdvicePensionFunds", "AdviceRetailPension", "AdviceSTCommercial", "AdviceSTDeposits", "AdviceSTPersonal", "AdviceSTPersonalA1", "AdviceSecurities", "AdviceShares", "AdviceStructuredDeposits", "AdviceWarrants", "AppointmentDate", "BusinessTel", "Created", "CreatedBy", "Fax", "HomeTel", "Modified", "ModifiedBy", "SupervisedBonds", "SupervisedDebentures", "SupervisedDerivatives", "SupervisedLTDeposits", "SupervisedLTSubCatA", "SupervisedLTSubCatB1", "SupervisedLTSubCatB1A", "SupervisedLTSubCatB2", "SupervisedLTSubCatB2A", "SupervisedLTSubCatC", "SupervisedMoneyMarket", "SupervisedParticipatoryInterestCollective", "SupervisedParticipatoryInterestHedge", "SupervisedPensionFunds", "SupervisedRetailPension", "SupervisedSTCommercial", "SupervisedSTDeposits", "SupervisedSTPersonal", "SupervisedSTPersonalA1", "SupervisedSecurities", "SupervisedShares", "SupervisedStructuredDeposits", "SupervisedWarrants", "Title", "UserId", "isActive", "isExternalBroker", "isSupervised" },
                values: new object[] { 1, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, new DateTime(2021, 5, 23, 9, 21, 15, 37, DateTimeKind.Local).AddTicks(9580), null, new DateTime(2022, 5, 23, 9, 21, 15, 37, DateTimeKind.Local).AddTicks(820), 0, null, null, new DateTime(2022, 5, 23, 9, 21, 15, 37, DateTimeKind.Local).AddTicks(8773), 0, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, null, 1, true, false, false });

            migrationBuilder.InsertData(
                table: "user_addresses",
                columns: new[] { "Id", "AddressSameAs", "City", "ComplexName", "Country", "Created", "CreatedBy", "InCareAddress", "InCareName", "Modified", "ModifiedBy", "PostalCode", "StreetName", "StreetNumber", "Suburb", "Type", "UnitNumber", "UserId", "YearsAtAddress" },
                values: new object[] { 2, false, "Johannesburg", "Postnet Suite 33", "South Africa", new DateTime(2022, 5, 23, 9, 21, 15, 70, DateTimeKind.Local).AddTicks(7861), 0, false, null, new DateTime(2022, 5, 23, 9, 21, 15, 70, DateTimeKind.Local).AddTicks(7866), 0, "2157", "Private Bag X 26", null, "Sunninghill", 0, null, 1, 0 });

            migrationBuilder.InsertData(
                table: "user_addresses",
                columns: new[] { "Id", "AddressSameAs", "City", "ComplexName", "Country", "Created", "CreatedBy", "InCareAddress", "InCareName", "Modified", "ModifiedBy", "PostalCode", "StreetName", "StreetNumber", "Suburb", "Type", "UnitNumber", "UserId", "YearsAtAddress" },
                values: new object[] { 1, false, "Pretoria", "FinTech Campus", "South Africa", new DateTime(2022, 5, 23, 9, 21, 15, 70, DateTimeKind.Local).AddTicks(4862), 0, false, null, new DateTime(2022, 5, 23, 9, 21, 15, 70, DateTimeKind.Local).AddTicks(4883), 0, "0081", "Cnr Illanga and Botterklapper", null, "The Willows", 1, null, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_advisor_disclosures_AdvisorId",
                table: "advisor_disclosures",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_advisor_disclosures_ClientId",
                table: "advisor_disclosures",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_advisors_UserId",
                table: "advisors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_application_documents_ApplicationId",
                table: "application_documents",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_application_purpose_and_funding_ApplicationId",
                table: "application_purpose_and_funding",
                column: "ApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_application_roa_AdvisorId",
                table: "application_roa",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_application_roa_ApplicationId",
                table: "application_roa",
                column: "ApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_application_roa_products_RecordOfAdviceId",
                table: "application_roa_products",
                column: "RecordOfAdviceId");

            migrationBuilder.CreateIndex(
                name: "IX_applications_AdvisorId",
                table: "applications",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_applications_ClientId",
                table: "applications",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_client_bank_details_ClientId",
                table: "client_bank_details",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_client_consumer_protection_ClientId",
                table: "client_consumer_protection",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_employment_details_ClientId",
                table: "client_employment_details",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_fna_AdvisorId",
                table: "client_fna",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_client_fna_ClientId",
                table: "client_fna",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_client_foreign_tax_TaxResidencyId",
                table: "client_foreign_tax",
                column: "TaxResidencyId");

            migrationBuilder.CreateIndex(
                name: "IX_client_kyc_data_ClientId",
                table: "client_kyc_data",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_lead_AdvisorId",
                table: "client_lead",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_client_lead_ClientId",
                table: "client_lead",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_mandate_ClientId",
                table: "client_mandate",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_marital_details_ClientId",
                table: "client_marital_details",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_passports_ClientId",
                table: "client_passports",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_client_products_ClientId",
                table: "client_products",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_client_products_ProductId",
                table: "client_products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_client_risk_profile_ClientId",
                table: "client_risk_profile",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_tax_residency_ClientId",
                table: "client_tax_residency",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clients_AdvisorId",
                table: "clients",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_clients_LeadId",
                table: "clients",
                column: "LeadId",
                unique: true,
                filter: "[LeadId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_clients_UserId",
                table: "clients",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_fna_accrual_FNAId",
                table: "fna_accrual",
                column: "FNAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_administration_costs_FNAId",
                table: "fna_administration_costs",
                column: "FNAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_assets_attracting_cgt_FNAId",
                table: "fna_assets_attracting_cgt",
                column: "FNAId");

            migrationBuilder.CreateIndex(
                name: "IX_fna_assets_exempt_from_cgt_FNAId",
                table: "fna_assets_exempt_from_cgt",
                column: "FNAId");

            migrationBuilder.CreateIndex(
                name: "IX_fna_assumptions_FNAId",
                table: "fna_assumptions",
                column: "FNAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_estate_duty_FNAId",
                table: "fna_estate_duty",
                column: "FNAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_estate_expenses_FNAId",
                table: "fna_estate_expenses",
                column: "FNAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_insurance_FNAId",
                table: "fna_insurance",
                column: "FNAId");

            migrationBuilder.CreateIndex(
                name: "IX_fna_liabilities_FNAId",
                table: "fna_liabilities",
                column: "FNAId");

            migrationBuilder.CreateIndex(
                name: "IX_fna_liquid_assets_FNAId",
                table: "fna_liquid_assets",
                column: "FNAId");

            migrationBuilder.CreateIndex(
                name: "IX_fna_primary_residence_FNAId",
                table: "fna_primary_residence",
                column: "FNAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_providing_on_death_FNAId",
                table: "fna_providing_on_death",
                column: "FNAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_providing_on_disability_FNAId",
                table: "fna_providing_on_disability",
                column: "FNAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_providing_on_dread_disease_FNAId",
                table: "fna_providing_on_dread_disease",
                column: "FNAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_retirement_pension_funds_FNAId",
                table: "fna_retirement_pension_funds",
                column: "FNAId");

            migrationBuilder.CreateIndex(
                name: "IX_fna_retirement_planning_FNAId",
                table: "fna_retirement_planning",
                column: "FNAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_retirement_preservation_funds_FNAId",
                table: "fna_retirement_preservation_funds",
                column: "FNAId");

            migrationBuilder.CreateIndex(
                name: "IX_user_addresses_UserId",
                table: "user_addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_documents_UserId",
                table: "user_documents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_otp_UserId",
                table: "user_otp",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_RSAIdNumber",
                table: "users",
                column: "RSAIdNumber",
                unique: true,
                filter: "[RSAIdNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "advisor_disclosures");

            migrationBuilder.DropTable(
                name: "application_documents");

            migrationBuilder.DropTable(
                name: "application_purpose_and_funding");

            migrationBuilder.DropTable(
                name: "application_roa_products");

            migrationBuilder.DropTable(
                name: "capital_gains_tax");

            migrationBuilder.DropTable(
                name: "client_bank_details");

            migrationBuilder.DropTable(
                name: "client_consumer_protection");

            migrationBuilder.DropTable(
                name: "client_employment_details");

            migrationBuilder.DropTable(
                name: "client_foreign_tax");

            migrationBuilder.DropTable(
                name: "client_kyc_data");

            migrationBuilder.DropTable(
                name: "client_lead");

            migrationBuilder.DropTable(
                name: "client_mandate");

            migrationBuilder.DropTable(
                name: "client_marital_details");

            migrationBuilder.DropTable(
                name: "client_passports");

            migrationBuilder.DropTable(
                name: "client_products");

            migrationBuilder.DropTable(
                name: "client_risk_profile");

            migrationBuilder.DropTable(
                name: "fna_accrual");

            migrationBuilder.DropTable(
                name: "fna_administration_costs");

            migrationBuilder.DropTable(
                name: "fna_assets_attracting_cgt");

            migrationBuilder.DropTable(
                name: "fna_assets_exempt_from_cgt");

            migrationBuilder.DropTable(
                name: "fna_assumptions");

            migrationBuilder.DropTable(
                name: "fna_estate_duty");

            migrationBuilder.DropTable(
                name: "fna_estate_expenses");

            migrationBuilder.DropTable(
                name: "fna_insurance");

            migrationBuilder.DropTable(
                name: "fna_liabilities");

            migrationBuilder.DropTable(
                name: "fna_liquid_assets");

            migrationBuilder.DropTable(
                name: "fna_primary_residence");

            migrationBuilder.DropTable(
                name: "fna_providing_on_death");

            migrationBuilder.DropTable(
                name: "fna_providing_on_disability");

            migrationBuilder.DropTable(
                name: "fna_providing_on_dread_disease");

            migrationBuilder.DropTable(
                name: "fna_retirement_pension_funds");

            migrationBuilder.DropTable(
                name: "fna_retirement_planning");

            migrationBuilder.DropTable(
                name: "fna_retirement_preservation_funds");

            migrationBuilder.DropTable(
                name: "fna_tax_lumpsum");

            migrationBuilder.DropTable(
                name: "user_addresses");

            migrationBuilder.DropTable(
                name: "user_documents");

            migrationBuilder.DropTable(
                name: "user_otp");

            migrationBuilder.DropTable(
                name: "application_roa");

            migrationBuilder.DropTable(
                name: "client_tax_residency");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "client_fna");

            migrationBuilder.DropTable(
                name: "applications");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "advisors");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

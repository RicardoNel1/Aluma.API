

namespace IDVerificationService
{
    public class IDVRepo
    {

        public interface IIDVerificationServiceRepo
        {
            //BankValidationResponseDto StartBankValidation(BankDetailsDto dto);

            //VerificationStatusResponse GetBankValidationStatus(string jobId);
        }

        public class IDVerificationServiceRepo : IIDVerificationServiceRepo
        {
            //public readonly SettingsDto _settings;

            public IDVerificationServiceRepo()
            {
                //var config = new ConfigurationBuilder();
                //// Get current directory will return the root dir of Base app as that is the running application
                //var path = Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
                //config.AddJsonFile(path, false);
                //var root = config.Build();
                //_settings = root.GetSection("PbVerifyBankValidation").Get<SettingsDto>();
            }

        }


    }
}
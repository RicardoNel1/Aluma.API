using DataService.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace SignatureService
{
    public interface ISignatureRepo
    {
        SignerListItemDto CreateSignerListItem(SignerDto dto);

        MultipleSignersCeremonyDto CreateMultipleSignersCeremony(byte[] docData,
            string docName, List<SignerListItemDto> signers);

        string RunMultiSignerCeremony(MultipleSignersCeremonyDto dto);
    }

    public class SignatureRepo : ISignatureRepo
    {
        public readonly SignatureSettingsDto _settings;

        public SignatureRepo()
        {
            var config = new ConfigurationBuilder();
            // Get current directory will return the root dir of Base app as that is the running application
            var path = Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _settings = root.GetSection("SigniflowSettings").Get<SignatureSettingsDto>();
        }

        public SignatureSettingsDto settings { get => _settings; }

        public SignerListItemDto CreateSignerListItem(SignerDto dto)
        {
            return new SignerListItemDto()
            {
                SignatureImageField = dto.Signature,
                SignatureImageIncludeBorderField = "true",
                SignatureImageIncludeReasonField = "false",
                SignatureImageIncludeSignedByField = dto.IncludeSignedBy == true ? "true" : "false",
                SignatureImageIncludeSignedDateField = "false",
                SignatureImageTypeField = 0,
                SignaturePageField = dto.Page,
                SignatureHField = dto.HField,
                SignatureWField = dto.WField,
                SignatureXField = dto.XField, // horizontal
                SignatureYField = dto.YField, // vertical
                SignerEmailField = dto.Email,
                SignerFullNameField = $"{dto.FirstName} {dto.LastName}",
                SignerIndentificationNumberField = dto.IdNo,
                SignerLocationField = _settings.SignerLocationField,
                SignerReasonField = _settings.SignerReasonField,
                SignerTrustOriginField = _settings.SignerTrustOriginField,
                SignerTrustReferenceField = _settings.SignerTrustReferenceField,
                SignerMobileNumberField = dto.Mobile,
            };
        }

        public MultipleSignersCeremonyDto CreateMultipleSignersCeremony(byte[] docData,
            string docName, List<SignerListItemDto> signers)
        {
            return new MultipleSignersCeremonyDto()
            {
                DocField = Convert.ToBase64String(docData),
                DocNameField = docName,
                LoginPasswordField = _settings.Password,
                LoginUserNameField = _settings.UserName,
                SignerListField = signers,
            };
        }

        public string RunMultiSignerCeremony(MultipleSignersCeremonyDto dto)
        {
            var client = new RestClient($"{_settings.BaseUrl}MultipleSignersSigningCeremony");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(dto), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException($"Couldn't Sign Document: {dto.DocNameField}");

            var signedDocData = JsonConvert.DeserializeObject<CeremonyResponseDto>(response.Content);

            return signedDocData.SignedDocumentField;
        }
    }
}
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataService.Dto
{
    public class SignatureSettingsDto
    {
        public string BaseUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SignerLocationField { get; set; }
        public string SignerReasonField { get; set; }
        public string SignerTrustOriginField { get; set; }
        public string SignerTrustReferenceField { get; set; }
    }

    public class MultipleSignersCeremonyDto
    {
        [JsonProperty("DocField")]
        public string DocField { get; set; }

        [JsonProperty("DocNameField")]
        public string DocNameField { get; set; }

        [JsonProperty("LoginPasswordField")]
        public string LoginPasswordField { get; set; }

        [JsonProperty("LoginUserNameField")]
        public string LoginUserNameField { get; set; }

        [JsonProperty("SignerListField")]
        public ICollection<SignerListItemDto> SignerListField { get; set; }
    }

    public class SignerDto
    {
        public string Signature { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdNo { get; set; }
        public string Mobile { get; set; }
        public int HField { get; set; }
        public int WField { get; set; }
        public int XField { get; set; }
        public int YField { get; set; }
        public int Page { get; set; }
        public bool IncludeSignedBy { get; set; }

        public SignerDto()
        {
            HField = 120;
            WField = 160;
            IncludeSignedBy = true;
        }
    }

    public class SignerListItemDto
    {
        [JsonProperty("SignatureHField")]
        public int SignatureHField { get; set; }

        [JsonProperty("SignatureImageField")]
        public string SignatureImageField { get; set; }

        [JsonProperty("SignatureImageIncludeBorderField")]
        public string SignatureImageIncludeBorderField { get; set; }

        [JsonProperty("SignatureImageIncludeReasonField")]
        public string SignatureImageIncludeReasonField { get; set; }

        [JsonProperty("SignatureImageIncludeSignedByField")]
        public string SignatureImageIncludeSignedByField { get; set; }

        [JsonProperty("SignatureImageIncludeSignedDateField")]
        public string SignatureImageIncludeSignedDateField { get; set; }

        [JsonProperty("SignatureImageTypeField")]
        public int SignatureImageTypeField { get; set; }

        [JsonProperty("SignaturePageField")]
        public int SignaturePageField { get; set; }

        [JsonProperty("SignatureWField")]
        public int SignatureWField { get; set; }

        [JsonProperty("SignatureXField")]
        public int SignatureXField { get; set; }

        [JsonProperty("SignatureYField")]
        public int SignatureYField { get; set; }

        [JsonProperty("SignerEmailField")]
        public string SignerEmailField { get; set; }

        [JsonProperty("SignerFullNameField")]
        public string SignerFullNameField { get; set; }

        [JsonProperty("SignerIndentificationNumberField")]
        public string SignerIndentificationNumberField { get; set; }

        [JsonProperty("SignerLocationField")]
        public string SignerLocationField { get; set; }

        [JsonProperty("SignerMobileNumberField")]
        public string SignerMobileNumberField { get; set; }

        [JsonProperty("SignerReasonField")]
        public string SignerReasonField { get; set; }

        [JsonProperty("SignerTrustOriginField")]
        public string SignerTrustOriginField { get; set; }

        [JsonProperty("SignerTrustReferenceField")]
        public string SignerTrustReferenceField { get; set; }
    }

    public class CeremonyResponseDto
    {
        [JsonProperty("CeremonyIDField")]
        public int CeremonyIDField { get; set; }

        [JsonProperty("ResultField")]
        public string ResultField { get; set; }

        [JsonProperty("SignedDocumentField")]
        public string SignedDocumentField { get; set; }
    }

    public class SigningCeremonyDTO
    {
        public string DocField { get; set; }
        public string DocNameField { get; set; }
        public string LoginPasswordField { get; set; }
        public string LoginUserNameField { get; set; }
        public int SignatureHField { get; set; }
        public string SignatureImageField { get; set; }
        public bool SignatureImageIncludeBorderField { get; set; }
        public bool SignatureImageIncludeReasonField { get; set; }
        public bool SignatureImageIncludeSignedByField { get; set; }
        public bool SignatureImageIncludeSignedDateField { get; set; }
        public int SignatureImageTypeField { get; set; }
        public int SignaturePageField { get; set; }
        public int SignatureWField { get; set; }
        public int SignatureXField { get; set; }
        public int SignatureYField { get; set; }
        public string SignerEmailField { get; set; }
        public string SignerIdentificationNumberField { get; set; }
        public string SignerLocationField { get; set; }
        public string SignerMobileNumberField { get; set; }
        public string SignerReasonField { get; set; }
        public string SignerTrustOriginField { get; set; }
        public string SignerTrustReferenceField { get; set; }
        public ICollection<CheckboxListDTO> CheckboxFieldsListField { get; set; }
        public ICollection<CheckboxListDTO> InitialFieldsListField { get; set; }
        public ICollection<TextFieldListDTO> TextFieldsListField { get; set; }
    }

    public class CheckboxListDTO
    {
        public int CheckboxHField { get; set; }
        public int CheckboxPageField { get; set; }
        public int CheckboxWField { get; set; }
        public int CheckboxXField { get; set; }
        public int CheckboxYField { get; set; }
        public bool IsCheckedField { get; set; }
    }

    public class InitialListDTO
    {
        public int InitialHField { get; set; }
        public string InitialImageField { get; set; }
        public int InitialImageTypeField { get; set; }
        public int InitialPageField { get; set; }
        public int InitialWField { get; set; }
        public int InitialXField { get; set; }
        public int InitialYField { get; set; }
    }

    public class TextFieldListDTO
    {
        public int TextFieldHField { get; set; }
        public int TextFieldPageField { get; set; }
        public string TextFieldValueField { get; set; }
        public int TextFieldWField { get; set; }
        public int TextFieldXField { get; set; }
        public int TextFieldYField { get; set; }
    }
}
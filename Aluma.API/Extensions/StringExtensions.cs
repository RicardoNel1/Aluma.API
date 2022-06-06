using DataService.Dto;
using System;

namespace Aluma.API.Extensions
{
    public static class StringExtensions
    {
        //public static DateTime GetDateOfBirthFromRSAId(this string rsaId)
        //{
        //    string dob = rsaId.Substring(0, 6);
        //    string year = dob.Substring(0,2);
        //    string month = dob.Substring(2, 4);
        //    string day = dob.Substring(4, 6);


        //    return age;
        //}

        public static string FormatAddress(this AddressDto addressDto)
        {
            if (addressDto == null)
                return string.Empty;

            string result = string.IsNullOrEmpty(addressDto.UnitNumber) && string.IsNullOrEmpty(addressDto.ComplexName) ? string.Empty : $"{addressDto.UnitNumber} {addressDto.ComplexName}";
            result += $"{addressDto.StreetNumber} {addressDto.StreetName}, {Environment.NewLine}";
            result += $"{addressDto.Suburb}, {Environment.NewLine}";
            result += $"{addressDto.City}, {Environment.NewLine}";
            result += $"{addressDto.Country}, {Environment.NewLine}";
            result += $"{addressDto.PostalCode}";

            return result;
        }
    }
}

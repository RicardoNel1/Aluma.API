using System;
using System.Linq;

namespace Aluma.API.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static string GetDateOfBirthFromRsaIdNumber(this string idNumber)
        {
            idNumber = (idNumber ?? string.Empty).Replace(" ", "");
            string dateOfBirth = string.Empty;

            if (idNumber.Length == 13)
            {
                var digits = new int[13];
                string second = string.Empty;
                int control2 = 0;

                for (int i = 0; i < 13; i++)
                {
                    digits[i] = int.Parse(idNumber.Substring(i, 1));
                }
                int control1 = digits.Where((v, i) => i % 2 == 0 && i < 12).Sum();

                digits.Where((v, i) => i % 2 != 0 && i < 12).ToList().ForEach(v => second += v.ToString());
                var string2 = (int.Parse(second) * 2).ToString();

                for (int i = 0; i < string2.Length; i++)
                {
                    control2 += int.Parse(string2.Substring(i, 1));
                }

                var control = (10 - (control1 + control2) % 10) % 10;
                if (digits[12] == control)
                {
                    dateOfBirth = DateTime.ParseExact(idNumber.Substring(0, 6), "yyMMdd", null).ToString("yyyy-MM-dd");
                }
            }

            return dateOfBirth;
        }

        public static string GetGenderFromRsaIdNumber(this string idNumber)
        {
            idNumber = (idNumber ?? string.Empty).Replace(" ", "");
            string gender = string.Empty;

            if (idNumber.Length == 13)
            {
                var digits = new int[13];
                string second = string.Empty;
                int control2 = 0;

                for (int i = 0; i < 13; i++)
                {
                    digits[i] = int.Parse(idNumber.Substring(i, 1));
                }
                int control1 = digits.Where((v, i) => i % 2 == 0 && i < 12).Sum();

                digits.Where((v, i) => i % 2 != 0 && i < 12).ToList().ForEach(v => second += v.ToString());
                var string2 = (int.Parse(second) * 2).ToString();

                for (int i = 0; i < string2.Length; i++)
                {
                    control2 += int.Parse(string2.Substring(i, 1));
                }

                var control = (10 - (control1 + control2) % 10) % 10;
                if (digits[12] == control)
                {
                    gender = digits[6] < 5 ? "Female" : "Male";
                }
            }

            return gender;
        }

    }
}

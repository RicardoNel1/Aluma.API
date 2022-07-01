using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Aluma.API.Helpers
{
    public class UtilityHelper
    {
        public Dictionary<string, int> BanksDictionary = new()
                {
                    {"FNB",250655},
                    {"NEDBANK",198765},
                    {"STANDARDBANK",051001},
                    {"CAPITEC",470010},
                    {"AFRICANBANK",430000},
                    {"ABSA",632005},
                    {"INVESTEC",580105},
                    {"BIDVESTBANK",462005},
                    {"SASFINBANK",683000},
                    {"DISCOVERYBANK",679000},
                    {"GRINDRODBANK",584000},
                    {"TYMEBANK",678910},
                };

        public Dictionary<string, string> AccountTypes = new()
                {
                    {"00","Unknown"},
                    {"01","Current / Cheque Account"},
                    {"02","Savings Account"},
                    {"03","Transmission Account"},
                    {"04","Bond Account"},
                    {"06","Subscription Share"}
                };

        public string Initials(string str)
        {
            var newStr = string.Empty;

            str.Split(' ').ToList().ForEach(e => newStr += e[0]);
            return newStr;
        }

        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new())
                {
                    using (CryptoStream cryptoStream = new((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new(buffer))
                {
                    using (CryptoStream cryptoStream = new((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
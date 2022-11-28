using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

namespace Aluma.API.Helpers
{
    public class CredentialCrypt
    {
        public static string EncryptToHash(string data)
        {
            byte[] key = new byte[16];
            byte[] iv = new byte[16];

            return Convert.ToBase64String(
              Encrypt(Encoding.UTF8.GetBytes(data), key, iv));
        }

        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (Aes algorithm = Aes.Create())
            using (ICryptoTransform encryptor = algorithm.CreateEncryptor(key, iv))
                return Crypt(data, encryptor);
        }

        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (Aes algorithm = Aes.Create())
            using (ICryptoTransform decryptor = algorithm.CreateDecryptor(key, iv))
                return Crypt(data, decryptor);
        }

        public static string Encrypt(string data, byte[] key, byte[] iv)
        {
            return Convert.ToBase64String(
              Encrypt(Encoding.UTF8.GetBytes(data), key, iv));
        }

        public static string Decrypt(string data, byte[] key, byte[] iv)
        {
            return Encoding.UTF8.GetString(
              Decrypt(Convert.FromBase64String(data), key, iv));
        }

        static byte[] Crypt(byte[] data, ICryptoTransform cryptor)
        {
            MemoryStream m = new MemoryStream();
            using (Stream c = new CryptoStream(m, cryptor, CryptoStreamMode.Write))
                c.Write(data, 0, data.Length);
            return m.ToArray();
        }
    }
}

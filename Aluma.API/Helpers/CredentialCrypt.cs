using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

namespace Aluma.API.Helpers
{
    public class CredentialCrypt
    {
        private static string _cryptoKey;
        private static string _cryptoIV;

        public CredentialCrypt()
        {
            _cryptoKey = "Yq3t6w9y$B&E)H@M";
            _cryptoIV = "PdSgVkYp3s6v9y$B";
        }

        public static string EncryptToHash(string data)
        {
            byte[] key = Encoding.UTF8.GetBytes(_cryptoKey);
            byte[] iv = Encoding.UTF8.GetBytes(_cryptoIV);

            var encryptedHash = Convert.ToBase64String(
              Encrypt(Encoding.UTF8.GetBytes(data), key, iv));

            Array.Clear(key, 0, key.Length);
            Array.Clear(iv, 0, iv.Length);

            return encryptedHash;

        }

        public static string DecryptFromHash(string data)
        {
            byte[] key = Encoding.UTF8.GetBytes(_cryptoKey);
            byte[] iv = Encoding.UTF8.GetBytes(_cryptoIV);

            string decryptedHash = Convert.ToBase64String(
              Decrypt(Encoding.UTF8.GetBytes(data), key, iv));


            Array.Clear(key, 0, key.Length);
            Array.Clear(iv, 0, iv.Length);

            return decryptedHash;
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

        static byte[] Crypt(byte[] data, ICryptoTransform cryptor)
        {
            MemoryStream m = new MemoryStream();
            using (Stream c = new CryptoStream(m, cryptor, CryptoStreamMode.Write))
                c.Write(data, 0, data.Length);
            return m.ToArray();
        }
    }
}

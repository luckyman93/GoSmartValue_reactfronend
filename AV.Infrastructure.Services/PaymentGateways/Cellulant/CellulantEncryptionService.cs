using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using AV.Contracts.ConfigurationDtos;

namespace AV.Infrastructure.Services.PaymentGateways.Cellulant
{
    public class CellulantEncryptionService
    {
        public CellulantEncryptionService()
        { }

        public string IvKey { get; set; }
        public string SecretKey { get; set; }

        public string Encrypt(string payload)
        {

            byte[] encrypted;

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Key = Encoding.UTF8.GetBytes(HashString(SecretKey).Substring(0, 32));
                aes.IV = Encoding.UTF8.GetBytes(HashString(IvKey).Substring(0, 16));
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                var enc = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, enc, CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(payload);
                        }

                        encrypted = ms.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Convert.ToBase64String(encrypted)));
        }

        private static string HashString(string str)
        {
            var stringBuilder = new StringBuilder();
            using var hash = SHA256.Create();
            var result = hash.ComputeHash(Encoding.UTF8.GetBytes(str));
            foreach (var x in result)
            {
                stringBuilder.Append(x.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
using System;
using System.Security.Cryptography;
using System.Text;

namespace WebApps.EncriptionService.Helpers
{
    public static class Cryptography
    {
        public static void CreateKey(string containerName)
        {
            var parameters = new CspParameters
            {
                KeyContainerName = containerName
            };

            using var rsa = new RSACryptoServiceProvider(parameters);
        }

        public static bool ExistsKey(string containerName)
        {
            var parameters = new CspParameters
            {
                Flags = CspProviderFlags.UseExistingKey,
                KeyContainerName = containerName
            };

            try
            {
                using (var rsa = new RSACryptoServiceProvider(parameters));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void DeleteKey(string containerName)
        {
            var parameters = new CspParameters
            {
                KeyContainerName = containerName
            };

            using (var rsa = new RSACryptoServiceProvider(parameters)
            {
                PersistKeyInCsp = false
            })
            {
                rsa.Clear();
            }
        }

        public static string Encrypt(string containerName, string secret)
        {
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentException("Secret mustn't be null or empty");
            }

            var parameters = new CspParameters
            {
                KeyContainerName = containerName
            };

            using (var rsa = new RSACryptoServiceProvider(parameters))
            {
                var data = Encoding.UTF8.GetBytes(secret);
                var res = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);

                return Convert.ToBase64String(res);
            }
        }

        public static string Decrypt(string containerName, string encryptedData)
        {
            if (string.IsNullOrEmpty(encryptedData))
            {
                throw new ArgumentException("Data mustn't be null or empty");
            }

            var parameters = new CspParameters
            {
                KeyContainerName = containerName
            };

            using (var rsa = new RSACryptoServiceProvider(parameters))
            {
                var data = Convert.FromBase64String(encryptedData);
                var res = rsa.Decrypt(data, RSAEncryptionPadding.Pkcs1);

                return Encoding.UTF8.GetString(res);
            }
        }
    }
}

using System;
using System.Security.Cryptography;
using System.Text;

namespace DocHelper.Infrastructure.Cache.Utilities
{
    public static class HashUtilities
    {
        public static string ComputeHash(string data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            using var sha256Hash = SHA256.Create();
            return GetHash(sha256Hash, data);
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var stringBuilder = new StringBuilder();

            foreach (var t in data)
            {
                stringBuilder.Append(t.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
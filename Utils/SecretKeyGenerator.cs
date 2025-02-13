using System;
using System.IO;
using System.Security.Cryptography;

namespace MotelAPI.Utils
{
    public static class SecretKeyGenerator
    {
        private const string EnvFilePath = ".env";

        public static void GenerateAndSaveSecretKey()
        {
            string secretKey = GenerateSecretKey();

            string[] lines = File.ReadAllLines(EnvFilePath);
            bool keyExists = false;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("SECRET_KEY"))
                {
                    lines[i] = $"SECRET_KEY={secretKey}";
                    keyExists = true;
                    break;
                }
            }

            if (!keyExists)
            {
                Array.Resize(ref lines, lines.Length + 1);
                lines[lines.Length - 1] = $"SECRET_KEY={secretKey}";
            }

            File.WriteAllLines(EnvFilePath, lines);
        }

        private static string GenerateSecretKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] secretKey = new byte[32];
                rng.GetBytes(secretKey);
                return Convert.ToBase64String(secretKey);
            }
        }
    }
}

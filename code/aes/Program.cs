using System;
using System.IO;
using System.Security.Cryptography;

namespace aes
{
    class Program
    {
        static void Main(string[] args)
        {
            var plainText = "Here is some data to encrypt!";
            var password = "secret password";

            var iv = new Byte[16];
            using(var r = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                r.GetBytes(iv);
            }

            Byte[] key;
            var keySalt = BitConverter.GetBytes(0xbeefbeefbeefbeef);
            var keyIterations = 1000;
            using(var keygen = new Rfc2898DeriveBytes(password, keySalt, keyIterations))
            {
                key = keygen.GetBytes(32);
            }
            

            using (var aesAlg = Aes.Create())
            {
                Console.WriteLine("Min Key Length: {0}", aesAlg.LegalKeySizes[0].MinSize);
                Console.WriteLine("Max Key Length: {0}", aesAlg.LegalKeySizes[0].MaxSize);
                Console.WriteLine("Key Step Length: {0}", aesAlg.LegalKeySizes[0].SkipSize);
                Console.WriteLine("Min Block Length: {0}", aesAlg.LegalBlockSizes[0].MinSize);
                Console.WriteLine("Max Block Length: {0}", aesAlg.LegalBlockSizes[0].MaxSize);
                Console.WriteLine("Block Step Length: {0}", aesAlg.LegalBlockSizes[0].SkipSize);

                aesAlg.KeySize = 256;         //this is default
                aesAlg.BlockSize = 128;       //this is default
                aesAlg.Mode = CipherMode.CBC; //this is default
                aesAlg.Key = key;
                aesAlg.IV = iv;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                byte[] encrypted;

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
        }
    }
}

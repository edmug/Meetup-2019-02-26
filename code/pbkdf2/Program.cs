using System;
using System.Security.Cryptography;

namespace pbkdf2
{
    class Program
    {
        static void Main(string[] args)
        {
            var badSalt = BitConverter.GetBytes(0xcbadcbadcbadcbad);
            var password = "UltraSecure123!";
            var iterations = 50000;

            using(var k1 = new Rfc2898DeriveBytes(password, badSalt, iterations))
            {
                Console.WriteLine(BitConverter.ToUInt64(k1.GetBytes(8)));
            }
        }
    }
}

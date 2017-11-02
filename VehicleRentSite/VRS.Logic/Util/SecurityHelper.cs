using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace VRS.Logic.Util
{
    public class SecurityHelper
    {
        public static byte[] HashPassword(string password, byte[] salt)
        {
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            byte[] passwordWithSaltBytes =
                new byte[passwordBytes.Length + salt.Length];

            for (int i = 0; i < passwordBytes.Length; i++)
            {
                passwordWithSaltBytes[i] = passwordBytes[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                passwordWithSaltBytes[passwordBytes.Length + i] = salt[i];
            }

            return new SHA256Managed().ComputeHash(passwordWithSaltBytes);
        }

        public static byte[] GenerateSalt()
        {
            return CreateSalt(256);
        }

        private static byte[] CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            
            return buff;
        }
    }
}
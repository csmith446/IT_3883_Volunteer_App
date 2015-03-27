using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_3883_Volunteer_App
{
    static public class MD5Hasher
    {
        static private MD5 md5 = new MD5CryptoServiceProvider();

        /// <summary>
        /// Hashes a string using MD5 and returns the hashed value.
        /// </summary>
        /// <param name="password">The password in plain text.</param>
        static public string GetHashedValue(string password)
        {
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashedBytes = md5.ComputeHash(passwordBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}

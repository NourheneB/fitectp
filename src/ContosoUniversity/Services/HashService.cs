using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ContosoUniversity.Services
{
    public class HashService
    {
        public static string GenerateSHA256String(string inputString)
        {
            //créer une instance de la classe SHA256Managed
            SHA256 sha256 = SHA256Managed.Create();

            // Converting string to byte array 
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);

            //Hashing a string with Sha256

            byte[] hash = sha256.ComputeHash(bytes);

            //Converting toString
            return BitConverter.ToString(hash);
        }
    }
}
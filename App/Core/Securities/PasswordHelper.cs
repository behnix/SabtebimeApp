using System;
using System.Security.Cryptography;
using System.Text;

namespace App.Core.Securities
{
    public class PasswordHelper
    {
        public static string EncodePasswordMd5(string pass)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var originalByte = Encoding.Default.GetBytes(pass);
            var encodedByte = md5.ComputeHash(originalByte);

            return BitConverter.ToString(encodedByte);
        }
    }
}

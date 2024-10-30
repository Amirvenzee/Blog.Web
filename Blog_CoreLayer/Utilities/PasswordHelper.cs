using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.Utilities
{
    public static class PasswordHelper
    {
        public static string EncodePassword(this string password)
        {
            Byte[] OrginalBytes;
            Byte[] encodeBytes;
            MD5 md5 = new MD5CryptoServiceProvider();
            OrginalBytes = ASCIIEncoding.Default.GetBytes(password);
            encodeBytes = md5.ComputeHash(OrginalBytes);
            return BitConverter.ToString(encodeBytes);
        }
    }
}

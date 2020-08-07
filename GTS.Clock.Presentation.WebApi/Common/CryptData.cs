using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GTS.Clock.Presentation.WebApi.Common
{
    public class CryptData
    {
        private readonly TripleDESCryptoServiceProvider _des;

        public CryptData(string cryptoKey)
        {
            var iv = new byte[] { 2, 5, 2, 7, 0, 2, 1, 7 };
            _des = new TripleDESCryptoServiceProvider();
            var md5 = new MD5CryptoServiceProvider();

            _des.Key = md5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(cryptoKey));
            _des.IV = iv;
        }

        public string EncryptData(string value)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            byte[] inArray = _des.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(inArray, 0, inArray.Length).Replace("+", "2B%");
        }

        public string DecryptData(string encryptedValue)
        {
            if (encryptedValue == null)
                return null;
            byte[] inputBuffer = Convert.FromBase64String(encryptedValue.Replace("2B%", "+"));
            return Encoding.ASCII.GetString(_des.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
        }
    }
}
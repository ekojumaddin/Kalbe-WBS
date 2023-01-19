using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WBSBE.Common.Library
{
    public class ClsRijndael
    {
        private static readonly string stringKey = "~m4MaN9@K4lB3Nutr!tI0n@l5~";

        public static string EncryptToHTTPEncode(string stringInputText)
        {
            return HttpUtility.UrlEncode(Encrypt(stringInputText));
        }

        public static string Encrypt(string stringInputText)
        {
            RijndaelManaged RijndaelCipher = new();
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(stringInputText);
            byte[] Salt = Encoding.ASCII.GetBytes(stringKey.Length.ToString());
            PasswordDeriveBytes SecretKey = new(stringKey, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new();
            CryptoStream cryptoStream = new(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(CipherBytes);
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="stringInputText"></param>
        /// <param name="stringKey"></param>
        /// <returns></returns>
        public static string Encrypt(string stringInputText, string stringKey)
        {
            RijndaelManaged RijndaelCipher = new();
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(stringInputText);
            byte[] Salt = Encoding.ASCII.GetBytes(stringKey.Length.ToString());
            PasswordDeriveBytes SecretKey = new(stringKey, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new();
            CryptoStream cryptoStream = new(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(CipherBytes);
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="stringInputText"></param>
        /// <returns></returns>
        public static string Decrypt(string stringInputText)
        {
            RijndaelManaged RijndaelCipher = new();
            byte[] EncryptedData = Convert.FromBase64String(stringInputText);
            byte[] Salt = Encoding.ASCII.GetBytes(stringKey.Length.ToString());
            PasswordDeriveBytes SecretKey = new(stringKey, Salt);
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new(EncryptedData);
            CryptoStream cryptoStream = new(memoryStream, Decryptor, CryptoStreamMode.Read);
            byte[] PlainText = new byte[EncryptedData.Length];
            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="stringInputText"></param>
        /// <param name="stringKey"></param>
        /// <returns></returns>
        public static string Decrypt(string stringInputText, string stringKey)
        {
            RijndaelManaged RijndaelCipher = new();
            byte[] EncryptedData = Convert.FromBase64String(stringInputText);
            byte[] Salt = Encoding.ASCII.GetBytes(stringKey.Length.ToString());
            PasswordDeriveBytes SecretKey = new(stringKey, Salt);
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new(EncryptedData);
            CryptoStream cryptoStream = new(memoryStream, Decryptor, CryptoStreamMode.Read);
            byte[] PlainText = new byte[EncryptedData.Length];
            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
        }
    }
}

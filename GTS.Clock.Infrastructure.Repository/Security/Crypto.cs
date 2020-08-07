using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;


/// <summary>
/// Summary description for Crypto
/// </summary>
public class Crypto
{
	byte[] keybytes, iv;
	public Crypto()
	{
		//
		// TODO: Add constructor logic here
		//
		keybytes = System.Text.Encoding.UTF8.GetBytes("www.ghadirco.net");
		iv = System.Text.Encoding.UTF8.GetBytes("1234567891234567");
	}

	 
       
	

		public  string DecryptStringAES(string encryptedString)
		{
			encryptedString = encryptedString.Replace("$$$###$$$!@#$%^&*()", "");
			
			

			// Decrypt the bytes to a string.
			var roundtrip = System.Text.Encoding.UTF8.GetBytes(encryptedString);

			//DECRYPT FROM CRIPTOJS
			var encrypted = Convert.FromBase64String(encryptedString);
			var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
			return decriptedFromJavascript;
				
		}
		private  byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
		{
			// Check arguments.
			if (plainText == null || plainText.Length <= 0)
			{
				throw new ArgumentNullException("plainText");
			}
			if (key == null || key.Length <= 0)
			{
				throw new ArgumentNullException("key");
			}
			if (iv == null || iv.Length <= 0)
			{
				throw new ArgumentNullException("key");
			}
			byte[] encrypted;
			// Create a RijndaelManaged object
			// with the specified key and IV.
			using (var rijAlg = new RijndaelManaged())
			{
				rijAlg.Mode = CipherMode.CBC;
				rijAlg.Padding = PaddingMode.PKCS7;
				rijAlg.FeedbackSize = 128;

				rijAlg.Key = key;
				rijAlg.IV = iv;

				// Create a decrytor to perform the stream transform.
				var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

				// Create the streams used for encryption.
				using (var msEncrypt = new MemoryStream())
				{
					using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (var swEncrypt = new StreamWriter(csEncrypt))
						{
							//Write all data to the stream.
							swEncrypt.Write(plainText);
						}
						encrypted = msEncrypt.ToArray();
					}
				}
			}
			return encrypted;
		}
		private  string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
		{
			// Check arguments.
			if (cipherText == null || cipherText.Length <= 0)
			{
				throw new ArgumentNullException("cipherText");
			}
			if (key == null || key.Length <= 0)
			{
				throw new ArgumentNullException("key");
			}
			if (iv == null || iv.Length <= 0)
			{
				throw new ArgumentNullException("key");
			}

			// Declare the string used to hold
			// the decrypted text.
			string plaintext = null;

			// Create an RijndaelManaged object
			// with the specified key and IV.
			using (var rijAlg = new RijndaelManaged())
			{
				//Settings
				rijAlg.Mode = CipherMode.CBC;
				rijAlg.Padding = PaddingMode.PKCS7;
				rijAlg.FeedbackSize = 128;

				rijAlg.Key = key;
				rijAlg.IV = iv;

				// Create a decrytor to perform the stream transform.
				var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

				// Create the streams used for decryption.
				using (var msDecrypt = new MemoryStream(cipherText))
				{
					using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (var srDecrypt = new StreamReader(csDecrypt))
						{
							// Read the decrypted bytes from the decrypting stream
							// and place them in a string.
							plaintext = srDecrypt.ReadToEnd();
						}
					}
				}
			}

			return plaintext;
		}
}
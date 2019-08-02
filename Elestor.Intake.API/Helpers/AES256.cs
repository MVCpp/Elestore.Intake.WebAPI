using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Elestor.Intake.API.Helpers
{
    public static class AES256
    {
        /// <summary>
        /// Key encription for generate the strings in the class
        /// </summary>
        private static string _encryptionKey = "@EEL3333333sssssttttttttttoooRRRRRRRR";

        /// <summary>
        /// Function for Encrypt the string or value
        /// </summary>
        /// <param name="encryptString">string who was processed for encrypt</param>
        /// <returns></returns>
        public static string Encrypt(this string encryptString)
        {
            try
            {
                byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);

                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_encryptionKey, new byte[]
                    {
                        0x49, 0x76, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                    });

                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        encryptString = Convert.ToBase64String(ms.ToArray());
                    }
                }

                return encryptString;
            }
            catch (System.FormatException exFrm)
            {
                //WriteException.WriteError("Format encrypt exception: " + exFrm.Message);
            }
            catch (System.Exception ex)
            {
                //WriteException.WriteError("Error general for encrypt: " + ex.Message);
            }

            return string.Empty;
        }
        /// <summary>
        /// Functio for decrypt the string or value
        /// </summary>
        /// <param name="cipherText">string encrypted</param>
        /// <returns>string decrypted</returns>
        public static string Decrypt(this string cipherText)
        {
            try
            {
                cipherText = cipherText.Replace(" ", "+");

                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_encryptionKey, new byte[]
                    {
                        0x49, 0x76, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                    });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }

                return cipherText;
            }
            catch (System.FormatException exFrm)
            {
                //WriteException.WriteError("Format decryption exception: " + exFrm.Message);
            }
            catch (System.Exception ex)
            {
                //WriteException.WriteError("Error general for decrypt: " + ex.Message);
            }

            return string.Empty;
        }



    }
}

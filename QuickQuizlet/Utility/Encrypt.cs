using System;
using System.IO;
using System.Security.Cryptography;

namespace QuickQuizlet.Utility
{
    class Encrypt
    {
        private static string key = "AksakAKJKNSKNiwialai2oasSsajj3Ss";
        private static string iv  = "AksakAKJKNSKNiwiWewe355sSsajj3Ss";

        static private RijndaelManaged getRijndaelManaged()
        {
            RijndaelManaged rm = new RijndaelManaged();
            rm.Padding = PaddingMode.Zeros;
            rm.Mode = CipherMode.CBC;
            rm.KeySize = 256;
            rm.BlockSize = 256;
            rm.Key = System.Text.Encoding.UTF8.GetBytes(Encrypt.key);
            rm.IV = System.Text.Encoding.UTF8.GetBytes(Encrypt.iv);
            return rm;
        }
        public static string encode(string strSource)
        {
            ICryptoTransform ts = Encrypt.getRijndaelManaged().CreateEncryptor();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms,ts,CryptoStreamMode.Write);
            Byte[] toEncrypt = System.Text.Encoding.UTF8.GetBytes(strSource);
            cs.Write(toEncrypt,0,toEncrypt.Length);
            cs.FlushFinalBlock();
            Byte[] encrypted = ms.ToArray();
            return Convert.ToBase64String(encrypted);
        }

        public static string decode(string strEncrypted)
        {
            Byte[] byteEncrypted = Convert.FromBase64String(strEncrypted);
            ICryptoTransform ts = Encrypt.getRijndaelManaged().CreateDecryptor();
            Byte[] decBytes = ts.TransformFinalBlock(byteEncrypted,0,byteEncrypted.Length);

            for (int idx = 0; idx < decBytes.Length - 1; idx++)
            {
                if (decBytes[idx] == 0)
                {
                    Array.Resize<byte>(ref decBytes, idx);
                    break;
                }
            }
            return System.Text.Encoding.UTF8.GetString(decBytes);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickQuizlet.Entity;
using System.IO;


namespace QuickQuizlet.Utility
{
    class CachesMgr
    {
        static string cacheDir = Directory.GetCurrentDirectory() + "\\caches\\";

        public static bool cacheExist(string cacheName)
        {
            if(!Directory.Exists(cacheDir))
            {
                Directory.CreateDirectory(cacheDir);
            }
            return File.Exists(cacheDir + cacheName);
        }

        public static bool cacheUserExist(string userName)
        {
            return CachesMgr.cacheExist("u_" + userName);
        }

        public static bool cacheSetExist(string setId)
        {
            return CachesMgr.cacheExist("s_" + setId);
        }

        public static List<SetDetail> getCacheSetOfUser(string userName)
        {
            string filePath = cacheDir + "u_" + userName;
            if (File.Exists(filePath))
            {
                string data = File.ReadAllText(filePath);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<SetDetail>>(data);
            }
            else
                return new List<SetDetail>();
        }

        public static void writeCacheSetOfUser(string userName, List<SetDetail>setOfUser)
        {
            string filePath = cacheDir + "u_" + userName;
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(setOfUser);
            File.WriteAllText(filePath, jsonData);
        }

        public static SetDetail getCacheSet(string setId)
        {
            string filePath = cacheDir + "s_" + setId;
            if (File.Exists(filePath))
            {
                string data = File.ReadAllText(filePath);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<SetDetail>(data);
            }
            else
                return new SetDetail();
        }

        public static void writeCacheSet(string setId, SetDetail setData)
        {
            string filePath = cacheDir + "s_" + setId;
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(setData);
            File.WriteAllText(filePath, jsonData);
        }
    }
}

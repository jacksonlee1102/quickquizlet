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

        public static bool cacheUserSetsExist(string userName)
        {
            return CachesMgr.cacheExist("us_" + userName);
        }

        public static bool cacheSetExist(string setId)
        {
            return CachesMgr.cacheExist("s_" + setId);
        }

        public static List<SetDetail> getCacheSetsOfUser(string userName)
        {
            string filePath = cacheDir + "us_" + userName;
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
            string filePath = cacheDir + "us_" + userName;
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

        internal static void writeCacheUserInfo(string username, UserDetail userDetail)
        {
            string filePath = cacheDir + "ui_" + username;
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(userDetail);
            File.WriteAllText(filePath, jsonData);
        }

        public static UserDetail getCacheUserInfo(string userName)
        {
            string filePath = cacheDir + "ui_" + userName;
            if (File.Exists(filePath))
            {
                string data = File.ReadAllText(filePath);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetail>(data);
            }
            else
                return new UserDetail();
        }
    }
}

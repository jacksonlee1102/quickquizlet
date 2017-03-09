using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickQuizlet.Entity;
using System.IO;

namespace QuickQuizlet.Utility
{
    class SettingMgr
    {
        static string settingFilePath = Directory.GetCurrentDirectory() + "\\setting.q";

        public static bool fileExist()
        {
            return File.Exists(settingFilePath);
        }

        public static Setting read()
        {
            if (File.Exists(settingFilePath))
            {
                string encryptedSetting = File.ReadAllText(settingFilePath);
                string decryptedSetting = Encrypt.decode(encryptedSetting);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Setting>(decryptedSetting);
            }
            return new Setting();
        }

        public static void write(Setting setting)
        {
            string jsonSetting = Newtonsoft.Json.JsonConvert.SerializeObject(setting);
            string encryptedSetting = Encrypt.encode(jsonSetting);
            File.WriteAllText(settingFilePath, encryptedSetting);
        }
    }
}

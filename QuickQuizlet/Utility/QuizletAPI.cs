using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickQuizlet.Entity;

namespace QuickQuizlet.Utility
{
    class QuizletAPI
    {
        const string BASE_URL = "https://api.quizlet.com/2.0/";

        public SetDetail getSetDetail(string setId, string clientId)
        {
            if (CachesMgr.cacheSetExist(setId))
            {
                return CachesMgr.getCacheSet(setId);
            }
            else
            {
                string url = BASE_URL + "sets/" + setId + "?client_id=" + clientId + "&whitespace=0";
                string strResponse = HTTPRequest.get(url);
                SetDetail setDetail =  Newtonsoft.Json.JsonConvert.DeserializeObject<SetDetail>(strResponse);
                CachesMgr.writeCacheSet(setId, setDetail);
                return setDetail;
            }
        }

        public UserDetail getUserSets(string username, string clientId)
        {
            if (CachesMgr.cacheUserInfoExist(username))
            {
                return CachesMgr.getCacheUserInfo(username);
            }
            else
            {
                string url = BASE_URL + "users/" + username + "?client_id=" + clientId + "&whitespace=0";
                string strResponse = HTTPRequest.get(url);
                //List<SetDetail> listSets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SetDetail>>(strResponse);
                UserDetail listSets = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetail>(strResponse);
                CachesMgr.writeCacheUserInfo(username, listSets);
                return listSets;
            }
        }

    }
}

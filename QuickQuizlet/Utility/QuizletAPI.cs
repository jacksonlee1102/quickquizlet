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
            string url = BASE_URL + "sets/" + setId + "?client_id=" + clientId + "&whitespace=0";
            string strResponse = HTTPRequest.get(url);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<SetDetail>(strResponse);
        }

        public List<SetDetail> getUserSets(string username, string clientId)
        {
            string url = BASE_URL + "users/"+username+"/sets?client_id=" + clientId + "&whitespace=0";
            string strResponse = HTTPRequest.get(url);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<SetDetail>>(strResponse);
        }

    }
}

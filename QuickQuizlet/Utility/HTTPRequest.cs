﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace QuickQuizlet.Utility
{
    class HTTPRequest
    {
        private const string URL = "https://api.quizlet.com/2.0/sets/158284560";
        private const string urlParameters = "";

        public static string get(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            
            return "";
        }
    }
}

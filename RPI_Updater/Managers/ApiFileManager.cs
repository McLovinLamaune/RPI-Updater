using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RPI_Updater.Managers
{    public class ApiFileManager
    {
        private static readonly Lazy<ApiFileManager> lazy = new Lazy<ApiFileManager>(() => new ApiFileManager());
        public static ApiFileManager Instance { get { return lazy.Value; } }

        LoggerManager logger = LoggerManager.Instance;
        private RestClient _client;
        private IRestRequest _request;

        private ApiFileManager()
        {
            _client = new RestClient("http://192.168.1.10:45455");
        }

        public bool DownloadFile(string serverFileName, string savePath)
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.DownloadFile(new Uri("http://192.168.1.10:45455/api/files?fileName=" + serverFileName), savePath);
                    return true;
                }
                catch
                {
                    return false;
                }             
            }
        }
    }
}

using System;
using System.Net;


namespace MultiThreaded_downloader
{
    struct DownloadableData
    {
        string _SourceUrl;
        long _minData;
        long _maxData;

        public DownloadableData(string Source, Int64 minData, Int64 maxData)
        {
            _SourceUrl = Source;
            _minData = minData;
            _maxData = maxData;

        }

        public WebResponse request()
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(_SourceUrl);
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36";
            webRequest.AddRange(_minData, _maxData);
            WebResponse webResponse = webRequest.GetResponse();
         
            return webResponse;
        }
        
    }
}

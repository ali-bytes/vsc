using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace VideoConverter.Services
{
    public class OvcReq
    {
        public string Host { get; set; }
        public string Referer { get; set; }
    }

    public class Result
    {
        public string id_process { get; set; }
        public string status { get; set; }
        public string serverId { get; set; }
        public string flvPath { get; set; }
        public string title { get; set; }
        public string titleFileName { get; set; }
        public string keyHash { get; set; }
        public string serverUrl { get; set; }
        public string dPageId { get; set; }
        public object jobpc { get; set; }
        public string videoToDownload { get; set; }
        public string videoToDownloadEncoded { get; set; }
        public string videoPreview { get; set; }
        public object error { get; set; }
        public string thumbnail { get; set; }
        public string length { get; set; }
    }

    public class OvcRootObject
    {
        public Result result { get; set; }
    }
    public class OvcWebClient : WebClient
    {
        private string _host { get; set; }
        private string _referer { get; set; }

        public OvcWebClient()
        {

        }
        public OvcWebClient(OvcReq req)
        {
            _host = req.Host.Replace("http://", "");
            _referer = req.Referer;
        }
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Timeout = 60000;
            request.Method = WebRequestMethods.Http.Get;
            request.Host = _host;
            request.KeepAlive = true;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.UserAgent =
              "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";
            request.Referer = _referer;
            request.Headers.Add("Accept-Language", "en-US,en;q=0.8");

            return request;
        }
    }
    public class Sc9WebClient : WebClient
    {
        private string _host { get; set; }
        private string _referer { get; set; }

        public Sc9WebClient()
        {

        }
        public Sc9WebClient(OvcReq req)
        {
            //_host = req.Host.Replace("http://", "");
            //_referer = req.Referer;
        }
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Timeout = 60000;
            request.Method = WebRequestMethods.Http.Get;
            if (address.ToString().Contains("cf-media"))
            {
                request.Host = "cf-media.sndcdn.com";
            }
            else if (address.ToString().Contains("ec-media"))
            {
                request.Host = "ec-media.sndcdn.com";                
            }
            request.KeepAlive = true;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.UserAgent =
              "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";
            //request.Referer = _referer;
            request.Headers.Add("Accept-Language", "en-US,en;q=0.8");

            return request;
        }
    }
}
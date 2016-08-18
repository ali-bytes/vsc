using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace VideoConverter.Services
{
    public class GZipWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Timeout = 60000;

            return request;
        }
    }
}
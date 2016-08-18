using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Boilerplate.Web.Mvc.Filters;
using Elmah;
using Glimpse.Core.Extensions;
using HtmlAgilityPack;
using NewIspNL.Helpers;
using VideoConverter.Constants;
using VideoConverter.Model;
using VideoConverter.Services;

namespace VideoConverter.Controllers
{
   
    public class HomeController : BaseController
    {

        [Route("", Name = HomeControllerRoute.GetIndex)]
        public ActionResult Index()
        {
            return View(HomeControllerAction.Index);
        }

        public ActionResult ChangeLang(string lang)
        {
            new SiteLanguages().SetLanguage(lang ?? "en");
            return RedirectToAction(HomeControllerAction.Index);
        }
        public string GetMp3(string url, string title)
        {
            // youtube mp3 download
            try
            {
                var mp3Url = GetMp3UrlOvc(url.Trim());
                var n= DownloadMp3(mp3Url, title);
                return n;
            }
            catch (Exception exception)
            {
                // Log to Elmah.
                ErrorSignal.FromCurrentContext().Raise(exception);
            }
            return null;

        }

        public string DownloadMp3(Mp3Data mp3Data, string title)
        {
           OvcReq oreq=new OvcReq()
           {
               Host = mp3Data.Host,
               Referer = mp3Data.Referer
           };
            String guid = Guid.NewGuid().ToString("N").Substring(0, 10); 
            var t = guid + ".mp3";
            var now = DateTime.Now;
            var deleteTime = DateTime.Now.AddHours(1);
            var n = QueryStringSecurity.Encrypt(t);
            var pth = Server.MapPath("~/download/" + t);
           
            if (System.IO.File.Exists(pth))
            {
                return "/tb/"+n;
            }
            using (var webClient = new OvcWebClient(oreq))
            {
                //Thread.Sleep(10000);
                webClient.DownloadFile(new Uri(mp3Data.Link), pth);
            }
           
            if (System.IO.File.Exists(pth))
            {

                using (var context = new VidFromContext())
                {
                    context.Mp3Record.Add(new Mp3Record
                    {
                        Name = t,
                        DownDate = now,
                        DeleteTime = deleteTime,
                        Path = pth
                    });
                    context.SaveChanges();
                }
                return "/tb/" + n;
            }
            return null;
        }
        public Mp3Data GetMp3UrlOvc(string url)
        {
            var mp3Data = new Mp3Data();
            url = Uri.EscapeDataString(url);
            //var encodedUrl = url.Replace("%2F", "/");
            var cookieJar = new CookieContainer();

            // first request 
            //"function=validate&args%5Bdummy%5D=1&args%5BurlEntryUser%5D=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Dduubkcex7PM&args%5BfromConvert%5D=youtubeconverter&args%5BrequestExt%5D=mp3&args%5BnbRetry%5D=0&args%5BvideoResolution%5D=-1&args%5BaudioBitrate%5D=0&args%5BaudioFrequency%5D=0&args%5Bchannel%5D=stereo&args%5Bvolume%5D=0&args%5BstartFrom%5D=-1&args%5BendTo%5D=-1&args%5Bcustom_resx%5D=-1&args%5Bcustom_resy%5D=-1&args%5BadvSettings%5D=false&args%5BaspectRatio%5D=-1"
             //function=validate&args%5Bdummy%5D=1&args%5BurlEntryUser%5D=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Dduubkcex7PM&args%5BfromConvert%5D=youtubeconverter&args%5BrequestExt%5D=mp3&args%5BnbRetry%5D=0&args%5BvideoResolution%5D=-1&args%5BaudioBitrate%5D=0&args%5BaudioFrequency%5D=0&args%5Bchannel%5D=stereo&args%5Bvolume%5D=0&args%5BstartFrom%5D=-1&args%5BendTo%5D=-1&args%5Bcustom_resx%5D=-1&args%5Bcustom_resy%5D=-1&args%5BadvSettings%5D=false&args%5BaspectRatio%5D=-1
            var fString = string.Format("function=validate&args%5Bdummy%5D=1&args%5BurlEntryUser%5D=" + url + "&args%5BfromConvert%5D=youtubeconverter&args%5BrequestExt%5D=mp3&args%5BnbRetry%5D=0&args%5BvideoResolution%5D=-1&args%5BaudioBitrate%5D=0&args%5BaudioFrequency%5D=0&args%5Bchannel%5D=stereo&args%5Bvolume%5D=0&args%5BstartFrom%5D=-1&args%5BendTo%5D=-1&args%5Bcustom_resx%5D=-1&args%5Bcustom_resy%5D=-1&args%5BadvSettings%5D=false&args%5BaspectRatio%5D=-1");
            var fReq =
                (HttpWebRequest)WebRequest.Create("http://www.onlinevideoconverter.com/webservice");
            fReq.CookieContainer = cookieJar;
            fReq.Method = WebRequestMethods.Http.Post;
            fReq.Host = "www.onlinevideoconverter.com";
            fReq.KeepAlive = true;
            fReq.Accept = "application/json, text/javascript, */*; q=0.01";
            fReq.Headers.Add("Origin", "http://www.onlinevideoconverter.com");
            fReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
            fReq.UserAgent =
              "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";
            fReq.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            fReq.Referer = "http://www.onlinevideoconverter.com/video-converter";
            fReq.Headers.Add("Accept-Language", "en-US,en;q=0.8");
         
            var fbytedata = Encoding.UTF8.GetBytes(fString);
            fReq.ContentLength = fbytedata.Length;
            var freqStream = fReq.GetRequestStream();
            freqStream.Write(fbytedata, 0, fbytedata.Length);
            freqStream.Close();

            var ftxt = "";
            using (var httpWebResponse2 = (HttpWebResponse)fReq.GetResponse())
            {
                using (var responseStream2 = httpWebResponse2.GetResponseStream())
                {
                    var sr = new StreamReader(responseStream2, Encoding.UTF8);
                    ftxt = sr.ReadToEnd();
                }
            }
            OvcRootObject fObj = Newtonsoft.Json.JsonConvert.DeserializeObject<OvcRootObject>(ftxt);

            // second request process video
            if (fObj.result.status == "incache")
            {
                return GetMp3FromCache(fObj);
            }
            //function=processVideo&args%5Bdummy%5D=1&args%5BurlEntryUser%5D=vM&args%5BfromConvert%5D=youtubeconverter&args%5BrequestExt%5D=mp3&args%5BserverId%5D=572777317a781-572777317a7c5&args%5BnbRetry%5D=0&args%5Btitle%5D=Dil+e+beqarar+promo+5&args%5BkeyHash%5D=duubkcex7PM&args%5BserverUrl%5D=http%3A%2F%2Fsv53.onlinevideoconverter.com&args%5Bid%5D=-1&args%5Bsid%5D=1408908&args%5BvideoResolution%5D=-1&args%5BaudioBitrate%5D=0&args%5BaudioFrequency%5D=0&args%5Bchannel%5D=stereo&args%5Bvolume%5D=0&args%5BstartFrom%5D=-1&args%5BendTo%5D=-1&args%5Bcustom_resx%5D=-1&args%5Bcustom_resy%5D=-1&args%5BadvSettings%5D=false&args%5BaspectRatio%5D=-1
           // new one
            //function=processVideo&args%5Bdummy%5D=1&args%5BurlEntryUser%5D=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DH4fDSb7C-Qo&args%5BfromConvert%5D=youtubeconverter&args%5BrequestExt%5D=mp3&args%5BserverId%5D=577a957dad9a6-577a957dad9e1&args%5BnbRetry%5D=0&args%5Btitle%5D=Once+Upon+a+Time+5x21+Promo+Season+5+Episode+21+Promo&args%5BkeyHash%5D=H4fDSb7C-Qo&args%5BserverUrl%5D=http%3A%2F%2Fsv54.onlinevideoconverter.com&args%5Bid_process%5D=b1i8j9i8g6c2i8c2&args%5BvideoResolution%5D=-1&args%5BaudioBitrate%5D=0&args%5BaudioFrequency%5D=0&args%5Bchannel%5D=stereo&args%5Bvolume%5D=0&args%5BstartFrom%5D=-1&args%5BendTo%5D=-1&args%5Bcustom_resx%5D=-1&args%5Bcustom_resy%5D=-1&args%5BadvSettings%5D=false&args%5BaspectRatio%5D=-1
            var poststring = string.Format("function=processVideo&args%5Bdummy%5D=1&args%5BurlEntryUser%5D=" + url + "&args%5BfromConvert%5D=youtubeconverter&args%5BrequestExt%5D=mp3&args%5BserverId%5D=" + fObj.result.serverId + "&args%5BnbRetry%5D=0&args%5Btitle%5D=" + fObj.result.title + "&args%5BkeyHash%5D=" + fObj.result.keyHash + "&args%5BserverUrl%5D=" + fObj.result.serverUrl + "&args%5Bid_process%5D=" + fObj.result.id_process + "&args%5BvideoResolution%5D=-1&args%5BaudioBitrate%5D=0&args%5BaudioFrequency%5D=0&args%5Bchannel%5D=stereo&args%5Bvolume%5D=0&args%5BstartFrom%5D=-1&args%5BendTo%5D=-1&args%5Bcustom_resx%5D=-1&args%5Bcustom_resy%5D=-1&args%5BadvSettings%5D=false&args%5BaspectRatio%5D=-1");
            var httpRequest =
                (HttpWebRequest)WebRequest.Create("http://www.onlinevideoconverter.com/webservice");
            httpRequest.CookieContainer = cookieJar;
            httpRequest.Method = WebRequestMethods.Http.Post;
            httpRequest.Host = "www.onlinevideoconverter.com";
            httpRequest.KeepAlive = true;
            httpRequest.Accept = "application/json, text/javascript, */*; q=0.01";
            httpRequest.Headers.Add("Origin", "http://www.onlinevideoconverter.com");
            httpRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            httpRequest.UserAgent =
              "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";

            httpRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            httpRequest.Referer = "http://www.onlinevideoconverter.com/video-converter";
            httpRequest.Headers.Add("Accept-Language", "en-US,en;q=0.8");
          
            var bytedata = Encoding.UTF8.GetBytes(poststring);
            httpRequest.ContentLength = bytedata.Length;
            var requestStream = httpRequest.GetRequestStream();
            requestStream.Write(bytedata, 0, bytedata.Length);
            requestStream.Close();

            var text = "";
            using (var httpWebResponse2 = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (var responseStream2 = httpWebResponse2.GetResponseStream())
                {
                    var sr = new StreamReader(responseStream2, Encoding.UTF8);
                    text = sr.ReadToEnd();
                }
            }
            OvcRootObject ob = Newtonsoft.Json.JsonConvert.DeserializeObject<OvcRootObject>(text);

            // third request get download page
            var Greq =
               (HttpWebRequest)WebRequest.Create("http://www.onlinevideoconverter.com/download?id=" + ob.result.dPageId + "&ext=mp3&v=" + ob.result.keyHash + "");
            //http://www.onlinevideoconverter.com/download?id=g6d3a0e4g6a0i8c2&ext=mp3&v=duubkcex7PM
            Greq.CookieContainer = cookieJar;
            Greq.Method = WebRequestMethods.Http.Get;
            Greq.Host = "www.onlinevideoconverter.com";
            Greq.KeepAlive = true;
            Greq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            Greq.UserAgent =
              "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";
            Greq.Referer = "http://www.onlinevideoconverter.com/video-converter";
            Greq.Headers.Add("Accept-Language", "en-US,en;q=0.8");
           
           // Greq.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
           //Greq.Headers.Add("X-Requested-With", "XMLHttpRequest");
            Greq.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            var Gtxt = "";
            using (var GreqRes = (HttpWebResponse)Greq.GetResponse())
            {
                using (var responseStream2 = GreqRes.GetResponseStream())
                {
                    var sr = new StreamReader(responseStream2, Encoding.UTF8);
                    Gtxt = sr.ReadToEnd();
                }
            }
            //----

            var doc = new HtmlDocument();

            doc.OptionAutoCloseOnEnd = true;
            doc.OptionFixNestedTags = true;
            doc.LoadHtml(Gtxt);
           
            var inputChk = doc.DocumentNode.SelectSingleNode("//*[@id='downloadq']");
            var href = string.Empty;
            if (inputChk != null)
            {
                if (inputChk.Attributes["href"] != null)
                {
                    href = inputChk.Attributes["href"].Value;
                }
            }
            var titleNode = doc.DocumentNode.SelectSingleNode("//*[@id='stepProcessEnd']/div[1]/div[2]/div/div/div/div[1]/div[2]/p[1]/b/a");
            var title = string.Empty;
            if (titleNode != null)
            {
                title = titleNode.InnerText.Trim();
            }
            if (string.IsNullOrEmpty(href))
            {
                return null;
            }

            string[] words = Regex.Split(href, "&title=");
            string[] titles = Regex.Split(words[1], "&");
            string esTitle = Uri.EscapeDataString(titles[0]);

            mp3Data.Name = Uri.EscapeDataString(title);
            mp3Data.Link = words[0] + "&title=" + esTitle + "&" + titles[1];
            mp3Data.Referer = Greq.Address.ToString();

            string[] hostwords = Regex.Split(href, "/download");
            mp3Data.Host = hostwords[0];
            return mp3Data;
        }
        public Mp3Data GetMp3FromCache(OvcRootObject ob)
        {
            ob.result.keyHash = ob.result.keyHash.Replace("m:", "");
            var mp3Data = new Mp3Data();
          
            var cookieJar = new CookieContainer();
            var Greq =
              (HttpWebRequest)WebRequest.Create("http://www.onlinevideoconverter.com/download?id=" + ob.result.dPageId + "&ext=mp3&v=" + ob.result.keyHash + "");
            //http://www.onlinevideoconverter.com/download?id=g6d3a0e4g6a0i8c2&ext=mp3&v=duubkcex7PM
            Greq.CookieContainer = cookieJar;
            Greq.Method = WebRequestMethods.Http.Get;
            Greq.Host = "www.onlinevideoconverter.com";
            Greq.KeepAlive = true;
            Greq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            Greq.UserAgent =
              "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";
            Greq.Referer = "http://www.onlinevideoconverter.com/video-converter";
            Greq.Headers.Add("Accept-Language", "en-US,en;q=0.8");

            // Greq.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //Greq.Headers.Add("X-Requested-With", "XMLHttpRequest");
            Greq.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            var Gtxt = "";
            using (var GreqRes = (HttpWebResponse)Greq.GetResponse())
            {
                using (var responseStream2 = GreqRes.GetResponseStream())
                {
                    var sr = new StreamReader(responseStream2, Encoding.UTF8);
                    Gtxt = sr.ReadToEnd();
                }
            }
            //----

            var doc = new HtmlDocument();

            doc.OptionAutoCloseOnEnd = true;
            doc.OptionFixNestedTags = true;
            doc.LoadHtml(Gtxt);

            var inputChk = doc.DocumentNode.SelectSingleNode("//*[@id='downloadq']");
            var href = string.Empty;
            if (inputChk != null)
            {
                if (inputChk.Attributes["href"] != null)
                {
                    href = inputChk.Attributes["href"].Value;
                }
            }
            var titleNode = doc.DocumentNode.SelectSingleNode("//*[@id='stepProcessEnd']/div[1]/div[2]/div/div/div/div[1]/div[2]/p[1]/b/a");
            var title = string.Empty;
            if (titleNode != null)
            {
                title = titleNode.InnerText.Trim();
            }
            if (string.IsNullOrEmpty(href))
            {
                return null;
            }
            
            string[] words = Regex.Split(href, "&title=");
             string[] titles = Regex.Split(words[1], "&");
            string esTitle= Uri.EscapeDataString(titles[0]);

            mp3Data.Name = Uri.EscapeDataString(title);
            mp3Data.Link = words[0] + "&title=" + esTitle + "&" + titles[1];
            mp3Data.Referer = Greq.Address.ToString();

            string[] hostwords = Regex.Split(href, "/download");
            mp3Data.Host = hostwords[0];
            return mp3Data;
        }
        public Mp3Data GetMp3Url(string url)
        {
            var mp3Data = new Mp3Data();
            //string url = Uri.EscapeDataString(value);
            //var encodedUrl = url.Replace("%2F", "/");

            var cookieJar = new CookieContainer();
            //"_token=DMZDNS8Rs7IGhAfYTlQGIBgaySHCICDF4TQAgrHY&url=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3D3WJY_zS9ApM&submit="
            var poststring = string.Format("_token=DMZDNS8Rs7IGhAfYTlQGIBgaySHCICDF4TQAgrHY&url=" + url + "&submit=");
            var httpRequest =
                (HttpWebRequest)WebRequest.Create("http://youtubemp3.io/converter");
            httpRequest.CookieContainer = cookieJar;
            httpRequest.Method = WebRequestMethods.Http.Post;
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            httpRequest.KeepAlive = true;
            httpRequest.Headers.Add("Accept-Language", "en-us,en;q=0.8");
            httpRequest.UserAgent =
                "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";
            var bytedata = Encoding.UTF8.GetBytes(poststring);
            httpRequest.ContentLength = bytedata.Length;
            var requestStream = httpRequest.GetRequestStream();
            requestStream.Write(bytedata, 0, bytedata.Length);
            requestStream.Close();

            var text = "";
            using (var httpWebResponse2 = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (var responseStream2 = httpWebResponse2.GetResponseStream())
                {
                    var sr = new StreamReader(responseStream2, Encoding.UTF8);
                    text = sr.ReadToEnd();
                }
            }


            var doc = new HtmlDocument();

            doc.OptionAutoCloseOnEnd = true;
            doc.OptionFixNestedTags = true;
            doc.LoadHtml(text);

            var inputChk = doc.DocumentNode.SelectSingleNode("//*[@id='download-btn']");
            var href = string.Empty;
            if (inputChk != null)
            {
                if (inputChk.Attributes["href"] != null)
                {
                    href = inputChk.Attributes["href"].Value;
                }
            }
            var titleNode = doc.DocumentNode.SelectSingleNode("//*[@id='preview']/div[1]/p[1]/text()");
            var title = string.Empty;
            if (titleNode != null)
            {
                title = titleNode.InnerText.Trim();
            }
            if (string.IsNullOrEmpty(href))
            {
                return null;
            }
            mp3Data.Name = Uri.EscapeDataString(title);
            mp3Data.Link = href;
            return mp3Data;
        }


        [Route("mp3")]
        public ActionResult Mp3(string n, string t)
        {
            // for render mp3 files for soundCloud and youtube to mp3
            if (!string.IsNullOrEmpty(n))
            {
                n = QueryStringSecurity.Decrypt(n);
                var pth = Server.MapPath("~/download/" + n);
                if (string.IsNullOrEmpty(t))
                {
                    t = n;
                }
                else
                {
                    t = t + ".mp3";
                }
                //string filename = Path.GetFileName(pth);
                //string filename = Uri.UnescapeDataString(n);

                var buff = new byte[0];
                using (var fs = new FileStream(pth, FileMode.Open, FileAccess.Read))
                {
                    //fs.Position = 0;
                    var br = new BinaryReader(fs);
                    long numBytes = new FileInfo(pth).Length;
                    buff = br.ReadBytes((int)numBytes);
                };
                //var cd = new System.Net.Mime.ContentDisposition
                //{
                //    // for example foo.bak
                //    FileName = t ?? n + ".mp3",

                //    // always prompt the user for downloading, set to true if you want 
                //    // the browser to try to show the file inline
                //    Inline = false,
                //};
                //Response.AppendHeader("Content-Disposition", cd.ToString());
                return File(buff, "audio/mpeg", t );
            }
            return null;
        }

        [Route("how-to-download-facebook-videos", Name = "HowToFacebook")]
        public ActionResult HowToFacebook()
        {
            return View("HowToFacebook");
        }

        [NoTrailingSlash]
        [AllowAnonymous]
        [OutputCache(CacheProfile = CacheProfileName.RobotsText)]
        [Route("robots.txt", Name = "RobotsText")]
        public ActionResult RobotsText()
        {
            return File(Server.MapPath("~/robots.txt"), "text/plain");
        }

        public ActionResult GetVid(string q)
        {

            return View();
        }
        [Route("terms")]
        public ActionResult Terms()
        {

            return View();
        }
        public ActionResult Down(string url)
        {
             if (string.IsNullOrEmpty(url) && !Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                return null;
            //     YoutubeResult rs= new YoutubeResult()
            //{
            //    Title = "test title",
            //    Duration = "111",
            //    DownloadLinks = new List<YoutubeLinks>()
            //    {
            //        new YoutubeLinks()
            //        {
            //            Link = "testlink",
            //            LinkTitle = "testlinktitle"
            //        }
            //    }
            //};
            //     return View(rs);
            }
            string urles = Uri.EscapeDataString(url.Trim());
            var encodedUrl = urles.Replace("%2F", "/");
            string pageString = GetVidUrls(encodedUrl);
            YoutubeResult yr = new YoutubeResult();
            yr = GetYoutubeLinks(pageString);
            return View(yr);
        }
        public YoutubeResult GetYoutubeLinks(string pagrHtml)
        {
            var doc = new HtmlDocument();

            doc.OptionAutoCloseOnEnd = true;
            doc.OptionFixNestedTags = true;
            doc.LoadHtml(pagrHtml);

            var title = string.Empty;
            var owner = string.Empty;
            var duration = string.Empty;
            var imgLink = string.Empty;
            List<YoutubeLinks> downloadLinks = new List<YoutubeLinks>();

            var titleNode = doc.DocumentNode.SelectSingleNode("//*[@id='info']/div[2]/a/h3");
            if (titleNode != null)
            {
                title = titleNode.InnerText.Trim();
            }
            var durationNode = doc.DocumentNode.SelectSingleNode("//*[@id='info']/div[2]/p[2]");
            if (durationNode != null)
            {

                duration = durationNode.InnerText.Trim();
            }

            var imgNode = doc.DocumentNode.SelectSingleNode("//*[@id='info']/div[1]/a/img");
            if (imgNode != null)
            {
                if (imgNode.Attributes["src"] != null)
                {
                    imgLink = imgNode.Attributes["src"].Value;
                }
            }

            var linksNode = doc.DocumentNode.SelectNodes("//*[@id='dl']/div[2]/ul/li");
            if (linksNode != null)
            {
                foreach (var li in linksNode)
                {
                    var a = string.Empty;
                    var b = li.SelectSingleNode("b");
                    var an = li.SelectSingleNode("a");
                    if (an.Attributes["href"] != null)
                    {
                        a = an.Attributes["href"].Value;
                    }
                    downloadLinks.Add(new YoutubeLinks()
                    {
                        Link = a,
                        LinkTitle = b.InnerText
                    });
                }

            }

            return new YoutubeResult()
            {
                Title = title,
                Duration = duration,
                DownloadLinks = downloadLinks,
                ImgLink = imgLink
            };

        }

        string GetVidUrls(string encodedUrl)
        {

            CookieContainer cookieJar = new CookieContainer();
            string ua =
                "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";


            //http://keepvid.com/?url=http%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Deni2UMjJiH8


            HttpWebRequest httpRequest =
                 (HttpWebRequest)
                     WebRequest.Create("http://keepvid.com/?url=" + encodedUrl);

            //httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            httpRequest.KeepAlive = true;
            //httpRequest.Headers.Add("Accept-Language", "en-us,en;q=0.8");

            httpRequest.Host = "keepvid.com";
            httpRequest.Referer = "http://www.keepvid.com/";
            httpRequest.CookieContainer = cookieJar;
            httpRequest.UserAgent = ua;
            httpRequest.Method = WebRequestMethods.Http.Get;
            httpRequest.Proxy = null;

            string strMessage = "";
            using (HttpWebResponse httpWebResponse2 = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream responseStream2 = httpWebResponse2.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(responseStream2, Encoding.UTF8);
                    strMessage = sr.ReadToEnd();
                    strMessage = strMessage.Substring(3);
                    strMessage = strMessage.Substring(0, strMessage.Length - 2);

                }
            }
            GetYoutubeLinks(strMessage);
            return strMessage;
        }
    }
    public class YoutubeResult
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Owner { get; set; }
        public string Duration { get; set; }
        public string ImgLink { get; set; }
        public List<YoutubeLinks> DownloadLinks { get; set; }
    }

    public class YoutubeLinks
    {
        public string Link { get; set; }
        public string LinkTitle { get; set; }
    }
}
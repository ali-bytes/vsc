using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Elmah;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VideoConverter.Services;


namespace VideoConverter.Controllers
{
    public class GetlkController : ApiController
    {
        // GET api/<controller>
        public string Getss()
        {
            try
            {
                CookieContainer cookieJar = new CookieContainer();
                string ua = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";

                HttpWebRequest httpRequest =
                    (HttpWebRequest)WebRequest.Create("http://srv1.keepvid.com/api/v2.php?url=www.youtube.com/watch%3Fv%3DjbzgeNsLg_Q&callback=jc&_=1452218059177");

                httpRequest.ContentType = "application/x-www-form-urlencoded";
                httpRequest.Accept = "application/json, text/javascript, */*";
                httpRequest.KeepAlive = true;
                httpRequest.Headers.Add("Accept-Language", "en-us,en;q=0.8");
                //httpRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
                httpRequest.Host = "srv1.keepvid.com";
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
                        //JObject jObject = JObject.Parse(strMessage);
                        //JToken jUser = jObject["url"];
                    }
                }

                return strMessage;

            }
            catch
            {

            }
            return "";
        }

        // GET api/<controller>/5
        public string Get(string url)
        {

            if (string.IsNullOrEmpty(url) && !Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                return null;
            }
            string urles = Uri.EscapeDataString(url.Trim());
            //var q = Request.RequestUri.ParseQueryString();
            var encodedUrl = urles.Replace("%2F", "/");
            //HttpContext.Current.Session["theUrl"] = encodedUrl;
            if (url.Contains("twitter.com"))
            {
                try
                {
                    return GetTwitterUrl(encodedUrl);
                }
                catch (Exception exception)
                {
                    // Log to Elmah.
                    ErrorSignal.FromCurrentContext().Raise(exception, HttpContext.Current);
                    try
                    {
                        return GetTwitterUrl(encodedUrl);
                    }
                    catch
                    {
                        try
                        {
                            return GetTwitterUrl(encodedUrl);
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
            }

            if (url.Contains("vimeo.com/"))
            {
                try
                {
                    return GetViemoLinks(encodedUrl);
                }
                catch (Exception exception)
                {
                    // Log to Elmah.
                    ErrorSignal.FromCurrentContext().Raise(exception, HttpContext.Current);
                    try
                    {
                        return GetViemoLinks(encodedUrl);
                    }
                    catch
                    {
                        try
                        {
                            return GetViemoLinks(encodedUrl);
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
            }

            if (url.Contains("soundcloud.com/"))
            {

                try
                {

                    var mp3Url = GetScUrl9(encodedUrl, url);
                    return Mp3Repo.DownloadMp3(mp3Url);

                }
                catch (Exception exception)
                {
                    // Log to Elmah.
                    ErrorSignal.FromCurrentContext().Raise(exception, HttpContext.Current);
                    try
                    {
                        var mp3Url = GetScUrl9(encodedUrl, url);
                        return Mp3Repo.DownloadMp3(mp3Url);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            if (url.Contains("facebook.com/"))
            {

                try
                {
                    var st=  GetFace(encodedUrl);
                    return st;

                }
                catch (Exception exception)
                {
                    // Log to Elmah.
                    ErrorSignal.FromCurrentContext().Raise(exception, HttpContext.Current);
                    try
                    {
                        return GetFace(encodedUrl);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }

            //try
            //{
            //    //var d = HttpContext.Current.Session["d"].ToString();
            //    //return GetVidUrls(encodedUrl);
            //}
            //catch (Exception exception)
            //{
            //    // Log to Elmah.
            //    ErrorSignal.FromCurrentContext().Raise(exception, HttpContext.Current);
            //    try
            //    {
            //        return GetVidUrls(encodedUrl);
            //    }
            //    catch
            //    {
            //        try
            //        {
            //            return GetVidUrls(encodedUrl);
            //        }
            //        catch
            //        {
            //            return null;
            //        }
            //    }
            //}
            return null;
        }
        string GetFace(string encodedUrl)
        {
            //URL=https%3A%2F%2Fwww.facebook.com%2Fmandoobtube%2Fvideos%2F619079001573602%2F
            string poststring = "URL=" + encodedUrl;
            CookieContainer cookieJar = new CookieContainer();
            string ua = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";


            HttpWebRequest httpRequest =
                (HttpWebRequest)
                    WebRequest.Create("http://www.fbdown.net/down.php");

            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            httpRequest.KeepAlive = true;
            httpRequest.Headers.Add("Accept-Language", "en-us,en;q=0.8");

            httpRequest.CookieContainer = cookieJar;
            httpRequest.UserAgent = ua;
            httpRequest.Method = WebRequestMethods.Http.Post;
            httpRequest.Proxy = null;

            byte[] bytedata = Encoding.UTF8.GetBytes(poststring);
            httpRequest.ContentLength = bytedata.Length;

            Stream requestStream = httpRequest.GetRequestStream();
            requestStream.Write(bytedata, 0, bytedata.Length);
            requestStream.Close();

            string strMessage = "";
            using (HttpWebResponse httpWebResponse2 = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream responseStream2 = httpWebResponse2.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(responseStream2, Encoding.UTF8);
                    strMessage = sr.ReadToEnd();

                }
            }

            HtmlDocument doc = new HtmlDocument();

            doc.OptionAutoCloseOnEnd = true;
            doc.OptionFixNestedTags = true;
            doc.LoadHtml(strMessage);
            //title
            HtmlNode bt = doc.DocumentNode.SelectSingleNode("//*[@id='scan']");
            string t = string.Empty;
            string img = string.Empty;
            string nq = string.Empty;
            string hq = string.Empty;
            if (bt != null)
            {
                var data =
                 from a in doc.DocumentNode.Descendants("a").Where(x => x.Attributes["onmouseover"] != null)
                select a;
                var d1 = data.FirstOrDefault();
                if (d1 != null && d1.Attributes["href"] != null)
                {
                    nq = d1.Attributes["href"].Value;
                }
                var a2 = data.LastOrDefault();
                if (a2 != null && a2.Attributes["href"] != null)
                {
                    hq = a2.Attributes["href"].Value;
                }

                t = bt.NextSibling.NextSibling.FirstChild.InnerText;
                HtmlNode imgnode = bt.PreviousSibling.PreviousSibling.PreviousSibling;
                if (imgnode != null && imgnode.Attributes["src"] != null)
                {
                    img = imgnode.Attributes["src"].Value;
                }
               
                Fb fb=new Fb()
                {
                    Title = t,
                    Image = img,
                    NormalQ = nq,
                    HdQ = hq
                };
                if (fb != null)
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    return js.Serialize(fb);
                }
            }           
            return null;
        }
    
        string GetTwitterUrl(string encodedUrl)
        {

            CookieContainer cookieJar = new CookieContainer();
            string ua = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";


            HttpWebRequest httpRequest =
                (HttpWebRequest)
                    WebRequest.Create("https://www.savedeo.com/download?url=" + encodedUrl);

            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            httpRequest.KeepAlive = true;
            httpRequest.Headers.Add("Accept-Language", "en-us,en;q=0.8");
            //httpRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            httpRequest.Host = "www.savedeo.com";

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

                }
            }

            HtmlDocument doc = new HtmlDocument();

            doc.OptionAutoCloseOnEnd = true;
            doc.OptionFixNestedTags = true;
            doc.LoadHtml(strMessage);

            HtmlNode inputChk = doc.DocumentNode.SelectSingleNode("//*[@id='main']/div/table/tbody/tr/td[2]/a");
            string href = string.Empty;
            if (inputChk != null)
            {
                if (inputChk.Attributes["href"] != null)
                {
                    href = inputChk.Attributes["href"].Value;
                }
            }

            if (string.IsNullOrEmpty(href))
            {
                return null;
            }
            else
            {
                return href;
            }

        }
        string GetViemoLinks(string encodedUrl)
        {

            CookieContainer cookieJar = new CookieContainer();
            string ua = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";
            //URL=https%3A%2F%2Fvimeo.com%2Fchannels%2Fstaffpicks%2F156107994
            string poststring = string.Format("URL=" + encodedUrl);

            HttpWebRequest httpRequest =
                (HttpWebRequest)
                    WebRequest.Create("http://yoodownload.com/download.php");

            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            httpRequest.KeepAlive = true;
            httpRequest.Headers.Add("Accept-Language", "en-US,en;q=0.8");
            //httpRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            httpRequest.Host = "yoodownload.com";


            httpRequest.CookieContainer = cookieJar;
            httpRequest.UserAgent = ua;
            httpRequest.Method = WebRequestMethods.Http.Post;
            httpRequest.Proxy = null;

            byte[] bytedata = Encoding.UTF8.GetBytes(poststring);
            httpRequest.ContentLength = bytedata.Length;

            Stream requestStream = httpRequest.GetRequestStream();
            requestStream.Write(bytedata, 0, bytedata.Length);
            requestStream.Close();

            string strMessage = "";
            using (HttpWebResponse httpWebResponse2 = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream responseStream2 = httpWebResponse2.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(responseStream2, Encoding.UTF8);
                    strMessage = sr.ReadToEnd();

                }
            }

            HtmlDocument doc = new HtmlDocument();

            doc.OptionAutoCloseOnEnd = true;
            doc.OptionFixNestedTags = true;
            doc.LoadHtml(strMessage);

            string href1 = string.Empty;
            string ql1 = string.Empty;
            string href2 = string.Empty;
            string ql2 = string.Empty;
            string href3 = string.Empty;
            string ql3 = string.Empty;
            string href4 = string.Empty;
            string ql4 = string.Empty;
            HtmlNode input1 = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[1]/div/center/div[2]/a[3]");

            if (input1 != null)
            {
                if (input1.Attributes["href"] != null)
                {
                    href1 = input1.Attributes["href"].Value;
                }
                if (input1.ChildNodes["strong"] != null)
                {
                    ql1 = input1.ChildNodes["strong"].InnerText;
                }
            }
            HtmlNode input2 = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[1]/div/center/div[2]/a[5]");
            if (input2 != null)
            {
                if (input2.Attributes["href"] != null)
                {
                    href2 = input2.Attributes["href"].Value;
                }
                if (input2.ChildNodes["strong"] != null)
                {
                    ql2 = input2.ChildNodes["strong"].InnerText;
                }
            }
            HtmlNode input3 = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[1]/div/center/div[2]/a[7]");
            if (input3 != null)
            {
                if (input3.Attributes["href"] != null)
                {
                    href3 = input3.Attributes["href"].Value;
                }
                if (input3.ChildNodes["strong"] != null)
                {
                    ql3 = input3.ChildNodes["strong"].InnerText;
                }
            }
            HtmlNode input4 = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[1]/div/center/div[2]/a[9]");
            if (input4 != null)
            {
                if (input4.Attributes["href"] != null)
                {
                    href4 = input4.Attributes["href"].Value;
                }
                if (input4.ChildNodes["strong"] != null)
                {
                    ql4 = input4.ChildNodes["strong"].InnerText;
                }
            }
            string imgSrc = string.Empty;
            HtmlNode img = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[1]/div/center/div[1]/img");
            if (img != null)
            {
                if (img.Attributes["src"] != null)
                {
                    imgSrc = img.Attributes["src"].Value;
                }

            }
            string title = string.Empty;
            HtmlNode titleinp = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[1]/div/center/div[1]");
            if (titleinp != null)
            {
                if (titleinp.ChildNodes["#text"] != null)
                {
                    title = titleinp.LastChild.InnerText.Trim();
                }

            }
            RootViemo viemo = new RootViemo();
            viemo.title = title;
            viemo.imgsrc = imgSrc;
            int x = 0;
            List<Links> links = new List<Links>();
            if (!string.IsNullOrEmpty(href1))
            {
                links.Add(new Links()
                {
                    url = href1,
                    quality = ql1
                });
                x++;
            }
            if (!string.IsNullOrEmpty(href2))
            {
                links.Add(new Links()
                {
                    url = href2,
                    quality = ql2
                });

            }

            if (!string.IsNullOrEmpty(href3))
            {
                links.Add(new Links()
                {
                    url = href3,
                    quality = ql3
                });

            }

            if (!string.IsNullOrEmpty(href4))
            {
                links.Add(new Links()
                {
                    url = href4,
                    quality = ql4
                });

            }
            viemo.links = links.ToArray();
           
            if (viemo != null)
            {
                string res = JsonConvert.SerializeObject(viemo);
                return res;
            }
            else
            {
                return null;
            }

        }
        public Mp3Data GetScUrl(string url, string mainUrl)
        {
            var mp3Data = new Mp3Data();

            var cookieJar = new CookieContainer();
            //_token=7uVGUdOYs3KHWwsHEoYtuk4q7NZVd9Mmqof1idn8&url=https%3A%2F%2Fsoundcloud.com%2Ftravisscott-2%2Fwonderful-ftthe-weeknd&submit=
            var poststring = string.Format("_token=7uVGUdOYs3KHWwsHEoYtuk4q7NZVd9Mmqof1idn8&url=" + url + "&submit=");
            var httpRequest =
                (HttpWebRequest)WebRequest.Create("http://soundcloudmp3.org/converter");
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
            ////*[@id="preview"]/div[1]/p[1]/text()
            var titleNode = doc.DocumentNode.SelectSingleNode("//*[@id='preview']/div[1]/p[1]/text()");
            var title = string.Empty;
            if (titleNode != null)
            {
                title = titleNode.InnerText.Trim();
            }
            if (!string.IsNullOrEmpty(title) && title.Contains("?"))
            {
                title = GetScTitle(mainUrl);
            }
            if (string.IsNullOrEmpty(href))
            {
                return null;
            }
            //mp3Data.Name = Uri.EscapeDataString(title);
            mp3Data.Name = title;

            mp3Data.Link = href;
            return mp3Data;
        }
        public string GetScTitle(string url)
        {
            var cookieJar = new CookieContainer();
            var httpRequest =
                 (HttpWebRequest)WebRequest.Create(url);
            httpRequest.CookieContainer = cookieJar;
            httpRequest.Method = WebRequestMethods.Http.Get;
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            httpRequest.KeepAlive = true;
            httpRequest.Headers.Add("Accept-Language", "en-us,en;q=0.8");
            httpRequest.UserAgent =
                "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";
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

            var inputChk = doc.DocumentNode.SelectSingleNode("//*[@property='og:title']");
            var tit = string.Empty;
            if (inputChk != null)
            {
                if (inputChk.Attributes["content"] != null)
                {
                    tit = inputChk.Attributes["content"].Value;
                }
            }


            return tit;
        }
        public Mp3Data GetScUrl9(string url, string mainUrl)
        {
            var mp3Data = new Mp3Data();
            var esUrl = Uri.EscapeDataString(mainUrl);
            var cookieJar = new CookieContainer();

            // get the cookie and access token
            var freq =
           (HttpWebRequest)WebRequest.Create("http://9soundclouddownloader.com/");
            freq.CookieContainer = cookieJar;
            freq.Method = WebRequestMethods.Http.Get;
            freq.Host = "9soundclouddownloader.com";
            //freq.ContentType = "application/x-www-form-urlencoded";
            freq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            freq.KeepAlive = true;
            freq.Headers.Add("Accept-Language", "en-US,en;q=0.8");
            freq.UserAgent =
                "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";
            //freq.Referer = "http://9soundclouddownloader.com/";
            //freq.Headers.Add("Origin", "http://9soundclouddownloader.com");
            var ftxt = "";
            using (var httpWebResponse2 = (HttpWebResponse)freq.GetResponse())
            {
                using (var responseStream2 = httpWebResponse2.GetResponseStream())
                {
                    var sr = new StreamReader(responseStream2, Encoding.UTF8);
                    ftxt = sr.ReadToEnd();
                }
            }

            var fdoc = new HtmlDocument();

            fdoc.OptionAutoCloseOnEnd = true;
            fdoc.OptionFixNestedTags = true;
            fdoc.LoadHtml(ftxt);
            var token = fdoc.DocumentNode.SelectSingleNode("//*[@name='csrfmiddlewaretoken']");
            var tokenstr = string.Empty;
            if (token != null)
            {
                if (token.Attributes["value"] != null)
                {
                    tokenstr = token.Attributes["value"].Value;
                }
            }
            //------------------

            //csrfmiddlewaretoken=f1w2LF5LJdPcXj75TnOfJI4cdIO3cxj5&sound-url=https%3A%2F%2Fsoundcloud.com%2Ftravisscott-2%2Fa-team
            var poststring = string.Format("csrfmiddlewaretoken=" + tokenstr + "&sound-url=" + esUrl + "");
            var httpRequest =
                (HttpWebRequest)WebRequest.Create("http://9soundclouddownloader.com/download-sound-track");
            httpRequest.CookieContainer = cookieJar;
            httpRequest.Method = WebRequestMethods.Http.Post;
            httpRequest.Host = "9soundclouddownloader.com";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            httpRequest.KeepAlive = true;
            httpRequest.Headers.Add("Accept-Language", "en-US,en;q=0.8");
            httpRequest.UserAgent =
                "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";
            httpRequest.Referer = "http://9soundclouddownloader.com/";
            httpRequest.Headers.Add("Origin", "http://9soundclouddownloader.com");
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
            var inputChk = doc.DocumentNode.Descendants("a").FirstOrDefault(x => x.Attributes["download"] != null);
            //var inputChk = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[2]/div[1]/center/a[1]");
            var href = string.Empty;
            if (inputChk != null)
            {
                if (inputChk.Attributes["href"] != null)
                {
                    href = inputChk.Attributes["href"].Value;
                }
            }
           
            if (string.IsNullOrEmpty(href))
            {
                return null;
            }
          
            //mp3Data.Name = title;

            mp3Data.Link = href.Replace("&amp;","&");
            return mp3Data;
        }
        // GET api/<controller>/6
        public void DownMp31()
        {
            if (HttpContext.Current.Session["audioPath"] != null)
            {
                var path = HttpContext.Current.Session["audioPath"];
                FileInfo f = new FileInfo(path.ToString());
                long s1 = f.Length;
                string filename = Path.GetFileName(path.ToString());

                var r = HttpContext.Current.Response;
                r.Clear();
                r.ContentType = "Application/octet-stream";
                r.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.sql", filename));
                if (s1 > 0)
                {
                    r.AddHeader("Content-Length", s1.ToString());
                }

                r.Write(path);
                r.End();
            }
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage DownMp3()
        {
            if (HttpContext.Current.Session["audioPath"] != null)
            {
                var path = HttpContext.Current.Session["audioPath"];
                var filePath = path.ToString();
                FileInfo f = new FileInfo(filePath);
                long s1 = f.Length;
                string filename = Path.GetFileName(filePath);
                byte[] fileData = File.ReadAllBytes(filePath);
                var stream = new MemoryStream();
                // processing the stream.
                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    //Content = new read(fileData)

                    //Content = new ByteArrayContent(stream.GetBuffer())
                    Content = new StreamContent(fileStream)
                };

                result.Headers.AcceptRanges.Add("bytes");
                result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = filename
                    };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentLength = s1;
                fileStream.Close();
                return result;
            }
            return null;
        }
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
      
    }
   

  

    public class RootObject
    {
        public string error { get; set; }
        public Info info { get; set; }
        public DownloadLinks[] download_links { get; set; }
        //public List<DownloadLinks> download_links { get; set; }
    }
    public class DownloadLinks
    {
        public string url { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string quality { get; set; }
        public object size { get; set; }
        public int newtab { get; set; }
        public int saveas { get; set; }
    }
    public class Info
    {
        public string title { get; set; }
        public string image { get; set; }
        public string url { get; set; }
        public string domain { get; set; }
        public string user { get; set; }
        public string duration { get; set; }
    }

    public class RootViemo
    {
        public string title { get; set; }
        public string imgsrc { get; set; }
        public Links[] links { get; set; }

    }
    public class Links
    {
        public string url { get; set; }
        public string quality { get; set; }
    }

    public class TestWebResponse : WebResponse
    {
        public override long ContentLength { get; set; }
        public override string ContentType { get; set; }
       
    }

    public class Mp3Data
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Host { get; set; }
        public string Referer { get; set; }

    }
    public class Fb
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string NormalQ { get; set; }
        public string HdQ { get; set; }

    }
  
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;

namespace VideoConverter.Controllers
{
    public class tController : Controller
    {
        //
        // GET: /t/
        public ActionResult Index()
        {

            return View();
        }

        //
        // GET: /t/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /t/Create
        public ActionResult s()
        {
            return View();
        }

        //
        // POST: /t/Create
        [HttpPost]
        public ActionResult s(FormCollection col)
        {
            try
            {
                // TODO: Add insert logic here
                string email = col.Get("email");

                var cookieJar = new CookieContainer();

                //// first request 
                //var fReq =
                //    (HttpWebRequest)WebRequest.Create("https://accounts.google.com/ServiceLogin?sacu=1&hl=ar&service=youtube");
                //fReq.CookieContainer = cookieJar;
                //fReq.Method = WebRequestMethods.Http.Get;
                //fReq.Host = "accounts.google.com";
                //fReq.KeepAlive = true;
                //fReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                //fReq.Headers.Add("Upgrade-Insecure-Requests", "1");
                //fReq.Headers.Add("X-Client-Data", "CI62yQEIo7bJAQjBtskBCP2VygE=");
                //fReq.UserAgent =
                //  "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36";
               
                //fReq.Headers.Add("Accept-Language", "en-US,en;q=0.8");

              

                //var ftxt = "";
                //using (var httpWebResponse2 = (HttpWebResponse)fReq.GetResponse())
                //{
                //    using (var responseStream2 = httpWebResponse2.GetResponseStream())
                //    {
                //        var sr = new StreamReader(responseStream2, Encoding.UTF8);
                //        ftxt = sr.ReadToEnd();
                //    }
                //}
                //var doc = new HtmlDocument();

                //doc.OptionAutoCloseOnEnd = true;
                //doc.OptionFixNestedTags = true;
                //doc.LoadHtml(ftxt);
                //var GalxValue = string.Empty;
                //var gxf2Value = string.Empty;

                //var inputChk = doc.DocumentNode.SelectSingleNode("//*[@name='GALX']");
                //if (inputChk != null)
                //{
                //    if (inputChk.Attributes["value"] != null)
                //    {
                //        GalxValue = inputChk.Attributes["value"].Value;
                //    }
                //}
                //var gxf2Nd = doc.DocumentNode.SelectSingleNode("//*[@name='gxf']");
                //if (gxf2Nd != null)
                //{
                //    if (gxf2Nd.Attributes["value"] != null)
                //    {
                //        gxf2Value = gxf2Nd.Attributes["value"].Value;
                //    }
                //}
                //var galx1 = GalxValue;
                //var gxf2 = Uri.EscapeDataString(gxf2Value);

                 //login email
                //Email=ali.public100%40gmail.com&requestlocation=https%3A%2F%2Faccounts.google.com%2FServiceLogin%3Fuilel%3D3%26passive%3Dtrue%26continue%3Dhttps%253A%252F%252Fwww.youtube.com%252Fsignin%253Fnext%253D%25252Fwatch%25253Fv%25253DW9JzaK_I4Ec%2526feature%253Dsign_in_button%2526action_handle_signin%253Dtrue%2526hl%253Den%2526app%253Ddesktop%26hl%3Den%26service%3Dyoutube%23identifier&bgresponse=!ZWZCHknqI0RvC29EbyTBWtRCSkgCAAAA0FIAAAEvmQFn93-fgtgLZGvaYW7PPjkmgRABfTVx-8D979B2Q-Z56_ocd1MBtKbCokekCLYKvF2s-1Vp9Bjd3Ft4NOuw9W0T89Vo9OLuKTvKY65sHMYCgwQSGD4govEOMAaq5UTFvVl0qJEtoiFlAYJlYcjr7OTvpetNvJPUSK1QwVA6LCtIMjDUIAwbXF_klbuBPPkARsA7T0FQRgqbDwN9PdHf-2VqO1K5owfK28ef3OQQc8dlG-SK7DJAmvu5-OugInOWudXsXvMXRvDi_XH3IA2BfnI-G45cyBYqzS2N6MKATZUGE81HfJGU31GHl1en5t8rxgRFO_DMY_F3Vhnb8uk0pPobG3TFegiQkJsPCTGcH31XaemYXwyvYTZWsIyoexnZuiY4APmfmJsUt8Z2BXv5irdSlxKbl5WF2JOSCc-qazE3aBr4w1rQ_AG2J6NtwoViI37EhPSaxA4zfEiWFt6-nLTgSu6CwfxA-Vw&Page=PasswordSeparationSignIn&GALX=83N1hq9vhfA&gxf=AFoagUXg0fLsWj5RXCiP2I1d2T1NpaZGBw%3A1462629352111&continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Fnext%3D%252Fwatch%253Fv%253DW9JzaK_I4Ec%26feature%3Dsign_in_button%26action_handle_signin%3Dtrue%26hl%3Den%26app%3Ddesktop&service=youtube&hl=en&_utf8=%E2%98%83&pstMsg=1&checkConnection=youtube%3A8435%3A1&checkedDomains=youtube&rmShown=1
                //var fString = string.Format("Email=ali.public100%40gmail.com&requestlocation=https%3A%2F%2Faccounts.google.com%2FServiceLogin%3Fuilel%3D3%26passive%3Dtrue%26continue%3Dhttps%253A%252F%252Fwww.youtube.com%252Fsignin%253Fnext%253D%25252Fwatch%25253Fv%25253DW9JzaK_I4Ec%2526feature%253Dsign_in_button%2526action_handle_signin%253Dtrue%2526hl%253Den%2526app%253Ddesktop%26hl%3Den%26service%3Dyoutube%23identifier&bgresponse=!ZWZCHknqI0RvC29EbyTBWtRCSkgCAAAA0FIAAAEvmQFn93-fgtgLZGvaYW7PPjkmgRABfTVx-8D979B2Q-Z56_ocd1MBtKbCokekCLYKvF2s-1Vp9Bjd3Ft4NOuw9W0T89Vo9OLuKTvKY65sHMYCgwQSGD4govEOMAaq5UTFvVl0qJEtoiFlAYJlYcjr7OTvpetNvJPUSK1QwVA6LCtIMjDUIAwbXF_klbuBPPkARsA7T0FQRgqbDwN9PdHf-2VqO1K5owfK28ef3OQQc8dlG-SK7DJAmvu5-OugInOWudXsXvMXRvDi_XH3IA2BfnI-G45cyBYqzS2N6MKATZUGE81HfJGU31GHl1en5t8rxgRFO_DMY_F3Vhnb8uk0pPobG3TFegiQkJsPCTGcH31XaemYXwyvYTZWsIyoexnZuiY4APmfmJsUt8Z2BXv5irdSlxKbl5WF2JOSCc-qazE3aBr4w1rQ_AG2J6NtwoViI37EhPSaxA4zfEiWFt6-nLTgSu6CwfxA-Vw&Page=PasswordSeparationSignIn&GALX="+galx1+"&gxf="+gxf2+"&continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Fnext%3D%252Fwatch%253Fv%253DW9JzaK_I4Ec%26feature%3Dsign_in_button%26action_handle_signin%3Dtrue%26hl%3Den%26app%3Ddesktop&service=youtube&hl=en&_utf8=%E2%98%83&pstMsg=1&checkConnection=youtube%3A8435%3A1&checkedDomains=youtube&rmShown=1");
                var fString = string.Format("Email=ali.public100%40gmail.com&requestlocation=https%3A%2F%2Faccounts.google.com%2FServiceLogin%3Fuilel%3D3%26passive%3Dtrue%26continue%3Dhttps%253A%252F%252Fwww.youtube.com%252Fsignin%253Fnext%253D%25252Fwatch%25253Fv%25253DW9JzaK_I4Ec%2526feature%253Dsign_in_button%2526action_handle_signin%253Dtrue%2526hl%253Den%2526app%253Ddesktop%26hl%3Den%26service%3Dyoutube%23identifier&bgresponse=!ZWZCHknqI0RvC29EbyTBWtRCSkgCAAAA0FIAAAEvmQFn93-fgtgLZGvaYW7PPjkmgRABfTVx-8D979B2Q-Z56_ocd1MBtKbCokekCLYKvF2s-1Vp9Bjd3Ft4NOuw9W0T89Vo9OLuKTvKY65sHMYCgwQSGD4govEOMAaq5UTFvVl0qJEtoiFlAYJlYcjr7OTvpetNvJPUSK1QwVA6LCtIMjDUIAwbXF_klbuBPPkARsA7T0FQRgqbDwN9PdHf-2VqO1K5owfK28ef3OQQc8dlG-SK7DJAmvu5-OugInOWudXsXvMXRvDi_XH3IA2BfnI-G45cyBYqzS2N6MKATZUGE81HfJGU31GHl1en5t8rxgRFO_DMY_F3Vhnb8uk0pPobG3TFegiQkJsPCTGcH31XaemYXwyvYTZWsIyoexnZuiY4APmfmJsUt8Z2BXv5irdSlxKbl5WF2JOSCc-qazE3aBr4w1rQ_AG2J6NtwoViI37EhPSaxA4zfEiWFt6-nLTgSu6CwfxA-Vw&Page=PasswordSeparationSignIn&GALX=83N1hq9vhfA&gxf=AFoagUXg0fLsWj5RXCiP2I1d2T1NpaZGBw%3A1462629352111&continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Fnext%3D%252Fwatch%253Fv%253DW9JzaK_I4Ec%26feature%3Dsign_in_button%26action_handle_signin%3Dtrue%26hl%3Den%26app%3Ddesktop&service=youtube&hl=en&_utf8=%E2%98%83&pstMsg=1&checkConnection=youtube%3A8435%3A1&checkedDomains=youtube&rmShown=1");
                var emailReq =
                   (HttpWebRequest)WebRequest.Create("https://accounts.google.com/accountLoginInfoXhr");
                emailReq.CookieContainer = cookieJar;
                emailReq.Method = WebRequestMethods.Http.Post;
                emailReq.Host = "accounts.google.com";
                emailReq.KeepAlive = true;
                emailReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                emailReq.Headers.Add("Origin", "https://accounts.google.com");
                emailReq.Headers.Add("X-Client-Data", "CI62yQEIo7bJAQjBtskBCP2VygE=");
                emailReq.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36";
                emailReq.ContentType = "application/x-www-form-urlencoded";
                emailReq.Referer = "https://accounts.google.com/ServiceLogin?uilel=3&passive=true&continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Fnext%3D%252Fwatch%253Fv%253DW9JzaK_I4Ec%26feature%3Dsign_in_button%26action_handle_signin%3Dtrue%26hl%3Den%26app%3Ddesktop&hl=en&service=youtube";
                emailReq.Headers.Add("Accept-Language", "en-US,en;q=0.8");

                var fbytedata = Encoding.UTF8.GetBytes(fString);
                emailReq.ContentLength = fbytedata.Length;
                var freqStream = emailReq.GetRequestStream();
                freqStream.Write(fbytedata, 0, fbytedata.Length);
                freqStream.Close();

                var emailtxt = "";
                using (var httpWebResponse2 = (HttpWebResponse)emailReq.GetResponse())
                {
                    using (var responseStream2 = httpWebResponse2.GetResponseStream())
                    {
                        var sr = new StreamReader(responseStream2, Encoding.UTF8);
                        emailtxt = sr.ReadToEnd();
                    }
                }
                // password req
                var passString = string.Format("Page=PasswordSeparationSignIn&GALX=83N1hq9vhfA&gxf=AFoagUXg0fLsWj5RXCiP2I1d2T1NpaZGBw%3A1462629352111&continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Fnext%3D%252Fwatch%253Fv%253DW9JzaK_I4Ec%26feature%3Dsign_in_button%26action_handle_signin%3Dtrue%26hl%3Den%26app%3Ddesktop&service=youtube&hl=en&ProfileInformation=APMTqumpC2h7PV8XXYjZLrzgdt4uzYPhYH5V2OBVk6rsap62zNPaZFiLP3q0-phYxxkTMCfnQ4JLwqT5Uifi9ybKm0KMv6bv_R2_C_i6pnsEJDWgG7QScMLoW4YZ7DFH9C90jZ5ogDuSwQ-5oFcaYyir5dOheQIQHWyDTS8ElUFqyFY_UGm84JR9ck5v8hMhWqAlB8EAv7DSecpt9G69DZ8G9BlFAbNiMwi05Yb6yt0OUbLUw2VA0Bl9kHNmVZk0xjEhq9F2RLYy&_utf8=%E2%98%83&bgresponse=%21JyRCHknqI0RvC29EbyTBWtRCSkgCAAAA0FIAAAAXmQFs93-fgtgLZGvaYW7PPjkmgRABfTVx-8D979B2Q-Z56_ocd1MBtKbCokekCLYKvF2s-1Vp9Bjd3Ft4NOuw9W0T89Vo9OLuKTvKY65sHMYCgwQSGD4govEOMAaq5UTFvVl0qJEtoiFlAYJlYcjr7OTvpetNvJPUSK1QwVA6LCtIMjDUIAwbXF_klbuBPPkARsA7T0FQRgqbDwN9PdHf-2VqO1K5owfK28ef3OQQc8dlG-SK7DJAmvu5-OugInOWudXsXvMXRvDi_XH3IA2BfnI-G45cyBYqzS2N6NY02AXFQJi_pwpP3cedxyGwtUFNubRIJ012gahVRj7XnVvDk0uwkdRXG2JMb6OMet6lWYtlPOVgmhqPpb34WJV6wXzxMo23sRD1qK3v5m8Pr7GBi96McVuNOl7tCZ-vihUJsL9gPRm-VCNEZKfK3y52sWr0ZiDpZEpkGAGCQcYjpxk5afPqsjRbMD5JJYG57Di7NA&pstMsg=1&dnConn=&checkConnection=youtube%3A8435%3A1&checkedDomains=youtube&identifiertoken=&identifiertoken_audio=&identifier-captcha-input=&Email=ali.public100%40gmail.com&Passwd=33303415067%40%24seo&PersistentCookie=yes&rmShown=1");
                var passReq =
                   (HttpWebRequest)WebRequest.Create("https://accounts.google.com/ServiceLoginAuth");
                passReq.CookieContainer = cookieJar;
                passReq.Method = WebRequestMethods.Http.Post;
                passReq.Host = "accounts.google.com";
                passReq.KeepAlive = true;
                passReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                passReq.Headers.Add("Origin", "https://accounts.google.com");
                passReq.Headers.Add("Upgrade-Insecure-Requests", "1");
                passReq.Headers.Add("X-Client-Data", "CI62yQEIo7bJAQjBtskBCP2VygE=");
                passReq.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36";
                passReq.ContentType = "application/x-www-form-urlencoded";
                passReq.Referer = "https://accounts.google.com/ServiceLogin?uilel=3&passive=true&continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Fnext%3D%252Fwatch%253Fv%253DW9JzaK_I4Ec%26feature%3Dsign_in_button%26action_handle_signin%3Dtrue%26hl%3Den%26app%3Ddesktop&hl=en&service=youtube";
                passReq.Headers.Add("Accept-Language", "en-US,en;q=0.8");

                var passBytes = Encoding.UTF8.GetBytes(passString);
                passReq.ContentLength = passBytes.Length;
                var passStream = passReq.GetRequestStream();
                passStream.Write(passBytes, 0, passBytes.Length);
                passStream.Close();

                var passTxt = "";
                using (var httpWebResponse2 = (HttpWebResponse)passReq.GetResponse())
                {
                    using (var responseStream2 = httpWebResponse2.GetResponseStream())
                    {
                        var sr = new StreamReader(responseStream2, Encoding.UTF8);
                        passTxt = sr.ReadToEnd();
                    }
                }

                // get youtube
                var getReq =
                 (HttpWebRequest)WebRequest.Create("https://www.youtube.com/");
                getReq.CookieContainer = cookieJar;
                getReq.Method = WebRequestMethods.Http.Get;
                getReq.Host = "www.youtube.com";
                getReq.KeepAlive = true;
                getReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                getReq.Headers.Add("Upgrade-Insecure-Requests", "1");
                getReq.Headers.Add("X-Client-Data", "CI62yQEIo7bJAQjBtskBCP2VygE=");
                getReq.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36";
                getReq.Headers.Add("Accept-Language", "en-US,en;q=0.8");
                var getTxt = "";
                using (var httpWebResponse2 = (HttpWebResponse)getReq.GetResponse())
                {
                    using (var responseStream2 = httpWebResponse2.GetResponseStream())
                    {
                        var sr = new StreamReader(responseStream2, Encoding.UTF8);
                        getTxt = sr.ReadToEnd();
                    }
                }


                // second password req
                //var secPassString = string.Format("Page=PasswordSeparationSignIn&GALX=83N1hq9vhfA&gxf=AFoagUXg0fLsWj5RXCiP2I1d2T1NpaZGBw%3A1462629352111&continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Fnext%3D%252Fwatch%253Fv%253DW9JzaK_I4Ec%26feature%3Dsign_in_button%26action_handle_signin%3Dtrue%26hl%3Den%26app%3Ddesktop&service=youtube&hl=en&ProfileInformation=APMTqumpC2h7PV8XXYjZLrzgdt4uzYPhYH5V2OBVk6rsap62zNPaZFiLP3q0-phYxxkTMCfnQ4JLwqT5Uifi9ybKm0KMv6bv_R2_C_i6pnsEJDWgG7QScMLoW4YZ7DFH9C90jZ5ogDuSwQ-5oFcaYyir5dOheQIQHWyDTS8ElUFqyFY_UGm84JR9ck5v8hMhWqAlB8EAv7DSecpt9G69DZ8G9BlFAbNiMwi05Yb6yt0OUbLUw2VA0Bl9kHNmVZk0xjEhq9F2RLYy&_utf8=%E2%98%83&bgresponse=%21JyRCHknqI0RvC29EbyTBWtRCSkgCAAAA0FIAAAAXmQFs93-fgtgLZGvaYW7PPjkmgRABfTVx-8D979B2Q-Z56_ocd1MBtKbCokekCLYKvF2s-1Vp9Bjd3Ft4NOuw9W0T89Vo9OLuKTvKY65sHMYCgwQSGD4govEOMAaq5UTFvVl0qJEtoiFlAYJlYcjr7OTvpetNvJPUSK1QwVA6LCtIMjDUIAwbXF_klbuBPPkARsA7T0FQRgqbDwN9PdHf-2VqO1K5owfK28ef3OQQc8dlG-SK7DJAmvu5-OugInOWudXsXvMXRvDi_XH3IA2BfnI-G45cyBYqzS2N6NY02AXFQJi_pwpP3cedxyGwtUFNubRIJ012gahVRj7XnVvDk0uwkdRXG2JMb6OMet6lWYtlPOVgmhqPpb34WJV6wXzxMo23sRD1qK3v5m8Pr7GBi96McVuNOl7tCZ-vihUJsL9gPRm-VCNEZKfK3y52sWr0ZiDpZEpkGAGCQcYjpxk5afPqsjRbMD5JJYG57Di7NA&pstMsg=1&dnConn=&checkConnection=youtube%3A8435%3A1&checkedDomains=youtube&identifiertoken=&identifiertoken_audio=&identifier-captcha-input=&Email=ali.public100%40gmail.com&Passwd=33303415067%40%24seo&PersistentCookie=yes&rmShown=1");
                //var secPassReq =
                //   (HttpWebRequest)WebRequest.Create("https://accounts.google.com/ListAccounts?source=ChromiumBrowser&json=standard");
                //secPassReq.CookieContainer = cookieJar;
                //secPassReq.Method = WebRequestMethods.Http.Post;
                //secPassReq.Host = "accounts.google.com";
                //secPassReq.KeepAlive = true;
                //secPassReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                //secPassReq.Headers.Add("Origin", "https://www.google.com");
                //secPassReq.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36";
                //secPassReq.ContentType = "application/x-www-form-urlencoded";
                //secPassReq.Headers.Add("Accept-Language", "en-US,en;q=0.8");

                //secPassReq.ContentLength = 0;

                //////var secPassBytes = Encoding.UTF8.GetBytes("+");
                ////////secPassReq.ContentLength = secPassBytes.Length;
                //////var secPassStream = secPassReq.GetRequestStream();
                //////secPassStream.Write(secPassBytes, 0, 1);
                //////secPassStream.Close();

                //var secPassTxt = "";
                //using (var httpWebResponse2 = (HttpWebResponse)secPassReq.GetResponse())
                //{
                //    ////using (var responseStream2 = httpWebResponse2.GetResponseStream())
                //    ////{
                //    ////    var sr = new StreamReader(responseStream2, Encoding.UTF8);
                //    ////    secPassTxt = sr.ReadToEnd();
                //    ////}
                //}

                 //third password req
                //var thPassReq =
                //   (HttpWebRequest)WebRequest.Create("https://accounts.google.com/CheckCookie?hl=en&checkedDomains=youtube&checkConnection=youtube%3A8435%3A1&pstMsg=1&chtml=LoginDoneHtml&service=youtube&continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Fnext%3D%252Fwatch%253Fv%253DW9JzaK_I4Ec%26feature%3Dsign_in_button%26action_handle_signin%3Dtrue%26hl%3Den%26app%3Ddesktop&gidl=CAA");
                //thPassReq.CookieContainer = cookieJar;
                //thPassReq.Method = WebRequestMethods.Http.Get;
                //thPassReq.Host = "accounts.google.com";
                //thPassReq.KeepAlive = true;
                //thPassReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                //thPassReq.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36";
                //thPassReq.Headers.Add("Upgrade-Insecure-Requests", "1");
                //thPassReq.Headers.Add("X-Client-Data", "CI62yQEIo7bJAQjBtskBCP2VygE=");
                //thPassReq.Referer = "https://accounts.google.com/ServiceLogin?uilel=3&passive=true&continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Fnext%3D%252Fwatch%253Fv%253DW9JzaK_I4Ec%26feature%3Dsign_in_button%26action_handle_signin%3Dtrue%26hl%3Den%26app%3Ddesktop&hl=en&service=youtube";
                //thPassReq.Headers.Add("Accept-Language", "en-US,en;q=0.8");

              

                //var thPassTxt = "";
                //using (var httpWebResponse2 = (HttpWebResponse)thPassReq.GetResponse())
                //{
                //    using (var responseStream2 = httpWebResponse2.GetResponseStream())
                //    {
                //        var sr = new StreamReader(responseStream2, Encoding.UTF8);
                //        thPassTxt = sr.ReadToEnd();
                //    }
                //}

                //// fourth password req
                //var fouPassReq =
                //   (HttpWebRequest)WebRequest.Create("https://accounts.youtube.com/accounts/SetSID?ssdc=1&sidt=ALWU2cszQmvi1NErMFaaoV%2FZHMZyF8lc04kt1oQOKutsgqbvWHtp4OuWZ7Rs2%2BgYUBXy%2FfoxthR8GDEgTsY0yPnbLcbFbZHl2tQfvaXH9ZFj%2FKZDxboJUD2cylZ6HsTXdENoV5jTswgYKfIX6nulLDs0%2BHIUJRcwJQYEeWjdSE4Sr21iaVOmX8NXjjSJIUv5wHVbNub8eJFkHlZkwnoT4HleEFkaYx3vnUgZVokZZI5lagx4DfS9UcBAf1Jn7OMLSCr3dso9E2sE&pmpo=https%3A%2F%2Faccounts.google.com");
                //fouPassReq.CookieContainer = cookieJar;
                //fouPassReq.Method = WebRequestMethods.Http.Get;
                //fouPassReq.Host = "accounts.youtube.com";
                //fouPassReq.KeepAlive = true;
                //fouPassReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                //fouPassReq.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36";
                //fouPassReq.Headers.Add("Upgrade-Insecure-Requests", "1");
                //fouPassReq.Headers.Add("X-Client-Data", "CI62yQEIo7bJAQjBtskBCP2VygE=");
                //fouPassReq.Referer = "https://accounts.google.com/CheckCookie?hl=en&checkedDomains=youtube&checkConnection=youtube%3A8435%3A1&pstMsg=1&chtml=LoginDoneHtml&service=youtube&continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Fnext%3D%252Fwatch%253Fv%253DW9JzaK_I4Ec%26feature%3Dsign_in_button%26action_handle_signin%3Dtrue%26hl%3Den%26app%3Ddesktop&gidl=CAA";
                //fouPassReq.Headers.Add("Accept-Language", "en-US,en;q=0.8");


                //var fouPassTxt = "";
                //using (var httpWebResponse2 = (HttpWebResponse)fouPassReq.GetResponse())
                //{
                //    using (var responseStream2 = httpWebResponse2.GetResponseStream())
                //    {
                //        var sr = new StreamReader(responseStream2, Encoding.UTF8);
                //        fouPassTxt = sr.ReadToEnd();
                //    }
                //}

                // fifth password req
                //var fifPassReq =
                //   (HttpWebRequest)WebRequest.Create("https://accounts.google.com.eg/accounts/SetSID?ssdc=1&sidt=ALWU2cuCO%2BW6YL72MDs7Rzhefjoj5huGcxjHPumLoY8OrVmeE9XMOZ3XJ3j1k6mIA%2F1cONSrPV3wlvIj06%2BC1xwqxKnVU0bPeLGq7nxxfRFZpaL4P%2FaFBd6jW3mOCEkfSjjniC4PmvymLAbiW2ky1v6zO3hSLP1LA2%2Fgh7RzypV6XFoM%2FSsZLHEkzkwGirqvEalWBXqkjVz3EAz71L1khZxZZdlUBOs0oc%2BovpvVV0cfXn4IxGU6JCJQGqFehhYgUmoLpN8oP%2Bvp&pmpo=https%3A%2F%2Faccounts.google.com");
                //fifPassReq.CookieContainer = cookieJar;
                //fifPassReq.Method = WebRequestMethods.Http.Get;
                //fifPassReq.Host = "accounts.google.com.eg";
                //fifPassReq.KeepAlive = true;
                //fifPassReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                //fifPassReq.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36";
                //fifPassReq.Headers.Add("Upgrade-Insecure-Requests", "1");
                //fifPassReq.Headers.Add("X-Client-Data", "CI62yQEIo7bJAQjBtskBCP2VygE=");
                //fifPassReq.Referer = "https://accounts.google.com/CheckCookie?hl=en&checkedDomains=youtube&checkConnection=youtube%3A8435%3A1&pstMsg=1&chtml=LoginDoneHtml&service=youtube&continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Fnext%3D%252Fwatch%253Fv%253DW9JzaK_I4Ec%26feature%3Dsign_in_button%26action_handle_signin%3Dtrue%26hl%3Den%26app%3Ddesktop&gidl=CAA";
                //fifPassReq.Headers.Add("Accept-Language", "en-US,en;q=0.8");


                //var fifPassTxt = "";
                //using (var httpWebResponse2 = (HttpWebResponse)fifPassReq.GetResponse())
                //{
                //    using (var responseStream2 = httpWebResponse2.GetResponseStream())
                //    {
                //        var sr = new StreamReader(responseStream2, Encoding.UTF8);
                //        fifPassTxt = sr.ReadToEnd();
                //    }
                //}

                // sex password req
                //var sexPassReq =
                //   (HttpWebRequest)WebRequest.Create("https://www.youtube.com/signin?next=%2Fwatch%3Fv%3DW9JzaK_I4Ec&feature=sign_in_button&action_handle_signin=true&hl=en&app=desktop&auth=QANH7kFbA6uBv95JFRBmK62kISVd9_apZRkCvd99Bw9sTOY_Y-SZobdx0riVn0bUTBxamA.");
                //sexPassReq.CookieContainer = cookieJar;
                //sexPassReq.Method = WebRequestMethods.Http.Get;
                //sexPassReq.Host = "www.youtube.com";
                //sexPassReq.KeepAlive = true;
                //sexPassReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                //sexPassReq.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36";
                //sexPassReq.Headers.Add("Upgrade-Insecure-Requests", "1");
                //sexPassReq.Headers.Add("X-Client-Data", "CI62yQEIo7bJAQjBtskBCP2VygE=");
                //sexPassReq.Referer = "https://accounts.google.com/CheckCookie?hl=en&checkedDomains=youtube&checkConnection=youtube%3A8435%3A1&pstMsg=1&chtml=LoginDoneHtml&service=youtube&continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Fnext%3D%252Fwatch%253Fv%253DW9JzaK_I4Ec%26feature%3Dsign_in_button%26action_handle_signin%3Dtrue%26hl%3Den%26app%3Ddesktop&gidl=CAA";
                //sexPassReq.Headers.Add("Accept-Language", "en-US,en;q=0.8");


                //var sexPassTxt = "";
                //using (var httpWebResponse2 = (HttpWebResponse)sexPassReq.GetResponse())
                //{
                //    using (var responseStream2 = httpWebResponse2.GetResponseStream())
                //    {
                //        var sr = new StreamReader(responseStream2, Encoding.UTF8);
                //        sexPassTxt = sr.ReadToEnd();
                //    }
                //}

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /t/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /t/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /t/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /t/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

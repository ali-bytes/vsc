using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using Elmah;
using Glimpse.AspNet.Tab;
using NewIspNL.Helpers;
using VideoConverter.Controllers;
using VideoConverter.Model;

namespace VideoConverter.Services
{
    public static class Mp3Repo
    {
        public static string DownloadMp3(Mp3Data mp3Data)
        {
            String guid = Guid.NewGuid().ToString("N").Substring(0, 10);

            var t = guid + ".mp3";
            var now = DateTime.Now;
            var deleteTime = DateTime.Now.AddHours(1);

            var pth = HttpContext.Current.Server.MapPath("~/download/" + t);
          
            var n = QueryStringSecurity.Encrypt(t);
            if (System.IO.File.Exists(pth))
            {
                //sc is for detect that the return string is soundcloud file its deleted is js file
                return "/sc/" + n;
            }
            using (var webClient = new Sc9WebClient())
            {
                //Thread.Sleep(2000);
                webClient.DownloadFile(new Uri(mp3Data.Link), pth);
            }
            if (System.IO.File.Exists(pth))
            {
                try
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
                }
                catch (Exception exception)
                {
                    // Log to Elmah.
                    ErrorSignal.FromCurrentContext().Raise(exception, HttpContext.Current);
                    throw;
                }

                return "/sc/" + n;
            }
            return null;
        }
    }
}
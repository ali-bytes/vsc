using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resources;
using VideoConverter.Constants;

namespace VideoConverter.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
           //var culture = CultureInfo.InstalledUICulture.IetfLanguageTag;
           
            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            string routLang = (string)ControllerContext.RouteData.Values["lang"];


            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                lang = "en";
            }
            if (!string.IsNullOrEmpty(routLang) && routLang != lang)
            {
                lang = routLang;
            }
            ViewData.Add("lang", lang ?? "en");
            
            new SiteLanguages().SetLanguage(lang??"en");

            return base.BeginExecuteCore(callback, state);
        }
       
        //
        // GET: /Base/
        public ActionResult Index()
        {
            return View();
        }
	}
}
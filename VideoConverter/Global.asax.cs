


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Elmah;
using VideoConverter;
using VideoConverter.Model;

namespace VideoConverter
{
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Boilerplate.Web.Mvc;

    using System.Web.Http;
    using System.Web;
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ThreadStart tsTask = TaskLoop;
            var myTask = new Thread(tsTask);
            myTask.Start();
            // Ensure that the X-AspNetMvc-Version HTTP header is not 
            //MvcHandler.DisableMvcResponseHeader = true;
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            ConfigureViewEngines();
            ConfigureAntiForgeryTokens();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
        static void TaskLoop()
        {
            // In this example, task will repeat in infinite loop
            // You can additional parameter if you want to have an option 
            // to stop the task from some page
            //if (myTask.IsAlive)
            while (true)
            {
                // Execute scheduled task
                ScheduledTask();

                // Wait for certain time interval
                Thread.Sleep(new TimeSpan(0, 0, 30, 0)); //wait for 24 Hours
            }
        }
        private static void ScheduledTask()
        {
            try
            {
                // Task code which is executed periodically
                using (var context = new VidFromContext())
                {
                    var now = DateTime.Now;
                    var records = context.Mp3Record.Where(x => x.DeleteTime <= now).ToList();
                    if (records.Count > 0)
                    {
                        var listToDelete = new List<Mp3Record>();
                        foreach (var r in records)
                        {
                            //string filePath1 = Path.Combine(HttpRuntime.AppDomainAppPath, "download", r.Name);
                            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/download/" + r.Name);
                            if (filePath == null) continue;
                            //var filePath = System.Web.HttpContext.Current.Server.MapPath("../download/" + r.Name);

                            //var filePath = path + "/" + r.Name;

                            // delete from file
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                                // check if delete from file success
                                if (!System.IO.File.Exists(filePath))
                                {
                                    try
                                    {
                                        listToDelete.Add(r);
                                    }
                                    catch (Exception exception)
                                    {
                                        // Log to Elmah.
                                        ErrorSignal.FromCurrentContext().Raise(exception, HttpContext.Current);
                                    }

                                }
                            }
                         

                        }
                        // if success delete from database
                        if (listToDelete.Count > 0)
                        {
                            context.Mp3Record.RemoveRange(listToDelete);
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                // Log to Elmah.
                //ErrorSignal.FromCurrentContext().Raise(exception, HttpContext.Current);
            }
        }
        private void Application_Error(object sender, EventArgs e)
        {
            try
            {
                HttpContext.Current.Response.Redirect(@"~/");
            }
            catch
            {
            }
           
        }
        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
        /// <summary>
        /// Configures the view engines. By default, Asp.Net MVC includes the Web Forms (WebFormsViewEngine) and 
        /// Razor (RazorViewEngine) view engines that supports both C# (.cshtml) and VB (.vbhtml). You can remove view 
        /// engines you are not using here for better performance and include a custom Razor view engine that only 
        /// supports C#.
        /// </summary>
        private static void ConfigureViewEngines()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CSharpRazorViewEngine());
        }

        /// <summary>
        /// Configures the anti-forgery tokens. See 
        /// http://www.asp.net/mvc/overview/security/xsrfcsrf-prevention-in-aspnet-mvc-and-web-pages
        /// </summary>
        private static void ConfigureAntiForgeryTokens()
        {
            // Rename the Anti-Forgery cookie from "__RequestVerificationToken" to "f". This adds a little security 
            // through obscurity and also saves sending a few characters over the wire. Sadly there is no way to change 
            // the form input name which is hard coded in the @Html.AntiForgeryToken helper and the 
            // ValidationAntiforgeryTokenAttribute to  __RequestVerificationToken.
            // <input name="__RequestVerificationToken" type="hidden" value="..." />
            AntiForgeryConfig.CookieName = "f";

            // If you have enabled SSL. Uncomment this line to ensure that the Anti-Forgery 
            // cookie requires SSL to be sent across the wire. 
            // AntiForgeryConfig.RequireSsl = true;
        }

    }
}

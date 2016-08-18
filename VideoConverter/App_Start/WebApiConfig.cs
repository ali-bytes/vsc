﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace VideoConverter
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

			config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
               routeTemplate: "api/{controller}/{action}/{url}",
                 defaults: new { url = RouteParameter.Optional }
            );

            // WebAPI when dealing with JSON & JavaScript!
            // Setup json serialization to serialize classes to camel (std. Json format)
            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            formatter.SerializerSettings.ContractResolver =
                new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KinartiProject_ruppin.Controllers;

namespace KinartiProject_ruppin
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new RequireHttpsAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

using System;
using InstaAPI.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web;

namespace InstaAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperWebConfiguration.Configure();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (ReferenceEquals(null, HttpContext.Current.Request.Headers["Authorization"]) &&
                HttpContext.Current.Request.Headers["access_token"] != null)
            {
                var token = HttpContext.Current.Request.Headers["access_token"];
                if (!string.IsNullOrEmpty(token))
                {
                    HttpContext.Current.Request.Headers.Add("Authorization", "Bearer " + token);
                }
            }
        }
    }
}

using System;
using InstaAPI.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web;
using System.Web.Configuration;
using Newtonsoft.Json;

namespace InstaAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();            
            var httpException = ex as HttpException;
            var info = ConfigurationData.ErrorInfo;
            var message = ConfigurationData.ErrorMessage;
            var statusCode = 500;

            if (httpException != null)
            {
                statusCode = httpException.GetHttpCode();
                info = httpException.GetHttpCode().ToString();
                message = httpException.GetHtmlErrorMessage();
                if (httpException.GetHttpCode() == 404)
                {
                    message = "Resource Not Found.  To see a valid list of API endpoints visit: http://instaapi-app.azurewebsites.net/ ";
                }
            }

            var compilationSection = (CompilationSection)System.Configuration.ConfigurationManager.GetSection(@"system.web/compilation");
            if (compilationSection.Debug)
            {
                message += ex.ToString();
            }

            var customError = new GlobalExceptionHandler.CustomError() { Info = info, Message = message };           
            var json = JsonConvert.SerializeObject(customError);

            Response.Clear();
            Response.StatusCode = statusCode;
            Response.Write(json);
            Response.End();
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperWebConfiguration.Configure();
            AutofacWebConfiguration.RegisterAutofac();
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

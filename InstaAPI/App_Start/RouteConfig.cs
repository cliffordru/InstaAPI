using InstaAPI.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace InstaAPI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // By default route the user to the Help area if accessing the base URI.
            routes.MapRoute(
                ConfigurationData.RouteHelp,
                "",
                new { controller = "Help", action = "Index" }
            ).DataTokens = new RouteValueDictionary(new { area = "HelpPage" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

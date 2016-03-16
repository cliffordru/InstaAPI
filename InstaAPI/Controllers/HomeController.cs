using InstaAPI.Helpers;
using System.Web.Mvc;

namespace InstaAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToRoute(ConfigurationData.RouteHelp);
        }
    }
}

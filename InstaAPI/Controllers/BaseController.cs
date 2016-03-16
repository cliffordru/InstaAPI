using System.Web.Http;

namespace InstaAPI.Controllers
{
    [Authorize]
    public abstract class BaseController : ApiController
    {
    }
}

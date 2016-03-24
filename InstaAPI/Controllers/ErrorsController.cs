using System.Web.Http;
using InstaAPI.Helpers;

namespace InstaAPI.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/errors")]
    public class ErrorsController : BaseApiController
    {
        [Route("404")]
        [HttpGet]
        public IHttpActionResult Custom404()
        {
            return Json(new GlobalExceptionHandler.CustomApiError() { Info = "404", Message = "Resource Not Found" });
        }
    }
}

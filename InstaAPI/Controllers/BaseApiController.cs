using System.Net.Http;
using System.Web.Http;
using InstaAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace InstaAPI.Controllers
{
    [Authorize]
    public abstract class BaseApiController : ApiController
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            protected set
            {
                _userManager = value;
            }
        }

        public ApplicationUser GetUser()
        {
            return UserManager.FindByName(User.Identity.GetUserName());
        }
    }
}

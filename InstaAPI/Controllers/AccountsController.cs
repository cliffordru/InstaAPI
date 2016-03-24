using InstaAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
#pragma warning disable 1573
#pragma warning disable 1584,1711,1572,1581,1580

namespace InstaAPI.Controllers
{
    /// <summary>
    /// API endpoints to manage your user account
    /// </summary>    
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
        private ApplicationUserManager _userManager;

        public AccountsController()
        {
        }

        public AccountsController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // POST api/Account/Login
        /// <summary>
        /// Get an access token for the API.         
        /// Parameters {username, password}
        /// </summary>        
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("token")]
        public IHttpActionResult Login(AccountBindingModel model)
        {
            return ResponseMessage(GenerateToken(model));
        }

        // POST api/Account/Register
        /// <summary>
        /// Register an account for the API.  
        /// Parameters {username, password}
        /// </summary>
        /// <param name="username"></param>
        /// /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("new")]
        public async Task<IHttpActionResult> Registration(AccountBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Username, Email = model.Username };

            var result = await UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return ResponseMessage(GenerateToken(model));

        }

        private HttpResponseMessage GenerateToken(AccountBindingModel model)
        {
            var identity = new ClaimsIdentity(Startup.OAuthOptions.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, model.Username));
            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            var currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromDays(14));
            var token = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<object>(new
                {
                    access_token = token,
                    token_type = "bearer",
                    user_name = model.Username,
                    issued = ticket.Properties.IssuedUtc,
                    expires = ticket.Properties.ExpiresUtc
                }, Configuration.Formatters.JsonFormatter)
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers        

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        /*
                private IAuthenticationManager Authentication
                {
                    get { return Request.GetOwinContext().Authentication; }
                }
        */

        /*
                private static class RandomOAuthStateGenerator
                {
                    private static readonly RandomNumberGenerator Random = new RNGCryptoServiceProvider();

                    public static string Generate(int strengthInBits)
                    {
                        const int bitsPerByte = 8;

                        if (strengthInBits % bitsPerByte != 0)
                        {
                            throw new ArgumentException("strengthInBits must be evenly divisible by 8.", nameof(strengthInBits));
                        }

                        int strengthInBytes = strengthInBits / bitsPerByte;

                        byte[] data = new byte[strengthInBytes];
                        Random.GetBytes(data);
                        return HttpServerUtility.UrlTokenEncode(data);
                    }
                }
        */

        #endregion
    }
}

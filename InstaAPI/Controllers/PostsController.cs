using AutoMapper;
using InstaAPI.Filters;
using InstaAPI.Models;
using InstaAPI.Services.BusinessLogicServices;
using InstaAPI.Services.BusinessLogicServices.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace InstaAPI.Controllers
{
    /// <summary>
    /// API endpoints for Instagram posts
    /// </summary>
    [Authorize]
    [RoutePrefix("api/posts")]
    public class PostsController : BaseController
    {
        private readonly IInstagramApiService _instagramApiService;
       
        public PostsController(IInstagramApiService instagramservice)
        {            
            _instagramApiService = instagramservice;
        }

        /// <summary>
        /// Search for posts by tag.  Requires access_token in header.
        /// </summary>        
        /// <returns></returns>
        [Route("tag/{tagname}")]
        [ValidateModel]
        public IHttpActionResult Get([FromUri]PostsTagBindingModel model)
        {            
            var result = _instagramApiService.GetPostsByTag(model.TagName);
            var vm = Mapper.Map<List<PostsTagViewModel>>(result.Data);

            return Json(vm);
        }

        /// <summary>
        /// Save a post as a favorite. Requires access_token in header.
        /// </summary>        
        /// <returns></returns>
        [Route("favorite")]
        [ValidateModel]
        public IHttpActionResult Post([FromBody]PostsFavoriteBindingModel model)
        {
            // TODO: See AccountController.Register for return type
            // TODO: Map model to domain DTO

            //var result = _instagramApiService.GetPostsByTag(model.Tag);
            //var vm = Mapper.Map<List<PostsTagViewModel>>(result.Data);

            //var id = model.InstagramId;
            //var userid = HttpContext.Current.User.Identity.GetUserId();
            

            return Ok();
        }

    }
}

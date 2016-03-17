using AutoMapper;
using InstaAPI.Filters;
using InstaAPI.Models;
using InstaAPI.Services.BusinessLogicServices;
using InstaAPI.Services.BusinessLogicServices.Interfaces;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using InstaAPI.Services.CommandModel;
using Microsoft.AspNet.Identity;

namespace InstaAPI.Controllers
{
    /// <summary>
    /// API endpoints for Instagram posts
    /// </summary>
    [Authorize]
    [RoutePrefix("api/posts")]
    public class PostsController : BaseApiController
    {
        private readonly IInstagramApiService _instagramApiService;
        private readonly IFavoriteCreationService _favoriteCreationService;


        public PostsController(IInstagramApiService instagramservice, IFavoriteCreationService favoriteCreationService)
        {
            _instagramApiService = instagramservice;
            _favoriteCreationService = favoriteCreationService;
        }

        /// <summary>
        /// Search for posts by tag.  Requires access_token in header.
        /// </summary>        
        /// <returns></returns>
        [Route("tag/{tag_name}")]
        [ValidateModel]
        public IHttpActionResult Get([FromUri]PostsTagBindingModel model)
        {            
            var result = _instagramApiService.GetPostsByTag(model.tag_name);
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
            // TODO: Map model to domain DTO automapper
            
            _favoriteCreationService.CreateFavorite(new FavoriteCreationSpec() {UserId = HttpContext.Current.User.Identity.GetUserId(), InstagramId = model.InstagramId, TagName = model.TagName});

            return Ok(); //TODO:201 on success
        }

    }
}

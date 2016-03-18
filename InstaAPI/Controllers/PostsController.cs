using AutoMapper;
using InstaAPI.Filters;
using InstaAPI.Models;
using InstaAPI.Services.BusinessLogicServices;
using InstaAPI.Services.BusinessLogicServices.Interfaces;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using InstaAPI.Services.CommandModel;
using InstaAPI.Services.DomainModel;
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
        private readonly IFavoriteQueryService _favoriteQueryService;


        public PostsController(IInstagramApiService instagramservice, IFavoriteCreationService favoriteCreationService, IFavoriteQueryService favoriteQueryService)
        {
            _instagramApiService = instagramservice;
            _favoriteCreationService = favoriteCreationService;
            _favoriteQueryService = favoriteQueryService;
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
        /// Parameters {instagram_id, tag_name}
        /// </summary>        
        /// <returns></returns>
        [Route("favorite")]
        [ValidateModel]
        public IHttpActionResult Post([FromBody]PostsFavoriteBindingModel model)
        {
            _favoriteCreationService.CreateFavorite(new FavoriteCreationSpec() {UserId = GetUser().Id, InstagramId = model.InstagramId, TagName = model.TagName});

            return Ok(); //TODO:201 on success
        }

        /// <summary>
        /// Get a list of your favorites. Requires access_token in header.
        /// </summary>
        /// <returns></returns>
        [Route("favorites")]
        public IHttpActionResult GetFavorites()
        {
            var result = _favoriteQueryService.GetFavorites(GetUser().Id);
            var vm = Mapper.Map<List<PostsFavoriteViewModel>>(result);

            return Json(vm);
        }

        /// <summary>
        /// Get the number of posts saved by tag name. Requires access_token in header.
        /// </summary>
        /// <returns></returns>
        [Route("favorites/metrics")]
        public IHttpActionResult GetMetrics()
        {
            var result = _favoriteQueryService.GetMetrics(GetUser().Id);
            var vm = Mapper.Map<List<PostsFavoriteMetricsViewModel>>(result);

            return Json(vm);
        }

    }
}

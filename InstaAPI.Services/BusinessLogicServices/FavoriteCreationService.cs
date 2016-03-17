using System;
using InstaAPI.Context;
using InstaAPI.Services.BusinessLogicServices.Interfaces;
using InstaAPI.Services.CommandModel;
using InstaAPI.Services.DomainModel;
using InstaAPI.Services.Repositories.Interfaces;

namespace InstaAPI.Services.BusinessLogicServices
{
    public class FavoriteCreationService : IFavoriteCreationService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IFavoriteRepository _favoritesRepository;
        private readonly IInstagramApiService _instagramApiService;

        public FavoriteCreationService(IDbContextScopeFactory dbContextScopeFactory, IFavoriteRepository favoriteRepository, IInstagramApiService instagramApiService)
        {
            if (dbContextScopeFactory == null) throw new ArgumentNullException(nameof(dbContextScopeFactory));
            if (favoriteRepository == null) throw new ArgumentNullException(nameof(favoriteRepository));
            _dbContextScopeFactory = dbContextScopeFactory;
            _favoritesRepository = favoriteRepository;
            _instagramApiService = instagramApiService;
        }

        void IFavoriteCreationService.CreateFavorite(FavoriteCreationSpec favoriteToCreate)
        {
            if (favoriteToCreate == null)
                throw new ArgumentNullException(nameof(favoriteToCreate));

            favoriteToCreate.Validate();

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                // Build domain models
                var favorite = _favoritesRepository.Get(favoriteToCreate.UserId, favoriteToCreate.InstagramId,
                    favoriteToCreate.TagName);

                //TODO: If exists consider returning 409 
                if (favorite == null)
                {
                    var post = _favoritesRepository.Get(favoriteToCreate.InstagramId);
                    
                    if (post == null)
                    {
                        var instaPostData = _instagramApiService.GetPost(favoriteToCreate.InstagramId);
                        post = new Post()
                        {
                            InstagramId = instaPostData.Data.Id,
                            InstagramUserId = instaPostData.Data.User.Id,
                            InstagramUserName = instaPostData.Data.User.Username,
                            Url = instaPostData.Data.Link,
                            CreatedOn = DateTime.Now
                        };
                    }

                    favorite = new Favorite()
                    {
                        UserId = favoriteToCreate.UserId,
                        InstagramId = favoriteToCreate.InstagramId,
                        TagName = favoriteToCreate.TagName,
                        Post = post,
                        CreatedOn = DateTime.UtcNow
                    };

                    // Persist
                    _favoritesRepository.Add(favorite);
                    dbContextScope.SaveChanges();
                }
            }
        }
    }
}
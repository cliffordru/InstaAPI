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

            // TODO: Call instagram to get URL and Instagram iUserId, UserName
            var instaPostData = _instagramApiService.GetPost(favoriteToCreate.InstagramId);

            // TODO: Post Entity will have InstagramId, iUserId, UserName, Url and insert first (PK InstagramId - consider surrogate key)

            // TODO: Favorite Entity will have aUserId, InstagramId, TagName (FK to Post on InstagramId)

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                // Build domain models
                //var post = new Post()

                var favorite = new Favorite()
                {
                    UserId = favoriteToCreate.UserId,
                    InstagramId = favoriteToCreate.InstagramId,
                    TagName = favoriteToCreate.TagName,
                    CreatedOn = DateTime.UtcNow
                };

                // Persist
                _favoritesRepository.Add(favorite);
                dbContextScope.SaveChanges();
            }
        }
    }
}
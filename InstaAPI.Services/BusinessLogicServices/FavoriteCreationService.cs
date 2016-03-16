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

        public FavoriteCreationService(IDbContextScopeFactory dbContextScopeFactory, IFavoriteRepository favoriteRepository)
        {
            if (dbContextScopeFactory == null) throw new ArgumentNullException(nameof(dbContextScopeFactory));
            if (favoriteRepository == null) throw new ArgumentNullException(nameof(favoriteRepository));
            _dbContextScopeFactory = dbContextScopeFactory;
            _favoritesRepository = favoriteRepository;
        }

        void IFavoriteCreationService.CreateFavorite(FavoriteCreationSpec favoriteToCreate)
        {
            if (favoriteToCreate == null)
                throw new ArgumentNullException(nameof(favoriteToCreate));

            favoriteToCreate.Validate();
           
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                // Build domain model
                var favorite = new Favorite()
                {
                    Id = favoriteToCreate.Id,                   
                    CreatedOn = DateTime.UtcNow
                };

                // Persist
                _favoritesRepository.Add(favorite);
                dbContextScope.SaveChanges();
            }
        }
    }
}
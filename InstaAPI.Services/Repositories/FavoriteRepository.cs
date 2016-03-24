using InstaAPI.Context;
using InstaAPI.Services.DatabaseContext;
using InstaAPI.Services.DomainModel;
using InstaAPI.Services.Repositories.Interfaces;
using System;
using System.Linq;

namespace InstaAPI.Services.Repositories
{
    public class FavoriteRepository : BaseRepository, IFavoriteRepository
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        private FavoriteManagementDbContext DbContext
        {
            get
            {
                var dbContext = _ambientDbContextLocator.Get<FavoriteManagementDbContext>();

                if (dbContext == null)
                    throw new InvalidOperationException("FavoriteManagementDbContext - " +
                                                        NoAmbientDbContextFoundMessage);

                return dbContext;
            }
        }

        public FavoriteRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if (ambientDbContextLocator == null) throw new ArgumentNullException(nameof(ambientDbContextLocator));
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        void IFavoriteRepository.Add(Favorite favorite)
        {
            DbContext.Favorites.Add(favorite);
        }

        Favorite IFavoriteRepository.Get(string userid, string instagramId, string tagname)
        {
            return (from f in DbContext.Favorites
                    where f.UserId == userid
                          && f.InstagramId == instagramId
                          && f.TagName == tagname
                    select f).SingleOrDefault();
        }

        Post IFavoriteRepository.Get(string instagramId)
        {
            return DbContext.Posts.Find(instagramId);
        }
    }
}
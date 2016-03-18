using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using InstaAPI.Context;
using InstaAPI.Services.BusinessLogicServices.Interfaces;
using InstaAPI.Services.DatabaseContext;
using InstaAPI.Services.DomainModel;

namespace InstaAPI.Services.BusinessLogicServices
{
    public class FavoriteQueryService : IFavoriteQueryService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public FavoriteQueryService(IDbContextScopeFactory dbContextScopeFactory)
        {
            if (dbContextScopeFactory == null) throw new ArgumentNullException(nameof(dbContextScopeFactory));            
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        /*
	    * An example of using DbContextScope for read-only queries. 
	    * Here, we access the Entity Framework DbContext directly from 
	    * the business logic service class.
	    * 
	    * Calling SaveChanges() is not necessary here (and in fact not 
	    * possible) since we created a read-only scope.
	    */
        List<Favorite> IFavoriteQueryService.GetFavorites(string userId)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbContextScope.DbContexts.Get<FavoriteManagementDbContext>();
                var favorites = (from f in dbContext.Favorites
                                 where f.UserId == userId
                    select f).Include(o => o.Post).ToList();
                
                return favorites;
            }            
        }

        List<FavoriteMetric> IFavoriteQueryService.GetMetrics(string userId)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbContextScope.DbContexts.Get<FavoriteManagementDbContext>();
                var metrics = (from f in dbContext.Favorites
                               where f.UserId == userId
                               group f by f.TagName into g
                                 select new FavoriteMetric() {TagName = g.Key, Count = g.Count()}).ToList();

                return metrics;
            }
        }
    }
}
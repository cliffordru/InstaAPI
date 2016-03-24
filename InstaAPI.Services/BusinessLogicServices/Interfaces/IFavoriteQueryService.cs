using InstaAPI.Services.DomainModel;
using System.Collections.Generic;

namespace InstaAPI.Services.BusinessLogicServices.Interfaces
{
    public interface IFavoriteQueryService
    {
        List<Favorite> GetFavorites(string userId);
        List<FavoriteMetric> GetMetrics(string userId);
    }
}

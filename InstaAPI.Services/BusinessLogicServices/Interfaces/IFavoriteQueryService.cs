using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using InstaAPI.Services.DomainModel;

namespace InstaAPI.Services.BusinessLogicServices.Interfaces
{
    public interface IFavoriteQueryService
    {
        List<Favorite> GetFavorites(string userId);
        List<FavoriteMetric> GetMetrics(string userId);
    }
}

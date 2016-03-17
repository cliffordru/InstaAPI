using InstaAPI.Services.DomainModel;

namespace InstaAPI.Services.Repositories.Interfaces
{
    public interface IFavoriteRepository
    {
        Favorite Get(string userid, string instagramId, string tagname);
        Post Get(string instagramId);
        void Add(Favorite favorite);
    }
}

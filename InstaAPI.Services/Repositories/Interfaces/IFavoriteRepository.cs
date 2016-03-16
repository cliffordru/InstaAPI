using InstaAPI.Services.DomainModel;

namespace InstaAPI.Services.Repositories.Interfaces
{
    public interface IFavoriteRepository
    {       
        void Add(Favorite favorite);
    }
}

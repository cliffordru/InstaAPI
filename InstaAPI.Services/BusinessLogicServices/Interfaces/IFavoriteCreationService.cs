using InstaAPI.Services.CommandModel;

namespace InstaAPI.Services.BusinessLogicServices.Interfaces
{
    public interface IFavoriteCreationService
    {
        void CreateFavorite(FavoriteCreationSpec favoriteToCreate);
    }
}

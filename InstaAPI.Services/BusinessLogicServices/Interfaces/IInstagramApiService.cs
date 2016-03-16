using InstaAPI.Services.DomainModel;

namespace InstaAPI.Services.BusinessLogicServices.Interfaces
{
    public interface IInstagramApiService
    {
        InstaPosts GetPostsByTag(string tag);
            
    }
}

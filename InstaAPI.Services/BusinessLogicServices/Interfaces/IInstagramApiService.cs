using InstaAPI.Services.DomainModel.Instragram;

namespace InstaAPI.Services.BusinessLogicServices.Interfaces
{
    public interface IInstagramApiService
    {
        InstaPostsRoot GetPostsByTag(string tag);

        InstaPostRoot GetPost(string instagramId);

    }
}

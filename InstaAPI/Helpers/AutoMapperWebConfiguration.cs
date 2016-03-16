using AutoMapper;
using InstaAPI.Models;
using InstaAPI.Services.DomainModel;

namespace InstaAPI.Helpers
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new PostsTagProfile());
            });
        }        
    }

    public class PostsTagProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<InstaPostsData, PostsTagViewModel>()
                .ForMember(dest => dest.InstagramId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Link));
        }
    }    
}
using AutoMapper;
using InstaAPI.Models;
using InstaAPI.Services.DomainModel;
using InstaAPI.Services.DomainModel.Instragram;

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
#pragma warning disable 618
            Mapper.CreateMap<InstaPostData, PostsTagViewModel>()

                .ForMember(dest => dest.InstagramId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Link));

            Mapper.CreateMap<Favorite, PostsFavoriteViewModel>()
                .ForMember(dest => dest.InstagramId, opt => opt.MapFrom(src => src.InstagramId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Post.InstagramUserName))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Post.Url))
                .ForMember(dest => dest.TagName, opt => opt.MapFrom(src => src.TagName));

            Mapper.CreateMap<FavoriteMetric, PostsFavoriteMetricsViewModel>()         
                .ForMember(dest => dest.TagName, opt => opt.MapFrom(src => src.TagName))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));
#pragma warning restore 618
        }
    }
}
using AutoMapper;
using WAG.Data.Models;
using WAG.ViewModels.Blog;

namespace WAG.WebApp.MappingConfigurations.Mapping
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            this.CreateMap<Article, ArticleDetailsViewModel>()
                .ForMember(dest => dest.ArticleId, opt => opt.MapFrom(src => src.Id));

            this.CreateMap<CreateArticleViewModel, Article>();

            this.CreateMap<Article, EditArticleViewModel>()
                .ForMember(dest => dest.ArticleId, opt => opt.MapFrom(src => src.Id));

            this.CreateMap<EditArticleViewModel, Article>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ArticleId));

            this.CreateMap<Article, DeleteArticleViewModel>()
                .ForMember(dest => dest.ArticleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ArticleTitle, opt => opt.MapFrom(src => src.Title));
        }
        
    }
}

using AutoMapper;
using WAG.Data.Models;
using WAG.ViewModels.ArtisticWorks;

namespace WAG.WebApp.MappingConfigurations.Mapping
{
    public class ArtisticWorkProfile : Profile
    {
        public ArtisticWorkProfile()
        {
            // artwork

            this.CreateMap<AddArtWorkViewModel, ArtisticWork>()
                .ForMember(dest => dest.ArtisticWorkCategoryId, opt => opt.MapFrom(src => src.CategoryId));

            this.CreateMap<ArtisticWork, EditArtWorkViewModel>();

            this.CreateMap<EditArtWorkViewModel, ArtisticWork>();

            this.CreateMap<ArtisticWork, ArtWorkDetailsViewModel>();

            this.CreateMap<ArtisticWork, DeleteArtWorkViewModel>();

            this.CreateMap<ArtisticWork, ArtWorkByCategoryViewModel>()
                .ForMember(dest => dest.ArtWorkId, opt => opt.MapFrom(src => src.Id));


            // artwork category

            this.CreateMap<ArtisticWorkCategory, ArtWorkCategoryViewModel>();

            this.CreateMap<AddCategoryViewModel, ArtisticWorkCategory>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.MainPictureFileName, opt => opt.MapFrom(src => src.PictureFileName));

            this.CreateMap<ArtisticWorkCategory, EditCategoryViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PictureFileName, opt => opt.MapFrom(src => src.MainPictureFileName));

            this.CreateMap<EditCategoryViewModel, ArtisticWorkCategory>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.MainPictureFileName, opt => opt.MapFrom(src => src.PictureFileName));

            this.CreateMap<ArtisticWorkCategory, DeleteCategoryViewModel>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id));
        }
    }
}

using AutoMapper;
using WAG.Data.Models;
using WAG.ViewModels.UserAccount;

namespace WAG.WebApp.MappingConfigurations.Mapping
{
    public class UserAccountProfile : Profile
    {
        public UserAccountProfile()
        {
            this.CreateMap<WAGUser, UserDetailsViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            this.CreateMap<RegisterInputViewModel, WAGUser>();

            this.CreateMap<WAGUser, EditUserProfileViewModel>();

        }
    }
}

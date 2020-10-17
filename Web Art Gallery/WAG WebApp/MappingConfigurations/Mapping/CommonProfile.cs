using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAG.Data.Models;
using WAG.ViewModels.Home;

namespace WAG.WebApp.MappingConfigurations.Mapping
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            this.CreateMap<ContactMessageViewModel, ContactMessage>();
        }
    }
}

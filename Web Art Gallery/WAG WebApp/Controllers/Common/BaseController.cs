using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WAG.WebApp.Controllers.Common
{
    public class BaseController : Controller
    {
        protected readonly IMapper mapper;

        public BaseController(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}
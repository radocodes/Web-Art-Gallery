using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Constants;
using WAG.WebApp.Controllers.Common;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdminRole)]
    [Area(GlobalConstants.AdminArea)]
    public class AdministrationController : BaseController
    {
        public AdministrationController(IMapper mapper)
            : base(mapper)
        {
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Constants;

namespace WAG.WebApp.Areas.BG.Controllers
{
    [Authorize(Roles = GlobalConstants.AdminRole)]
    [Area(GlobalConstants.BGArea)]
    public class BGController : Controller
    {
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Constants;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdminRole)]
    [Area(GlobalConstants.AdminArea)]
    public class AdministrationController : Controller
    {
    }
}
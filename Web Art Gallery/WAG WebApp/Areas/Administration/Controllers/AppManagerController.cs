using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class AppManagerController : AdministrationController
    {
        public AppManagerController(IMapper mapper) 
            : base(mapper)
        {

        }
        public IActionResult IndexAdmin()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class AppManagerController : AdministrationController
    {
        public IActionResult IndexAdmin()
        {
            return View();
        }
    }
}
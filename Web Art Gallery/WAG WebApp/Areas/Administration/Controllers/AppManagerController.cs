using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WAG.ViewModels.InputViewModels;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    
    public class AppManagerController : AdministrationController
    {
        public IActionResult IndexAdmin()
        {
            return View();
        }

        public IActionResult ManageUsers()
        {
            return View();
        }

        public IActionResult AddUserInRole()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WAG.Data.Models;
using WAG.Services.Interfaces;
using WAG.ViewModels.Users;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class UserController : AdministrationController
    {
        private IUserAccountService UserAccountService;
        private IUserService UserService;

        public UserController(IUserService userService, IUserAccountService userAccountService)
        {
            this.UserAccountService = userAccountService;
            this.UserService = userService;
        }

        public IActionResult ManageUsers()
        {
            return View();
        }

        public IActionResult AllUsers(AllUsersViewModel allUsersViewModel)
        {
            allUsersViewModel.AllUsers = this.UserService.GetAllUsers().ToList();

            return View(allUsersViewModel);
        }

        public IActionResult UserDetails(string id, UserDetailsViewModel userDetailsViewModel)
        {
            var currUser = this.UserService.GetUserById(id);

            var userRoles = this.UserService.GetUserRolesNameById(id).ToList();

            userDetailsViewModel.User = currUser;

            userDetailsViewModel.IdentityRoles = userRoles;

            return View(userDetailsViewModel);
        }

        public IActionResult DeleteUser(string id, UserDetailsViewModel userDetailsViewModel)
        {
            userDetailsViewModel.User = this.UserService.GetUserById(id);

            return View(userDetailsViewModel);
        }

        [HttpPost]
        public IActionResult DeleteUser(string id)
        {
            this.UserAccountService.DeleteUser(id);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult AddUserInRole()
        {
            return View();
        }
    }
}
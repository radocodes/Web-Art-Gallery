using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WAG.Common.User;
using WAG.Data.Models;
using WAG.Services.Interfaces;
using WAG.ViewModels.UserAccount;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class UserAccountController : AdministrationController
    {
        private IUserAccountService UserAccountService;

        public UserAccountController(IUserAccountService userAccountService)
        {
            this.UserAccountService = userAccountService;
        }

        public IActionResult ManageUsers()
        {
            return View();
        }

        public IActionResult AllUsers(AllUsersViewModel allUsersViewModel)
        {
            allUsersViewModel.AllUsers = this.UserAccountService.GetAllUsers().ToList();

            return View(allUsersViewModel);
        }

        public IActionResult UserDetails(string id, UserDetailsViewModel userDetailsViewModel)
        {
            var currUser = this.UserAccountService.GetUserById(id);

            var userRoles = this.UserAccountService.GetUserRolesNameById(id).ToList();

            userDetailsViewModel.User = currUser;

            userDetailsViewModel.IdentityRoles = userRoles;

            return View(userDetailsViewModel);
        }

        public IActionResult DeleteUser(string id, UserDetailsViewModel userDetailsViewModel)
        {
            userDetailsViewModel.User = this.UserAccountService.GetUserById(id);

            return View(userDetailsViewModel);
        }

        [HttpPost]
        public IActionResult DeleteUser(string id)
        {
            this.UserAccountService.DeleteUser(id);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult MakeUserAdmin(string userId, MakeUserAdminViewModel makeUserAdminViewModel)
        { 
            makeUserAdminViewModel.User = this.UserAccountService.GetUserById(userId);

            return View(makeUserAdminViewModel);
        }

        [HttpPost]
        public IActionResult MakeUserAdmin(string userId)
        {
            var adminRoleName = nameof(UserRoles.Admin);

            var addingResult = this.UserAccountService.AddUserInRoleAsync(userId, adminRoleName).Result;

            if (addingResult.Succeeded)
            {
                return RedirectToAction("Success", "Home", new { area = "" });
            }

            return RedirectToAction("AllUsers", "UserAccount", new { area = "Administration"});
        }

        public IActionResult RemoveUserFromAdminRole(string userId, RemoveUserFromAdminRoleViewModel removeUserFromAdminRoleViewModel)
        {
            removeUserFromAdminRoleViewModel.User = this.UserAccountService.GetUserById(userId);

            return View(removeUserFromAdminRoleViewModel);
        }

        [HttpPost]
        public IActionResult RemoveUserFromAdminRole(string id)
        {
            var adminRoleName = nameof(UserRoles.Admin);

            var removingResult = this.UserAccountService.RemoveUserFromRoleAsync(id, adminRoleName).Result;

            if (removingResult.Succeeded)
            {
                return RedirectToAction("Success", "Home", new { area = "" });
            }

            return RedirectToAction("AllUsers", "UserAccount", new { area = "Administration" });
        }
    }
}
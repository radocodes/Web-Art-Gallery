using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Constants;
using WAG.Services.Interfaces;
using WAG.ViewModels.UserAccount;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class UserAccountController : AdministrationController
    {
        private IUserAccountService UserAccountService;

        public UserAccountController(IUserAccountService userAccountService, IMapper mapper)
            : base(mapper)
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

        public IActionResult UserDetails(string id)
        {
            var currUser = this.UserAccountService.GetUserById(id);

            if (currUser == null)
            {
                return RedirectToAction("AllUsers", "UserAccount");
            }

            UserDetailsViewModel viewModel = mapper.Map<UserDetailsViewModel>(currUser);
            viewModel.IdentityRoles = this.UserAccountService.GetUserRolesNameById(id).ToList();

            return View(viewModel);
        }

        public IActionResult DeleteUser(string id)
        {
            var UserToDelete = this.UserAccountService.GetUserById(id);

            if (UserToDelete == null)
            {
                return RedirectToAction("AllUsers", "UserAccount");
            }

            UserDetailsViewModel viewModel = mapper.Map<UserDetailsViewModel>(UserToDelete);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteUserPost(string id)
        {
            var user = this.UserAccountService.GetUserById(id);

            if (user == null)
            {
                return RedirectToAction("AllUsers", "UserAccount");
            }

            this.UserAccountService.DeleteUser(id);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult MakeUserAdmin(string userId)
        {
            var viewModel = new MakeUserAdminViewModel()
            {
                User = this.UserAccountService.GetUserById(userId)
            };

            if (viewModel.User == null)
            {
                return RedirectToAction("AllUsers", "UserAccount");
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult MakeUserAdminPost(string userId)
        {
            var adminRoleName = GlobalConstants.AdminRole;

            var user = this.UserAccountService.GetUserById(userId);

            if (user == null)
            {
                return RedirectToAction("AllUsers", "UserAccount");
            }

            var addingResult = this.UserAccountService.AddUserInRoleAsync(user, adminRoleName).Result;

            if (addingResult.Succeeded)
            {
                return RedirectToAction("Success", "Home", new { area = "" });
            }

            return RedirectToAction("AllUsers", "UserAccount", new { area = "Administration" });
        }

        public IActionResult RemoveUserFromAdminRole(string userId)
        {
            var removeUserFromAdminRoleViewModel = new RemoveUserFromAdminRoleViewModel()
            {
                User = this.UserAccountService.GetUserById(userId)
            };

            if (removeUserFromAdminRoleViewModel.User == null)
            {
                return RedirectToAction("AllUsers", "UserAccount");
            }

            return View(removeUserFromAdminRoleViewModel);
        }

        [HttpPost]
        public IActionResult RemoveUserFromAdminRolePost(string id)
        {
            var user = this.UserAccountService.GetUserById(id);

            if (user == null)
            {
                return RedirectToAction("AllUsers", "UserAccount");
            }

            var adminRoleName = GlobalConstants.AdminRole;
            var removingResult = this.UserAccountService.RemoveUserFromRoleAsync(user, adminRoleName).Result;

            if (removingResult.Succeeded)
            {
                return RedirectToAction("Success", "Home", new { area = "" });
            }

            return RedirectToAction("AllUsers", "UserAccount", new { area = "Administration" });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WAG.Common.User;
using WAG.Data;
using WAG.Data.Models;
using WAG.Services.Interfaces;
using WAG.ViewModels.UserAccount;

namespace WAG.Services
{
    public class UserAccountService : IUserAccountService
    {
        private SignInManager<WAGUser> SignInManager;
        private UserManager<WAGUser> UserManager;
        private RoleManager<IdentityRole> RoleManager;
        private WAGDbContext DbContext;

        public UserAccountService(SignInManager<WAGUser> signInManager, UserManager<WAGUser> userManager, RoleManager<IdentityRole> roleManager, WAGDbContext dbContext)
        {
            this.SignInManager = signInManager;
            this.UserManager = userManager;
            this.RoleManager = roleManager;
            this.DbContext = dbContext;
        }

        public SignInResult LoginUserSuccessfully(LoginInputViewModel loginInputViewModel)
        {
            var user = this.DbContext.Users.FirstOrDefault(u => u.UserName == loginInputViewModel.Username);

            if (user == null)
            {
                return SignInResult.Failed;
            }

            var inputPassword = loginInputViewModel.Password;

            return this.SignInManager.PasswordSignInAsync(user, inputPassword, true, false).Result;
        }

        public async Task<IdentityResult> RegisterUserSuccessfullyAsync(RegisterInputViewModel registerInputViewModel)
        {
            var user = new WAGUser()
            {
                UserName = registerInputViewModel.UserName,
                FirstName = registerInputViewModel.FirstName,
                LastName = registerInputViewModel.LastName,
                City = registerInputViewModel.City,
                Email = registerInputViewModel.Email,
                PhoneNumber = registerInputViewModel.PhoneNumber,
                Address = registerInputViewModel.Address
            };

            return await this.UserManager.CreateAsync(user, registerInputViewModel.Password);
        }

        public async void Logout()
        {
            await this.SignInManager.SignOutAsync();
        }

        public void EditUserProfile(WAGUser user, EditUserProfileInputViewModel editUserProfileInputViewModel)
        {
            this.DbContext.Users.First(u => u == user).FirstName = editUserProfileInputViewModel.FirstName;
            this.DbContext.Users.First(u => u == user).LastName = editUserProfileInputViewModel.LastName;
            this.DbContext.Users.First(u => u == user).Email = editUserProfileInputViewModel.Email;
            this.DbContext.Users.First(u => u == user).PhoneNumber = editUserProfileInputViewModel.PhoneNumber;
            this.DbContext.Users.First(u => u == user).City = editUserProfileInputViewModel.City;
            this.DbContext.Users.First(u => u == user).Address = editUserProfileInputViewModel.Address;

            this.DbContext.SaveChanges();
        }

        public void ChangePassword(WAGUser user, string currPassword, string newPassword)
        {
             var result = this.UserManager.ChangePasswordAsync(user, currPassword, newPassword).Result;
        }

        public void DeleteUser(string id)
        {
            var user = GetUserById(id);

            if (user != null)
            {
                this.DbContext.Users.Remove(user);

                this.DbContext.SaveChanges();
            }
        }

        public IEnumerable<WAGUser> GetAllUsers()
        {
            var allUsers = this.DbContext.Users.ToList();

            return allUsers;
        }

        public WAGUser GetUserById(string id)
        {
            var currUser = DbContext.Users.FirstOrDefault(user => user.Id == id);

            return currUser;
        }

        public IList<string> GetUserRolesNameById(string id)
        {
            var user = GetUserById(id);

            var roles = UserManager.GetRolesAsync(user).Result;

            return roles;
        }

        public async Task<IdentityResult> AddUserInRoleAsync(string userId, string role)
        {
            var user = GetUserById(userId);

            var result = new IdentityResult();

            if (user != null)
            {
                if (this.RoleManager.RoleExistsAsync(role).Result)
                {
                    if (!UserManager.IsInRoleAsync(user, role).Result)
                    {
                        result = await this.UserManager.AddToRoleAsync(user, role);
                    }
                }
            }

            return result;
        }

        public async Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string role)
        {
            var user = GetUserById(userId);

            var result = new IdentityResult();

            if (user != null)
            {
                if (this.RoleManager.RoleExistsAsync(role).Result)
                {
                    if (UserManager.IsInRoleAsync(user, role).Result)
                    {
                        result = await this.UserManager.RemoveFromRoleAsync(user, role);
                    }
                }
            }

            return result;
        }

        public List<string> GetRolesList()
        {
            var roles = new List<string>();

            foreach (var role in Enum.GetNames(typeof(UserRoles)))
            {
                roles.Add(role);
            }

            return roles;
        }

        public WAGUser GetCurrentUser(HttpContext httpContext)
        {
            var currentUser = this.UserManager.GetUserAsync(httpContext.User).Result;

            return currentUser;
        }
    }
}

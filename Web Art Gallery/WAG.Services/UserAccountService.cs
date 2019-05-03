using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<SignInResult> RegisterUserSuccessfullyAsync(RegisterInputViewModel registerInputViewModel)
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

            var created = this.UserManager.CreateAsync(user, registerInputViewModel.Password).Result;

            if (!created.Succeeded)
            {
                //(TODO Error)?
            }

            var result = await this.SignInManager.PasswordSignInAsync(user, registerInputViewModel.Password, true, false);

            return result;
        }

        public async void Logout()
        {
            await this.SignInManager.SignOutAsync();
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
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WAG.Data.Models;
using WAG.ViewModels.UserAccount;

namespace WAG.Services.Interfaces
{
    public interface IUserAccountService
    {
        SignInResult LoginUserSuccessfully(LoginInputViewModel loginInputViewModel);

        Task<IdentityResult> RegisterUserSuccessfullyAsync(RegisterInputViewModel registerInputViewModel);

        void Logout();

        void EditUserProfile(WAGUser user, EditUserProfileInputViewModel editUserProfileInputViewModel);

        void ChangePassword(WAGUser user, string currPassword, string newPassword);

        void DeleteUser(string id);

        IEnumerable<WAGUser> GetAllUsers();

        WAGUser GetUserById(string id);

        IList<string> GetUserRolesNameById(string id);

        Task<IdentityResult> AddUserInRoleAsync(string userId, string role);

        Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string role);

        List<string> GetRolesList();

        WAGUser GetCurrentUser(HttpContext httpContext);

    }
}

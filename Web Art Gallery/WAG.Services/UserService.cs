using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WAG.Data;
using WAG.Data.Models;
using WAG.Services.Interfaces;

namespace WAG.Services
{
    public class UserService : IUserService
    {
        private WAGDbContext DbContext;
        private UserManager<WAGUser> UserManager;

        public UserService(WAGDbContext dbContext, UserManager<WAGUser> userManager)
        {
            this.DbContext = dbContext;
            this.UserManager = userManager;
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

    }
}

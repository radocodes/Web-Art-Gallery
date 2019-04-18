using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAG.Data;
using WAG.Data.Models;

namespace WAG.WebApp.Middlewares
{
    public class SeedDataMiddleware
    {
        private readonly RequestDelegate _next;

        public SeedDataMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<WAGUser> userManager,
                                      RoleManager<IdentityRole> roleManager, WAGDbContext db)
        {
            SeedRole(roleManager, "Admin").GetAwaiter().GetResult();

            SeedUserInRoles(userManager).GetAwaiter().GetResult();

            await _next(context);
        }

        private static async Task SeedRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        private static async Task SeedUserInRoles(UserManager<WAGUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new WAGUser
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    FirstName = "AdminFirstName",
                    LastName = "AdminLastName",
                };

                var password = "123456";

                var userCreated = await userManager.CreateAsync(user, password);

                if (userCreated.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}

using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using Barbarians.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barbarians.Data.Seeders
{
    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var userService = new UsersService(dbContext);

            if (dbContext.Users.Count() >= 3)
            {
                return;
            }

            var users = new List<(string Username, string Email, string Password, string Role, int Health)>
            {
                ("Salamander", "asd@abv.bg", "Asd123", "User", 100),
                ("SalamanderAdmin", "asd@abv.bg", "Asd123", "Admin", 100),
                ("SalamanderOwner", "asd@abv.bg", "Asd123", "Owner", 100),
            };

            foreach (var user in users)
            {
                var foo = new ApplicationUser
                {
                    UserName = user.Username,
                    Email = user.Email,
                    Health = user.Health,
                };

                var result = await userManager.CreateAsync(foo, user.Password);

                if (result.Succeeded)
                {
                    string role = IdentityRoles.UserRoleName;
                    if (foo.UserName == "SalamanderAdmin")
                    {
                        role = IdentityRoles.AdministratorRoleName;
                    }
                    else if (foo.UserName == "SalamanderOwner")
                    {
                        role = IdentityRoles.OwnerRoleName;
                    }

                    await userManager.AddToRoleAsync(foo, role);
                    await userService.SeedDatabaseOnSuccessfulRegister(foo.Id);
                }
            }
        }
    }
}

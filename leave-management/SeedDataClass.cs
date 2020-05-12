using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management
{
    public static class SeedDataClass
    {
        public static void Seed(UserManager<IdentityUser> par_locUserManager,
            RoleManager<IdentityRole> par_locRoleManager)
        {
            SeedRoles(par_locRoleManager);
            SeedUsers(par_locUserManager);

        }

        private static void SeedRoles(RoleManager<IdentityRole> par_locRoleManager)
        {
            if (!par_locRoleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole locIdentityClass = new IdentityRole
                {
                    Name = "Administrator"
                };

                var varResult = par_locRoleManager.CreateAsync(locIdentityClass).Result;

            }

            if (!par_locRoleManager.RoleExistsAsync("Employee").Result)
            {
                IdentityRole locIdentityClass = new IdentityRole
                {
                    Name = "Employee"
                };

                var varResult = par_locRoleManager.CreateAsync(locIdentityClass).Result;

            }
        }

        private static void SeedUsers(UserManager<IdentityUser> par_locUserManager)
        {   
            if (par_locUserManager.FindByEmailAsync("Admin@Localhost.co.za").Result == null)
            {
                IdentityUser locIdentityUser = new IdentityUser
                {
                    UserName = "Admin@Localhost.co.za",
                    Email = "Admin@Localhost.co.za"
                };

                var varResult = par_locUserManager.CreateAsync(locIdentityUser, "NikNax0107!").Result;
                if (varResult.Succeeded)
                {
                    var varResultX = par_locUserManager.AddToRoleAsync(locIdentityUser, "Administrator").Result;
                }
            }
        }
    }
}
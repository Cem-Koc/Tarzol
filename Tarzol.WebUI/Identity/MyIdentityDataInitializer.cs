using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Identity
{
    public static class MyIdentityDataInitializer
    {
        public static void SeedRoles(RoleManager<AppRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                AppRole role = new AppRole();
                role.Name = "Administrator";
                role.Status = Core.Enums.Status.Active;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                AppRole role = new AppRole();
                role.Name = "User";
                role.Status = Core.Enums.Status.Active;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Seller").Result)
            {
                AppRole role = new AppRole();
                role.Name = "Seller";
                role.Status = Core.Enums.Status.Active;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}

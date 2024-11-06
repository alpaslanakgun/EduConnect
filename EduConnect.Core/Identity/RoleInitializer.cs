using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Core.Identity
{
    public class RoleInitializer
    {

        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Rol yoksa oluştur
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}

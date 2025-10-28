using Microsoft.AspNetCore.Identity;
using SoldadosDoImperador.Areas.Identity.Data;

namespace SoldadosDoImperador.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            
            string rolePrimarch = "PRIMARCH";
            string roleAstartes = "Astartes"; 

          
            if (!await roleManager.RoleExistsAsync(rolePrimarch))
            {
                await roleManager.CreateAsync(new IdentityRole(rolePrimarch));
            }
           
            if (!await roleManager.RoleExistsAsync(roleAstartes)) 
            {
                await roleManager.CreateAsync(new IdentityRole(roleAstartes));
            }

           
            string adminEmail = "PrimarchFerreira@ultramarine.com";
            string adminPassword = "Astarte!123"; 

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, rolePrimarch); 
                }
            }
            else
            {
                if (!await userManager.IsInRoleAsync(adminUser, rolePrimarch))
                {
                    await userManager.AddToRoleAsync(adminUser, rolePrimarch);
                }
            }
            
        }
    }
}
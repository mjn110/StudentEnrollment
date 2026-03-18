using Microsoft.AspNetCore.Identity;
using StudentEnrolment.Shared.Models;

namespace StudentEnrolment.Server.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Ensure Admin role exists
            const string adminRole = "Admin";
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Ensure Admin user exists
            const string adminEmail = "admin@auth.com";
            const string adminPassword = "PASSw0rd!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
                else
                {
                    throw new Exception("Failed to create admin user: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }

    }
}

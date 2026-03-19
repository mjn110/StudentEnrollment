using Microsoft.AspNetCore.Identity;
using StudentEnrolment.Shared.Models;

namespace StudentEnrolment.Server.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger("IdentitySeeder");


            try
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                logger.LogInformation("Starting identity seeding...");

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    logger.LogInformation("Admin role does not exist. Creating...");
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                var admin = await userManager.FindByEmailAsync("admin@auth.com");

                if (admin == null)
                {
                    logger.LogInformation("Admin user not found. Creating...");
                    admin = new ApplicationUser
                    {
                        UserName = "admin@auth.com",
                        Email = "admin@auth.com",
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(admin, "Admin123!");

                    if (!result.Succeeded)
                    {
                        logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }

                if (!await userManager.IsInRoleAsync(admin, "Admin"))
                {
                    logger.LogInformation("Assigning Admin role...");
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

                logger.LogInformation("Identity seeding completed.");
            }
            catch (Exception ex)
            {
                var logger2 = serviceProvider
                    .GetRequiredService<ILoggerFactory>()
                    .CreateLogger("IdentitySeeder");
                logger2.LogError(ex, "Error during identity seeding");
                throw;
            }
        }

    }
}

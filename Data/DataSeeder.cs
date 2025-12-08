using Microsoft.AspNetCore.Identity;
using YfitopsApp.Entities;

namespace YfitopsApp.Data
{
    public static class DataSeeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "User", "Artist"};
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public static async Task SeedAdminUser(UserManager<User> userManager, YfitopsDbContext context)
        {
            string adminEmail = "ado.strba@gmail.com";
            string fullName = "Andrej Štrba";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = fullName
                };

                var result = await userManager.CreateAsync(user, "asd461436");
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                var roleResult = await userManager.AddToRoleAsync(user, "Admin");
                if (!roleResult.Succeeded)
                {
                    throw new Exception("Failed to assign admin role: " + string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                }
                var roleResultArtist = await userManager.AddToRoleAsync(user, "Artist");
                if (!roleResultArtist.Succeeded)
                {
                    throw new Exception("Failed to assign artist role: " + string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}

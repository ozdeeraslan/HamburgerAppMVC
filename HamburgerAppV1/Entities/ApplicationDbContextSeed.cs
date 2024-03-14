using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HamburgerAppV1.Entities
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleMAnager, UserManager<IdentityUser> userManager)
        {
            if (!await roleMAnager.RoleExistsAsync("Administrator"))
            {
                await roleMAnager.CreateAsync(new IdentityRole("Administrator"));
            }

            if (!await userManager.Users.AnyAsync(u => u.UserName == "admin@example.com"))
            {
                var user = new IdentityUser()
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, "P@ssword1");
                await userManager.AddToRoleAsync(user, "Administrator");
            }
        }
    }
}

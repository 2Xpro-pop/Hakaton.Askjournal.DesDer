
using DesDer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class DataSeeder 
{
    public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationContext appContext, IWebHostEnvironment webHostEnvironment)
    {
        await AddUsers(userManager, roleManager);

        await AddHeaderBanners(appContext, webHostEnvironment);
    }

    private static async Task AddHeaderBanners(ApplicationContext appContext, IWebHostEnvironment webHostEnvironment)
    {
        var first = await appContext.Files.Where(x => x.Path.Contains("/imgs/banner/header")).FirstOrDefaultAsync();

        if(first is null)
        {
            var files = Directory.GetFiles(webHostEnvironment.WebRootPath + "/imgs/banner/header");
            foreach (var file in files)
            {
                var name = Path.GetFileName(file);
                var path = $"/imgs/banner/header/{name}";
                var fileModel = new FileModel
                {
                    Name = name,
                    Path = path
                };
                await appContext.Files.AddAsync(fileModel);
            }

            await appContext.SaveChangesAsync();
        }
    }

    private static async Task AddUsers(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Создание ролей
        foreach (var role in Enum.GetNames<UserRole>())
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        // Создание пользователей
        if (await userManager.FindByNameAsync("admin") == null)
        {
            var user = new User
            {
                UserName = "admin",
                FullName = "Администратор",
                Role = UserRole.Admin
            };
            var result = await userManager.CreateAsync(user, "Admin@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "admin");
            }
        }
        if (await userManager.FindByNameAsync("editor") == null)
        {
            var user = new User
            {
                UserName = "editor",
                FullName = "Пользователь",
                Role = UserRole.Editor
            };
            var result = await userManager.CreateAsync(user, "Editor@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Editor");
            }
        }
    }
}
using Microsoft.AspNetCore.Identity;
using test_versta.Models;

namespace test_versta.Services
{
    /// <summary>
    /// Класс для начальной инициализации данных в системе.
    /// Создает роли и администратора, если они отсутствуют.
    /// </summary>
    public class InitialData
    {
        /// <summary>
        /// Инициализирует роли и администратора в системе.
        /// </summary>
        /// <param name="userManager">Менеджер пользователей.</param>
        /// <param name="roleManager">Менеджер ролей.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task SeedRolesAndAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Administrator", "Client" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            const string adminEmail = "admin@example.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Администратор"
                };
                
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Administrator");
                }
            }
        }
    }
}
using Microsoft.AspNetCore.Identity;

namespace test_versta.Models
{
    /// <summary>
    /// Представляет пользователя в системе с поддержкой ASP.NET Identity.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Полное имя пользователя.
        /// </summary>
        public string FullName { get; set; }
    }
}
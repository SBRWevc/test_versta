using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using test_versta.Models;

namespace test_versta.Data
{
    /// <summary>
    /// Контекст базы данных приложения, расширяющий IdentityDbContext для работы с аутентификацией и авторизацией.
    /// </summary>
    /// <remarks>
    /// Этот контекст содержит таблицы для пользователей и заказов.
    /// </remarks>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <param name="options">Опции конфигурации для контекста базы данных.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Коллекция заказов в базе данных.
        /// </summary>
        public DbSet<Order> Orders { get; set; }
    }
}
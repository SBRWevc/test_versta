using System.ComponentModel.DataAnnotations;

namespace test_versta.ViewModels
{
    /// <summary>
    /// Модель представления для регистрации нового пользователя.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Полное имя пользователя.
        /// </summary>
        [Required]
        public string FullName { get; set; }
        
        /// <summary>
        /// Адрес электронной почты пользователя.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        /// <summary>
        /// Подтверждение пароля. Должно совпадать с полем <see cref="Password"/>.
        /// </summary>
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }
        
        /// <summary>
        /// URL-адрес, на который будет перенаправлен пользователь после успешной регистрации.
        /// </summary>
        public string? ReturnUrl { get; set; }
    }
}
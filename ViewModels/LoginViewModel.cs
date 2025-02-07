using System.ComponentModel.DataAnnotations;

namespace test_versta.ViewModels
{
    /// <summary>
    /// Модель представления для входа в систему.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        [Required(ErrorMessage = "Введите адрес электронной почты.")]
        [EmailAddress(ErrorMessage = "Введите корректный адрес электронной почты.")]
        public string Email { get; set; }
        
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required(ErrorMessage = "Введите пароль.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        /// <summary>
        /// Запомнить пользователя при следующем входе.
        /// </summary>
        public bool RememberMe { get; set; }
        
        /// <summary>
        /// URL, на который будет перенаправлен пользователь после успешного входа.
        /// </summary>
        public string? ReturnUrl { get; set; }
    }
}
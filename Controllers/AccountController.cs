using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using test_versta.Models;
using test_versta.ViewModels;

namespace test_versta.Controllers
{
    /// <summary>
    /// Контроллер для управления учетными записями пользователей.
    /// Позволяет выполнять вход, регистрацию и выход из системы.
    /// </summary>
    public class AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        : Controller
    {
        /// <summary>
        /// Отображает страницу входа.
        /// </summary>
        /// <param name="returnUrl">URL для перенаправления после успешного входа.</param>
        /// <returns>Страница входа.</returns>
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        
        /// <summary>
        /// Обрабатывает попытку входа пользователя.
        /// </summary>
        /// <param name="model">Модель данных для входа.</param>
        /// <returns>Перенаправляет на указанную страницу или страницу заказов.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);
                return RedirectToAction("Index", "Orders");
            }
            
            ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
            return View(model);
        }
        
        /// <summary>
        /// Отображает страницу регистрации нового пользователя.
        /// </summary>
        /// <param name="returnUrl">URL для перенаправления после успешной регистрации.</param>
        /// <returns>Страница регистрации.</returns>
        [HttpGet]
        public IActionResult Register(string? returnUrl = null)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }
        
        /// <summary>
        /// Обрабатывает регистрацию нового пользователя.
        /// </summary>
        /// <param name="model">Модель данных для регистрации.</param>
        /// <returns>Перенаправляет на указанную страницу или страницу заказов.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            var user = new ApplicationUser 
            { 
                UserName = model.Email, 
                Email = model.Email, 
                FullName = model.FullName 
            };
            
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Client");
                await signInManager.SignInAsync(user, isPersistent: false);
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);
                return RedirectToAction("Index", "Orders");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
        
        /// <summary>
        /// Выполняет выход пользователя из системы.
        /// </summary>
        /// <returns>Перенаправляет на страницу заказов.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Orders");
        }
    }
}

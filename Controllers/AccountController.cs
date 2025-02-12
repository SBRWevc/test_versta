
using Microsoft.AspNetCore.Mvc;
using test_versta.Interfaces;
using test_versta.ViewModels;

namespace test_versta.Controllers
{
    public class AccountController(IAccountService accountService) : Controller
    {
        [HttpGet]
        public IActionResult Login(string? returnUrl = null) 
            => View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await accountService.LoginAsync(model);
            if (result.Succeeded)
                return !string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)
                    ? Redirect(model.ReturnUrl)
                    : RedirectToAction("Index", "Orders");

            ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string? returnUrl = null) 
            => View(new RegisterViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await accountService.RegisterAsync(model);
            if (result.Succeeded)
                return !string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)
                    ? Redirect(model.ReturnUrl)
                    : RedirectToAction("Index", "Orders");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await accountService.LogoutAsync();
            return RedirectToAction("Create", "Orders");
        }
    }
}

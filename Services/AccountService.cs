using Microsoft.AspNetCore.Identity;
using test_versta.Interfaces;
using test_versta.Models;
using test_versta.ViewModels;

namespace test_versta.Services;

public class AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) 
    : IAccountService
{
    public async Task<SignInResult> LoginAsync(LoginViewModel model)
    {
        return await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
    }

    public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.FullName };
        var result = await userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Client");
            await signInManager.SignInAsync(user, isPersistent: false);
        }
        return result;
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }
}
using Microsoft.AspNetCore.Identity;
using test_versta.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace test_versta.Interfaces;

public interface IAccountService
{
    Task<SignInResult> LoginAsync(LoginViewModel model);
    Task<IdentityResult> RegisterAsync(RegisterViewModel model);
    Task LogoutAsync();
}
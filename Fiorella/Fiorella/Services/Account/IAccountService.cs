using Microsoft.AspNetCore.Identity;
using Fiorella.ViewModels.Identity;

namespace Fiorella.Services.Account;

public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(RegisterVM register);
    Task<SignInResult> LoginAsync(LoginVM login);
    Task LogoutAsync();
}
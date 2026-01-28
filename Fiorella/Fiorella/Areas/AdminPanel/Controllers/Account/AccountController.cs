
using Microsoft.AspNetCore.Mvc;
using Fiorella.Models;
using Fiorella.Services.Account;
using Fiorella.ViewModels.Identity;

namespace WebApplication1.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IAccountService accountService, ILogger<AccountController> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        try
        {
            
            if (!ModelState.IsValid) return View(model);
        
            if (ModelState.IsValid)
            {
             var result = await _accountService.RegisterAsync(model);
                
                if (result.Succeeded)
                {
                    await _accountService.LoginAsync(new LoginVM { Email = model.Email, Password = model.Password });
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }


        }
        catch (Exception e)
        {
            _logger.LogError(e, "Register error : {Email}", model.Email);
            ModelState.AddModelError(string.Empty, e.Message + (e.InnerException != null ? " -> " + e.InnerException.Message : ""));            return View(model);
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM model)
    {
        try
        {
            if (!ModelState.IsValid) return View(model);
         
                var result = await _accountService.LoginAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Username or Email or password is incorrect");
            
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Login error : {Email}", model.Email);
            ModelState.AddModelError(string.Empty, "Error during login.Please try again later");
            return View(model);
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        _accountService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
}

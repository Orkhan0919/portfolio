using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Mvc;      
using System.Threading.Tasks;        

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var isExist = await _userManager.FindByEmailAsync(model.Email);
        if (isExist != null)
        {
            ModelState.AddModelError("Email", "This email is already in use!");
            return View(model);
        }

        var newUser = new ApplicationUser
        {
            UserName = model.Username,
            Email = model.Email,
            Name = model.Name,
            Surname = model.Surname
        };

        var result = await _userManager.CreateAsync(newUser, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(newUser, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }
}
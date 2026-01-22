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
    private readonly IEmailService _emailService;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {
        if (!ModelState.IsValid) 
        {
            return View(model);
        }
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null) {
            if (!await _userManager.IsEmailConfirmedAsync(user)) {
                ModelState.AddModelError("", "Please vertificate your email first.");
                return View(model);
            }
        }
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Invalid email or password.");
        return View(model);
    }
    public async Task<IActionResult> Logout()    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
        
    }
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string token) {
        if (userId == null || token == null) return RedirectToAction("Index", "Home");

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded) {
            ViewBag.Message = "Your email vertificated successfully! Login now!";
            return RedirectToAction("login", "Account");
        }

        return Content("Something went wrong.");
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
            
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var confirmationLink = Url.Action("ConfirmEmail", "Account", 
                new { userId = newUser.Id, token = token }, Request.Scheme);

            string body = $@"
                <h3>Welcome to Pronia!</h3>
                <p>Please confirm your email by clicking the button below:</p>
                <a href='{confirmationLink}' style='background-color: #28a745; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>
                    Confirm Email
                </a>";

            await _emailService.SendEmailAsync(newUser.Email, "Confirm Your Account", body);

            return Content("Registration successful! Please check your email to verify your account.");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }
}
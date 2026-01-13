using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.AdminPanel.Controllers;

public class DashController : Controller
{
    [Area("AdminPanel")]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public IActionResult Index()
    {
        return View();
    }
}
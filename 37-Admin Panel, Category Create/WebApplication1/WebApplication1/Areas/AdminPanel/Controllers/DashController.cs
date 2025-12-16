using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.AdminPanel.Controllers;

public class DashController : Controller
{
    [Area("AdminPanel")]

    public IActionResult Index()
    {
        return View();
    }
}
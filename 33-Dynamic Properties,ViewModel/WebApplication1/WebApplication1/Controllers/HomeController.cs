using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        List<string> names = new List<string>()
        {
            "Orkhan",
            "Ali",
            "Mammad",
            "Nigar"
        };
        ViewBag.names = names;
        
        TempData["message"] = "Hello World!";
        
        return View(); 
    }
}
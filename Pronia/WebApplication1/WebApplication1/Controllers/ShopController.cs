using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Utilities.Enums;
using WebApplication1.Utilities.Extentions;

namespace WebApplication1.Controllers;

public class ShopController : Controller
{
    private readonly AppDbContext _context;
    
    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> About(int? id)
    {
        if (id == null) return BadRequest();

        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductTags)
            .ThenInclude(pt => pt.Tag)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return NotFound();

        return View();
    }
}
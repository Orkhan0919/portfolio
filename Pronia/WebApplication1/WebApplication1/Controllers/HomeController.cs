using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            var products = await _context.Products
                .Include(p => p.Category) 
                .ToListAsync();

            var flowers = await _context.Flowers .ToListAsync();

            HomeVM viewModel = new HomeVM
            {
                Products = products,
                Flowers = flowers,

                ProductTags = await _context.ProductTags.ToListAsync(),

                Categories = await _context.Categories
                    .Select(c => new CategoryItemVM 
                    {
                        Id = c.Id,     
                        Name = c.Name, 
                        ProductCount = c.Products.Count 
                    })
                    .ToListAsync(),

                Tags = await _context.Tags
                    .Select(t => new TagItemVM
                    {
                        Id = t.TagId,      
                        Name = t.TagName   
                    })
                    .ToListAsync()
            };

            return View(viewModel);
        }
        
    }
}
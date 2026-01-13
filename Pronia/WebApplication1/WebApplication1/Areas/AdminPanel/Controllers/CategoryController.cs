using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync(); 
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                var nameFromForm = Request.Form["Name"];
                category.Name = nameFromForm;
            }
            bool isExist = await _context.Categories.AnyAsync(c => c.Name.ToLower() == category.Name.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Name", "This name is using!");
                return View(category);
            }

            if (!string.IsNullOrEmpty(category.Name))
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }
        public IActionResult Details(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound(); 
            }

            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
    
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id) return BadRequest();
            bool isExist = await _context.Categories.AnyAsync(c => c.Name.ToLower() == category.Name.ToLower().Trim() && c.Id != id);

            if (isExist)
            {
                ModelState.AddModelError("Name", "This name is using!");
                return View(category);
            }
            if (!string.IsNullOrEmpty(category.Name))
            {
                _context.Update(category);
           

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
    
            return RedirectToAction(nameof(Index));
        }
    }
}
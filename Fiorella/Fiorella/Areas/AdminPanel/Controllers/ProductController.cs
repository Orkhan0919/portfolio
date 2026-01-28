using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fiorella.DAL;
using Fiorella.Models;

namespace Fiorella.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .ToListAsync();
            return View(list);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id.Value);
            if (product == null) return NotFound();
            return View(product);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
                return View(product);
            }
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            var product = await _context.Products.FindAsync(id.Value);
            if (product == null) return NotFound();
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
                return View(product);
            }
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Products.AnyAsync(p => p.Id == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id.Value);
            if (product == null) return NotFound();
            if (product.ProductImages != null && product.ProductImages.Any())
            {
                _context.ProductImages.RemoveRange(product.ProductImages);
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fiorella.DAL;
using Fiorella.Models;

namespace Fiorella.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductImageController : Controller
    {
        private readonly AppDbContext _context;

        public ProductImageController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.ProductImages
                .Include(pi => pi.Product)
                .ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Products = await _context.Products.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductImage image)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Products = await _context.Products.ToListAsync();
                return View(image);
            }
            await _context.ProductImages.AddAsync(image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            var image = await _context.ProductImages.FindAsync(id.Value);
            if (image == null) return NotFound();
            ViewBag.Products = await _context.Products.ToListAsync();
            return View(image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductImage image)
        {
            if (id != image.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewBag.Products = await _context.Products.ToListAsync();
                return View(image);
            }
            try
            {
                _context.ProductImages.Update(image);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.ProductImages.AnyAsync(pi => pi.Id == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();
            var image = await _context.ProductImages
                .Include(pi => pi.Product)
                .FirstOrDefaultAsync(pi => pi.Id == id.Value);
            if (image == null) return NotFound();
            return View(image);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var image = await _context.ProductImages.FindAsync(id.Value);
            if (image == null) return NotFound();
            _context.ProductImages.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

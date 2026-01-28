using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fiorella.DAL;
using Fiorella.Models;

namespace Fiorella.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ShippingController : Controller
    {
        private readonly AppDbContext _context;

        public ShippingController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Shippings.ToListAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shipping shipping)
        {
            if (!ModelState.IsValid) return View(shipping);
            await _context.Shippings.AddAsync(shipping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            var shipping = await _context.Shippings.FindAsync(id.Value);
            if (shipping == null) return NotFound();
            return View(shipping);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Shipping shipping)
        {
            if (id != shipping.Id) return BadRequest();
            if (!ModelState.IsValid) return View(shipping);
            try
            {
                _context.Shippings.Update(shipping);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Shippings.AnyAsync(s => s.Id == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();
            var shipping = await _context.Shippings
                .FirstOrDefaultAsync(s => s.Id == id.Value);
            if (shipping == null) return NotFound();
            return View(shipping);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var shipping = await _context.Shippings.FindAsync(id.Value);
            if (shipping == null) return NotFound();
            _context.Shippings.Remove(shipping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

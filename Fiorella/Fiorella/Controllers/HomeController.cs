using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fiorella.DAL;
using Fiorella.Models;
using Fiorella.ViewModels;

namespace Fiorella.Controllers
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
            var sliders = await _context.Sliders.OrderBy(s => s.Order).ToListAsync();
            var shippings = await _context.Shippings.ToListAsync();
            var products = await _context.Products
                .Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null))
                .ToListAsync();

            var homeVM = new HomeVM
            {
                Sliders = sliders,
                Shippings = shippings,
                Products = products
            };

            return View(homeVM);
        }


        public async Task<IActionResult> Sliders()
        {
            var list = await _context.Sliders.OrderBy(s => s.Order).ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> SliderDetails(int? id)
        {
            if (id == null) return NotFound();
            var slider = await _context.Sliders.FindAsync(id.Value);
            if (slider == null) return NotFound();
            return View(slider);
        }

        public IActionResult CreateSlider() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSlider(Slider slider)
        {
            if (!ModelState.IsValid) return View(slider);
            _context.Sliders.Add(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Sliders));
        }

        public async Task<IActionResult> EditSlider(int? id)
        {
            if (id == null) return NotFound();
            var slider = await _context.Sliders.FindAsync(id.Value);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSlider(int id, Slider slider)
        {
            if (id != slider.Id) return BadRequest();
            if (!ModelState.IsValid) return View(slider);

            try
            {
                _context.Update(slider);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Sliders.AnyAsync(s => s.Id == id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Sliders));
        }

        public async Task<IActionResult> DeleteSlider(int? id)
        {
            if (id == null) return NotFound();
            var slider = await _context.Sliders.FindAsync(id.Value);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost, ActionName("DeleteSlider")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSliderConfirmed(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider != null)
            {
                _context.Sliders.Remove(slider);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Sliders));
        }


        public async Task<IActionResult> Shippings()
        {
            var list = await _context.Shippings.ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> ShippingDetails(int? id)
        {
            if (id == null) return NotFound();
            var shipping = await _context.Shippings.FindAsync(id.Value);
            if (shipping == null) return NotFound();
            return View(shipping);
        }

        public IActionResult CreateShipping() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateShipping(Shipping shipping)
        {
            if (!ModelState.IsValid) return View(shipping);
            _context.Shippings.Add(shipping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Shippings));
        }

        public async Task<IActionResult> EditShipping(int? id)
        {
            if (id == null) return NotFound();
            var shipping = await _context.Shippings.FindAsync(id.Value);
            if (shipping == null) return NotFound();
            return View(shipping);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditShipping(int id, Shipping shipping)
        {
            if (id != shipping.Id) return BadRequest();
            if (!ModelState.IsValid) return View(shipping);

            try
            {
                _context.Update(shipping);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Shippings.AnyAsync(s => s.Id == id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Shippings));
        }

        public async Task<IActionResult> DeleteShipping(int? id)
        {
            if (id == null) return NotFound();
            var shipping = await _context.Shippings.FindAsync(id.Value);
            if (shipping == null) return NotFound();
            return View(shipping);
        }

        [HttpPost, ActionName("DeleteShipping")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteShippingConfirmed(int id)
        {
            var shipping = await _context.Shippings.FindAsync(id);
            if (shipping != null)
            {
                _context.Shippings.Remove(shipping);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Shippings));
        }

        // -----------------------
        // Products CRUD (basic)
        // -----------------------
        public async Task<IActionResult> Products()
        {
            var list = await _context.Products
                .Include(p => p.ProductImages)
                .ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id.Value);
            if (product == null) return NotFound();
            return View(product);
        }

        public IActionResult CreateProduct() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (!ModelState.IsValid) return View(product);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Products));
        }

        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id.Value);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, Product product)
        {
            if (id != product.Id) return BadRequest();
            if (!ModelState.IsValid) return View(product);

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Products.AnyAsync(p => p.Id == id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Products));
        }

        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products.FindAsync(id.Value);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                if (product.ProductImages != null && product.ProductImages.Any())
                {
                    _context.ProductImages.RemoveRange(product.ProductImages);
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Products));
        }
    }
}

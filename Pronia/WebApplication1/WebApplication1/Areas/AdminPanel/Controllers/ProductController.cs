using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Utilities.Enums;
using WebApplication1.Utilities.Extentions;

namespace WebApplication1.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public ProductController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.Include(p => p.Category).ToListAsync();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        
        if (product.MainPhoto == null) ModelState.AddModelError("MainPhoto", "Required");
        
        else if (product.MainPhoto != null && !product.MainPhoto.
                ValidatorType("image/")) ModelState.AddModelError("MainPhoto", "Type");
        
        else if (product.MainPhoto != null && !product.MainPhoto.
                ValidatorSize(2, Sizes.MB)) ModelState.AddModelError("MainPhoto", "Size");
        
        if (product.SecondaryPhoto == null) ModelState.AddModelError("SecondaryPhoto", "Required");
        
        else if (product.SecondaryPhoto != null && !product.SecondaryPhoto.
                ValidatorType("image/")) ModelState.AddModelError("SecondaryPhoto", "Type");
        
        else if (product.SecondaryPhoto != null && !product.SecondaryPhoto.
                ValidatorSize(2, Sizes.MB)) ModelState.AddModelError("SecondaryPhoto", "Size");
        
        bool isExist = await _context.Products.AnyAsync(p => p.Name.ToLower() == product.Name.ToLower());
        if (isExist)
        {
            ModelState.AddModelError("Name", "This product name is already in use!");
            return View(product);
        }

        ModelState.Remove("PrimaryImg");
        ModelState.Remove("SecondaryImg");
        ModelState.Remove("Category");

        if (!ModelState.IsValid)
        {
            return View(product);
        }

        product.PrimaryImg = await product.MainPhoto.CreateFileAsync(_env.WebRootPath, "pronia", "assets", "images", "products");
        product.SecondaryImg = await product.SecondaryPhoto.CreateFileAsync(_env.WebRootPath, "pronia", "assets", "images", "products");

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        ViewBag.Categories = await _context.Categories.ToListAsync();
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id) return BadRequest();

        ViewBag.Categories = await _context.Categories.ToListAsync();

        var existingProduct = await _context.Products.FindAsync(id);
        if (existingProduct == null) return NotFound();
        if (product.MainPhoto == null) ModelState.AddModelError("MainPhoto", "Required");
        
        else if (product.MainPhoto != null && !product.MainPhoto.
                     ValidatorType("image/")) ModelState.AddModelError("MainPhoto", "Type");
        
        else if (product.MainPhoto != null && !product.MainPhoto.
                     ValidatorSize(2, Sizes.MB)) ModelState.AddModelError("MainPhoto", "Size");
        
        if (product.SecondaryPhoto == null) ModelState.AddModelError("SecondaryPhoto", "Required");
        
        else if (product.SecondaryPhoto != null && !product.SecondaryPhoto.
                     ValidatorType("image/")) ModelState.AddModelError("SecondaryPhoto", "Type");
        
        else if (product.SecondaryPhoto != null && !product.SecondaryPhoto.
                     ValidatorSize(2, Sizes.MB)) ModelState.AddModelError("SecondaryPhoto", "Size");

        bool isExist = await _context.Products.AnyAsync(p => p.Name.ToLower() == product.Name.ToLower() && p.Id != id);
        if (isExist)
        {
            ModelState.AddModelError("Name", "This product name is already in use!");
            return View(product);
        }

        if (product.MainPhoto != null)
        {
            if (!product.MainPhoto.ValidatorType("image/"))
            {
                ModelState.AddModelError("Photo", "File type is invalid!");
                return View(product);
            }

            if (!product.MainPhoto.ValidatorSize(2, Sizes.MB))
            {
                ModelState.AddModelError("Photo", "File size is invalid! (Max 2MB)");
                return View(product);
            }

            string oldPath = Path.Combine(_env.WebRootPath, "pronia", "assets", "images", "products", existingProduct.PrimaryImg);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            existingProduct.PrimaryImg = await product.MainPhoto.CreateFileAsync(_env.WebRootPath, "pronia", "assets", "images", "products");
        }
        existingProduct.PrimaryImg = await product.MainPhoto.CreateFileAsync(_env.WebRootPath, "pronia", "assets", "images", "products");
        existingProduct.SecondaryImg = await product.SecondaryPhoto.CreateFileAsync(_env.WebRootPath, "pronia", "assets", "images", "products");
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Description = product.Description;
        existingProduct.SKU = product.SKU;
        existingProduct.CategoryId = product.CategoryId;
        

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        if (!string.IsNullOrEmpty(product.PrimaryImg))
        {
            string path = Path.Combine(_env.WebRootPath, "pronia", "assets", "images", "products", product.PrimaryImg);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    public IActionResult Details(int id)
    {
        var product = _context.Products.Find(id);

        if (product == null)
        {
            return NotFound(); 
        }

        return View(product);
    }
}
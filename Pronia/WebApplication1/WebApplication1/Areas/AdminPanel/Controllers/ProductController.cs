using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Utilities.Enums;
using WebApplication1.Utilities.Extentions;
using WebApplication1.ViewModels;

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
        ViewBag.Tags = await _context.Tags.ToListAsync();
        ;
        return View();
    }

    [HttpPost]
public async Task<IActionResult> Create(ProductCreateVM vm)
{
    ViewBag.Categories = await _context.Categories.ToListAsync();
    ViewBag.Tags = await _context.Tags.ToListAsync();

    if (vm.MainPhoto == null) 
        ModelState.AddModelError("MainPhoto", "Required");
    else if (!vm.MainPhoto.ValidatorType("image/")) 
        ModelState.AddModelError("MainPhoto", "Type");
    else if (!vm.MainPhoto.ValidatorSize(2, Sizes.MB)) 
        ModelState.AddModelError("MainPhoto", "Size");

    if (vm.SecondaryPhoto == null) 
        ModelState.AddModelError("SecondaryPhoto", "Required");
    else if (!vm.SecondaryPhoto.ValidatorType("image/")) 
        ModelState.AddModelError("SecondaryPhoto", "Type");
    else if (!vm.SecondaryPhoto.ValidatorSize(2, Sizes.MB)) 
        ModelState.AddModelError("SecondaryPhoto", "Size");

    bool isExist = await _context.Products.AnyAsync(p => p.Name.ToLower() == vm.Name.ToLower());
    if (isExist)
    {
        ModelState.AddModelError("Name", "This product name is already in use!");
        return View(vm);
    }

    if (!ModelState.IsValid)
    {
        return View(vm);
    }

    Product newProduct = new Product
    {
        Name = vm.Name,
        Price = vm.Price,
        Description = vm.Description,
        SKU = vm.SKU,
        CategoryId = vm.CategoryId,
        PrimaryImg = await vm.MainPhoto.CreateFileAsync(_env.WebRootPath, "pronia", "assets", "images", "products"),
        SecondaryImg = await vm.SecondaryPhoto.CreateFileAsync(_env.WebRootPath, "pronia", "assets", "images", "products")
    };

    await _context.Products.AddAsync(newProduct);
    await _context.SaveChangesAsync();

    if (vm.TagIds != null)
    {
        foreach (var tagId in vm.TagIds)
        {
            ProductTag pt = new ProductTag
            {
                ProductId = newProduct.Id,
                TagId = tagId
            };
            _context.ProductTags.Add(pt);
        }
        await _context.SaveChangesAsync();
    }

    return RedirectToAction(nameof(Index));
}
 [HttpGet]
public async Task<IActionResult> Edit(int id)
{
    var product = await _context.Products
        .Include(p => p.ProductTags)
        .FirstOrDefaultAsync(p => p.Id == id);

    if (product == null) return NotFound();

    ViewBag.Categories = await _context.Categories.ToListAsync();
    ViewBag.Tags = await _context.Tags.ToListAsync();

    ProductUpdateVM vm = new ProductUpdateVM
    {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        SKU = product.SKU,
        CategoryId = product.CategoryId,
        ExistingPrimaryImg = product.PrimaryImg,
        ExistingSecondaryImg = product.SecondaryImg,
        TagIds = product.ProductTags.Select(pt => pt.TagId).ToArray()
    };

    return View(vm);
}

[HttpPost]
public async Task<IActionResult> Edit(int id, ProductUpdateVM vm)
{
    ViewBag.Categories = await _context.Categories.ToListAsync();
    ViewBag.Tags = await _context.Tags.ToListAsync();

    if (id != vm.Id) return BadRequest();

    var existingProduct = await _context.Products
        .Include(p => p.ProductTags)
        .FirstOrDefaultAsync(p => p.Id == id);

    if (existingProduct == null) return NotFound();

    if (vm.MainPhoto != null)
    {
        if (!vm.MainPhoto.ValidatorType("image/")) ModelState.AddModelError("MainPhoto", "Type");
        if (!vm.MainPhoto.ValidatorSize(2, Sizes.MB)) ModelState.AddModelError("MainPhoto", "Size");
    }

    if (vm.SecondaryPhoto != null)
    {
        if (!vm.SecondaryPhoto.ValidatorType("image/")) ModelState.AddModelError("SecondaryPhoto", "Type");
        if (!vm.SecondaryPhoto.ValidatorSize(2, Sizes.MB)) ModelState.AddModelError("SecondaryPhoto", "Size");
    }

    if (!ModelState.IsValid) return View(vm);

    if (vm.MainPhoto != null)
    {
        string oldPath = Path.Combine(_env.WebRootPath, "pronia", "assets", "images", "products", existingProduct.PrimaryImg);
        if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
        
        existingProduct.PrimaryImg = await vm.MainPhoto.CreateFileAsync(_env.WebRootPath, "pronia", "assets", "images", "products");
    }

    if (vm.SecondaryPhoto != null)
    {
        string oldSecPath = Path.Combine(_env.WebRootPath, "pronia", "assets", "images", "products", existingProduct.SecondaryImg);
        if (System.IO.File.Exists(oldSecPath)) System.IO.File.Delete(oldSecPath);
        
        existingProduct.SecondaryImg = await vm.SecondaryPhoto.CreateFileAsync(_env.WebRootPath, "pronia", "assets", "images", "products");
    }

    _context.ProductTags.RemoveRange(existingProduct.ProductTags);
    if (vm.TagIds != null)
    {
        foreach (var tagId in vm.TagIds)
        {
            existingProduct.ProductTags.Add(new ProductTag { TagId = tagId });
        }
    }

    existingProduct.Name = vm.Name;
    existingProduct.Price = vm.Price;
    existingProduct.Description = vm.Description;
    existingProduct.SKU = vm.SKU;
    existingProduct.CategoryId = vm.CategoryId;

    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}
[Authorize(Roles = "SuperAdmin")]
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
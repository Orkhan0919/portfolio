using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Utilities.Enums;
using WebApplication1.Utilities.Extentions;

namespace WebApplication1.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
[Authorize(Roles = "SuperAdmin, Admin")]

public class SliderController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    
    public SliderController(AppDbContext context,IWebHostEnvironment env)
    {
        _env = env;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var flowers = await _context.Flowers.ToListAsync(); 
        string path2 = "~/pronia/assets/images/website-images/";
        ViewBag.Path2 = path2;
        return View(flowers);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
public async Task<IActionResult> Create(Flowers flowers)
{
    if (flowers.Photo == null)
    {
        ModelState.AddModelError("Photo", "Please choose a photo!");
        return View(flowers);
    }

    if (!FileValidator.ValidatorType(flowers.Photo,"image/"))
    {
        ModelState.AddModelError("Photo", "File type is invalid!");
        return View(flowers);
    }

    if (!FileValidator.ValidatorSize(flowers.Photo,2,Sizes.MB))
    {
        ModelState.AddModelError("Photo", "File size is invalid! (Max 2MB)");
        return View(flowers);
    }

    if (string.IsNullOrEmpty(flowers.Title1))
    {
        var nameFromForm = Request.Form["Name"];
        flowers.Title1 = nameFromForm.ToString();
    }

    if (string.IsNullOrWhiteSpace(flowers.Title1))
    {
        ModelState.AddModelError("Title1", "Title cannot be empty!");
        return View(flowers);
    }

    if (string.IsNullOrWhiteSpace(flowers.Title2))
    {
        ModelState.AddModelError("Title2", "Title 2 cannot be empty!");
        return View(flowers);
    }

    bool isExist = await _context.Flowers.AnyAsync(c => c.Title1.ToLower() == flowers.Title1.ToLower().Trim());
    if (isExist)
    {
        ModelState.AddModelError("Title1", "This title is already in use!");
        return View(flowers);
    }

    
    ModelState.Remove("ImageUrl");
    if (!ModelState.IsValid)
    {
        return View(flowers);
    }
    flowers.ImageUrl = await flowers.Photo.CreateFileAsync(_env.WebRootPath, "pronia", "assets", "images", "website-images");

    await _context.Flowers.AddAsync(flowers);
    await _context.SaveChangesAsync();
    
    return RedirectToAction(nameof(Index));
}
    public IActionResult Details(int id)
    {
        var slider = _context.Flowers.Find(id);

        if (slider == null)
        {
            return NotFound(); 
        }

        return View(slider);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var slider= await _context.Flowers.FindAsync(id);
        if (slider== null) return NotFound();
    
        return View(slider);
    }

    [HttpPost]
public async Task<IActionResult> Edit(int id, Flowers flowers)
{
    if (id != flowers.Id) return BadRequest();

    var existingFlower = await _context.Flowers.FindAsync(id);
    if (existingFlower == null) return NotFound();

    bool isExist = await _context.Flowers.AnyAsync(c => c.Title1.ToLower() == flowers.Title1.ToLower().Trim() && c.Id != id);
    if (isExist)
    {
        ModelState.AddModelError("Title1", "This title is already in use!");
        return View(flowers);
    }

    if (flowers.Photo != null)
    {
        if (!flowers.Photo.ValidatorType("image/"))
        {
            ModelState.AddModelError("Photo", "File type is invalid!");
            return View(flowers);
        }

        if (!flowers.Photo.ValidatorSize(2, Sizes.MB))
        {
            ModelState.AddModelError("Photo", "File size is invalid! (Max 2MB)");
            return View(flowers);
        }

        string oldPath = Path.Combine(_env.WebRootPath, "pronia", "assets", "images", "website-images", existingFlower.ImageUrl);
        if (System.IO.File.Exists(oldPath))
        {
            System.IO.File.Delete(oldPath);
        }

        existingFlower.ImageUrl = await flowers.Photo.CreateFileAsync(_env.WebRootPath, "pronia", "assets", "images", "website-images");
    }

    existingFlower.Title1 = flowers.Title1;
    existingFlower.Title2 = flowers.Title2;

    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id)
    {
        var slider= await _context.Flowers.FindAsync(id);
        if (slider == null) return NotFound();

        _context.Flowers.Remove(slider);
        await _context.SaveChangesAsync();
    
        return RedirectToAction(nameof(Index));
    }
}
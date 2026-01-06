using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models; 
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class TagController : Controller
{
    private readonly AppDbContext _context;

    public TagController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var tags = await _context.Tags.ToListAsync(); 
        return View(tags);
    }
    public IActionResult Details(int id)
    {
        var tag = _context.Tags.Find(id);

        if (tag == null)
        {
            return NotFound(); 
        }
        return View(tag);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Tags tag)
    {
        if (string.IsNullOrEmpty(tag.TagName))
        {
            var nameFromForm = Request.Form[nameof(Tags.TagName)];
            tag.TagName = nameFromForm;
        }
        bool isExist = await _context.Tags.AnyAsync(c => c.TagName.ToLower() == tag.TagName.ToLower().Trim());

        if (isExist)
        {
            ModelState.AddModelError(nameof(Tags.TagName), "This name is using!");
            return View(tag);
        }

        if (!string.IsNullOrEmpty(tag.TagName))
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(tag);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null) return NotFound();
    
        return View(tag);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Tags tag)
    {
        if (id != tag.TagId) return BadRequest();
        bool isExist = await _context.Tags.AnyAsync(c => c.TagName.ToLower() == tag.TagName.ToLower().Trim() && c.TagId != id);

        if (isExist)
        {
            ModelState.AddModelError(nameof(Tags.TagName), "This name is using!");
            return View(tag);
        }
        if (!string.IsNullOrEmpty(tag.TagName))
        {
            _context.Update(tag);
           

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(tag);
    }
    public async Task<IActionResult> Delete(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null) return NotFound();

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();
    
        return RedirectToAction(nameof(Index));
    }
}
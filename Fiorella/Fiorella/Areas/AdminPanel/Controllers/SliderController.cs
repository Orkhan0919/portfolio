using Fiorella.DAL;
using Fiorella.Models;
using Fiorella.Utilities.Enums;
using Fiorella.Utilities.Extensions;
using Fiorella.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null || id < 1) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id.Value);
            if (slider is null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Slider slider)
        {
            if (id != slider.Id) return BadRequest();
            Slider sliderDb = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (sliderDb is null) return NotFound();

            if (slider.Photo != null && slider.Photo.Length > 0)
            {
                if (!slider.Photo.ValidatorType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type is incorrect!");
                }
                if (!slider.Photo.ValidatorSize(FileSize.MB, slider.Photo.Length))
                {
                    ModelState.AddModelError("Photo", "File size must be less than 2 mb");
                }
                if (!ModelState.IsValid) return View(sliderDb);

                sliderDb.ImageURL.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
                sliderDb.ImageURL = await slider.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");
            }

            if (!ModelState.IsValid) return View(sliderDb);

            sliderDb.Title = slider.Title;
            sliderDb.SubTitle = slider.SubTitle;
            sliderDb.Description = slider.Description;
            sliderDb.Order = slider.Order;

            try
            {
                _context.Sliders.Update(sliderDb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Sliders.AnyAsync(s => s.Id == id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();

            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!slider.Photo.ValidatorType("image/"))
            {
                ModelState.AddModelError("Photo", "File type is incorrect!");
                return View();
            }

            if (!slider.Photo.ValidatorSize(FileSize.MB, slider.Photo.Length))
            {
                ModelState.AddModelError("Photo", "File size must be less than 2 mb");
                return View();
            }

            if(!ModelState.IsValid) return View();

            slider.ImageURL = await slider.Photo.CreateFileAsync(_env.WebRootPath,"assets","images","website-images");

            await _context.Sliders.AddAsync(slider);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();
            var slider = await _context.Sliders
                .FirstOrDefaultAsync(s => s.Id == id.Value);
            if (slider == null) return NotFound();
            return View(slider);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Slider slider  = await _context.Sliders.FirstOrDefaultAsync(s=>s.Id == id);

            if (slider == null) return NotFound();

            slider.ImageURL.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");

            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync(); 

            return RedirectToAction(nameof(Index));
      
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);

            if (slider is null) return NotFound();

            return View(slider);
        }


    }
}

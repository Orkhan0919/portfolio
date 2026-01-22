using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers;

public class BasketController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public BasketController(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index()
    {
        List<BasketItemVM> basketItems = new List<BasketItemVM>();

        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var dbItems = await _context.BasketItems
                .Where(b => b.AppUserId == user.Id)
                .Include(b => b.Product)
                .ToListAsync();

            foreach (var item in dbItems)
            {
                basketItems.Add(new BasketItemVM
                {
                    Id = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    Count = item.Count,
                    Image = item.Product.PrimaryImg 
                });
            }
        }
        else
        {
            var json = Request.Cookies["Basket"];
            if (json != null)
            {
                var cookieItems = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(json);
                foreach (var item in cookieItems)
                {
                    var product = await _context.Products.FindAsync(item.Id);
                    if (product != null)
                    {
                        basketItems.Add(new BasketItemVM 
                        { 
                            Id = product.Id, 
                            Name = product.Name, 
                            Price = product.Price, 
                            Count = item.Count, 
                            Image = product.PrimaryImg 
                        });
                    }
                }
            }
        }
        return View(basketItems);
    }
[HttpPost]
    public async Task<IActionResult> AddBasket(int id)
    {
        if (id <= 0) return BadRequest();
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var basketItem = await _context.BasketItems
                .FirstOrDefaultAsync(b => b.ProductId == id && b.AppUserId == user.Id);

            if (basketItem == null)
            {
                basketItem = new BasketItem
                {
                    AppUserId = user.Id,
                    ProductId = product.Id,
                    Count = 1
                };
                await _context.BasketItems.AddAsync(basketItem);
            }
            else
            {
                basketItem.Count++;
            }
            await _context.SaveChangesAsync();
        }
        else
        {
            List<BasketCookieItemVM> basket;
            var json = Request.Cookies["Basket"];

            if (json != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(json);
                var existedProduct = basket.FirstOrDefault(p => p.Id == id);
                if (existedProduct != null)
                {
                    existedProduct.Count++;
                }
                else
                {
                    basket.Add(new BasketCookieItemVM { Id = id, Count = 1 });
                }
            }
            else
            {
                basket = new List<BasketCookieItemVM>
                {
                    new BasketCookieItemVM { Id = id, Count = 1 }
                };
            }

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(14)
            };
            Response.Cookies.Append("Basket", JsonConvert.SerializeObject(basket), cookieOptions);
        }

        return Ok();
    }
    public async Task<IActionResult> RemoveItem(int id)
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
        
            var basketItem = await _context.BasketItems
                .FirstOrDefaultAsync(b => b.ProductId == id && b.AppUserId == user.Id);

            if (basketItem != null)
            {
                _context.BasketItems.Remove(basketItem);
                await _context.SaveChangesAsync();
            }
        }
        else
        {
            var json = Request.Cookies["Basket"];
            if (json != null)
            {
                List<BasketCookieItemVM> basket = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(json);

                var itemToRemove = basket.FirstOrDefault(p => p.Id == id);
            
                if (itemToRemove != null)
                {
                    basket.Remove(itemToRemove); 

                    Response.Cookies.Append("Basket", JsonConvert.SerializeObject(basket), new CookieOptions 
                    { 
                        Expires = DateTime.UtcNow.AddDays(14) 
                    });
                }
            }
        }

        return RedirectToAction(nameof(Index));
    }
}
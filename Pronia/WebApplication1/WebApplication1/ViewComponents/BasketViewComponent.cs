using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.ViewComponents;

public class BasketViewComponent : ViewComponent
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public BasketViewComponent(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
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
                    Image = item.Product.PrimaryImg,
                    Price = item.Product.Price,
                    Count = item.Count
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
                            Image = product.PrimaryImg,
                            Price = product.Price,
                            Count = item.Count
                        });
                    }
                }
            }
        }

        return View(basketItems);
    }
}
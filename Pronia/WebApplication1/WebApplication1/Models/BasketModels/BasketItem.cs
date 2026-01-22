using WebApplication1.Models;

namespace WebApplication1.Models;

public class BasketItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string AppUserId { get; set; }
    public ApplicationUser AppUser { get; set; }
    public int Count { get; set; }
}